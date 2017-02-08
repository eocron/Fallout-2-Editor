using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using F2E.Core;

namespace F2E.Frm.Dto
{
    public enum FrmDirectionType
    {
        A = 0,
        B,
        C,
        D,
        E,
        F
    }

    [DataContract]
    public sealed class FrmModel
    {
        [DataMember]
        public int Version { get; set; }

        [DataMember]
        public int Fps { get; set; }

        [DataMember]
        public int ActionFrame { get; set; }

        [DataMember]
        public int FrameCountInEachDirection { get; set; }

        [DataMember]
        public Dictionary<FrmDirectionType, FrmDirectionModel> Directions { get; set; }

        public FrmModel()
        {
            Directions = new Dictionary<FrmDirectionType, FrmDirectionModel>();
        }
    }

    [DataContract]
    public sealed class FrmDirectionModel
    {
        [DataMember]
        public RVector2 Shift { get; set; }

        [DataMember]
        public int FirtsFrameOffset { get; set; }

        [DataMember]
        public List<FrameModel> Frames { get; set; }
    }

    [DataContract]
    public sealed class FrameModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public RVector2 Offset { get; set; }

        [IgnoreDataMember]
        public int Width => Bitmap.Width;

        [IgnoreDataMember]
        public int Height => Bitmap.Height;

        [IgnoreDataMember]
        public Bitmap Bitmap { get; set; }
    }
}
