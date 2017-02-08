using System.Drawing;
using F2E.Core;

namespace F2E.Frm.Filters
{
    public class FalloutAlphaFilter : PixelFilter
    {
        private readonly bool _whiteIsTransparent;
        private static readonly Color Transparrent = Color.FromArgb(0);
        private static readonly int WhiteArgb = Color.White.ToArgb();

        public FalloutAlphaFilter(bool whiteIsTransparent)
        {
            _whiteIsTransparent = whiteIsTransparent;
        }

        public override Color Apply(Color color)
        {
            if (color.A < 255 || _whiteIsTransparent && color.ToArgb() == WhiteArgb)
            {
                return Transparrent;
            }
            return color;
        }
    }
}
