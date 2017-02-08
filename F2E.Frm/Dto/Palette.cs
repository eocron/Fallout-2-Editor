using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using F2E.Core;

namespace F2E.Frm.Dto
{
    public sealed class Palette
    {
        public readonly IReadOnlyList<Color> Colors;

        public Palette(IEnumerable<Color> colors, IEnumerable<Tuple<Color, byte>> colorToIndex)
        {
            Colors = colors.ToList();
            if (Colors.Count != 256)
            {
                throw new ArgumentException("Invalid palette color count", nameof(colors));
            }
        }

        public IReadOnlyList<Color> GetWithBrightness(float brightness)
        {
            if (brightness == 1)
                return Colors;

            if(brightness < 0)
                throw new ArgumentOutOfRangeException(nameof(brightness));

            return Colors.Select(x => x.Multiply(brightness)).ToList();
        }
    }
}
