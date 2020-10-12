using AprajitaRetailsWatcher.Ops;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AprajitaRetailsWatcher
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                BasicOps.LogServiceStartStop(false);
                fileWatcher.Path = FileInfos.LocationName1;
            }
            catch (Exception ex)
            {
                BasicOps.ErrorLog(ex);                
            }
        }

        protected override void OnStop()
        {
            try
            {
                BasicOps.LogServiceStartStop(true);
            }
            catch (Exception ex)
            {

                BasicOps.ErrorLog(ex);
            }
        }

        private void FileWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                Thread.Sleep(70000);
                //Then we need to check file is exist or not which is created.  
                if (BasicOps. CheckFileExistance(FileInfos.LocationName1, e.Name))
                {
                    BasicOps.LogFileInfo(e.Name);
                    BasicOps.CopyAllXMLFile(FileInfos.LocationName1, FileInfos.XMLFolder);
                       
                }

            }
            catch (Exception ex)
            {

                BasicOps.ErrorLog(ex);
            }

        }

        private void FileWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void FileWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {

        }
    }
}
