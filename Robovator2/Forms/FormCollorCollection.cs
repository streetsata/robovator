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
    public partial class FormCollorCollection : Form
    {
        Color selectedColor = Color.FromArgb(0, 0, 0, 0);
        Boolean colorIsChecked = false;

        public bool ColorIsChecked { get { return colorIsChecked; } }

        public FormCollorCollection()
        {
            InitializeComponent();

            ColorCollection cc = new ColorCollection(
                Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "colorArr01.ini"
                );


            foreach (LocalColor lc in cc.Colors)
            {
                ListViewItem lvi = new ListViewItem(lc.ColorName);
                lvi.BackColor = lc.ColorValue;
                lvi.ForeColor = invert(lc.ColorValue);
                listView1.Items.Add(lvi);

            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            selectedColor = listView1.SelectedItems[0].BackColor;
            colorIsChecked = true;
            this.Close();
        }

        public Color SelectedColor { get { return selectedColor; } }

        public Color invert(Color c)
        {
            return Color.FromArgb(c.A, 0xFF - c.R, 0xFF - c.G, 0xFF - c.B);
        }
    }
}
