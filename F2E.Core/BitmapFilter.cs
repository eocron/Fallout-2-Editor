using System.Drawing;

namespace F2E.Core
{
    public abstract class BitmapFilter : IFilter
    {
        public abstract Bitmap Apply(Bitmap bitmap);
    }
}
