using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using F2E.Frm.Dto;

namespace F2E.Frm
{
    public static class ImageHelper
    {
        public static Palette LoadPalette(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            var colors = new List<Color>();
            for (int i = 0; i < 256; i++)
            {
                var idx = i*3;
                var R = bytes[idx];
                var G = bytes[idx + 1];
                var B = bytes[idx + 2];
                var color = Color.FromArgb(R, G, B);
                colors.Add(color);
            }

            var colortToIndex = new List<Tuple<Color, byte>>();
            for (int r = 0; r < 32; r++)
            {
                for (int g = 0; g < 32; g++)
                {
                    for (int b = 0; b < 32; b++)
                    {
                        var idx = r*32*32 + g*32 + b;
                        var index = bytes[idx];
                        var color = Color.FromArgb(r, g, b);
                        colortToIndex.Add(Tuple.Create(color, index));
                    }
                }

            }

            return new Palette(colors, colortToIndex);
        }

        public static byte[] GetPaletteIndexes(Bitmap bitmap)
        {
            BitmapData data = null;
            try
            {
                data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                var length = data.Width * data.Height;
                byte[] bytes = new byte[length];
                IntPtr scan0 = data.Scan0;
                for (int i = 0; i < data.Height; i++)
                {
                    Marshal.Copy(scan0, bytes, i * data.Width, data.Width);
                    scan0 = new IntPtr(scan0.ToInt64() + data.Stride);
                }
                return bytes;
            }
            finally
            {
                if (data != null)
                {
                    bitmap.UnlockBits(data);
                }
            }
        }

        public static Bitmap CreateBitmapFromPaletteIndexes(byte[] indexes, int width, int height, Palette palette)
        {
            Bitmap bitmap = null;
            BitmapData data = null;
            try
            {
                bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                data = bitmap.LockBits(new Rectangle(0, 0, width, height),
                                           ImageLockMode.WriteOnly,
                                           PixelFormat.Format8bppIndexed);

                var pal = bitmap.Palette;
                for (int i = 0; i < palette.Colors.Count; i++)
                {
                    pal.Entries[i] = palette.Colors[i];
                }
                bitmap.Palette = pal;

                IntPtr scan0 = data.Scan0;
                for (int h = 0; h < data.Height; h++)
                {
                    Marshal.Copy(indexes, h * data.Width, scan0, data.Width);
                    scan0 = new IntPtr(scan0.ToInt64() + data.Stride);
                }
                return bitmap;
            }
            finally
            {
                if (bitmap != null && data != null)
                {
                    bitmap.UnlockBits(data);
                }
            }

        }


        public static Bitmap CreateGridBackground(int width, int height, Color backColor, Color gridColor)
        {
            const int gridSize = 10;
            var bitmap = new Bitmap(width, height);
            bool drawGrid = gridColor.ToArgb() != backColor.ToArgb();
            using (Graphics gfx = Graphics.FromImage(bitmap))
            {
                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    gfx.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);

                    if (!drawGrid)
                        return bitmap;

                    using (var pen = new Pen(gridColor))
                    {
                        var w = bitmap.Width/gridSize;
                        for (int i = 0; i < w; i++)
                        {
                            var wi = i*gridSize;
                            gfx.DrawLine(pen, new Point(wi, 0), new Point(wi, bitmap.Height));
                        }

                        var h = bitmap.Height / gridSize;
                        for (int i = 0; i < h; i++)
                        {
                            var hi = i * gridSize;
                            gfx.DrawLine(pen, new Point(0, hi), new Point(bitmap.Width, hi));
                        }
                    }
                }
            }

            return bitmap;
        }

        public static Bitmap CreateSolidBackground(int width, int height, Color backColor)
        {
            var bitmap = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(bitmap))
            {
                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    gfx.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
                }
            }

            return bitmap;
        }
    }
}
