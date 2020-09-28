using AprajitaRetails.Areas.Accountings.Models;
using AprajitaRetails.Data.Json;
using Castle.Components.DictionaryAdapter;
using Newtonsoft.Json;
//using Remotion.Mixins.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Uploader.Models
{
    public class InputData
    {
        public int StartRow { get; set; }
        public int StartCol { get; set; }
        public int EndRow { get; set; }
        public int EndCol { get; set; }

        public string ColDefined { get; set; }
        public string AccountNumber { get; set; }

        public int TransDate { get; set; }
        public int ValueDate { get; set; }
        public int ChequeNumber { get; set; }
        public int Trans { get; set; }
        public int InAmount { get; set; }
        public int OutAmount { get; set; }
        public int BalAmount { get; set; }

        public bool WantToSave { get; set; }

        public static void CopyObjects(string Data, out InputData inputData)
        {

            List<JsonToObject> jsonArray = JsonConvert.DeserializeObject<List<JsonToObject>>(Data);

            InputData d = new InputData() { WantToSave = false };

            foreach (var item in jsonArray)
            {
                switch (item.Name)
                {
                    case "WantToSave": if (item.Value == "on") d.WantToSave = true; else d.WantToSave = false; break;
                    case "StartRow": d.StartRow = Int32.Parse(item.Value); break;
                    case "StartCol": d.StartCol = Int32.Parse(item.Value); break;
                    case "EndRow": d.EndRow = Int32.Parse(item.Value); break;
                    case "EndCol": d.EndCol = Int32.Parse(item.Value); break;

                    case "AccountNumber": d.AccountNumber = item.Value; break;
                    case "ColDefined": d.ColDefined = item.Value; break;
                    //TODO: it should parse and validate 
                    case "TransDate": if (d.ColDefined == "Define") d.TransDate = Int32.Parse(item.Value); break;
                    case "ValueDate": if (d.ColDefined == "Define") d.ValueDate = Int32.Parse(item.Value); break;
                    case "ChequeNumber": if (d.ColDefined == "Define") d.ChequeNumber = Int32.Parse(item.Value); break;
                    case "Trans": if (d.ColDefined == "Define") d.Trans = Int32.Parse(item.Value); break;
                    case "InAmount": if (d.ColDefined == "Define") d.InAmount = Int32.Parse(item.Value); break;
                    case "OutAmount": if (d.ColDefined == "Define") d.OutAmount = Int32.Parse(item.Value); break;
                    case "BalAmount": if (d.ColDefined == "Define") d.BalAmount = Int32.Parse(item.Value); break;


                    default:
                        break;
                }

            }



            inputData = d;
        }
    }

    public static class InputDataExtension
    {
        public static InputData CopyInto(this InputData inputData, string Data)
        {
            List<JsonToObject> jsonArray = JsonConvert.DeserializeObject<List<JsonToObject>>(Data);
            InputData d = new InputData() { WantToSave = false };
            foreach (var item in jsonArray)
            {
                switch (item.Name)
                {
                    case "WantToSave": if (item.Value == "on") d.WantToSave = true; else d.WantToSave = false; break;
                    case "StartRow": d.StartRow = Int32.Parse(item.Value); break;
                    case "StartCol": d.StartCol = Int32.Parse(item.Value); break;
                    case "EndRow": d.EndRow = Int32.Parse(item.Value); break;
                    case "EndCol": d.EndCol = Int32.Parse(item.Value); break;

                    case "AccountNumber": d.AccountNumber = item.Value; break;
                    case "ColDefined": d.ColDefined = item.Value; break;

                    case "TransDate": d.TransDate = Int32.Parse(item.Value); break;
                    case "ValueDate": d.ValueDate = Int32.Parse(item.Value); break;
                    case "ChequeNumber": d.ChequeNumber = Int32.Parse(item.Value); break;
                    case "Trans": d.Trans = Int32.Parse(item.Value); break;
                    case "InAmount": d.InAmount = Int32.Parse(item.Value); break;
                    case "OutAmount": d.OutAmount = Int32.Parse(item.Value); break;
                    case "BalAmount": d.BalAmount = Int32.Parse(item.Value); break;


                    default:
                        break;
                }

            }
            inputData = d;
            return d;
        }
    }

    public class BankSetting
    {
        //[System.ComponentModel.DataAnnotations.Key]
        public int ID { get; set; }
        public string SettingName { get; set; }
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }

        public int StartRow { get; set; }
        public int StartCol { get; set; }
        public int EndRow { get; set; }
        public int EndCol { get; set; }

        public int TransDate { get; set; }
        public int ValueDate { get; set; }
        public int ChequeNumber { get; set; }
        public int Trans { get; set; }
        public int InAmount { get; set; }
        public int OutAmount { get; set; }
        public int BalAmount { get; set; }
    }

    public class AccSetting
    {
        public int ID { get; set; }

        [ForeignKey("BankSetting")]
        public int BankSettingId { get; set; }
        public virtual BankSetting BankSetting { get; set; }

        public int AccountNumberId { get; set; }
        //public virtual AccountNumber AccountNumber { get; set; }
    }

}
