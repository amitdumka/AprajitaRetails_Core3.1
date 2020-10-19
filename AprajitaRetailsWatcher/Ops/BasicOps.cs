using System;
using System.IO;
using System.Reflection;

namespace AprajitaRetailsWatcher.Ops
{
    public class FileInfos
    {
        public const string BaseFolder = "d:\\aprajitaretails";
        public const string LogFolder = "logFiles";
        public const string ErrorFolder = "ErrorLog";
        public const string ServiceLogFileName = "ServiceLogs_";
        public const string FileWatcherName1 = "invoice.xml";
        public const string LocationName1 = "C:\\Capillary";
        public const string XMLFolder = "d:\\aprajitaretails\\newxml";
        public const string LocalURi = "https://localhost:4550";
        public const string AppUri = "https://aprajitaretails.in";
        public const string JsonFileName = "JsonData_";
        public const string ActionName = "api/VoyBill";
    }

    public class PathList
    {
        public const string InvoiceXMLPath = @"C:\Capillary";
        public const string TabletSaleXMLPath = @"D:\VoyagerRetail\TabletSale";
        public const string InvoiceXMLFile = "invoice.xml";
        public const string VoyBillXMLFile = "VoyBill.XML";
        public const string TabletSaleXMLFile = "TabletBill.XML";
        public const string TailoringHubXMLPath = @"D:\VoyagerRetail\TailoringHub";
        public const string DataBasePath = @"D:\AprajitaRetails\Databases";
    }

    public class BasicOps
    {
        /// <summary>
        ///  This method will copy a file to new destination
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFolder"></param>
        public static string CopyFile(string sourceFile, string destinationFolder)
        {
            try
            {
                string fileName = Path.GetFileName(sourceFile);
                string newFileName = Path.Combine(destinationFolder, fileName);
                FileInfo file = new FileInfo(sourceFile);
                if (!(file.Exists && Directory.Exists(destinationFolder))) 
                    return sourceFile;
                File.Copy(sourceFile, newFileName, true);
                return newFileName;

            }
            catch (Exception ex)
            {

                ErrorLog(ex);
                return sourceFile;
            }
        }
        /// </summary>
        /// The purpose of this method is to copy xml from one location to another location at specified time period.
        /// </summary>
        public static void CopyAllXMLFile(string sourceFolder, string destinationFolder)
        {
            try
            {
                string filename = string.Empty;

                if (!(Directory.Exists(destinationFolder) && Directory.Exists(sourceFolder)))
                    return;
                string[] Templateexcelfile = Directory.GetFiles(sourceFolder);
                foreach (string file in Templateexcelfile)
                {
                    if (Templateexcelfile[0].Contains("Template"))
                    {
                        filename = System.IO.Path.GetFileName(file);
                        destinationFolder = System.IO.Path.Combine(destinationFolder, filename.Replace(".xml", DateTime.Now.ToUniversalTime().ToString()) + ".xml");
                        System.IO.File.Copy(file, destinationFolder, true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }
        }

        /// <summary>
        /// purpose of this method is to maintain error log in text file.
        /// </summary>
        /// <param name="exx"></param>
        public static void ErrorLog(Exception exx)
        {
            StreamWriter SW;
            if (!File.Exists(Path.Combine(FileInfos.BaseFolder,FileInfos.ErrorFolder, "txt_" + DateTime.Now.ToString("yyyyMMdd") + ".txt")))
            {
                SW = File.CreateText(Path.Combine(FileInfos.BaseFolder, FileInfos.ErrorFolder, "txt_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"));
                SW.Close();
            }
            using (SW = File.AppendText(Path.Combine(FileInfos.BaseFolder, FileInfos.ErrorFolder, "txt_" + DateTime.Now.ToString("yyyyMMdd") + ".txt")))
            {
                string[] str = new string[] { exx.Message==null?"":exx.Message.ToString(), exx.StackTrace==null?"":exx.StackTrace.ToString(),
            exx.InnerException==null?"":exx.InnerException.ToString()};
                for (int i = 0; i < str.Length; i++)
                {
                    SW.Write("\r\n\n");
                    if (str[i] == str[0])
                        SW.WriteLine("Exception Message:" + str[i]);
                    else if (str[i] == str[1])
                        SW.WriteLine("StackTrace:" + str[i]);
                    else if (str[i] == str[2])
                        SW.WriteLine("InnerException:" + str[i]);
                }

                SW.Close();
            }
        }

        public static void LogFileInfo(string InputFileName)
        {
            string fileName = string.Empty;
            StreamWriter SW;
            if (Directory.Exists(Path.Combine(FileInfos.BaseFolder, FileInfos.LogFolder)))
            {
                fileName = System.IO.Path.Combine(FileInfos.BaseFolder, FileInfos.LogFolder, FileInfos.ServiceLogFileName + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                if (!File.Exists(fileName))
                {
                    SW = File.CreateText(fileName);
                    SW.Close();
                }
            }
            using (SW = File.AppendText(fileName))
            {
                SW.Write("\r\n\n");
                SW.WriteLine($"FileInfo:({InputFileName}) changes at  " + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"));
                SW.Close();
            }
        }

        public static void LogInfo(string msg)
        {
            string fileName = string.Empty;
            StreamWriter SW;
            if ( Directory.Exists (Path.Combine (FileInfos.BaseFolder, FileInfos.LogFolder)) )
            {
                fileName = System.IO.Path.Combine(FileInfos.BaseFolder, FileInfos.LogFolder, FileInfos.ServiceLogFileName + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                if (!File.Exists(fileName))
                {
                    SW = File.CreateText(fileName);
                    SW.Close();
                }
            }
            using (SW = File.AppendText(fileName))
            {
                SW.Write("\r\n");
                SW.WriteLine(DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + $" : {msg}");
                SW.Close();
            }
        }

        /// </summary>
        /// The purpose of this method is maintain service stop information in text file.
        /// </summary>
        public static void LogServiceStartStop(bool isStop = false)
        {
            string fileName = string.Empty;
            StreamWriter SW;
            if ( Directory.Exists (Path.Combine (FileInfos.BaseFolder, FileInfos.LogFolder)) )
            {
                fileName = System.IO.Path.Combine(FileInfos.BaseFolder, FileInfos.LogFolder, FileInfos.ServiceLogFileName + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                if (!File.Exists(fileName))
                {
                    SW = File.CreateText(fileName);
                    SW.Close();
                }
            }
            using (SW = File.AppendText(fileName))
            {
                if (isStop)
                {
                    SW.Write("\r\n\n");
                    SW.WriteLine("Service Stopped at: " + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"));
                    SW.Close();
                }
                else
                {
                    SW.Write("\r\n\n");
                    SW.WriteLine("Service Stared at: " + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"));
                    SW.Close();
                }
            }
        }

        public static bool CheckFileExistance(string FullPath, string FileName)
        {
            // Get the subdirectories for the specified directory.'
            bool IsFileExist = false;
            DirectoryInfo dir = new DirectoryInfo(FullPath);
            if (!dir.Exists)
                IsFileExist = false;
            else
            {
                string FileFullPath = Path.Combine(FullPath, FileName);
                if (File.Exists(FileFullPath))
                    IsFileExist = true;
            }
            return IsFileExist;
        }
    }
}