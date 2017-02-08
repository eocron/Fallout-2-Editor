using System.Collections.Generic;

namespace F2E.Frm.Dto
{
    public sealed class FrmFileData
    {
        public uint Version { get; set; }

        public ushort Fps { get; set; }

        public ushort ActionFrame { get; set; }

        public ushort FrameCountInEachDirection { get; set; }

        public ushort[] XShiftPerDirection { get; set; }

        public ushort[] YShiftPerDirection { get; set; }

        public uint[] FirstFrameOffsetPerDirection { get; set; }

        public uint FrameAreaSize { get; set; }

        public List<FrameData> Frames { get; set; }

        public FrmFileData()
        {
            Frames = new List<FrameData>();
        }
    }

    public sealed class FrameData
    {
        public ushort FrameWidth { get; set; }

        public ushort FrameHeight { get; set; }

        public uint FrameSize { get; set; }

        public short XOffset { get; set; }

        public short YOffset { get; set; }

        public byte[] PixelIndexFrames { get; set; }
    }


}
