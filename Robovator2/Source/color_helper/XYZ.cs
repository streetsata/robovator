

using System;
using System.Collections.Generic;

namespace ColorJizz
{
    public class XYZ : AbstractColor
    {
        public double x;
        public double y;
        public double z;

        public XYZ(double x, double y, double z)
        {

            this.toSelf = ConversionMethod.toXYZ;
            this.x = this.roundDec(x, 3);
            this.y = this.roundDec(y, 3);
            this.z = this.roundDec(z, 3);
        }
        public override Hex toHex()
        {
            return this.toRGB().toHex();
        }
        public override RGB toRGB()
        {
            double var_X = this.x / 100;
            double var_Y = this.y / 100;
            double var_Z = this.z / 100;
            double var_R = var_X * 3.2406 + var_Y * -1.5372 + var_Z * -0.4986;
            double var_G = var_X * -0.9689 + var_Y * 1.8758 + var_Z * 0.0415;
            double var_B = var_X * 0.0557 + var_Y * -0.2040 + var_Z * 1.0570;


            if (var_R > 0.0031308)
            {
                var_R = 1.055 * Math.Pow(var_R, (double)1 / 2.4) - 0.055;
            }
            else
            {
                var_R = 12.92 * var_R;
            }
            if (var_G > 0.0031308)
            {
                var_G = 1.055 * Math.Pow(var_G, (double)1 / 2.4) - 0.055;
            }
            else
            {
                var_G = 12.92 * var_G;
            }
            if (var_B > 0.0031308)
            {
                var_B = 1.055 * Math.Pow(var_B, (double)1 / 2.4) - 0.055;
            }
            else
            {
                var_B = 12.92 * var_B;
            }
            int r = Convert.ToInt32(Math.Round(var_R * 255));
            int g = Convert.ToInt32(Math.Round(var_G * 255));
            int b = Convert.ToInt32(Math.Round(var_B * 255));

            return new RGB(r, g, b);
        }
        public override XYZ toXYZ()
        {
            return this;
        }
        public override Yxy toYxy()
        {
            double Y = this.y;
            double x = this.x / (this.x + this.y + this.z);
            double y = this.y / (this.x + Y + this.z);
            return new Yxy(Y, x, y);
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
            double Xn = 95.047;
            double Yn = 100.000;
            double Zn = 108.883;

            double x = this.x / Xn;
            double y = this.y / Yn;
            double z = this.z / Zn;

            if (x > 0.008856)
            {
                x = Math.Pow(x, (double)1 / 3);
            }
            else
            {
                x = (7.787 * x) + (16 / 116);
            }
            if (y > 0.008856)
            {
                y = Math.Pow(y, (double)1 / 3);
            }
            else
            {
                y = (7.787 * y) + (16 / 116);
            }
            if (z > 0.008856)
            {
                z = Math.Pow(z, (double)1 / 3);
            }
            else
            {
                z = (7.787 * z) + (16 / 116);
            }
            double l;
            if (y > 0.008856)
            {
                l = (116 * y) - 16;
            }
            else
            {
                l = 903.3 * y;
            }
            double a = 500 * (x - y);
            double b = 200 * (y - z);


            return new CIELab(l, a, b);
        }
        public override CIELCh toCIELCh()
        {
            return this.toCIELab().toCIELCh();
        }
        public override string ToString()
        {
            return String.Format("{0},{1},{2}", this.x, this.y, this.z);
        }

    }
}

