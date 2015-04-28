using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using AForge.Imaging;
using System.Drawing.Imaging;
using Robovator2.Forms;
using ColorJizz;

namespace Robovator2.Controls
{
    public partial class UserControlOneCam : UserControl
    {
        FilterInfoCollection CaptureDevice; // Cameras
        public VideoCaptureDevice FinalFrame; // Frame
        Bitmap frameSource, frameFilter; // images in both pictureBox
        volatile List<Color> arrColor = new List<Color>();
        EuclideanColorFiltering filter = new EuclideanColorFiltering();
        Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
        BlobCounter blobCounter = new BlobCounter();
        Bitmap tmpImg = null;
        Color color = Color.Black;
        short range = 0;
        byte rAvg = 0;
        byte gAvg = 0;
        byte bAvg = 0;
        int objectX = 0;
        int objectY = 0;
        public delegate void ObjectIsFound(UserControlOneCam sender);
        public event ObjectIsFound OnObjectFound;
        public delegate void NoObject(UserControlOneCam sender);
        public event NoObject No_Object;
        volatile bool objectFound = false;
        volatile public bool startStop = false;
        int countOfObj = 0;
        int safeDist = 0;

        public UserControlOneCam()
        {
            InitializeComponent();

            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
                comboBoxDevices.Items.Add(Device.Name);
            comboBoxDevices.SelectedIndex = 0;
        }

