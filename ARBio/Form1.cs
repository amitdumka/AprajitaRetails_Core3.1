using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zkemkeeper;

namespace ARBio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            CZKEM axCZKEM1 = new CZKEM();
            try
            {
                string ip = txtIpAddress.Text.Trim();
                int port = Int32.Parse(txtPortNo.Text.Trim());


                bool bIsConnected = axCZKEM1.Connect_Net(ip, port);   // 4370 is port no of attendance machine
                if (bIsConnected == true)
                {
                     MessageBox.Show("Device Connected Successfully");

                }
                else
                {
                     MessageBox.Show("Device Not Connect");
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
