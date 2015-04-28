

using System;
using System.Collections.Generic;

namespace ColorJizz
{
    public class HSV : AbstractColor
    {
        public double h;
        public double s;
        public double v;

        public HSV(double h, double s, double v)
        {
            this.toSelf = ConversionMethod.toHSV;
            this.h = h;
            this.s = s;
            this.v = v;
        }
        public override Hex toHex()
        {
            return this.toRGB().toHex();
        }
        public override RGB toRGB()
        {
            double h = this.h / 360;
            double s = this.s / 100;
            double v = this.v / 100;
            double r;
            double g;
            double b;
            double var_h, var_i, var_1, var_2, var_3, var_r, var_g, var_b;
            if (s == 0)
            {
                r = v * 255;
                g = v * 255;
                b = v * 255;
            }
            else
            {
                var_h = h * 6;
                var_i = Math.Floor(var_h);
                var_1 = v * (1 - s);
                var_2 = v * (1 - s * (var_h - var_i));
                var_3 = v * (1 - s * (1 - (var_h - var_i)));

                if (var_i == 0)
                {
                    var_r = v;
                    var_g = var_3;
                    var_b = var_1;
                }
                else if (var_i == 1)
                {
                    var_r = var_2;
                    var_g = v;
                    var_b = var_1;
                }
                else if (var_i == 2)
                {
                    var_r = var_1;
                    var_g = v;
                    var_b = var_3;
                }
                else if (var_i == 3)
                {
                    var_r = var_1;
                    var_g = var_2;
                    var_b = v;
                }
                else if (var_i == 4)
                {
                    var_r = var_3;
                    var_g = var_1;
                    var_b = v;
                }
                else
                {
                    var_r = v;
                    var_g = var_1;
                    var_b = var_2;
                }

                r = var_r * 255;
                g = var_g * 255;
                b = var_b * 255;
            }
            return new RGB(Convert.ToInt32(Math.Round(r)), Convert.ToInt32(Math.Round(g)), Convert.ToInt32(Math.Round(b)));
        }
        public override XYZ toXYZ()
        {
            return this.toRGB().toXYZ();
        }
        public override Yxy toYxy()
        {
            return this.toXYZ().toYxy();
        }
        public override HSV toHSV()
        {
            return this;
        }
        public override CMY toCMY()
        {
            return this.toRGB().toCMY();
        }
        public override CMYK toCMYK()
        {
            return this.toCMY().toCMYK();
        }
        public override CIELab toCIELab()
        {
            return this.toRGB().toCIELab();
        }
        public override CIELCh toCIELCh()
        {
            return this.toCIELab().toCIELCh();
        }
        public override string ToString()
        {
            return String.Format("{0},{1},{2}", this.h, this.s, this.v);
        }


    }
}

