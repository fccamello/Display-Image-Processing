using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Display_Image_Processing
{
    static class BasicDIP
    {
        public static void Brightness(ref Bitmap a, ref Bitmap b, int value)
        {
            b = new Bitmap(a.Width, a.Height);
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    Color temp = a.GetPixel(x, y);
                    Color changed;
                    if (value < 0)
                    {
                        changed = Color.FromArgb(Math.Min(temp.R + value, 255), Math.Min(temp.G + value, 255), Math.Min(temp.B + value, 255));
                    }
                    else
                    {
                        changed = Color.FromArgb(Math.Max(temp.R + value, 0), Math.Max(temp.G + value, 0), Math.Max(temp.B + value, 0));
                    }
                    b.SetPixel(x, y, changed);
                }
            }
        }
        public static void Hist(ref Bitmap a, ref Bitmap b)
        {
            Color sample;
            Color gray;
            Byte graydata;

            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    sample = a.GetPixel(x, y);
                    graydata = (byte)((sample.R + sample.G + sample.B) / 3);
                    gray = Color.FromArgb(graydata, graydata, graydata);
                    a.SetPixel(x, y, gray);
                }
            }
            int[] hisdata = new int[256];
            for (int x = 0; (x < a.Width); x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    sample = a.GetPixel(x, y);
                    hisdata[sample.R]++;
                }
            }

            b = new Bitmap(256, 800);

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 800; y++)
                {
                    b.SetPixel(x, y, Color.White);
                }
            }

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < Math.Min(hisdata[x] / 5, b.Height - 1); y++)
                {
                    b.SetPixel(x, (b.Height - 1) - y, Color.Black);
                }
            }
        }

        public static void Sepia(ref Bitmap a, ref Bitmap b)
        {
            b = new Bitmap(a.Width, a.Height);

            Color p;

            for (int y = 0; y < a.Height; y++)
            {
                for (int x = 0; x < a.Width; x++)
                {
                    p = a.GetPixel(x, y);

                    int c = p.A;
                    int r = p.R;
                    int g = p.G;
                    int d = p.B;

                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * d);
                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * d);
                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * d);

                    if (tr > 255)
                        r = 255;
                    else
                        r = tr;

                    if (tg > 255)
                        g = 255;
                    else
                        g = tg;

                    if (tb > 255)
                        d = 255;
                    else
                        d = tb;

                    b.SetPixel(x, y, Color.FromArgb(c, r, g, d));
                }
            }
        }
    }
}
    

