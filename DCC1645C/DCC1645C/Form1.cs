using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCC1645C
{
    public partial class Form1 : Form
    {   
        
        CameraDriver _cameraDriver;
        public Form1()
        {
            InitializeComponent();
            _cameraDriver = new CameraDriver(this.Handle.ToInt64());
            _cameraDriver.Init(pictureBox.Handle.ToInt64());
            _cameraDriver.CameraCapture += _cameraDriver_CameraCapture;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages
            if (_cameraDriver != null)
            {
                switch (m.Msg)
                {
                    case uc480.IS_UC480_MESSAGE:
                        _cameraDriver.HandleMessage(m.Msg, m.LParam.ToInt64(), m.WParam.ToInt32());
                        break;
                }

            }
            base.WndProc(ref m);
        }
        void _cameraDriver_CameraCapture(object sender, Bitmap b)
        {
            pictureBox.Image = b;
        }
        private void Capture_Click(object sender, EventArgs e)
        {
            _cameraDriver.StartVideo();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            _cameraDriver.StopVideo();
        }
    }
}
