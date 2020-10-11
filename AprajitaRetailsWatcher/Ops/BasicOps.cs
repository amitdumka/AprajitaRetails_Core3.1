using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AprajitaRetailsWatcher.Ops
{
    public class FileInfos
    {
        public const string LogFolder = "d:\\logFiles";
        public const string ServiceLogFileName = "ServiceLogs_";

        public const string FileWatcherName1 = "invoice.xml";
        public const string LocationName1 = "c:\\capilary";

        public const string XMLFolder = "d:\\aprajitaretails\\newxml";
        public const string LocalURi = "https://localhost:4550/api/UploadInvoice";
        public const string AppUri = "https://aprajitaretails.in/api/UploadInvoice";
        public const string JsonFileName = "JsonData_";

    }

    public class BasicOps
    {
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
                Create_ErrorFile(ex);
            }

        }
        /// <summary>  
        /// purpose of this method is to maintain error log in text file.  
        /// </summary>  
        /// <param name="exx"></param>  
        public static void Create_ErrorFile(Exception exx)
        {
            StreamWriter SW;
            if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "txt_" + DateTime.Now.ToString("yyyyMMdd") + ".txt")))
            {
                SW = File.CreateText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "txt_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"));
                SW.Close();
            }
            using (SW = File.AppendText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "txt_" + DateTime.Now.ToString("yyyyMMdd") + ".txt")))
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
            if (Directory.Exists(FileInfos.LogFolder))
            {
                fileName = System.IO.Path.Combine(FileInfos.LogFolder, FileInfos.ServiceLogFileName + DateTime.Now.ToString("yyyyMMdd") + ".txt");
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

        /// </summary>
        /// The purpose of this method is maintain service stop information in text file.  
        /// </summary>  
        public static void LogServiceStartStop(bool isStop = false)
        {

            string fileName = string.Empty;
            StreamWriter SW;
            if (Directory.Exists(FileInfos.LogFolder))
            {
                fileName = System.IO.Path.Combine(FileInfos.LogFolder, FileInfos.ServiceLogFileName + DateTime.Now.ToString("yyyyMMdd") + ".txt");
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


        public static string XmlToJson(string fileName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                string json = JsonConvert.SerializeXmlNode(doc);
                
                return json;

            }
            catch (Exception ex)
            {

                Create_ErrorFile(ex);
                return "Error";
            }
        }


        public static void SendDataToServer(string jsonData)
        {
            VoyBill bill = JsonConvert.DeserializeObject<VoyBill>(jsonData);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(FileInfos.LocalURi);

                var postTask = client.PostAsJsonAsync<VoyBill>("UploadInvoice", bill);
                
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<VoyBill>();
                    readTask.Wait();

                    var insertedStudent = readTask.Result;

                    Console.WriteLine("Student {0} inserted with id: {1}", insertedStudent.InvNo, insertedStudent.Id);
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }

        }

        public static void GenerateJsonFile(string jsonData)
        {
            string fileName = string.Empty;
            StreamWriter SW;
            if (Directory.Exists(FileInfos.LogFolder))
            {
                fileName = System.IO.Path.Combine(FileInfos.LogFolder, FileInfos.ServiceLogFileName + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                if (!File.Exists(fileName))
                {
                    SW = File.CreateText(fileName);
                    SW.Close();
                }
            }
            using (SW = File.AppendText(fileName))
            {
                SW.Write("\r\n\n");
                SW.Write($"\n Added At {DateTime.Now.ToString()}\n\n");
                SW.WriteLine(jsonData);
                SW.WriteLine("End of Data============================================>");
                SW.Close();
            }
        }
    }
    public class VoyBill
    {
        public int Id { get; set; }
       public  string InvNo { get; set; }

    }
}
