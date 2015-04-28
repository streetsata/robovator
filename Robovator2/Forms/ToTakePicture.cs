using ColorJizz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robovator2.Forms
{
    public partial class ToTakePicture : Form
    {
        public ToTakePicture()
        {
            InitializeComponent();
        }

        public ToTakePicture(Bitmap bmp)
            : this()
        {
            pictureBox1.Image = (Image)bmp;
            pictureBox1.Size = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
        }

        List<Color> arrColor = new List<Color>();
        public List<Color> ArrColor { get { return this.arrColor; } }
        Color averageColor = new Color();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs arg = e as MouseEventArgs;
            Color tmpColor = new Bitmap(pictureBox1.Image).GetPixel(arg.X, arg.Y);
            arrColor.Add(tmpColor);

            averageColor = Color.FromArgb(
                AbstractColor.getAverage(arrColor.ToArray(), "RED"),
                AbstractColor.getAverage(arrColor.ToArray(), "GREEN"),
                AbstractColor.getAverage(arrColor.ToArray(), "BLUE"));
                //(byte)arrColor.Average((a) => a.R),
                //(byte)arrColor.Average((a) => a.G),
                //(byte)arrColor.Average((a) => a.B));
            label1.Text = String.Format("R:{0}; G:{1}; B:{2};", averageColor.R, averageColor.G, averageColor.B);
            panel1.BackColor = averageColor;
            listBox1.Items.Add(String.Format("R:{0} | G:{1} | B:{2}", tmpColor.R, tmpColor.G, tmpColor.B));
        }

      
        private void btnOK_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorCollection tmp = new ColorCollection(
                Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "colorArr01.ini"
                );
            tmp.addColor(textBox1.Text, averageColor);
        }
    }
}
