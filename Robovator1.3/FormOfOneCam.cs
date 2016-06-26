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

namespace Robovator1._3
{
    public partial class FormOfOneCam : Form
    {
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
        bool isObjectFound = false;
        int totalCountOfObjects = 0;
        int objectX = 0;
        int objectY = 0;

        public FormOfOneCam()
        {
            InitializeComponent();

            blobCounter.MinWidth = 100;
            blobCounter.MinHeight = 100;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;

            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                foreach (FilterInfo device in videoDevices)
                {
                    comboBoxDevices.Items.Add(device.Name);
                }

                comboBoxDevices.SelectedIndex = 0;
            }
            catch (ApplicationException)
            {
                comboBoxDevices.Items.Add("No local capture devices");
                videoDevices = null;
            }
        }
 
        private void videoSourcePlayer1_NewFrame_1(object sender, ref Bitmap image)
        {
            tmpImg = (Bitmap)image.Clone();

            Graphics g1 = Graphics.FromImage(image);
            Pen pen1 = new Pen(Color.FromArgb(160, 255, 160), 2);
            g1.DrawLine(pen1, 0, 60, 640, 60);
            g1.DrawLine(pen1, 0, 400, 640, 400);
            g1.DrawLine(pen1, 40, 0, 40, 480);
            g1.DrawLine(pen1, 600, 0, 600, 480);
            g1.Dispose();
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
                Graphics g1 = Graphics.FromImage(image);
                Pen pen1 = new Pen(Color.FromArgb(160, 255, 160), 2);
                g1.DrawLine(pen1, 0, 60, 640, 60);
                g1.DrawLine(pen1, 0, 400, 640, 400);
                g1.DrawLine(pen1, 40, 0, 40, 480);
                g1.DrawLine(pen1, 600, 0, 600, 480);
                g1.Dispose();

                if (blobCounter.ObjectsCount == 1 && objectY > 100)
                    isObjectFound = true;
                else
                    isObjectFound = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                MessageBox.Show(ex.ToString());
            }

            if (isObjectFound == true)
            {
                totalCountOfObjects++;
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.WaitForStop();
            videoSourcePlayer2.SignalToStop();
            videoSourcePlayer2.WaitForStop();
        }

        private void FormOfOneCam_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.WaitForStop();
            videoSourcePlayer2.SignalToStop();
            videoSourcePlayer2.WaitForStop();
        }

        private void btnToTalePicture_Click(object sender, EventArgs e)
        {
            try
            {
                if (tmpImg == null)
                    MessageBox.Show("Image not defined!");
                else
                {
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

        private void RadiusTrackBar_Scroll(object sender, EventArgs e)
        {
            range = (short)RadiusTrackBar.Value;
            RadiusTrackBar.Value = range;
            labelRadiusNum.Text = RadiusTrackBar.Value.ToString();
        }      
    }
}