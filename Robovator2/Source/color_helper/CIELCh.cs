

using System;
using System.Collections.Generic;

namespace ColorJizz
{
    public class CIELCh : AbstractColor
    {
        public double l;
        public double c;
        public double h;

        public CIELCh(double l, double c, double h)
        {

            this.toSelf = ConversionMethod.toCIELCh;
            this.l = l;
            this.c = c;
            this.h = h < 360 ? h : (h - 360);
        }
        public override Hex toHex()
        {
            return this.toCIELab().toHex();
        }
        public override RGB toRGB()
        {
            return this.toCIELab().toRGB();
        }
        public override XYZ toXYZ()
        {
            return this.toCIELab().toXYZ();
        }
        public override Yxy toYxy()
        {
            return this.toXYZ().toYxy();
        }
        public override HSV toHSV()
        {
            return this.toCIELab().toHSV();
        }
        public override CMY toCMY()
        {
            return this.toCIELab().toCMY();
        }
        public override CMYK toCMYK()
        {
            return this.toCIELab().toCMYK();
        }
        public override CIELab toCIELab()
        {
            double l = this.l;
            double hradi = this.h * (Math.PI / 180);
            double a = Math.Cos(hradi) * this.c;
            double b = Math.Sin(hradi) * this.c;
            return new CIELab(l, a, b);
        }
        public override CIELCh toCIELCh()
        {
            return this;
        }
        public override string ToString()
        {
            return String.Format("{0},{1},{2}", this.l, this.c, this.h);
        }
    }
}


