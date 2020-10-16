//using System.Net.Http;

namespace AprajitaRetailsWatcher.Ops
{
    //public class InvoiceUploader
    //{
    //    public static root XmltoObject(string filename)
    //    {
    //        // Now we can read the serialized book ...  
    //        System.Xml.Serialization.XmlSerializer reader =
    //            new System.Xml.Serialization.XmlSerializer (typeof (root));
    //        System.IO.StreamReader file = new System.IO.StreamReader (
    //            filename);
    //        root data = (root) reader.Deserialize (file);
    //        file.Close ();
    //        return data;
    //    }
    //    public static void ObjectToXml(root data, string fileName)
    //    {
    //        var writer = new System.Xml.Serialization.XmlSerializer (typeof (root));
    //        var wfile = new System.IO.StreamWriter (Path.Combine (FileInfos.LogFolder, fileName));
    //        writer.Serialize (wfile, data);
    //        wfile.Close ();
    //    }
    //    public static void SyncData(root data)
    //    {
    //        using ( var client = new HttpClient () )
    //        {
    //            client.BaseAddress = new Uri (FileInfos.LocalURi);

    //            var postTask = client.PostAsXmlAsync<root> (FileInfos.ActionName, data);

    //            postTask.Wait ();

    //            var result = postTask.Result;
    //            if ( result.IsSuccessStatusCode )
    //            {

    //                var readTask = result.Content.ReadAsAsync<ServerReturn> ();
    //                readTask.Wait ();

    //                var insertedStudent = readTask.Result;

    //                // Write to LogFile
    //            }
    //            else
    //            {
    //                // Write to LogFile
    //            }
    //        }
    //    }

    //}
}

