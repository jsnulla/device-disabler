using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevicesTest
{
    public partial class Form1 : Form
    {
        Guid kinectCameraGuiId          = new Guid("{3A0339CD-B5F0-421C-8661-F243EEF1528C}");
        string kinectCameraInstanceId   = @"USB\VID_045E&PID_02BF\0000000000000000";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeviceHelper.SetDeviceEnabled(kinectCameraGuiId, kinectCameraInstanceId, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeviceHelper.SetDeviceEnabled(kinectCameraGuiId, kinectCameraInstanceId, false);
        }
    }
}
