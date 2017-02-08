using System;
using System.Drawing;
using F2E.Core;

namespace F2E.Frm.Filters
{
    public sealed class BrightnessFilter : PixelFilter
    {
        private readonly float _multiplier;

        public BrightnessFilter(float multiplier)
        {
            if (multiplier < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(multiplier));
            }
            _multiplier = multiplier;
        }

        public override Color Apply(Color color)
        {
            return color.Multiply(_multiplier);
        }
    }
}
