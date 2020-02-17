using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AprajitaRetails.Ops.Helpers
{
    public static class XMLExporter
    {
        public static string WriteToXML(AprajitaRetailsContext db)
        {
            var p = db.Employees;
            string json = JsonConvert.SerializeObject(p);
            
            return json;
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    var serializer = new XmlSerializer(typeof(TranscationMode));
            //    var p = db.TranscationModes.First();
            //    serializer.Serialize(stream, p);
               
            //    stream.Seek(0, SeekOrigin.Begin);
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        string xml = reader.ReadToEnd();

            //        return xml;
            //    }

            //}
        }
    
    
        public static void ToXML()
        {

            using MemoryStream stream = new MemoryStream ();
            // Create an XML document. Write our specific values into the document.
            XmlTextWriter xmlWriter = new XmlTextWriter (stream, System.Text.Encoding.ASCII);
            // Write the XML document header.
            xmlWriter.WriteStartDocument ();
            // Write our first XML header.
            xmlWriter.WriteStartElement ("AprajitaRetails");
            // Write an element representing a single web application object.
            xmlWriter.WriteStartElement ("AprajitaRetailsContext");
            // Write child element data for our web application object.
            xmlWriter.WriteElementString ("Date", DateTime.Now.ToString ());
            xmlWriter.WriteElementString ("DataBaseName", "AprajitaRetails");
            xmlWriter.WriteElementString ("ApplicationName", "AprajitaRetails");
            // End the element WebApplication
            xmlWriter.WriteEndElement ();
            // End the document WebApplications
            xmlWriter.WriteEndElement ();
            // Finilize the XML document by writing any required closing tag.
            xmlWriter.WriteEndDocument ();

            // To be safe, flush the document to the memory stream.
            xmlWriter.Flush ();
            // Convert the memory stream to an array of bytes.
            byte [] byteArray = stream.ToArray ();

            // Send the XML file to the web browser for download.
            //Response.Clear();
            //Response.AppendHeader("Content-Disposition", "filename=MyExportedFile.xml");
            //Response.AppendHeader("Content-Length", byteArray.Length.ToString());
            //Response.ContentType = "application/octet-stream";
            //Response.BinaryWrite(byteArray);

            xmlWriter.Close ();
        }
    
    }
}
