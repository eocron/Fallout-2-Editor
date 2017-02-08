using System.Drawing;

namespace F2E.Core
{
    public abstract class PixelFilter : IFilter
    {
        public abstract Color Apply(Color color);
    }
}
