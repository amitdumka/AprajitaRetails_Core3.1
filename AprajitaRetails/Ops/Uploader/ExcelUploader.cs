using AprajitaRetails.Areas.Uploader.Models;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using AprajitaRetails.Areas.Voyager.Data;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;

namespace AprajitaRetails.Ops.Uploader
{
    public class ExcelUploaders
    {
        public UploadReturns UploadExcel(VoyagerContext db, UploadTypes UploadType, IFormFile FileUpload, string targetpath, bool IsVat, bool IsLocal)
        {

            //UploadType = "InWard";
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;

                    string pathToExcelFile = Path.GetTempPath() + filename;
                    using (var stream = new FileStream(pathToExcelFile, FileMode.Create))
                    {
                        FileUpload.CopyTo(stream);
                    }

                    if (UploadType == UploadTypes.Purchase)
                    {
                        try
                        {
                            ImportPurchase(db, pathToExcelFile, IsVat, IsLocal);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            return UploadReturns.Error;
                        }
                    }
                    else if (UploadType == UploadTypes.SaleItemWise)
                    {
                        try
                        {
                            ImportSaleItemWise(db, pathToExcelFile, IsVat, IsLocal);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            return UploadReturns.Error;
                        }
                    }
                    else if (UploadType == UploadTypes.SaleRegister)
                    {
                        try
                        {
                            ImportPurchase(db, pathToExcelFile, IsVat, IsLocal);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            return UploadReturns.Error;
                        }
                    }
                    else if (UploadType == UploadTypes.InWard)
                    {
                        try
                        {
                            ImportPurchase(db, pathToExcelFile, IsVat, IsLocal);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            return UploadReturns.Error;
                        }
                    }
                    else
                    {
                        return UploadReturns.ImportNotSupported;
                    }

                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return UploadReturns.Success;



                }//end of if contexttype
                else
                {
                    return UploadReturns.NotExcelType;
                }

            }//end of if fileupload
            else
            {
                return UploadReturns.FileNotFound;
            }

        }//end of function

        public UploadReturns UploadExcel_DotNet(VoyagerContext db, UploadTypes UploadType, IFormFile FileUpload, string targetpath, bool IsVat, bool IsLocal)
        {

            //UploadType = "InWard";
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    //TODO: pass from calling function of http post
                    // string targetpath = Server.MapPath("~/Doc/"); 
                    //FileUpload.SaveAs(targetpath + filename);
                    using (var stream = new FileStream(targetpath + filename, FileMode.Create))
                    {
                        FileUpload.CopyTo(stream);
                    }

                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    // var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    //var ds = new DataSet();

                    //adapter.Fill(ds, "ExcelTable");

                    //DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);

                    if (UploadType == UploadTypes.Purchase)
                    {
                        var currentImports = from a in excelFile.Worksheet<ImportPurchase>(sheetName) select a;
                        foreach (var a in currentImports)
                        {
                            try
                            {
                                a.ImportTime = DateTime.Now;
                                //Vat & Local 
                                a.IsLocal = IsLocal; a.IsVatBill = IsVat;

                                db.ImportPurchases.Add(a);
                                db.SaveChanges();
                            }
                            catch (Exception DbEntityValidationException)
                            {
                                //TODO: need to handel this
                                return UploadReturns.Error;
                            }
                        }
                    }
                    else if (UploadType == UploadTypes.SaleItemWise)
                    {
                        var currentImports = from a in excelFile.Worksheet<ImportSaleItemWiseVM>(sheetName) select a;
                        foreach (var a in currentImports)
                        {
                            try
                            {
                                a.ImportTime = DateTime.Now;
                                a.IsDataConsumed = false;
                                db.ImportSaleItemWises.Add(ImportSaleItemWiseVM.ToTable(a));
                                db.SaveChanges();
                            }
                            catch (Exception DbEntityValidationException)
                            {
                                //TODO: need to handel this
                                return UploadReturns.Error;
                            }
                        }
                    }
                    else if (UploadType == UploadTypes.SaleRegister)
                    {
                        var currentImports = from a in excelFile.Worksheet<ImportSaleRegister>(sheetName) select a;
                        foreach (var a in currentImports)
                        {
                            try
                            {
                                a.ImportTime = DateTime.Now;
                                a.IsConsumed = false;
                                db.ImportSaleRegisters.Add(a);
                                db.SaveChanges();
                            }
                            catch (Exception DbEntityValidationException)
                            {
                                //TODO: need to handel this
                                return UploadReturns.Error;
                            }
                        }
                    }
                    else if (UploadType == UploadTypes.InWard)
                    {
                        var currentImports = from a in excelFile.Worksheet<ImportInWard>(sheetName) select a;
                        foreach (var a in currentImports)
                        {
                            try
                            {
                                // Inward No   Inward Date Invoice No  Invoice Date    Party Name  Total Qty   Total MRP Value Total Cost
                                ImportInWard inw = new ImportInWard
                                {
                                    ImportDate = DateTime.Today,
                                    InvoiceDate = a.InvoiceDate,
                                    InvoiceNo = a.InvoiceNo,
                                    InWardDate = a.InWardDate,
                                    InWardNo = a.InWardNo,
                                    PartyName = a.PartyName,
                                    TotalCost = a.TotalCost,
                                    TotalMRPValue = a.TotalMRPValue,
                                    TotalQty = a.TotalQty
                                };
                                db.ImportInWards.Add(inw);
                                db.SaveChanges();


                            }
                            catch (Exception DbEntityValidationException)
                            {
                                //TODO: need to handel this

                                return UploadReturns.Error;
                            }
                        }
                    }
                    else
                    {
                        return UploadReturns.ImportNotSupported;
                    }

                    //deleting excel file from folder  


                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return UploadReturns.Success;



                }//end of if contexttype
                else
                {
                    return UploadReturns.NotExcelType;
                }

            }//end of if fileupload
            else
            {
                return UploadReturns.FileNotFound;
            }

        }//end of function

        public int ImportSaleItemWise(VoyagerContext db, string fileName, bool IsVat, bool IsLocal)
        {
            //string rootFolder = IHostingEnvironment.WebRootPath;
            //string fileName = @"ImportCustomers.xlsx";
            FileInfo file = new FileInfo(fileName);

            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;

            List<ImportSaleItemWise> saleList = new List<ImportSaleItemWise>();
            //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3
            int xo = 0;
            for (int i = 2; i <= totalRows; i++)
            {
                ImportSaleItemWise p = new ImportSaleItemWise
                {
                    InvoiceType = workSheet.Cells[i, 1].Value.ToString(),
                    InvoiceDate = (DateTime)workSheet.Cells[i, 4].GetValue<DateTime>(),
                    InvoiceNo = workSheet.Cells[i, 3].Value.ToString(),
                    BrandName = workSheet.Cells[i, 5].Value.ToString(),
                    Barcode = workSheet.Cells[i, 6].Value.ToString(),
                    ProductName = workSheet.Cells[i, 7].Value.ToString(),
                    StyleCode = workSheet.Cells[i, 8].Value.ToString(),
                    ItemDesc = workSheet.Cells[i, 9].Value.ToString(),
                    Quantity = (double)workSheet.Cells[i, 10].Value,
                    MRP = (decimal)workSheet.Cells[i, 11].GetValue<decimal>(),
                    BillAmnt = (decimal)workSheet.Cells[i, 12].GetValue<decimal>(),
                    Discount = (decimal)workSheet.Cells[i, 13].GetValue<decimal>(),
                    LineTotal = (decimal)workSheet.Cells[i, 14].GetValue<decimal>(),
                    BasicRate = (decimal)workSheet.Cells[i, 15].GetValue<decimal>(),
                    IsDataConsumed = false,
                    ImportTime = DateTime.Today,
                    // = IsLocal,
                    //IsVatBill = IsVat
                };

                saleList.Add(p);


                xo++;
            }

            db.ImportSaleItemWises.AddRange(saleList);
            return db.SaveChanges();

            //return purchaseList;
        }

        public int ImportPurchase(VoyagerContext db, string fileName, bool IsVat, bool IsLocal)
        {


            //string rootFolder = IHostingEnvironment.WebRootPath;
            //string fileName = @"ImportCustomers.xlsx";
            FileInfo file = new FileInfo(fileName);

            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;

            List<ImportPurchase> purchaseList = new List<ImportPurchase>();
            //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3

            for (int i = 2; i <= totalRows; i++)
            {
                purchaseList.Add(new ImportPurchase
                {
                    GRNNo = workSheet.Cells[i, 1].Value.ToString(),
                    GRNDate = (DateTime)workSheet.Cells[i, 2].GetValue<DateTime>(),
                    InvoiceNo = workSheet.Cells[i, 3].Value.ToString(),
                    InvoiceDate = (DateTime)workSheet.Cells[i, 4].GetValue<DateTime>(),
                    SupplierName = workSheet.Cells[i, 5].Value.ToString(),
                    Barcode = workSheet.Cells[i, 6].Value.ToString(),
                    ProductName = workSheet.Cells[i, 7].Value.ToString(),
                    StyleCode = workSheet.Cells[i, 8].Value.ToString(),
                    ItemDesc = workSheet.Cells[i, 9].Value.ToString(),
                    Quantity = (double)workSheet.Cells[i, 10].Value,
                    MRP = (decimal)workSheet.Cells[i, 11].GetValue<decimal>(),
                    MRPValue = (decimal)workSheet.Cells[i, 12].GetValue<decimal>(),
                    Cost = (decimal)workSheet.Cells[i, 13].GetValue<decimal>(),
                    CostValue = (decimal)workSheet.Cells[i, 14].GetValue<decimal>(),
                    TaxAmt = (decimal)workSheet.Cells[i, 15].GetValue<decimal>(),
                    IsDataConsumed = false,
                    ImportTime = DateTime.Today,
                    IsLocal = IsLocal,
                    IsVatBill = IsVat
                });

            }

            db.ImportPurchases.AddRange(purchaseList);
            return db.SaveChanges();

            //return purchaseList;
        }


        public string ExportPurchase(VoyagerContext db, string fileName)
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            //string fileName = @"ExportCustomers.xlsx";

            FileInfo file = new FileInfo(fileName);

            using (ExcelPackage package = new ExcelPackage(file))
            {

                IList<ImportPurchase> purchaseList = db.ImportPurchases.ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Purchase");
                int totalRows = purchaseList.Count();

                worksheet.Cells[1, 1].Value = "Bardcode";
                worksheet.Cells[1, 2].Value = "MRP";
                worksheet.Cells[1, 3].Value = "Cost";
                worksheet.Cells[1, 4].Value = "Qty";
                worksheet.Cells[1, 5].Value = "Total";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = purchaseList[i].Barcode;
                    worksheet.Cells[row, 2].Value = purchaseList[i].MRP;
                    worksheet.Cells[row, 3].Value = purchaseList[i].Cost;
                    worksheet.Cells[row, 4].Value = purchaseList[i].Quantity;
                    worksheet.Cells[row, 5].Value = purchaseList[i].CostValue;
                    i++;
                }

                package.Save();

            }

            return " Purchase list has been exported successfully";
        }

    }
}
