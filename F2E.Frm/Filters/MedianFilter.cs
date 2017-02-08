using System.Collections.Generic;
using System.Drawing;
using F2E.Core;

namespace F2E.Frm.Filters
{
    public sealed class MedianFilter : BitmapFilter
    {
        private readonly int _radius;

        public MedianFilter(int radius)
        {
            _radius = radius;
        }
        public override Bitmap Apply(Bitmap bitmap)
        {
            return Apply(bitmap, _radius);
        }

        public static Bitmap Apply(Bitmap image, int size)
        {
            var tempBitmap = image;
            var newBitmap = new Bitmap(tempBitmap.Width, tempBitmap.Height);
            using (var newGraphics = Graphics.FromImage(newBitmap))
            {
                newGraphics.DrawImage(tempBitmap,
                                      new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
                                      new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
                                      GraphicsUnit.Pixel);
            }
            var apetureMin = -(size/2);
            var apetureMax = size/2;
            var RValues = new List<int>();
            var GValues = new List<int>();
            var BValues = new List<int>();
            for (var x = 0; x < newBitmap.Width; ++x)
                for (var y = 0; y < newBitmap.Height; ++y)
                {
                    RValues.Clear();
                    GValues.Clear();
                    BValues.Clear();
                    for (var x2 = apetureMin; x2 < apetureMax; ++x2)
                    {
                        var tempX = x + x2;
                        if ((tempX >= 0) && (tempX < newBitmap.Width))
                            for (var y2 = apetureMin; y2 < apetureMax; ++y2)
                            {
                                var tempY = y + y2;
                                if ((tempY >= 0) && (tempY < newBitmap.Height))
                                {
                                    var tempColor = tempBitmap.GetPixel(tempX, tempY);
                                    bool isTransparent = tempColor.A < 255;
                                    if (!isTransparent)
                                    {
                                        RValues.Add(tempColor.R);
                                        GValues.Add(tempColor.G);
                                        BValues.Add(tempColor.B);
                                    }
                                }
                            }
                    }

                    if (RValues.Count != 0)
                    {
                        RValues.Sort();
                        GValues.Sort();
                        BValues.Sort();
                        var medianPixel = Color.FromArgb(RValues[RValues.Count/2],
                                                         GValues[GValues.Count/2],
                                                         BValues[BValues.Count/2]);
                        newBitmap.SetPixel(x, y, medianPixel);
                    }
                    else
                    {
                        var medianPixel = tempBitmap.GetPixel(x, y);
                        newBitmap.SetPixel(x, y, medianPixel);
                    }
                }
            return newBitmap;
        }
    }
}