using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Robovator2.Controls;

namespace Robovator2
{
    public partial class MainForm : Form
    {
        private class FoundObject
        {
            public long objStart;
            public long objLenght;
            public bool isVisible = false;
        }

        char[] ch = new char[2];
        public int countEncoder = 0;
        int currentCountEncoder = 0;
        private delegate void LineRecevidEvent(string command);
        long encoderCount = 0;
        int tmpEncoderCount = 0;
        private volatile Queue<FoundObject> quObj = new Queue<FoundObject>();
        private FoundObject currentObj = null;
        private int codeEnable = 0;
        public int objectFounds = 0;
        public long delta = 10;

        private List<Color> colorCollection = new List<Color>();
        private Dictionary<String, UserControlOneCam> arrOneCam = new Dictionary<string, UserControlOneCam>();

        public long Delta { get { return this.delta; } set { this.delta = value; } }

        public MainForm()
        {
            InitializeComponent();

            initCam();

            try
            {
                serialPort1.PortName = "COM3";
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
                serialPort1.DataReceived += serialPort1_DataReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No COM PORT!");
            }

            foreach (KeyValuePair<String, UserControlOneCam> kvp in arrOneCam)
            {
                TabPage tmpTabPage = new TabPage(kvp.Key);
                tmpTabPage.Name = kvp.Key;
                tmpTabPage.Text = kvp.Key;
                tmpTabPage.Controls.Add(kvp.Value);
                tabControl1.TabPages.Add(tmpTabPage);
            }
        }

        void UserControlOneCam_No_Object(UserControlOneCam sender)
        {

            //if (sender.startStop == true)
            //{
                currentObj.objLenght = countEncoder - currentObj.objStart;
                currentObj.objStart = currentObj.objStart + currentObj.objLenght;
                if (currentObj.objLenght != 0 && currentObj.objStart != 0)
                    quObj.Enqueue(currentObj);
                currentObj = null;
            //}
        }

        private void initCam()
        {
            UserControlOneCam tmpObj = new Robovator2.Controls.UserControlOneCam();
            tmpObj.FormName = "Camera 1";
            tmpObj.OnObjectFound += UserControlOneCam_OnObjectFound;
            tmpObj.No_Object += UserControlOneCam_No_Object;
            arrOneCam.Add("Camera 1", tmpObj);

            tmpObj = new Robovator2.Controls.UserControlOneCam();
            tmpObj.FormName = "Camera 2";
            tmpObj.OnObjectFound += UserControlOneCam_OnObjectFound;
            tmpObj.No_Object += UserControlOneCam_No_Object;
            arrOneCam.Add("Camera 2", tmpObj);

            tmpObj = new Robovator2.Controls.UserControlOneCam();
            tmpObj.FormName = "Camera 3";
            tmpObj.OnObjectFound += UserControlOneCam_OnObjectFound;
            tmpObj.No_Object += UserControlOneCam_No_Object;
            arrOneCam.Add("Camera 3", tmpObj);

            tmpObj = new Robovator2.Controls.UserControlOneCam();
            tmpObj.FormName = "Camera 4";
            tmpObj.OnObjectFound += UserControlOneCam_OnObjectFound;
            tmpObj.No_Object += UserControlOneCam_No_Object;
            arrOneCam.Add("Camera 4", tmpObj);

            tmpObj = new Robovator2.Controls.UserControlOneCam();
            tmpObj.FormName = "Camera 5";
            tmpObj.OnObjectFound += UserControlOneCam_OnObjectFound;
            tmpObj.No_Object += UserControlOneCam_No_Object;
            arrOneCam.Add("Camera 5", tmpObj);

            tmpObj = new Robovator2.Controls.UserControlOneCam();
            tmpObj.FormName = "Camera 6";
            tmpObj.OnObjectFound += UserControlOneCam_OnObjectFound;
            tmpObj.No_Object += UserControlOneCam_No_Object;
            arrOneCam.Add("Camera 6", tmpObj);
        }

        void UserControlOneCam_OnObjectFound(UserControlOneCam sender)
        {
            //if (sender.startStop == true)
            //{
                objectFounds++;
                currentObj = new FoundObject();
                currentObj.objStart = countEncoder;
            //}
        }

        void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string command = serialPort1.ReadLine();
                this.BeginInvoke(new LineRecevidEvent(LineRecevid), command);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LineRecevid(string command)
        {
            UserControlOneCam uc = new UserControlOneCam();
            try
            {
                //if (uc.StartStop == true)
                //{
                    tmpEncoderCount++;
                    if (tmpEncoderCount >= 1)
                    {
                        tmpEncoderCount = 0;
                        encoderCount++;
                        countEncoder++;
                        proc();
                    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void proc()
        {
            try
            {
                if (quObj.Count > 0)
                {
                    FoundObject tmpObj = quObj.Peek();
                    if (countEncoder >= tmpObj.objStart + delta
                        && tmpObj.isVisible == false)
                    {
                        tmpObj.isVisible = true;
                        ch[0] = 'q';
                        serialPort1.Write(ch, 0, 1);
                    }
                    else
                    {
                        if (countEncoder >= tmpObj.objStart + tmpObj.objLenght + delta)
                        {
                            quObj.Dequeue();
                            ch[0] = 'w';
                            serialPort1.Write(ch, 0, 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (KeyValuePair<String, UserControlOneCam> kvp in arrOneCam)
                if (kvp.Value.FinalFrame != null && kvp.Value.FinalFrame.IsRunning == true)
                {
                    kvp.Value.FinalFrame.SignalToStop();
                    kvp.Value.FinalFrame.WaitForStop();
                }

            if (serialPort1.IsOpen) serialPort1.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
