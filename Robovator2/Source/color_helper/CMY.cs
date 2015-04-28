

using System;
using System.Collections.Generic;

namespace ColorJizz
{
    public class CMY : AbstractColor
    {
        public double c;
        public double m;
        public double y;

        public CMY(double c, double m, double y)
        {
            this.toSelf = ConversionMethod.toCMY;
            this.c = c;
            this.m = m;
            this.y = y;
        }
        public override Hex toHex()
        {
            return this.toRGB().toHex();
        }
        public override RGB toRGB()
        {
            int R = (int)((1 - this.c) * 255);
            int G = (int)((1 - this.m) * 255);
            int B = (int)((1 - this.y) * 255);
            return new RGB(R, G, B);
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
            return this;
        }
        public override CMYK toCMYK()
        {
            double var_K = 1;
            double C = this.c;
            double M = this.m;
            double Y = this.y;
            if (C < var_K) var_K = C;
            if (M < var_K) var_K = M;
            if (Y < var_K) var_K = Y;
            if (var_K == 1)
            {
                C = 0;
                M = 0;
                Y = 0;
            }
            else
            {
                C = (C - var_K) / (1 - var_K);
                M = (M - var_K) / (1 - var_K);
                Y = (Y - var_K) / (1 - var_K);
            }

            double K = var_K;

            return new CMYK(C, M, Y, K);
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
            return String.Format("{0},{1},{2}", this.c, this.m, this.y);
        }
    }
}

