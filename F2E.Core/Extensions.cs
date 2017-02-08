using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace F2E.Core
{
    public static class Extensions
    {
        public static Color Multiply(this Color color, float value)
        {
            return Color.FromArgb(color.A, (int)Math.Min(color.R*value,255), (int)Math.Min(color.G * value, 255), (int)Math.Min(color.B * value, 255));
        }

        public static Bitmap Apply(this Bitmap bitmap, IEnumerable<IFilter> filters)
        {
            if (!filters.Any())
                return bitmap;
            var bmp = new Bitmap(bitmap);
            var patches = filters.Patches((x, y) => x.GetType() == y.GetType()).ToList();
            foreach (var patch in patches)
            {
                var isSingle = patch.First() is PixelFilter;
                if (isSingle)
                {
                    var funcs = patch.Cast<PixelFilter>().Select(x => (Func<Color,Color>)x.Apply).ToArray();
                    for (int i = 0; i < bmp.Width; i++)
                    {
                        for (int j = 0; j < bmp.Height; j++)
                        {
                            var color = bmp.GetPixel(i, j);
                            for (int k = 0; k < funcs.Length; k++)
                            {
                                color = funcs[k](color);
                            }
                            bmp.SetPixel(i, j, color);
                        }
                    }
                }
                else
                {
                    foreach (var filter in patch.Cast<BitmapFilter>())
                    {
                        bmp = filter.Apply(bmp);
                    }
                }
            }
            return bmp;
        }

        public static IEnumerable<List<T>> Patches<T>(this IEnumerable<T> collection, Func<T,T,bool> cmp) where T:class 
        {
            if (collection == null)
            {
                yield break;
            }
            var enumer = collection.GetEnumerator();
            if (!enumer.MoveNext())
            {
                yield break;
            }

            var prevList = new List<T>();
            T prev = enumer.Current;
            prevList.Add(prev);

            while (enumer.MoveNext())
            {
                if (!cmp(prev, enumer.Current))
                {
                    yield return prevList;
                    prevList = new List<T>();
                }
                prev = enumer.Current;
                prevList.Add(prev);
            }

            if (prevList.Count > 0)
            {
                yield return prevList;
            }
        }
    }
}
