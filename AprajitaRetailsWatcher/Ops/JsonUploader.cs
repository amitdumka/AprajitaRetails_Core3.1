using AprajitaRetails.Models.JsonData;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace AprajitaRetailsWatcher.Ops
{
    public class JsonUploader
    {
        public static void GenerateJsonFile(string jsonData)
        {
            string fileName = string.Empty;
            StreamWriter SW;
            if (Directory.Exists(FileInfos.LogFolder))
            {
                fileName = System.IO.Path.Combine(FileInfos.LogFolder, FileInfos.JsonFileName + DateTime.Now.ToString("yyyyMMdd") + ".txt");
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
        public static void GenerateJsonFile(string jsonData, string billNo)
        {
            string fileName = string.Empty;
            StreamWriter SW;
            if (Directory.Exists(FileInfos.LogFolder))
            {
                fileName = System.IO.Path.Combine(FileInfos.LogFolder, FileInfos.JsonFileName + billNo + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
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
                BasicOps.ErrorLog(ex);
                return "Error";
            }
        }

        public static async Task<ServerReturn> SendDataToServerAsync(Bill data)
        {
            using (var client = new HttpClient())
            {
                BasicOps.LogInfo($"Info: Uploading Started....\n");
                client.BaseAddress = new Uri(FileInfos.AppUri);
                var result = await client.PostAsJsonAsync<Bill>(FileInfos.ActionName, data);

                if (result.IsSuccessStatusCode)
                {
                    var returndata = await result.Content.ReadAsAsync<ServerReturn>();
                    if (returndata.Success)
                    {
                        BasicOps.LogInfo(returndata.SuccessMessage);
                        GenerateJsonFile(JsonConvert.SerializeObject(data), data.bill_number);

                    }
                    else if (returndata.Error)
                    {
                        BasicOps.LogInfo($"#Error:{returndata.ErrorMessage}");

                    }
                    return returndata;
                    // Write to log
                }
                else
                {

                    ServerReturn serverReturn = new ServerReturn { Error = true, Success = false, SuccessMessage = "Error from API Server", ErrorMessage = "Error:#Msg:" + result.ReasonPhrase };
                    // Write to log
                    BasicOps.LogFileInfo($"Error:#From API Server # Msg: {result.ReasonPhrase}");
                    return serverReturn;
                }
            }
        }

        /// <summary>
        /// Upload a Invoice.xml file to Web API
        /// </summary>
        /// <param name="fileName">filename with complete path.</param>
        /// <returns>returns server message</returns>
        public static Task<ServerReturn> UpLoadXMLFile(string fileName)
        {
            string data = XmlToJson(fileName);
            BasicOps.LogInfo("InvoiceData: \n" + data);
            try
            {
                Rootobject inv = JsonConvert.DeserializeObject<Rootobject>(data);
                return SendDataToServerAsync(inv.root.bill);

            }
            catch (Exception ex)
            {

                BasicOps.LogFileInfo($"Error:{ex.Message}\n Trying again with differnt method.");

                string newData = ConvertJsonToArray(data);
                Rootobject inv = JsonConvert.DeserializeObject<Rootobject>(newData);
                BasicOps.LogFileInfo($"Info:Success in converting to object with different method.");

                return SendDataToServerAsync(inv.root.bill);
            }


        }
        private static string ConvertJsonToArray(string data)
        {

            int loc = data.IndexOf("line_item\":");
            data = data.Insert(loc + 11, "[");
            int dloc = data.IndexOf("},", loc);

            data = data.Insert(dloc, "]");
            return data;
        }

    }
}