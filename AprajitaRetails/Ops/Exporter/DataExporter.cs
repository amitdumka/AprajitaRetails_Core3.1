using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using OfficeOpenXml;

public enum ExportMode { PDF, XLS, ONSCREEN }
namespace AprajitaRetails.Ops.Exporter
{
    public abstract class DataExporter<T>
    {
        protected static  ExcelWorksheet AddDocumentHeader(ExcelWorksheet worksheet, string typeOfReport)
        {
            worksheet.Cells [1, 3].Value = "Aprajita Retails";
            worksheet.Cells [2, 3].Value = "Bhagalpur Road, Dumka";
            worksheet.Cells [3, 2].Value = "Date: " + DateTime.Now.Date.ToShortDateString ();
            worksheet.Cells [5, 3].Value = typeOfReport;
            return worksheet;
        }
    }


    public class AttendanceReport : DataExporter<Attendance>
    {
        public static string ToExcel( Attendance item)
        {
            string fileName = "AttendanceReport_" + DateTime.Now.ToUniversalTime ().ToString ();
            fileName += ".xls";
            FileInfo file = new FileInfo (Path.Combine ("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage (file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add ("Attendance");
            worksheet2 = AddDocumentHeader (worksheet2, "Attendance");
            worksheet2.Cells [7, 1].Value = "Date";
            worksheet2.Cells [7, 2].Value = "StaffName";
            worksheet2.Cells [7, 3].Value = "Status";
            worksheet2.Cells [7, 4].Value = "Entry Time";
            worksheet2.Cells [7, 5].Value = "Remarks";
            int row = 8;
                worksheet2.Cells [row, 1].Value = item.AttDate;
                worksheet2.Cells [row, 2].Value = item.Employee.StaffName;
                worksheet2.Cells [row, 3].Value = item.Status;
                worksheet2.Cells [row, 4].Value = item.EntryTime;
                worksheet2.Cells [row, 5].Value = item.Remarks;
                row++;
            package.Save ();
            return fileName;
        }

        public string ToExcel(List<Attendance> lists)
        {
            string fileName = "AttendanceReport_"+DateTime.Now.ToUniversalTime().ToString();
            fileName += ".xls";
            FileInfo file = new FileInfo (Path.Combine ("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage (file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add ("Attendance");
            worksheet2 = AddDocumentHeader (worksheet2, "Attendance");
           
            worksheet2.Cells [7, 1].Value = "Date";
            worksheet2.Cells [7, 2].Value = "StaffName";
            worksheet2.Cells [7, 3].Value = "Status";
            worksheet2.Cells [7, 4].Value = "Entry Time";
            worksheet2.Cells [7, 5].Value = "Remarks";

            int row = 8;
            foreach ( var item in lists )
            {
                worksheet2.Cells [row, 1].Value = item.AttDate;
                worksheet2.Cells[row, 2].Value = item.Employee.StaffName;
                worksheet2.Cells [row, 3].Value = item.Status;
                worksheet2.Cells [row, 4].Value = item.EntryTime;
                worksheet2.Cells [row, 5].Value = item.Remarks;
                row++;
            }
            package.Save ();
            return fileName;
        }

        public string ToExcelForEmployee(List<Attendance> lists, string fileName)
        {
           // string fileName = "AttendanceReport_ForEmo_" + lists.First ().Employee.StaffName + "_" + DateTime.Now.ToUniversalTime ().ToString ();
           // fileName += ".xls";
            FileInfo file = new FileInfo (Path.Combine ("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage (file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add (lists.First ().Employee.StaffName);
            worksheet2 = AddDocumentHeader (worksheet2, "Attendance");

            worksheet2.Cells [6, 2].Value = "Staff Name";
            worksheet2.Cells [6, 3].Value = lists.First ().Employee.StaffName;

            worksheet2.Cells [8, 1].Value = "Date";
            worksheet2.Cells [8, 2].Value = "Status";
            worksheet2.Cells [8, 3].Value = "Entry Time";
            worksheet2.Cells [8, 4].Value = "Remarks";

            int totalAbsent = 0;
            int totalPresent = 0;
            int totalHalfday = 0;
            int totalSunday = 0;

            int row = 8;
            foreach ( var item in lists )
            {
                worksheet2.Cells [row, 1].Value = item.AttDate;
                worksheet2.Cells [row, 2].Value = item.Status;
                worksheet2.Cells [row, 3].Value = item.EntryTime;
                worksheet2.Cells [row, 4].Value = item.Remarks;
                row++;
                switch ( item.Status )
                {
                    case AttUnits.Present:
                        totalPresent++;
                        break;
                    case AttUnits.Absent:
                        totalAbsent++;
                        break;
                    case AttUnits.HalfDay:
                        totalHalfday++;
                        break;
                    case AttUnits.Sunday:
                        totalSunday++;
                        break;
                    case AttUnits.Holiday:

                        break;
                    case AttUnits.StoreClosed:

                        break;
                    default:
                        break;
                }
            }

            row = row + 2;
            worksheet2.Cells [row, 2].Value = "Total Absent";
            worksheet2.Cells [row, 3].Value = totalAbsent;
            row++;
            worksheet2.Cells [row, 2].Value = "Total Half Day";
            worksheet2.Cells [row, 3].Value = totalHalfday;

            row++;
            worksheet2.Cells [row, 2].Value = "Total Sunday Present";
            worksheet2.Cells [row, 3].Value = totalSunday;
            row++;
            worksheet2.Cells [row, 2].Value = "Total Present";
            worksheet2.Cells [row, 3].Value = totalPresent;
            row++;
            worksheet2.Cells [row, 2].Value = "Net Present";
            worksheet2.Cells [row, 3].Value = totalPresent + totalSunday + ( totalHalfday / 2 );



            package.Save ();
            return fileName;
        }
        public string ToExcelForEmployee(List<Attendance> lists)
        {
            string fileName = "AttendanceReport_ForEmo_"+lists.First().Employee.StaffName+"_" + DateTime.Now.ToUniversalTime ().ToString ();
            fileName += ".xls";
            FileInfo file = new FileInfo (Path.Combine ("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage (file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add (lists.First ().Employee.StaffName);
            worksheet2 = AddDocumentHeader (worksheet2, "Attendance");

            worksheet2.Cells[6,2].Value = "Staff Name";
            worksheet2.Cells [6, 3].Value = lists.First ().Employee.StaffName;

            worksheet2.Cells [8, 1].Value = "Date";
            worksheet2.Cells [8, 2].Value = "Status";
            worksheet2.Cells [8, 3].Value = "Entry Time";
            worksheet2.Cells [8, 4].Value = "Remarks";

            int totalAbsent = 0;
            int totalPresent = 0;
            int totalHalfday = 0;
            int totalSunday = 0;

            int row = 8;
            foreach ( var item in lists )
            {
                worksheet2.Cells [row, 1].Value = item.AttDate;
                worksheet2.Cells [row, 2].Value = item.Status;
                worksheet2.Cells [row, 3].Value = item.EntryTime;
                worksheet2.Cells [row, 4].Value = item.Remarks;
                row++;
                switch ( item.Status )
                {
                    case AttUnits.Present: totalPresent++;
                        break;
                    case AttUnits.Absent:
                        totalAbsent++;
                        break;
                    case AttUnits.HalfDay:
                        totalHalfday++;
                        break;
                    case AttUnits.Sunday:
                        totalSunday++;
                        break;
                    case AttUnits.Holiday:
                       
                        break;
                    case AttUnits.StoreClosed:
                       
                        break;
                    default:
                        break;
                }
            }

            row = row + 2;
            worksheet2.Cells [row, 2].Value = "Total Absent";
            worksheet2.Cells [row,3].Value = totalAbsent;
            row++;
            worksheet2.Cells [row, 2].Value = "Total Half Day";
            worksheet2.Cells [row, 3].Value = totalHalfday;

            row++;
            worksheet2.Cells [row, 2].Value = "Total Sunday Present";
            worksheet2.Cells [row, 3].Value = totalSunday;
            row++;
            worksheet2.Cells [row, 2].Value = "Total Present";
            worksheet2.Cells [row, 3].Value = totalPresent;
            row++;
            worksheet2.Cells [row, 2].Value = "Net Present";
            worksheet2.Cells [row, 3].Value = totalPresent + totalSunday + ( totalHalfday / 2 );



            package.Save ();
            return fileName;
        }

        
    }


}
