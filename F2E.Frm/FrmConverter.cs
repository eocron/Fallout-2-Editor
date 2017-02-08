using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using F2E.Core;
using F2E.Frm.Dto;
using MoreLinq;

namespace F2E.Frm
{
    public static class FrmConverter
    {


        public static FrmFileData ConvertToData(FrmModel model)
        {
            var result = new FrmFileData
                         {
                             Version = (uint) model.Version,
                             ActionFrame = (ushort) model.ActionFrame,
                             Fps = (ushort) model.Fps,
                             FrameCountInEachDirection = (ushort) model.FrameCountInEachDirection,
                             XShiftPerDirection = new ushort[FrmParser.DirCount],
                             YShiftPerDirection = new ushort[FrmParser.DirCount],
                             FirstFrameOffsetPerDirection = new uint[FrmParser.DirCount]
                         };
            foreach (var dir in model.Directions)
            {
                var dirIdx = (int) dir.Key;
                result.XShiftPerDirection[dirIdx] = (ushort) dir.Value.Shift.x;
                result.YShiftPerDirection[dirIdx] = (ushort) dir.Value.Shift.y;
                result.FirstFrameOffsetPerDirection[dirIdx] = (uint) dir.Value.FirtsFrameOffset;
            }

            var frames = model.Directions.OrderBy(x => x.Key).SelectMany(x => x.Value.Frames);
            foreach (var frame in frames)
            {
                var rframe = new FrameData
                             {
                                 FrameWidth = (ushort) frame.Width,
                                 FrameHeight = (ushort) frame.Height,
                                 FrameSize = (uint) (frame.Width*frame.Height),
                                 XOffset = (short) frame.Offset.x,
                                 YOffset = (short) frame.Offset.y,
                                 PixelIndexFrames = ImageHelper.GetPaletteIndexes(frame.Bitmap)
                             };

                result.Frames.Add(rframe);
            }
            return result;
        }



        public static FrmModel ConvertToModel(FrmFileData info, Palette palette)
        {
            var result = new FrmModel
                         {
                             Version = (int) info.Version,
                             ActionFrame = info.ActionFrame,
                             Fps = info.Fps,
                             FrameCountInEachDirection = info.FrameCountInEachDirection,
                         };

            var frameDirs = GetFrames(info, palette).Batch(result.FrameCountInEachDirection).ToList();
            for(int i = 0; i < frameDirs.Count;i++)
            {
                var direction = (FrmDirectionType) i;
                var frameDir = new FrmDirectionModel
                               {
                                   Shift = new RVector2(info.XShiftPerDirection[i], info.YShiftPerDirection[i]),
                                   FirtsFrameOffset = (int) info.FirstFrameOffsetPerDirection[i],
                                   Frames = frameDirs[i].ToList()
                               };

                result.Directions.Add(direction, frameDir);
            }

            return result;
        }

        private static IEnumerable<FrameModel> GetFrames(FrmFileData info, Palette palette)
        {
            for(int i = 0; i < info.Frames.Count;i++)
            {
                var id = i%info.FrameCountInEachDirection + 1;
                var frame = info.Frames[i];
                var bitmap = ConvertToModel(id, frame, palette);
                yield return bitmap;
            }
        }


        private static FrameModel ConvertToModel(int id, FrameData frame, Palette palette)
        {
            return new FrameModel
                   {
                       Id = id,
                       Bitmap = ImageHelper.CreateBitmapFromPaletteIndexes(frame.PixelIndexFrames, frame.FrameWidth, frame.FrameHeight, palette),
                       Offset = new RVector2(frame.XOffset, frame.YOffset)
                   };
        }
    }
}
