﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;
using AForge.Video.DirectShow;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Controls;
using log4net;
using System.IO.Ports;

namespace Robovator1._3
{
    public partial class FormOfOneCam : Form
    {
        //VideoCapabilities vc;
        private static ILog _logger = LogManager.GetLogger(typeof(FormOfOneCam));
        FilterInfoCollection videoDevices;
        EuclideanColorFiltering filter = new EuclideanColorFiltering();
        Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
        BlobCounter blobCounter = new BlobCounter();
        Bitmap tmpImg = null;
        Color color = Color.Black;
        volatile List<Color> arrColor = new List<Color>();
        short range = 0;
        byte rAvg = 0;
        byte gAvg = 0;
        byte bAvg = 0;
        int objectX = 0;
        int objectY = 0;
        public delegate void ObjectIsFound();
        public event ObjectIsFound OnObjectFound;
        public delegate void NoObject();
        public event NoObject No_Object;
        double dtStart = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
        volatile bool objectFound = false;
        int countFrame = 0;

        public FormOfOneCam()
        {
            InitializeComponent();

            blobCounter.MinWidth = 135;
            blobCounter.MinHeight = 135;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;

            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                foreach (FilterInfo device in videoDevices)
                    comboBoxDevices.Items.Add(device.Name);



                comboBoxDevices.SelectedIndex = 0;
            }
            catch (ApplicationException apEx)
            {
                comboBoxDevices.Items.Add("No local capture devices");
                videoDevices = null;
                _logger.Warn("No local capture devices", apEx);
            }
        }

        private void videoSourcePlayer1_NewFrame_1(object sender, ref Bitmap image)
        {
            tmpImg = (Bitmap)image.Clone();
           

            Graphics g1 = Graphics.FromImage(image);
            StringBuilder imageResolition = new StringBuilder(image.Width + " x " + image.Height);
            g1.DrawString
                (
                imageResolition.ToString(),
                new Font("Arial", 22, FontStyle.Regular),
                new SolidBrush(Color.GreenYellow), 40, 40
                );

            //MainForm mf = new MainForm();
            //Graphics g2 = Graphics.FromImage(image);
            //try
            //{
            //    StringBuilder fps = new StringBuilder("FPS: " );
            //}
            //catch (Exception ex)
            //{
            //    int i = 0;
            //}
            //g2.DrawString
            //(
            //    fps.ToString(),
            //    new Font("Arial", 22, FontStyle.Regular),
            //    new SolidBrush(Color.GreenYellow), 40, 80
            //);
        }
        
        private void videoSourcePlayer2_NewFrame(object sender, ref Bitmap image)
        {
            try
            {

                if (arrColor.Count > 0)
                {
                    rAvg = (byte)arrColor.Average((a) => a.R);
                    gAvg = (byte)arrColor.Average((a) => a.G);
                    bAvg = (byte)arrColor.Average((a) => a.B);
                }

                filter.CenterColor = new RGB(rAvg, gAvg, bAvg);
                filter.Radius = range;
                filter.ApplyInPlace(image);

                BitmapData objectsData = image.LockBits(new Rectangle(0, 0,
                    image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
                UnmanagedImage grayImage = grayscaleFilter.Apply(new UnmanagedImage(objectsData));
                image.UnlockBits(objectsData);

                blobCounter.ProcessImage(grayImage);
                Rectangle[] rects = blobCounter.GetObjectsRectangles();

                if (rects.Length > 0)
                {
                    Rectangle objectRect = rects[0];
                    Graphics g = Graphics.FromImage(image);

                    using (Pen pen = new Pen(Color.FromArgb(160, 255, 160), 2))
                    {
                        g.DrawRectangle(pen, objectRect);
                    }
                    g.Dispose();

                    objectX = objectRect.X + objectRect.Width / 2 - image.Width / 2;
                    objectY = image.Height / 2 - (objectRect.Y + objectRect.Height / 2);
                }

                if (blobCounter.ObjectsCount == 1)
                {
                    if (objectFound == false)
                        if (OnObjectFound != null)
                        {
                            objectFound = true;
                            OnObjectFound();                            
                        }
                }
                else
                {
                    if (objectFound == true)
                    {
                        objectFound = false;
                        No_Object();
                    }
                }

                
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void BtnConnetct_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.WaitForStop();
            videoSourcePlayer2.SignalToStop();
            videoSourcePlayer2.WaitForStop();

            try
            {
                VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[comboBoxDevices.SelectedIndex].MonikerString);
                videoSource.VideoResolution = videoSource.VideoCapabilities[0];

                videoSourcePlayer1.VideoSource = videoSource;
                videoSourcePlayer1.Start();
                videoSourcePlayer2.VideoSource = videoSource;
                videoSourcePlayer2.Start();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                videoSourcePlayer1.SignalToStop();
                videoSourcePlayer1.WaitForStop();
                videoSourcePlayer2.SignalToStop();
                videoSourcePlayer2.WaitForStop();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void FormOfOneCam_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                videoSourcePlayer1.SignalToStop();
                videoSourcePlayer1.WaitForStop();
                videoSourcePlayer2.SignalToStop();
                videoSourcePlayer2.WaitForStop();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void btnToTalePicture_Click(object sender, EventArgs e)
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
                _logger.Error(ex);
            }
        }

        private void RadiusTrackBar_Scroll(object sender, EventArgs e)
        {
            range = (short)RadiusTrackBar.Value;
            RadiusTrackBar.Value = range;
            labelRadiusNum.Text = RadiusTrackBar.Value.ToString();
        }

        private void ColorDialog_Click(object sender, EventArgs e)
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
                _logger.Error(ex);
            }
        }

        private void numericUpDownMinWith_ValueChanged(object sender, EventArgs e)
        {
            //blobCounter.MinWidth = (int)numericUpDownMinWith.Value;
            //numericUpDownMinWith.Value = blobCounter.MinWidth;
        }

        private void videoSourcePlayer1_Click(object sender, EventArgs e)
        {

        }
    }
}
