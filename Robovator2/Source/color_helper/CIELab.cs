

using System;
using System.Collections.Generic;

namespace ColorJizz
{
    public class CIELab : AbstractColor
    {
        public double l;
        public double a;
        public double b;

        public CIELab(double l, double a, double b)
        {

            this.toSelf = ConversionMethod.toCIELab;
            this.l = l;
            this.a = a;
            this.b = b;
        }
        public override Hex toHex()
        {
            return this.toRGB().toHex();
        }
        public override RGB toRGB()
        {
            return this.toXYZ().toRGB();
        }
        public override XYZ toXYZ()
        {
            double ref_X = 95.047;
            double ref_Y = 100.000;
            double ref_Z = 108.883;

            double var_Y = (this.l + 16) / 116;
            double var_X = this.a / 500 + var_Y;
            double var_Z = var_Y - this.b / 200;

            if (Math.Pow(var_Y, 3) > 0.008856)
            {
                var_Y = Math.Pow(var_Y, 3);
            }
            else
            {
                var_Y = (var_Y - 16 / 116) / 7.787;
            }
            if (Math.Pow(var_X, 3) > 0.008856)
            {
                var_X = Math.Pow(var_X, 3);
            }
            else
            {
                var_X = (var_X - 16 / 116) / 7.787;
            }
            if (Math.Pow(var_Z, 3) > 0.008856)
            {
                var_Z = Math.Pow(var_Z, 3);
            }
            else
            {
                var_Z = (var_Z - 16 / 116) / 7.787;
            }
            double x = ref_X * var_X;
            double y = ref_Y * var_Y;
            double z = ref_Z * var_Z;
            return new XYZ(x, y, z);
        }
        public override Yxy toYxy()
        {
            return this.toXYZ().toYxy();
        }
        public override HSV toHSV()
        {
            return this.toRGB().toHSV();
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
            return this;
        }
        public override CIELCh toCIELCh()
        {
            double var_H = Math.Atan2(this.b, this.a);

            if (var_H > 0)
            {
                var_H = (var_H / Math.PI) * 180;
            }
            else
            {
                var_H = 360 - (Math.Abs(var_H) / Math.PI) * 180;
            }

            double l = this.l;
            double c = Math.Sqrt(Math.Pow(this.a, 2) + Math.Pow(this.b, 2));
            double h = var_H;

            return new CIELCh(l, c, h);
        }
        public override String ToString()
        {
            return String.Format("{0},{1},{2}", this.l, this.a, this.b);
        }
    }
}