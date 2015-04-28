using System;
using System.Collections.Generic;
using System.Drawing;

namespace ColorJizz
{
    public enum ConversionMethod
    {
        toCIELab,
        toCIELCh,
        toXYZ,
        toYxy,
        toRGB,
        toHex,
        toHSV,
        toCMY,
        toCMYK
    }

     
    public abstract class AbstractColor
    {
        public static int getAverage(Color[] value, String clr)
        {
            int retVal = 0;
            foreach (Color c in value)
            {
                if (clr == "RED")
                    retVal += c.R;
                if (clr == "GREEN")
                    retVal += c.G;
                if (clr == "BLUE")
                    retVal += c.B;
            }
            return retVal / value.Length;
        }

        protected ConversionMethod toSelf;
        public AbstractColor this[ConversionMethod type]
        {
            get
            {
                switch (type)
                {
                    case ConversionMethod.toCIELab:
                        return toCIELab();
                    case ConversionMethod.toCIELCh:
                        return toCIELCh();
                    case ConversionMethod.toCMY:
                        return toCMY();
                    case ConversionMethod.toCMYK:
                        return toCMYK();
                    case ConversionMethod.toHex:
                        return toHex();
                    case ConversionMethod.toHSV:
                        return toHSV();
                    case ConversionMethod.toRGB:
                        return toRGB();
                    case ConversionMethod.toXYZ:
                        return toXYZ();
                    case ConversionMethod.toYxy:
                        return toYxy();
                    default:
                        return toRGB();
                }
            }
        }

