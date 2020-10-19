using AprajitaRetailsWatcher.Ops;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AprajitaRetailsWatcher
{
    public partial class VoyWatcher : ServiceBase
    {
        public VoyWatcher()
        {
            InitializeComponent ();
        }

        protected override void OnStart(string [] args)
        {
            try
            {
                BasicOps.LogServiceStartStop (false);
                fileWatcher.Path = FileInfos.LocationName1;
            }
            catch ( Exception ex )
            {
                BasicOps.ErrorLog (ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                BasicOps.LogServiceStartStop (true);
            }
            catch ( Exception ex )
            {

                BasicOps.ErrorLog (ex);
            }
        }

        private void FileWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                Thread.Sleep (70000);
                //Then we need to check file is exist or not which is created.  
                if ( BasicOps.CheckFileExistance (FileInfos.LocationName1, e.Name) )
                {
                    BasicOps.LogFileInfo (e.Name);
                    // BasicOps.CopyAllXMLFile(FileInfos.LocationName1, FileInfos.XMLFolder);
                    string fileName = BasicOps.CopyFile (Path.Combine (FileInfos.LocationName1, FileInfos.FileWatcherName1), FileInfos.XMLFolder);
                    // Task t = Task.Factory.StartNew(async () =>        {
                    // task code here
                    var data = JsonUploader.UpLoadXMLFile (fileName);
                    data.Wait ();
                    BasicOps.LogInfo ($"serverreturn\t success:{data.Result.SuccessMessage} \n error:{data.Result.ErrorMessage}");
                    //});


                }

            }
            catch ( Exception ex )
            {

                BasicOps.ErrorLog (ex);
            }

        }

        private void FileWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            FileWatcher_Changed (sender, e);
        }

        private void FileWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            // FileWatcher_Changed(sender, e);
        }
    }
}
