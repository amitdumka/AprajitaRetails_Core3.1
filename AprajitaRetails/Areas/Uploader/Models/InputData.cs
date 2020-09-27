using System;

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
        }
    }

}
