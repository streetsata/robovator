using HealthCheck;
using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Robovator2
{
    public class LocalColor
    {
        public String ColorName = "";
        public Color ColorValue;
    }

    internal class ColorCollection
    {

        private String filePath = "";
        private List<LocalColor> colorCollection = new List<LocalColor>();

        INIConfig config = null;

        public ColorCollection(String path)
        {
            this.filePath = path;
            this.config = new INIConfig(this.filePath);

            foreach (String section in this.config.GetSections())
            {
                Color tmpColor = Color.FromArgb(
                    int.Parse(this.config[section]["r"]),
                    int.Parse(this.config[section]["g"]),
                    int.Parse(this.config[section]["b"])
                    );
                LocalColor lclColor = new LocalColor();
                lclColor.ColorValue = tmpColor;
                lclColor.ColorName = this.config[section]["color_name"];
                colorCollection.Add(lclColor);
            }
        }

        public void addColor(String colorName, Color color)
        {
            if (this.config.containsKey(colorName))
                throw new ArgumentException("this color is already defined");
            else
            {
                String sectionName = Guid.NewGuid().ToString();
                this.config[sectionName, "color_name"] = colorName;
                this.config[sectionName, "r"] = color.R.ToString();
                this.config[sectionName, "g"] = color.G.ToString();
                this.config[sectionName, "b"] = color.B.ToString();
            }
        }

        public List<LocalColor> Colors
        {
            get { return this.colorCollection; }
        }
    }
}
