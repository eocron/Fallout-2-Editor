using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using F2E.Frm.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace F2E.Frm
{
    /// <summary>
    /// Manages Editor Frame Archives (EFA) and their [load/save] [efa/frm] to [stream/file]
    /// Editor Frame Archive is a simple [.zip] archive what contains [MANIFEST.json] with all Fallout configuration
    /// of [FRM] file in [JSON] text format, and folders from [A] to [F] (each folder is a direction in fallout system, 
    /// A means 0, B means 1, etc.) which will contain frames for this direction in [.bmp] format. It can be easily modified or
    /// created on your own with simple text editor, archivation tool and paint.
    /// 
    /// Each pixel of [.bmp] file is either transparent (R=0,G=0,B=0) or not, so it is important to not use
    /// completely white pixels when you editing [.bmp] file.
    /// </summary>
    public sealed class FrmManager
    {
        private const string ManifestName = "MANIFEST.json";
        private readonly Palette _palette;

        public FrmManager(Palette palette)
        {
            _palette = palette;
        }

        public FrmModel ImportFrom(Bitmap img)
        {
            var model = new FrmModel
                        {
                            Fps = 1,
                            FrameCountInEachDirection = 1,
                            Version = 1
                        };
            model.Directions.Add(FrmDirectionType.A, new FrmDirectionModel
                                                     {
                                                         Frames = new List<FrameModel>
                                                                  {
                                                                      new FrameModel
                                                                      {
                                                                          Bitmap = img,
                                                                          Id = 1
                                                                      }
                                                                  }
                                                     });
            return model;
        }

        public FrmModel LoadFromFrm(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                return LoadFromFrm(stream);
            }
        }

        public FrmModel LoadFromFrm(Stream stream)
        {
            try
            {
                var frmData = FrmParser.LoadFrom(stream);
                var frmModel = FrmConverter.ConvertToModel(frmData, _palette);
                return frmModel;
            }
            catch (Exception e)
            {
                throw new Exception("Exception occured when loading model from FRM representation.", e);
            }
        }

        public FrmModel LoadFromEfa(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                return LoadFromEfa(stream);
            }
        }

        public FrmModel LoadFromEfa(Stream stream)
        {
            try
            {
                FrmModel result;
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Read, true))
                {
                    using (var reader = new StreamReader(archive.GetEntry(ManifestName).Open()))
                    {
                        using (var jreader = new JsonTextReader(reader))
                        {
                            var serializer = GetSerializer();
                            result = serializer.Deserialize<FrmModel>(jreader);
                        }
                    }

                    foreach (var dir in result.Directions)
                    {
                        foreach (var frame in dir.Value.Frames)
                        {
                            var name = string.Format(@"{0}/{1}.bmp", dir.Key, frame.Id);
                            var file = archive.GetEntry(name);
                            using (var entryStream = file.Open())
                            {
                                var bitmap = new Bitmap(entryStream);
                                frame.Bitmap = bitmap;
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Exception occured when loading model from EFA representation.", e);
            }
        }

        public void SaveToFrm(string fileName, FrmModel model)
        {
            using (var stream = File.Open(fileName, FileMode.Create))
            {
                SaveToFrm(stream, model);
            }
        }

        public void SaveToFrm(Stream stream, FrmModel model)
        {
            try
            {
                var data = FrmConverter.ConvertToData(model);
                FrmParser.SaveTo(stream, data);
            }
            catch (Exception e)
            {
                throw new Exception("Exception occured when saving model as FRM.", e);
            }
        }

        public void SaveToEfa(string fileName, FrmModel model)
        {
            using (var stream = File.OpenWrite(fileName))
            {
                SaveToEfa(stream, model, true);
            }
        }

        public void SaveToEfa(Stream stream, FrmModel model, bool leaveOpen)
        {
            try
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    foreach (var dir in model.Directions)
                    {
                        foreach (var frame in dir.Value.Frames)
                        {
                            var name = string.Format(@"{0}/{1}.bmp", dir.Key, frame.Id);
                            var file = archive.CreateEntry(name);
                            using (var entryStream = file.Open())
                            {
                                frame.Bitmap.Save(entryStream, ImageFormat.Bmp);
                            }
                        }
                    }

                    using (var entryStream = archive.CreateEntry(ManifestName).Open())
                    {
                        using (var writer = new StreamWriter(entryStream))
                        {
                            using (var jsonWriter = new JsonTextWriter(writer))
                            {
                                var ser = GetSerializer();
                                ser.Serialize(jsonWriter, model);
                                jsonWriter.Flush();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Exception occured when saving model as EFA.", e);
            }
        }

        private JsonSerializer GetSerializer()
        {
            return new JsonSerializer
                   {
                       Formatting = Formatting.Indented,
                       Converters =
                       {
                           new StringEnumConverter()
                       },
                       DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
                   };
        }
    }
}
