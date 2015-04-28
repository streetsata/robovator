

using System;
using System.Collections.Generic;

namespace ColorJizz
{
    public class CMYK : AbstractColor
    {
        public double c;
        public double m;
        public double y;
        public double k;

        public CMYK(double c, double m, double y, double k)
        {
            this.toSelf = ConversionMethod.toCMYK;
            this.c = c;
            this.m = m;
            this.y = y;
            this.k = k;
        }
        public override Hex toHex()
        {
            return this.toRGB().toHex();
        }
        public override RGB toRGB()
        {
            return this.toCMY().toRGB();
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
            return this.toRGB().toHSV();
        }
        public override CMY toCMY()
        {
            double C = (this.c * (1 - this.k) + this.k);
            double M = (this.m * (1 - this.k) + this.k);
            double Y = (this.y * (1 - this.k) + this.k);
            return new CMY(C, M, Y);
        }
        public override CMYK toCMYK()
        {
            return this;
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
            return String.Format("{0},{1},{2},{3}", this.c, this.m, this.y, this.k);
        }

    }
}

