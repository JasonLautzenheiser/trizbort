using System;
using System.Drawing;

// thanks to https://github.com/sanjayatpilcrow/SharpSnippets/blob/master/POCs/POCs/Sanjay/SharpSnippets/Drawing/ColorExtensions.cs

namespace Trizbort
{
    public static class ColorExtensions
    {
        private static Random _randomizer = new Random();

        public static bool ColorsAreClose(this Color a, Color c2, int threshold = 50)
        {
          int r = (int)a.R - c2.R,
              g = (int)a.G - c2.G,
              b = (int)a.B - c2.B;
          return (r * r + g * g + b * b) <= threshold * threshold;
        }

        public static Color GetContrast(this Color Source, bool PreserveOpacity)
        {
            Color inputColor = Source;
            //if RGB values are close to each other by a diff less than 10%, then if RGB values are lighter side, decrease the blue by 50% (eventually it will increase in conversion below), if RBB values are on darker side, decrease yellow by about 50% (it will increase in conversion)
            byte avgColorValue = (byte) ((Source.R + Source.G + Source.B)/3);
            int diff_r = Math.Abs(Source.R - avgColorValue);
            int diff_g = Math.Abs(Source.G - avgColorValue);
            int diff_b = Math.Abs(Source.B - avgColorValue);
            if (diff_r < 20 && diff_g < 20 && diff_b < 20) //The color is a shade of gray
            {
                if (avgColorValue < 123) //color is dark
                {
                    inputColor = Color.FromArgb(Source.A, 220, 230, 50);
                }
                else
                {
                    inputColor = Color.FromArgb(Source.A, 255, 255, 50);
                }
            }
            byte sourceAlphaValue = Source.A;
            if (!PreserveOpacity)
            {
                sourceAlphaValue = Math.Max(Source.A, (byte) 127); //We don't want contrast color to be more than 50% transparent ever.
            }
            RGB rgb = new RGB {R = inputColor.R, G = inputColor.G, B = inputColor.B};
            HSB hsb = ConvertToHSB(rgb);
            hsb.H = hsb.H < 180 ? hsb.H + 180 : hsb.H - 180;
            //_hsb.B = _isColorDark ? 240 : 50; //Added to create dark on light, and light on dark
            rgb = ConvertToRGB(hsb);
            return Color.FromArgb((int) sourceAlphaValue, (int) rgb.R, (int) rgb.G, (int) rgb.B);
        }

        #region Code from MSDN

        internal static RGB ConvertToRGB(HSB hsb)
        {
            // Following code is taken as it is from MSDN. See link below.
            // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
            double chroma = hsb.S*hsb.B;
            double hue2 = hsb.H/60;
            double x = chroma*(1 - Math.Abs(hue2%2 - 1));
            double r1 = 0d;
            double g1 = 0d;
            double b1 = 0d;
            if (hue2 >= 0 && hue2 < 1)
            {
                r1 = chroma;
                g1 = x;
            }
            else if (hue2 >= 1 && hue2 < 2)
            {
                r1 = x;
                g1 = chroma;
            }
            else if (hue2 >= 2 && hue2 < 3)
            {
                g1 = chroma;
                b1 = x;
            }
            else if (hue2 >= 3 && hue2 < 4)
            {
                g1 = x;
                b1 = chroma;
            }
            else if (hue2 >= 4 && hue2 < 5)
            {
                r1 = x;
                b1 = chroma;
            }
            else if (hue2 >= 5 && hue2 <= 6)
            {
                r1 = chroma;
                b1 = x;
            }
            double m = hsb.B - chroma;
            return new RGB()
            {
                R = r1 + m,
                G = g1 + m,
                B = b1 + m
            };
        }

        internal static HSB ConvertToHSB(RGB rgb)
        {
            // Following code is taken as it is from MSDN. See link below.
            // By: <a href="http://blogs.msdn.com/b/codefx/archive/2012/02/09/create-a-color-picker-for-windows-phone.aspx" title="MSDN" target="_blank">Yi-Lun Luo</a>
            double r = rgb.R;
            double g = rgb.G;
            double b = rgb.B;

            double max = Max(r, g, b);
            double min = Min(r, g, b);
            double chroma = max - min;
            double hue2 = 0d;
            if (chroma != 0)
            {
                if (max == r)
                {
                    hue2 = (g - b)/chroma;
                }
                else if (max == g)
                {
                    hue2 = (b - r)/chroma + 2;
                }
                else
                {
                    hue2 = (r - g)/chroma + 4;
                }
            }
            double hue = hue2*60;
            if (hue < 0)
            {
                hue += 360;
            }
            double brightness = max;
            double saturation = 0;
            if (chroma != 0)
            {
                saturation = chroma/brightness;
            }
            return new HSB()
            {
                H = hue,
                S = saturation,
                B = brightness
            };
        }

        private static double Max(double d1, double d2, double d3)
        {
            if (d1 > d2)
            {
                return Math.Max(d1, d3);
            }
            return Math.Max(d2, d3);
        }

        private static double Min(double d1, double d2, double d3)
        {
            if (d1 < d2)
            {
                return Math.Min(d1, d3);
            }
            return Math.Min(d2, d3);
        }

        internal struct RGB
        {
            internal double R;
            internal double G;
            internal double B;
        }

        internal struct HSB
        {
            internal double H;
            internal double S;
            internal double B;
        }

        #endregion //Code from MSDN
    }
}
