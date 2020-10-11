using AprajitaRetailsWatcher.Model;
using AprajitaRetailsWatcher.Ops;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AprajitaRetailsXMLUploader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFileName_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string filename = txtFileName.Text.Trim();
            if (!String.IsNullOrEmpty(filename))
            {

                if (BasicOps.CheckFileExistance(filename))
                {
                    // string jsonData = BasicOps.XmlToJson(filename);
                    // txtAreaData.Text = jsonData;
                    //ss BasicOps.GenerateJsonFile(jsonData);

                    //root rootData = BasicOps.XmltoObject(filename);
                    //BasicOps.ObjectToXml(rootData);
                    txtAreaData.Text = BasicOps.JsontoJson(filename);
                }
                else
                {
                    MessageBox.Show("Kindly enter fileName which exist");
                }
            }
            else
            {
                MessageBox.Show("Kindly enter fileName");
            }
        }
    }
}
