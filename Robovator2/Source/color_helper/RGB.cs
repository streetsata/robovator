

using System;
using System.Collections.Generic;

namespace ColorJizz
{
    public class RGB : AbstractColor
    {
        public int r;
        public int g;
        public int b;

        public RGB(int r, int g, int b)
        {
            this.toSelf = ConversionMethod.toRGB;
            this.r = Math.Min(255, Math.Max(r, 0));
            this.g = Math.Min(255, Math.Max(g, 0));
            this.b = Math.Min(255, Math.Max(b, 0));
        }
        public override Hex toHex()
        {
            return new Hex(Convert.ToUInt32(this.r << 16 | this.g << 8 | this.b));
        }

        public override RGB toRGB()
        {
            return this;
        }
        public override XYZ toXYZ()
        {
            double tmp_r = (double)this.r / 255;
            double tmp_g = (double)this.g / 255;
            double tmp_b = (double)this.b / 255;
            if (tmp_r > 0.04045)
            {
                tmp_r = Math.Pow((double)((double)tmp_r + 0.055) / 1.055, 2.4);
            }
            else
            {
                tmp_r = tmp_r / 12.92;
            }
            if (tmp_g > 0.04045)
            {
                tmp_g = Math.Pow((double)((double)tmp_g + 0.055) / 1.055, 2.4);
            }
            else
            {
                tmp_g = tmp_g / 12.92;
            }
            if (tmp_b > 0.04045)
            {
                tmp_b = Math.Pow((double)((double)tmp_b + 0.055) / 1.055, 2.4);
            }
            else
            {
                tmp_b = tmp_b / 12.92;
            }
            tmp_r = tmp_r * 100;
            tmp_g = tmp_g * 100;
            tmp_b = tmp_b * 100;
            double x = tmp_r * 0.4124 + tmp_g * 0.3576 + tmp_b * 0.1805;
            double y = tmp_r * 0.2126 + tmp_g * 0.7152 + tmp_b * 0.0722;
            double z = tmp_r * 0.0193 + tmp_g * 0.1192 + tmp_b * 0.9505;

            return new XYZ(x, y, z);
        }
        public override Yxy toYxy()
        {
            return this.toXYZ().toYxy();
        }
        public override HSV toHSV()
        {
            double r = (double)this.r / 255;
            double g = (double)this.g / 255;
            double b = (double)this.b / 255;

            double h, s, v;
            double min, max, delta;

            min = Math.Min(r, Math.Min(g, b));
            max = Math.Max(r, Math.Min(g, b));

            v = max;
            delta = max - min;
            if (max != 0)
            {
                s = delta / max;
            }
            else
            {
                s = 0;
                h = -1;
                return new HSV(h, s, v);
            }
            if (r == max)
            {
                h = (g - b) / delta;
            }
            else if (g == max)
            {
                h = 2 + (b - r) / delta;
            }
            else
            {
                h = 4 + (r - g) / delta;
            }
            h *= 60;
            if (h < 0)
            {
                h += 360;
            }

            return new HSV(h, s * 100, v * 100);
        }
        public override CMY toCMY()
        {
            double C = 1 - ((double)this.r / 255);
            double M = 1 - ((double)this.g / 255);
            double Y = 1 - ((double)this.b / 255);
            return new CMY(C, M, Y);
        }
        public override CMYK toCMYK()
        {
            return this.toCMY().toCMYK();
        }
        public override CIELab toCIELab()
        {
            return this.toXYZ().toCIELab();
        }
        public override CIELCh toCIELCh()
        {
            return this.toCIELab().toCIELCh();
        }
        public override string ToString()
        {
            return String.Format("{0},{1},{2}", this.r, this.g, this.b);
        }
    }
}

