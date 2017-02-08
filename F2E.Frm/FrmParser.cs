using F2E.Frm.Dto;
using System;
using System.IO;

namespace F2E.Frm
{
    public class FrmParser
    {
        public static readonly int DirCount = Enum.GetValues(typeof(FrmDirectionType)).Length;

        public static FrmFileData LoadFrom(Stream stream)
        {
            using (var reader = new BinaryReader(stream, System.Text.Encoding.Default, true))
            {
                var result = new FrmFileData
                             {
                                 Version = reader.ReadUInt32BE(),
                                 Fps = reader.ReadUInt16BE(),
                                 ActionFrame = reader.ReadUInt16BE(),
                                 FrameCountInEachDirection = reader.ReadUInt16BE(),
                                 XShiftPerDirection = new ushort[DirCount]
                             };
                for(int i = 0; i < result.XShiftPerDirection.Length; i++)
                {
                    result.XShiftPerDirection[i] = reader.ReadUInt16BE();
                }
                result.YShiftPerDirection = new ushort[DirCount];
                for (int i = 0; i < result.YShiftPerDirection.Length; i++)
                {
                    result.YShiftPerDirection[i] = reader.ReadUInt16BE();
                }
                result.FirstFrameOffsetPerDirection = new uint[DirCount];
                for (int i = 0; i < result.FirstFrameOffsetPerDirection.Length; i++)
                {
                    result.FirstFrameOffsetPerDirection[i] = reader.ReadUInt32BE();
                }
                result.FrameAreaSize = reader.ReadUInt32BE();

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    for (int k = 0; k < result.FrameCountInEachDirection; k++)
                    {
                        var frame = new FrameData
                                    {
                                        FrameWidth = reader.ReadUInt16BE(),
                                        FrameHeight = reader.ReadUInt16BE(),
                                        FrameSize = reader.ReadUInt32BE(),
                                        XOffset = reader.ReadInt16BE(),
                                        YOffset = reader.ReadInt16BE()
                                    };
                        var index = reader.ReadBytes((int)frame.FrameSize);
                        frame.PixelIndexFrames = index;
                        result.Frames.Add(frame);
                    }
                }

                return result;
            }
        }

        public static void SaveTo(Stream stream, FrmFileData result)
        {
            using (var writer = new BinaryWriter(stream, System.Text.Encoding.Default, true))
            {
                writer.WriteBE(result.Version);
                writer.WriteBE(result.Fps);
                writer.WriteBE(result.ActionFrame);
                writer.WriteBE(result.FrameCountInEachDirection);
                foreach (ushort t in result.XShiftPerDirection)
                {
                    writer.WriteBE(t);
                }
                foreach (ushort t in result.YShiftPerDirection)
                {
                    writer.WriteBE(t);
                }
                foreach (uint t in result.FirstFrameOffsetPerDirection)
                {
                    writer.WriteBE(t);
                }
                writer.WriteBE(result.FrameAreaSize);

                foreach (var frame in result.Frames)
                {
                    writer.WriteBE(frame.FrameWidth);
                    writer.WriteBE(frame.FrameHeight);
                    writer.WriteBE(frame.FrameSize);
                    writer.WriteBE(frame.XOffset);
                    writer.WriteBE(frame.YOffset);
                    writer.Write(frame.PixelIndexFrames);
                }
                writer.Flush();
            }
        }
    }
}
