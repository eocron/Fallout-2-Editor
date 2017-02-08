using System;
using System.Collections.Generic;
using System.Drawing;
using F2E.Core;
using F2E.Frm.Dto;

namespace F2E.Frm.Filters
{
    public sealed class FalloutConvertFilter : BitmapFilter
    {
        private readonly Palette _palette;
        private readonly float _brightnessInTargetPalette;

        public FalloutConvertFilter(Palette palette, float brightnessInTargetPalette)
        {
            _palette = palette;
            _brightnessInTargetPalette = brightnessInTargetPalette;
        }

        public override Bitmap Apply(Bitmap bmp)
        {
            var palette = _palette.GetWithBrightness(_brightnessInTargetPalette);
            byte[] indexes = new byte[bmp.Width*bmp.Height];
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    var idInIndexes = i * bmp.Width + j;
                    var oldColor = bmp.GetPixel(j, i);
                    var isTransparent = oldColor.A < 255;
                    var idInPalette = isTransparent ? 0 : IndexOfNearest(palette, oldColor);
                    indexes[idInIndexes] = (byte)idInPalette;
                }
            }
            return ImageHelper.CreateBitmapFromPaletteIndexes(indexes, bmp.Width, bmp.Height, _palette);
        }

        private static int IndexOfNearest(IReadOnlyList<Color> list, Color value)
        {
            int resI = 0;
            var diff = double.MaxValue;
            for (int i = 1; i < 229; i++)
            {
                var f = list[i];
                var ndiff = Math.Pow(f.R - value.R, 2) + Math.Pow(f.G - value.G, 2) + Math.Pow(f.B - value.B, 2);
                if (ndiff < diff)
                {
                    diff = ndiff;
                    resI = i;
                }
            }
            return resI;
        }
    }
}