        private void BtnConnetct_Click(object sender, EventArgs e)
        {
            try
            {
                if (FinalFrame != null && FinalFrame.IsRunning == true)
                {
                    BtnConnetct.BackColor = Color.Blue;
                    FinalFrame.SignalToStop();
                    FinalFrame.WaitForStop();
                    pictureBoxSource.Image = null;
                    pictureBoxDest.Image = null;
                    pictureBoxSource.Invalidate();
                    pictureBoxDest.Invalidate();
                }
                else
                {
                    BtnConnetct.BackColor = Color.Red;
                    FinalFrame = new VideoCaptureDevice(CaptureDevice[comboBoxDevices.SelectedIndex].MonikerString);
                    FinalFrame.VideoResolution = FinalFrame.VideoCapabilities[6];
                    FinalFrame.NewFrame += FinalFrame_NewFrame;
                    FinalFrame.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        const short process = 3;
        short countProc = 0;
        float range1 = 0.0F;
        float range2 = 0.0F;
        float range3 = 0.0F;
        float range4 = 0.0F;
        float range5 = 0.0F;
        float range6 = 0.0F;
        void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (countProc < process)
            {
                countProc++;
                return;
            }
            countProc = 0;

            tmpImg = frameSource = (Bitmap)eventArgs.Frame.Clone();
            // Resolution//////////////////////////////////////////////////////////////////////////////////////
            Graphics g1 = Graphics.FromImage(frameSource);
            StringBuilder imageResolition = new StringBuilder(frameSource.Width + " x " + frameSource.Height);
            g1.DrawString
                (
                imageResolition.ToString(),
                new Font("Arial", 10, FontStyle.Regular),
                new SolidBrush(Color.GreenYellow), 10, 10
                );
            //FPS//////////////////////////////////////////////////////////////////////////////////////////////
            Graphics g2 = Graphics.FromImage(frameSource);
            StringBuilder fps = new StringBuilder(FinalFrame.VideoCapabilities[6].AverageFrameRate + " FPS");
            g2.DrawString
                (
                fps.ToString(),
                new Font("Arial", 10, FontStyle.Regular),
                new SolidBrush(Color.GreenYellow), 10, 30
                );
            //Count of Objects
            Graphics g3 = Graphics.FromImage(frameSource);
            StringBuilder objFound = new StringBuilder("AMOUNT OF FOUND OBJECTS: " + countOfObj);
            g3.DrawString
                (
                objFound.ToString(),
                new Font("Arial", 10, FontStyle.Regular),
                new SolidBrush(Color.GreenYellow), 10, 50
                );
            //////////////////////////////////////////////////////////////////////////////////////////////////
            // Filtering and object search
            frameFilter = (Bitmap)eventArgs.Frame.Clone();
            //if(frameFilter == null)
                //frameFilter = new Bitmap(@"C:\Users\serg\Desktop\Outlook.com\IMAG0243.jpg");
           
            blobCounter.MinWidth = hScrollBar2.Value;
            blobCounter.MinHeight = hScrollBar3.Value;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;
            try
            {

                if (arrColor.Count > 0)
                {
                    rAvg = (byte)AbstractColor.getAverage(arrColor.ToArray(), "RED");
                    gAvg = (byte)AbstractColor.getAverage(arrColor.ToArray(), "GREEN");
                    bAvg = (byte)AbstractColor.getAverage(arrColor.ToArray(), "BLUE");
                }

                filter.CenterColor = new AForge.Imaging.RGB(rAvg, gAvg, bAvg);
                filter.Radius = range;
                //filter.ApplyInPlace(frameFilter);
                //AbstractColor tmpClr = new ColorJizz.RGB(rAvg, gAvg, bAvg);
                //CIELab mainCILab = tmpClr.toCIELab();

                YCbCr mainColor = YCbCr.FromRGB(new AForge.Imaging.RGB(rAvg, gAvg, bAvg));

                float yMin = range1;// mainColor.Y - range1 < -0.5F ? -0.5F : mainColor.Y - range1;
                float yMax = range4;// mainColor.Y + range4 > 0.5F ? 0.5F : mainColor.Y + range4;

                float cbMin = range2;// mainColor.Cb - range2 < -0.5F ? -0.5F : mainColor.Cb - range2;
                float cbMax = range5;// mainColor.Cb + range5 > 0.5F ? 0.5F : mainColor.Cb + range5;

                float crMin = range3;// mainColor.Cr - range3 < -0.5F ? -0.5F : mainColor.Cr - range3;
                float crMax = range6;// mainColor.Cr + range6 > 0.5F ? 0.5F : mainColor.Cr + range6;
               

                // create filter
                YCbCrFiltering filter2 = new YCbCrFiltering();
                // set color ranges to keep
                filter2.Y = new Range(yMin, yMax);
                filter2.Cb = new Range(cbMin, cbMax);
                filter2.Cr = new Range(crMin, crMax);

                //filter2.Y = new Range(0.0f, 1.0f);
                //filter2.Cb = new Range(-0.5f, 0);
                //filter2.Cr = new Range(-0.5f, 0);
                //// apply the filter
                filter2.ApplyInPlace(frameFilter);

                BitmapData objectsData = frameFilter.LockBits(new Rectangle(0, 0,
                    frameFilter.Width, frameFilter.Height), ImageLockMode.ReadOnly, frameFilter.PixelFormat);
                UnmanagedImage grayImage = grayscaleFilter.Apply(new UnmanagedImage(objectsData));
                frameFilter.UnlockBits(objectsData);

                blobCounter.ProcessImage(grayImage);
                Rectangle[] rects = blobCounter.GetObjectsRectangles();

                /////////////////////////////////////////////////////////////////////////////////
                Graphics lLine = Graphics.FromImage(frameFilter);
                lLine.DrawLine(
                    new Pen(Color.FromArgb(160, 255, 160), 2), safeDist, 0, (0 + safeDist), 480);
                Graphics rLine = Graphics.FromImage(frameFilter);
                rLine.DrawLine(
                    new Pen(Color.FromArgb(160, 255, 160), 2),
                    (640 - safeDist), 0, (640 - safeDist), 480);

                /////////////////////////////////////////////////////////////////////////////////

                if (rects.Length > 0)
                {
                    Rectangle objectRect = rects[0];
                    Graphics g = Graphics.FromImage(frameFilter);

                    using (Pen pen = new Pen(Color.FromArgb(160, 255, 160), 2))
                    {
                        g.DrawRectangle(pen, objectRect);
                    }
                    g.Dispose();

                    if (objectRect.X <= safeDist)
                        label7.BackColor = Color.Red;
                    else
                        label7.BackColor = Color.Lime;

                    if (objectRect.X + objectRect.Width >= 640 - safeDist)
                        label8.BackColor = Color.Red;
                    else
                        label8.BackColor = Color.Lime;

                    //objectX = objectRect.X + objectRect.Width / 2 - frameFilter.Width / 2;
                    //objectY = frameFilter.Height / 2 - (objectRect.Y + objectRect.Height / 2);                    
                }


                if (blobCounter.ObjectsCount == 1)
                {
                    if (objectFound == false)
                        if (OnObjectFound != null)
                        {
                            countOfObj++;
                            objectFound = true;
                            OnObjectFound(this);
                        }
                }
                else
                {
                    if (objectFound == true)
                    {
                        objectFound = false;
                        No_Object(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            pictureBoxSource.Image = frameSource;
            pictureBoxDest.Image = frameFilter;
        }

        private void btnToTakePicture_Click(object sender, EventArgs e)
        {
            try
            {
                if (tmpImg == null)
                    MessageBox.Show("Image not defined!");
                else
                {
                    arrColor.Clear();
                    ToTakePicture tmpWnd = new ToTakePicture(tmpImg);
                    tmpWnd.ShowDialog();
                    arrColor = tmpWnd.ArrColor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnColorDialog_Click(object sender, EventArgs e)
        {
            try
            {
                if (tmpImg == null)
                    MessageBox.Show("Image not defined!");
                else
                {
                    arrColor.Clear();
                    ColorDialog cd = new ColorDialog();
                    cd.ShowDialog();
                    arrColor.Add(cd.Color);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            range = (short)hScrollBar1.Value;
            hScrollBar1.Value = range;
            labelRadiusNum.Text = hScrollBar1.Value.ToString();
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            label3.Text = hScrollBar2.Value.ToString();
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            label5.Text = hScrollBar3.Value.ToString();
        }

        private void btnColorCollection_Click(object sender, EventArgs e)
        {
            FormCollorCollection fColColl = new FormCollorCollection();
            fColColl.ShowDialog();
            if (fColColl.ColorIsChecked)
            {
                arrColor.Clear();
                arrColor.Add(fColColl.SelectedColor);
            }
        }

        private void btnStarStop_Click(object sender, EventArgs e)
        {
            if (btnStarStop.BackColor == Color.Red)
            {
                btnStarStop.BackColor = Color.Blue;
                startStop = false;
                countOfObj = 0;
            }
            else
            {
                btnStarStop.BackColor = Color.Red;
                startStop = true;
            }
        }

        public String FormName { get { return this.formName.Text; } set { this.formName.Text = value; } }
        public bool StartStop { get { return this.startStop; } set { this.startStop = value; } }

        private void hScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            //MainForm mf = new MainForm();
            //mf.Delta = hScrollBar4.Value;
        }

        private void hScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            safeDist = hScrollBar5.Value;
        }

        private void hScrollBar6_Scroll(object sender, ScrollEventArgs e)
        {
            float newValue = (float)e.NewValue / 1000.0F;
            range1 = newValue;
        }

        private void hScrollBar7_Scroll(object sender, ScrollEventArgs e)
        {
            float newValue = (float)e.NewValue / 1000.0F;
            range2 = newValue;
        }

        private void hScrollBar8_Scroll(object sender, ScrollEventArgs e)
        {
            float newValue = (float)e.NewValue / 1000.0F;
            range3 = newValue;
        }

        private void hScrollBar11_Scroll(object sender, ScrollEventArgs e)
        {
            float newValue = (float)e.NewValue / 1000.0F;
            range4 = newValue;
        }

        private void hScrollBar10_Scroll(object sender, ScrollEventArgs e)
        {
            float newValue = (float)e.NewValue / 1000.0F;
            range5 = newValue;
        }

        private void hScrollBar9_Scroll(object sender, ScrollEventArgs e)
        {
            float newValue = (float)e.NewValue / 1000.0F;
            range6 = newValue;
        }

        private void formName_Click(object sender, EventArgs e)
        {

        }
    }
}
