using AprajitaRetails.Areas.Uploader.Models;
using LinqToExcel;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using AprajitaRetails.Areas.Voyager.Data;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using AprajitaRetails.Data;
using AprajitaRetails.Areas.AddressBook.Models;

namespace AprajitaRetails.Ops.Uploader
{
    public class ExcelUploaders
    {
        public UploadReturns UploadExcel(VoyagerContext db, UploadTypes UploadType, IFormFile FileUpload, bool IsVat, bool IsLocal)
        {

            //UploadType = "InWard";
            //List<string> data = new List<string> ();
            if ( FileUpload != null )
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if ( FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" )
                {
                    string filename = FileUpload.FileName;

                    string pathToExcelFile = Path.GetTempPath () + filename;
                    using ( var stream = new FileStream (pathToExcelFile, FileMode.Create) )
                    {
                        FileUpload.CopyTo (stream);
                    }

                    if ( UploadType == UploadTypes.Purchase )
                    {
                        try
                        {
                            ImportPurchase (db, pathToExcelFile, IsVat, IsLocal);
                        }
                        catch ( Exception ex )
                        {
                            Console.WriteLine ("Error: " + ex.Message);
                            return UploadReturns.Error;
                        }
                    }
                    else if ( UploadType == UploadTypes.SaleItemWise )
                    {
                        try
                        {
                            ImportSaleItemWise (db, pathToExcelFile, IsVat, IsLocal);
                        }
                        catch ( Exception ex )
                        {
                            Console.WriteLine ("Error: " + ex.Message);
                            return UploadReturns.Error;
                        }
                    }
                    else if ( UploadType == UploadTypes.SaleRegister )
                    {
                        try
                        {
                            ImportSaleRegister (db, pathToExcelFile);
                        }
                        catch ( Exception ex )
                        {
                            Console.WriteLine ("Error: " + ex.Message);
                            return UploadReturns.Error;
                        }
                    }
                    else if ( UploadType == UploadTypes.InWard )
                    {
                        try
                        {
                            ImportPurchaseInward (db, pathToExcelFile);
                        }
                        catch ( Exception ex )
                        {
                            Console.WriteLine ("Error: " + ex.Message);
                            return UploadReturns.Error;
                        }
                    }
                    else
                    {
                        return UploadReturns.ImportNotSupported;
                    }

                    if ( ( System.IO.File.Exists (pathToExcelFile) ) )
                    {
                        System.IO.File.Delete (pathToExcelFile);
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

        private int ImportSaleItemWise(VoyagerContext db, string fileName, bool IsVat, bool IsLocal)
        {
            //string rootFolder = IHostingEnvironment.WebRootPath;
            //string fileName = @"ImportCustomers.xlsx";
            FileInfo file = new FileInfo (fileName);

            using ExcelPackage package = new ExcelPackage (file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets ["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;

            List<ImportSaleItemWise> saleList = new List<ImportSaleItemWise> ();
            //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3
            int xo = 0;
            for ( int i = 2 ; i <= totalRows ; i++ )
            {
                //Invoice No 1	Invoice Date 2	Invoice Type 3	
                //Brand Name 4	Product Name 5 Item Desc 6	HSN Code 7	BAR CODE 8	//
                //Style Code 9	Quantity 10	MRP	11 Discount Amt 12	Basic Amt 13	Tax Amt 14	SGST Amt 15	
                //CGST Amt 16	CESS Amt 17	Line Total 18	Round Off 19	Bill Amt 20	Payment Mode 21	SalesMan Name 22	//
                //Coupon %	Coupon Amt	SUB TYPE	Bill Discount	LP Flag	Inst Order CD	TAILORING FLAG

                try
                {

                    ImportSaleItemWise p = new ImportSaleItemWise
                    {
                        InvoiceNo = ( workSheet.Cells [i, 1].Value ?? string.Empty ).ToString (),
                        InvoiceDate = (DateTime) workSheet.Cells [i, 2].GetValue<DateTime> (),
                        InvoiceType = ( workSheet.Cells [i, 3].Value ?? string.Empty ).ToString (),

                        BrandName = ( workSheet.Cells [i, 4].Value ?? string.Empty ).ToString (),
                        ProductName = ( workSheet.Cells [i, 5].Value ?? string.Empty ).ToString (),
                        ItemDesc = ( workSheet.Cells [i, 6].Value ?? string.Empty ).ToString (),

                        Barcode = ( workSheet.Cells [i, 8].Value ?? string.Empty ).ToString (),

                        StyleCode = ( workSheet.Cells [i, 9].Value ?? string.Empty ).ToString (),
                        PaymentType = ( workSheet.Cells [i, 21].Value ?? string.Empty ).ToString (),
                        Saleman = ( workSheet.Cells [i, 22].Value ?? string.Empty ).ToString (),

                        IsDataConsumed = false,
                        ImportTime = DateTime.Today,
                        IsLocal = IsLocal,
                        IsVatBill = IsVat

                    };

                    p.HSNCode = ( workSheet.Cells [i, 7].Value ?? string.Empty ).ToString ();
                    p.Quantity = (double) workSheet.Cells [i, 10].GetValue<double> ();
                    p.MRP = (decimal) workSheet.Cells [i, 11].GetValue<decimal> ();
                    p.Discount = (decimal) workSheet.Cells [i, 12].GetValue<decimal> ();
                    p.BasicRate = (decimal) workSheet.Cells [i, 13].GetValue<decimal> ();
                    p.Tax = (decimal) workSheet.Cells [i, 14].GetValue<decimal> ();
                    p.SGST = (decimal) workSheet.Cells [i, 15].GetValue<decimal> ();

                    p.CGST = (decimal) workSheet.Cells [i, 16].GetValue<decimal> ();
                    //p.CESS = (decimal)workSheet.Cells[i, 17].GetValue<decimal>();
                    p.LineTotal = (decimal) workSheet.Cells [i, 18].GetValue<decimal> ();
                    p.RoundOff = (decimal) workSheet.Cells [i, 19].GetValue<decimal> ();
                    p.BillAmnt = (decimal) workSheet.Cells [i, 20].GetValue<decimal> ();


                    saleList.Add (p);


                    xo++;
                }
                catch ( Exception ex )
                {
                    Console.WriteLine ("Error: " + ex.Message);
                    // return UploadReturns.Error;
                    throw;
                }

            }

            db.ImportSaleItemWises.AddRange (saleList);
            return db.SaveChanges ();

            //return purchaseList;
        }

        private int ImportPurchase(VoyagerContext db, string fileName, bool IsVat, bool IsLocal)
        {


            //string rootFolder = IHostingEnvironment.WebRootPath;
            //string fileName = @"ImportCustomers.xlsx";
            FileInfo file = new FileInfo (fileName);

            using ExcelPackage package = new ExcelPackage (file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets ["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;

            List<ImportPurchase> purchaseList = new List<ImportPurchase> ();
            //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3

            for ( int i = 2 ; i <= totalRows ; i++ )
            {
                purchaseList.Add (new ImportPurchase
                {
                    GRNNo = workSheet.Cells [i, 1].Value.ToString (),
                    GRNDate = (DateTime) workSheet.Cells [i, 2].GetValue<DateTime> (),
                    InvoiceNo = workSheet.Cells [i, 3].Value.ToString (),
                    InvoiceDate = (DateTime) workSheet.Cells [i, 4].GetValue<DateTime> (),
                    SupplierName = workSheet.Cells [i, 5].Value.ToString (),
                    Barcode = workSheet.Cells [i, 6].Value.ToString (),
                    ProductName = workSheet.Cells [i, 7].Value.ToString (),
                    StyleCode = workSheet.Cells [i, 8].Value.ToString (),
                    ItemDesc = workSheet.Cells [i, 9].Value.ToString (),
                    Quantity = (double) workSheet.Cells [i, 10].Value,
                    MRP = (decimal) workSheet.Cells [i, 11].GetValue<decimal> (),
                    MRPValue = (decimal) workSheet.Cells [i, 12].GetValue<decimal> (),
                    Cost = (decimal) workSheet.Cells [i, 13].GetValue<decimal> (),
                    CostValue = (decimal) workSheet.Cells [i, 14].GetValue<decimal> (),
                    TaxAmt = (decimal) workSheet.Cells [i, 15].GetValue<decimal> (),
                    IsDataConsumed = false,
                    ImportTime = DateTime.Today,
                    IsLocal = IsLocal,
                    IsVatBill = IsVat
                });

            }

            db.ImportPurchases.AddRange (purchaseList);
            return db.SaveChanges ();

            //return purchaseList;
        }

        private int ImportPurchaseInward(VoyagerContext db, string fileName)
        {
            //string rootFolder = IHostingEnvironment.WebRootPath;
            //string fileName = @"ImportCustomers.xlsx";
            FileInfo file = new FileInfo (fileName);

            using ExcelPackage package = new ExcelPackage (file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets ["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;

            List<ImportInWard> purchaseList = new List<ImportInWard> ();
            //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3

            for ( int i = 2 ; i <= totalRows ; i++ )
            {
                purchaseList.Add (new ImportInWard
                {
                    InWardNo = workSheet.Cells [i, 1].Value.ToString (),
                    InWardDate = (DateTime) workSheet.Cells [i, 2].GetValue<DateTime> (),
                    InvoiceNo = workSheet.Cells [i, 3].Value.ToString (),
                    InvoiceDate = (DateTime) workSheet.Cells [i, 4].GetValue<DateTime> (),
                    PartyName = workSheet.Cells [i, 5].Value.ToString (),
                    TotalQty = (decimal) workSheet.Cells [i, 6].Value,
                    TotalMRPValue = (decimal) workSheet.Cells [i, 7].GetValue<decimal> (),
                    TotalCost = (decimal) workSheet.Cells [i, 8].GetValue<decimal> (),

                    IsDataConsumed = false,
                    ImportDate = DateTime.Today,
                });

            }

            db.ImportInWards.AddRange (purchaseList);
            return db.SaveChanges ();

            //return purchaseList;
        }

        private int ImportSaleRegister(VoyagerContext db, string fileName)
        {
            //string rootFolder = IHostingEnvironment.WebRootPath;
            //string fileName = @"ImportCustomers.xlsx";
            FileInfo file = new FileInfo (fileName);

            using ExcelPackage package = new ExcelPackage (file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets ["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;

            List<ImportSaleRegister> saleList = new List<ImportSaleRegister> ();
            //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3
            int xo = 0;
            for ( int i = 2 ; i <= totalRows ; i++ )
            {
                //Invoice No 1	Invoice Date 2	Invoice Type 3	
                //Brand Name 4	Product Name 5 Item Desc 6	HSN Code 7	BAR CODE 8	//
                //Style Code 9	Quantity 10	MRP	11 Discount Amt 12	Basic Amt 13	Tax Amt 14	SGST Amt 15	
                //CGST Amt 16	CESS Amt 17	Line Total 18	Round Off 19	Bill Amt 20	Payment Mode 21	SalesMan Name 22	//
                //Coupon %	Coupon Amt	SUB TYPE	Bill Discount	LP Flag	Inst Order CD	TAILORING FLAG
                ImportSaleRegister p = new ImportSaleRegister
                {
                    InvoiceNo = workSheet.Cells [i, 1].Value.ToString (),
                    InvoiceDate = workSheet.Cells [i, 2].Value.ToString (),
                    InvoiceType = workSheet.Cells [i, 3].Value.ToString (),
                    Quantity = (double) workSheet.Cells [i, 4].Value,
                    MRP = (decimal) workSheet.Cells [i, 5].GetValue<decimal> (),
                    Discount = (decimal) workSheet.Cells [i, 6].GetValue<decimal> (),
                    BasicRate = (decimal) workSheet.Cells [i, 7].GetValue<decimal> (),
                    Tax = (decimal) workSheet.Cells [i, 8].GetValue<decimal> (),
                    RoundOff = (decimal) workSheet.Cells [i, 9].GetValue<decimal> (),
                    BillAmnt = (decimal) workSheet.Cells [i, 10].GetValue<decimal> (),
                    PaymentType = workSheet.Cells [i, 11].Value.ToString (),
                    ImportTime = DateTime.Today,
                    IsConsumed = false
                };

                saleList.Add (p);


                xo++;
            }

            db.ImportSaleRegisters.AddRange (saleList);
            return db.SaveChanges ();

            //return purchaseList;
        }

        public UploadReturns UploadAddressBook(AprajitaRetailsContext db, IFormFile FileUpload)
        {
            if ( FileUpload != null )
            {
                if ( FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" )
                {
                    string filename = FileUpload.FileName;

                    string pathToExcelFile = Path.GetTempPath () + filename;
                    using ( var stream = new FileStream (pathToExcelFile, FileMode.Create) )
                    {
                        FileUpload.CopyTo (stream);
                    }


                    try
                    {
                        FileInfo file = new FileInfo (pathToExcelFile);

                        using ExcelPackage package = new ExcelPackage (file);
                        ExcelWorksheet workSheet = package.Workbook.Worksheets ["Sheet1"];
                        int totalRows = workSheet.Dimension.Rows;
                        List<Contact> addList = new List<Contact> ();

                        int xo = 0;
                        for ( int i = 2 ; i <= totalRows ; i++ )
                        {
                            Contact c = new Contact
                            {

                                FirstName = (workSheet.Cells [i, 1].Value ?? string.Empty ).ToString (),
                                LastName = (workSheet.Cells [i, 2].Value ?? string.Empty ).ToString (),
                                Remarks = ( workSheet.Cells [i, 3].Value ?? string.Empty ).ToString (),
                                EMailAddress = ( workSheet.Cells [i, 4].Value ?? string.Empty ).ToString (),
                                MobileNo = ( workSheet.Cells [i, 6].Value ?? string.Empty ).ToString (),
                                PhoneNo = ( workSheet.Cells [i, 5].Value ?? string.Empty ).ToString ()

                            };
                            addList.Add (c);
                            xo++;
                        }
                        db.Contact.AddRange (addList);
                        db.SaveChanges ();
                    }
                    catch ( Exception ex )
                    {
                        Console.WriteLine ("Error: " + ex.Message);
                        return UploadReturns.Error;
                    }

                    if ( ( System.IO.File.Exists (pathToExcelFile) ) )
                    {
                        System.IO.File.Delete (pathToExcelFile);
                    }
                    return UploadReturns.Success;


                }
                else
                {
                    return UploadReturns.NotExcelType;
                }
            }
            else
            {
                return UploadReturns.FileNotFound;
            }
        }
    }
}
