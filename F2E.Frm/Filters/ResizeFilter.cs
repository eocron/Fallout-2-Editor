using System.Drawing;
using System.Drawing.Drawing2D;
using F2E.Core;

namespace F2E.Frm.Filters
{
    public class ResizeFilter : BitmapFilter
    {
        private readonly InterpolationMode _interpolationMode;
        private readonly Size _size;

        public ResizeFilter(InterpolationMode interpolationMode, Size size)
        {
            _interpolationMode = interpolationMode;
            _size = size;
        }

        public override Bitmap Apply(Bitmap bitmap)
        {
            if (_size == bitmap.Size)
                return bitmap;

            var b = new Bitmap(_size.Width, _size.Height);
            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.InterpolationMode = _interpolationMode;
                g.DrawImage(bitmap, 0, 0, _size.Width, _size.Height);
            }
            return b;
        }
    }
}
