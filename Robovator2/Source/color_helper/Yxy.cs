

using System;
using System.Collections.Generic;

namespace ColorJizz
{
    public class Yxy : AbstractColor
    {
        public double Y;
        public double x;
        public double y;

        public Yxy(double Y, double x, double y)
        {
            this.toSelf = ConversionMethod.toYxy;
            this.Y = Y;
            this.x = x;
            this.y = y;
        }
        public override Hex toHex()
        {
            return this.toXYZ().toHex();
        }
        public override RGB toRGB()
        {
            return this.toXYZ().toRGB();
        }
        public override XYZ toXYZ()
        {
            double X = this.x * (this.Y / this.y);
            double Y = this.Y;
            double Z = (1 - this.x - this.y) * (this.Y / this.y);
            return new XYZ(X, Y, Z);
        }
        public override Yxy toYxy()
        {
            return this;
        }
        public override HSV toHSV()
        {
            return this.toXYZ().toHSV();
        }
        public override CMY toCMY()
        {
            return this.toXYZ().toCMY();
        }
        public override CMYK toCMYK()
        {
            return this.toXYZ().toCMYK();
        }
        public override CIELab toCIELab()
        {
            return this.toXYZ().toCIELab();
        }
        public override CIELCh toCIELCh()
        {
            return this.toXYZ().toCIELCh();
        }
        public override string ToString()
        {
            return String.Format("{0},{1},{2}", this.Y, this.x, this.y);
        }

    }
}