        public abstract Hex toHex();
        public abstract RGB toRGB();
        public abstract XYZ toXYZ();
        public abstract Yxy toYxy();
        public abstract CIELab toCIELab();
        public abstract CIELCh toCIELCh();
        public abstract CMY toCMY();
        public abstract CMYK toCMYK();
        public abstract HSV toHSV();
        public double distance(AbstractColor destinationColor)
        {
            CIELab a = this.toCIELab();
            CIELab b = destinationColor.toCIELab();
            return Math.Sqrt(Math.Pow(a.l - b.l, 2) + Math.Pow(a.a - b.a, 2) + Math.Pow(a.b - b.b, 2));
        }
        public AbstractColor websafe()
        {

            string[] c = new string[] { "00", "CC", "33", "66", "99", "FF" };
            List<AbstractColor> palette = new List<AbstractColor>();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        palette.Add(Hex.fromString(c[i] + c[j] + c[k]));
                    }
                }
            }
            return this.match(palette);
        }
        public AbstractColor match(List<AbstractColor> palette)
        {
            double distance = double.PositiveInfinity;
            AbstractColor closest = null;
            for (int i = 0; i < palette.Count; i++)
            {
                double cdistance = this.distance(palette[i]);
                if (distance == double.PositiveInfinity || cdistance < distance)
                {
                    distance = cdistance;
                    closest = palette[i];
                }
            }
            return closest[this.toSelf];
        }
        public List<AbstractColor> equal(int parts, bool includeSelf)
        {
            if (parts < 2) parts = 2;
            CIELCh current = this.toCIELCh();
            double distance = 360 / parts;
            List<AbstractColor> palette = new List<AbstractColor>();
            if (includeSelf) palette.Add(this);
            for (int i = 1; i < parts; i++)
            {
                palette.Add(new CIELCh(current.l, current.c, current.h + (distance * i))[this.toSelf]);
            }
            return palette;
        }
        public List<AbstractColor> split(bool includeSelf)
        {
            List<AbstractColor> rtn = new List<AbstractColor>();
            rtn.Add(this.hue(-150)[this.toSelf]);
            if (includeSelf) rtn.Add(this);
            rtn.Add(this.hue(150)[this.toSelf]);
            return rtn;
        }
        public List<AbstractColor> complement(bool includeSelf)
        {
            List<AbstractColor> rtn = new List<AbstractColor>();
            rtn.Add(this.hue(180)[this.toSelf]);
            if (includeSelf) rtn.Insert(0, this);
            return rtn;
        }
        public List<AbstractColor> sweetspot(bool includeSelf)
        {
            List<HSV> colors = new List<HSV>(6);
            colors.Add(this.toHSV());
            colors.Add(new HSV(colors[0].h, Math.Round(colors[0].s * 0.3), Math.Min(Math.Round(colors[0].v * 1.3), 100)));
            colors.Add(null);
            colors.Add(new HSV((colors[0].h + 300) % 360, colors[0].s, colors[0].v));
            colors[2] = new HSV(colors[1].h, Math.Min(Math.Round(colors[1].s * 1.2), 100), Math.Min(Math.Round(colors[1].v * 0.5), 100));
            colors.Add(new HSV(colors[2].h, 0, (colors[2].v + 50) % 100));
            colors.Add(new HSV(colors[4].h, colors[4].s, (colors[4].v + 50) % 100));
            List<AbstractColor> rtn = new List<AbstractColor>();
            for (int i = includeSelf ? 0 : 1; i < colors.Count; i++)
            {
                rtn.Add(colors[i][this.toSelf]);
            }
            return rtn;
        }
        public List<AbstractColor> analogous(bool includeSelf)
        {
            List<AbstractColor> rtn = new List<AbstractColor>();
            rtn.Add(this.hue(-30)[this.toSelf]);
            if (includeSelf) rtn.Add(this);
            rtn.Add(this.hue(30)[this.toSelf]);
            return rtn;
        }
        public List<AbstractColor> rectangle(int sideLength, bool includeSelf)
        {
            int side1 = sideLength;
            int side2 = (360 - (sideLength * 2)) / 2;
            CIELCh current = this.toCIELCh();
            List<AbstractColor> rtn = new List<AbstractColor>();
            rtn.Add(new CIELCh(current.l, current.c, current.h + side1)[this.toSelf]);
            rtn.Add(new CIELCh(current.l, current.c, current.h + side1 + side2)[this.toSelf]);
            rtn.Add(new CIELCh(current.l, current.c, current.h + side1 + side2)[this.toSelf]);
            if (includeSelf) rtn.Insert(0, this);
            return rtn;
        }
        public List<AbstractColor> range(AbstractColor destinationColor, int steps, bool includeSelf)
        {
            RGB a = this.toRGB();
            RGB b = destinationColor.toRGB();
            List<AbstractColor> colors = new List<AbstractColor>();
            steps--;
            double nr, ng, nb;
            for (int n = 1; n < steps; n++)
            {
                nr = Math.Floor((double)a.r + (n * (b.r - a.r) / steps));
                ng = Math.Floor((double)a.g + (n * (b.g - a.g) / steps));
                nb = Math.Floor((double)a.b + (n * (b.b - a.b) / steps));
                colors.Add(new RGB(Convert.ToInt32(nr), Convert.ToInt32(ng), Convert.ToInt32(nb))[this.toSelf]);
            }
            if (includeSelf)
            {
                colors.Insert(0, this);
                colors.Add(destinationColor[this.toSelf]);
            }
            return colors;
        }
        public AbstractColor greyscale()
        {
            RGB a = this.toRGB();
            double ds = a.r * 0.3 + a.g * 0.59 + a.b * 0.11;
            int ds_i = Convert.ToInt32(ds);
            return new RGB(ds_i, ds_i, ds_i)[this.toSelf];
        }
        public AbstractColor hue(double degreeModifier)
        {
            CIELCh a = this.toCIELCh();
            a.h += degreeModifier;
            return a[this.toSelf];
        }
        public AbstractColor saturation(double satModifier)
        {
            HSV a = this.toHSV();
            a.s += (satModifier / 100);
            a.s = Math.Min(1, Math.Max(0, a.s));
            return a[this.toSelf];
        }
        public AbstractColor brightness(double brightnessModifier)
        {
            CIELab a = this.toCIELab();
            a.l += brightnessModifier;
            return a[this.toSelf];
        }
        protected double roundDec(double numIn, double decimalPlaces)
        {
            double nExp = Math.Pow(10, decimalPlaces);
            double nRetVal = Math.Round(numIn * nExp) / nExp;
            return nRetVal;
        }
    }
}

