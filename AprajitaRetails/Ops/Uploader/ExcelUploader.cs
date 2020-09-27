using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using AprajitaRetails.Areas.AddressBook.Models;
using AprajitaRetails.Areas.Uploader.Models;

using AprajitaRetails.Data;
using AprajitaRetails.Models;
using iText.Kernel.Colors.Gradients;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace AprajitaRetails.Ops.Uploader
{
    //Store Based Changes is made in this class , All function support Store
    public class ExcelUploaders
    {
        public UploadReturns UploadExcel(AprajitaRetailsContext db, UploadTypes UploadType, IFormFile FileUpload, string StoreCode, bool IsVat, bool IsLocal)
        {

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
                            ImportPurchase(db, pathToExcelFile, StoreCode, IsVat, IsLocal);
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
                            ImportSaleItemWise(db, pathToExcelFile, StoreCode, IsVat, IsLocal);
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
                            ImportSaleRegister(db, StoreCode, pathToExcelFile);
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
                            ImportPurchaseInward(db, StoreCode, pathToExcelFile);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            return UploadReturns.Error;
                        }
                    }
                    //else if (UploadType == UploadTypes.Attendance)
                    //{
                    //    try
                    //    {
                    //        UploadAttendance(db, StoreCode, pathToExcelFile, IsLocal);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine("Error: " + ex.Message);
                    //        return UploadReturns.Error;
                    //    }
                    //}
                    else
                    {
                        return UploadReturns.ImportNotSupported;
                    }

                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return UploadReturns.Success;
                }//end of if context type
                else
                {
                    return UploadReturns.NotExcelType;
                }
            }//end of if file upload
            else
            {
                return UploadReturns.FileNotFound;
            }
        }//end of function

        private int ImportSaleItemWise(AprajitaRetailsContext db, string fileName, string StoreCode, bool IsVat, bool IsLocal)
        {
            //string rootFolder = IHostingEnvironment.WebRootPath;
            //string fileName = @"ImportCustomers.xlsx";
            FileInfo file = new FileInfo(fileName);

            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;
            int StoreID = 1;//Default
            StoreID = db.Stores.Where(c => c.StoreCode == StoreCode).Select(c => c.StoreId).FirstOrDefault();
            if (StoreID < 1)
                StoreID = 1;
            List<ImportSaleItemWise> saleList = new List<ImportSaleItemWise>();
            //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3
            int xo = 0;
            for (int i = 2; i <= totalRows; i++)
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
                        InvoiceNo = (workSheet.Cells[i, 1].Value ?? string.Empty).ToString(),
                        InvoiceDate = (DateTime)workSheet.Cells[i, 2].GetValue<DateTime>(),
                        InvoiceType = (workSheet.Cells[i, 3].Value ?? string.Empty).ToString(),

                        BrandName = (workSheet.Cells[i, 4].Value ?? string.Empty).ToString(),
                        ProductName = (workSheet.Cells[i, 5].Value ?? string.Empty).ToString(),
                        ItemDesc = (workSheet.Cells[i, 6].Value ?? string.Empty).ToString(),

                        Barcode = (workSheet.Cells[i, 8].Value ?? string.Empty).ToString(),

                        StyleCode = (workSheet.Cells[i, 9].Value ?? string.Empty).ToString(),
                        PaymentType = (workSheet.Cells[i, 21].Value ?? string.Empty).ToString(),
                        Saleman = (workSheet.Cells[i, 22].Value ?? string.Empty).ToString(),

                        IsDataConsumed = false,
                        //ImportTime = DateTime.Today,
                        IsLocal = IsLocal,
                        IsVatBill = IsVat,
                        StoreId = StoreID
                    };

                    p.HSNCode = (workSheet.Cells[i, 7].Value ?? string.Empty).ToString();
                    p.Quantity = (double)workSheet.Cells[i, 10].GetValue<double>();
                    p.MRP = (decimal)workSheet.Cells[i, 11].GetValue<decimal>();
                    p.Discount = (decimal)workSheet.Cells[i, 12].GetValue<decimal>();
                    p.BasicRate = (decimal)workSheet.Cells[i, 13].GetValue<decimal>();
                    p.Tax = (decimal)workSheet.Cells[i, 14].GetValue<decimal>();
                    p.SGST = (decimal)workSheet.Cells[i, 15].GetValue<decimal>();

                    p.CGST = (decimal)workSheet.Cells[i, 16].GetValue<decimal>();
                    //p.CESS = (decimal)workSheet.Cells[i, 17].GetValue<decimal>();
                    p.LineTotal = (decimal)workSheet.Cells[i, 18].GetValue<decimal>();
                    p.RoundOff = (decimal)workSheet.Cells[i, 19].GetValue<decimal>();
                    p.BillAmnt = (decimal)workSheet.Cells[i, 20].GetValue<decimal>();

                    saleList.Add(p);

                    xo++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // return UploadReturns.Error;
                    throw;
                }
            }

            db.ImportSaleItemWises.AddRange(saleList);
            return db.SaveChanges();

            //return purchaseList;
        }

        private int ImportPurchase(AprajitaRetailsContext db, string fileName, string StoreCode, bool IsVat, bool IsLocal)
        {
            FileInfo file = new FileInfo(fileName);

            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;

            int StoreID = 1;//Default
            StoreID = db.Stores.Where(c => c.StoreCode == StoreCode).Select(c => c.StoreId).FirstOrDefault();
            if (StoreID < 1)
                StoreID = 1;

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

                    IsLocal = IsLocal,
                    IsVatBill = IsVat,
                    StoreId = StoreID
                });
            }

            db.ImportPurchases.AddRange(purchaseList);
            return db.SaveChanges();

            //return purchaseList;
        }

        private int ImportPurchaseInward(AprajitaRetailsContext db, string StoreCode, string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;
            int StoreID = 1;//Default
            StoreID = db.Stores.Where(c => c.StoreCode == StoreCode).Select(c => c.StoreId).FirstOrDefault();
            if (StoreID < 1) StoreID = 1;
            List<ImportInWard> purchaseList = new List<ImportInWard>();
            for (int i = 2; i <= totalRows; i++)
            {
                purchaseList.Add(new ImportInWard
                {
                    InWardNo = workSheet.Cells[i, 1].Value.ToString(),
                    InWardDate = (DateTime)workSheet.Cells[i, 2].GetValue<DateTime>(),
                    InvoiceNo = workSheet.Cells[i, 3].Value.ToString(),
                    InvoiceDate = (DateTime)workSheet.Cells[i, 4].GetValue<DateTime>(),
                    PartyName = workSheet.Cells[i, 5].Value.ToString(),
                    TotalQty = (decimal)workSheet.Cells[i, 6].Value,
                    TotalMRPValue = (decimal)workSheet.Cells[i, 7].GetValue<decimal>(),
                    TotalCost = (decimal)workSheet.Cells[i, 8].GetValue<decimal>(),
                    StoreId = StoreID,
                    IsDataConsumed = false,
                });
            }
            db.ImportInWards.AddRange(purchaseList);
            return db.SaveChanges();
        }

        private int ImportSaleRegister(AprajitaRetailsContext db, string StoreCode, string fileName)
        {
            FileInfo file = new FileInfo(fileName);

            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
            int totalRows = workSheet.Dimension.Rows;
            int StoreID = 1;//Default
            StoreID = db.Stores.Where(c => c.StoreCode == StoreCode).Select(c => c.StoreId).FirstOrDefault();
            if (StoreID < 1) StoreID = 1;
            List<ImportSaleRegister> saleList = new List<ImportSaleRegister>();
            int xo = 0;
            for (int i = 2; i <= totalRows; i++)
            {
                //Invoice No 1	Invoice Date 2	Invoice Type 3
                //Brand Name 4	Product Name 5 Item Desc 6	HSN Code 7	BAR CODE 8	//
                //Style Code 9	Quantity 10	MRP	11 Discount Amt 12	Basic Amt 13	Tax Amt 14	SGST Amt 15
                //CGST Amt 16	CESS Amt 17	Line Total 18	Round Off 19	Bill Amt 20	Payment Mode 21	SalesMan Name 22	//
                //Coupon %	Coupon Amt	SUB TYPE	Bill Discount	LP Flag	Inst Order CD	TAILORING FLAG
                ImportSaleRegister p = new ImportSaleRegister
                {
                    InvoiceNo = workSheet.Cells[i, 1].Value.ToString(),
                    InvoiceDate = workSheet.Cells[i, 2].Value.ToString(),
                    InvoiceType = workSheet.Cells[i, 3].Value.ToString(),
                    Quantity = (double)workSheet.Cells[i, 4].Value,
                    MRP = (decimal)workSheet.Cells[i, 5].GetValue<decimal>(),
                    Discount = (decimal)workSheet.Cells[i, 6].GetValue<decimal>(),
                    BasicRate = (decimal)workSheet.Cells[i, 7].GetValue<decimal>(),
                    Tax = (decimal)workSheet.Cells[i, 8].GetValue<decimal>(),
                    RoundOff = (decimal)workSheet.Cells[i, 9].GetValue<decimal>(),
                    BillAmnt = (decimal)workSheet.Cells[i, 10].GetValue<decimal>(),
                    PaymentType = workSheet.Cells[i, 11].Value.ToString(),
                    StoreId = StoreID,
                    IsConsumed = false
                };
                saleList.Add(p);
                xo++;
            }
            db.ImportSaleRegisters.AddRange(saleList);
            return db.SaveChanges();
        }

        public UploadReturns UploadAddressBook(AprajitaRetailsContext db, IFormFile FileUpload)
        {
            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string pathToExcelFile = Path.GetTempPath() + filename;
                    using (var stream = new FileStream(pathToExcelFile, FileMode.Create))
                    {
                        FileUpload.CopyTo(stream);
                    }
                    try
                    {
                        FileInfo file = new FileInfo(pathToExcelFile);
                        using ExcelPackage package = new ExcelPackage(file);
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                        int totalRows = workSheet.Dimension.Rows;
                        List<Contact> addList = new List<Contact>();
                        int xo = 0;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            Contact c = new Contact
                            {
                                FirstName = (workSheet.Cells[i, 1].Value ?? string.Empty).ToString(),
                                LastName = (workSheet.Cells[i, 2].Value ?? string.Empty).ToString(),
                                Remarks = (workSheet.Cells[i, 3].Value ?? string.Empty).ToString(),
                                EMailAddress = (workSheet.Cells[i, 4].Value ?? string.Empty).ToString(),
                                MobileNo = (workSheet.Cells[i, 6].Value ?? string.Empty).ToString(),
                                PhoneNo = (workSheet.Cells[i, 5].Value ?? string.Empty).ToString()
                            };
                            addList.Add(c);
                            xo++;
                        }
                        db.Contact.AddRange(addList);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return UploadReturns.Error;
                    }
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
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

        public UploadReturns UploadAttendance(AprajitaRetailsContext db, string StoreCode, IFormFile FileUpload)
        {

            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;

                    string pathToExcelFile = Path.GetTempPath() + filename;
                    using (var stream = new FileStream(pathToExcelFile, FileMode.Create))
                    {
                        FileUpload.CopyTo(stream);
                    }

                    try
                    {
                        FileInfo file = new FileInfo(pathToExcelFile);

                        using ExcelPackage package = new ExcelPackage(file);
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                        int totalRows = workSheet.Dimension.Rows;
                        List<AttendanceVM> addList = new List<AttendanceVM>();

                        int StoreID = 1;//Default
                        StoreID = db.Stores.Where(c => c.StoreCode == StoreCode).Select(c => c.StoreId).FirstOrDefault();
                        if (StoreID < 1)
                            StoreID = 1;


                        int xo = 0;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            AttendanceVM c = new AttendanceVM
                            {
                                EmployeeName = (workSheet.Cells[i, 1].Value ?? string.Empty).ToString(),
                                AttDate = (DateTime)workSheet.Cells[i, 2].GetValue<DateTime>(),
                                EntryTime = (workSheet.Cells[i, 3].Value ?? string.Empty).ToString(),
                                Status = (int)workSheet.Cells[i, 4].GetValue<int>(),
                                Remarks = (workSheet.Cells[i, 5].Value ?? string.Empty).ToString(),
                                IsTailoring = (bool)workSheet.Cells[i, 6].GetValue<bool>(),
                                StoreCode = StoreID,
                                IsDataConsumed = false,
                            };
                            addList.Add(c);
                            xo++;
                        }
                        db.AttendancesImport.AddRange(addList);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return UploadReturns.Error;
                    }

                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
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

        public UploadReturns UploadAttendanceForEmp(AprajitaRetailsContext db, IFormFile FileUpload, int EmpId)
        {

            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;

                    string pathToExcelFile = Path.GetTempPath() + filename;
                    using (var stream = new FileStream(pathToExcelFile, FileMode.Create))
                    {
                        FileUpload.CopyTo(stream);
                    }

                    try
                    {
                        FileInfo file = new FileInfo(pathToExcelFile);

                        using ExcelPackage package = new ExcelPackage(file);
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                        int totalRows = workSheet.Dimension.Rows;
                        List<Attendance> addList = new List<Attendance>();

                        int StoreID = 1;//Default
                        //StoreID = voydb.Stores.Where(c => c.StoreCode == StoreCode).Select(c => c.StoreId).FirstOrDefault();
                        //if (StoreID < 1)
                        //    StoreID = 1;


                        int xo = 0;
                        decimal att = -1;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            att = (decimal)workSheet.Cells[i, 4].GetValue<decimal>();
                            if (att > -1)
                            {
                                Attendance c = new Attendance
                                {
                                    EmployeeId = EmpId,
                                    AttDate = (DateTime)workSheet.Cells[i, 2].GetValue<DateTime>(),
                                    EntryTime = (workSheet.Cells[i, 3].Value ?? string.Empty).ToString(),
                                    Status = AttUnits.Absent,
                                    Remarks = (workSheet.Cells[i, 5].Value ?? string.Empty).ToString(),
                                    IsTailoring = (bool)workSheet.Cells[i, 6].GetValue<bool>(),
                                    StoreId = StoreID,
                                    //IsDataConsumed = false,
                                };

                                switch (att)
                                {
                                    case 0: c.Status = AttUnits.Absent; break;
                                    case 1: c.Status = AttUnits.Present; break;
                                    case 0.5M: c.Status = AttUnits.HalfDay; break; //Halfday;
                                    case 3: c.Status = AttUnits.Holiday; break; // holiday
                                    case 4: c.Status = AttUnits.StoreClosed; break; //closed
                                }
                                addList.Add(c);
                                xo++;
                            }

                        }
                        db.Attendances.AddRange(addList);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return UploadReturns.Error;
                    }

                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
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
        public UploadReturns UploadBookEntry(AprajitaRetailsContext db, IFormFile FileUpload)
        {
            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;

                    string pathToExcelFile = Path.GetTempPath() + filename;
                    using (var stream = new FileStream(pathToExcelFile, FileMode.Create))
                    {
                        FileUpload.CopyTo(stream);
                    }

                    try
                    {
                        FileInfo file = new FileInfo(pathToExcelFile);

                        using ExcelPackage package = new ExcelPackage(file);
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                        int totalRows = workSheet.Dimension.Rows;
                        List<BookEntry> addList = new List<BookEntry>();

                        int xo = 0;
                        string tempstr = "";
                        for (int i = 2; i <= totalRows; i++)
                        {
                            BookEntry c = new BookEntry
                            {
                                IsConsumed = false,
                                OnDate = (DateTime)workSheet.Cells[i, 1].GetValue<DateTime>(),
                                Amount = (decimal)workSheet.Cells[i, 4].GetValue<decimal>(),
                                Naration = (workSheet.Cells[i, 6].Value ?? string.Empty).ToString(),
                                LedgerBy = LedgerBy.Suspense,
                                LedgerTo = LedgerTo.Suspense,
                                VoucherType = VoucherType.JV

                            };
                            c.Naration = c.Naration + "   #OrgDate: " + (workSheet.Cells[i, 7].Value ?? string.Empty).ToString();
                            tempstr = (workSheet.Cells[i, 2].Value ?? string.Empty).ToString();
                            switch (tempstr)
                            {


                                case "Cash": c.LedgerBy = LedgerBy.Cash; break;
                                case "EDCHDFC": c.LedgerBy = LedgerBy.EDCHDFC; break;
                                case "EXP UNDEF": c.LedgerBy = LedgerBy.EXPUNDEF; break;
                                case "EDCICICI": c.LedgerBy = LedgerBy.EDCICICI; break;
                                case "Suspense": c.LedgerBy = LedgerBy.Suspense; break;
                                case "Zafar": c.LedgerBy = LedgerBy.Zafar; break;
                                case "HDFC CA": c.LedgerBy = LedgerBy.HDFCCA; break;
                                case "ICICI Bank CA": c.LedgerBy = LedgerBy.ICICIBankCA; break;
                                case "Others": c.LedgerBy = LedgerBy.Others; break;
                                case "Amit Kumar": c.LedgerBy = LedgerBy.AmitKumar; break;
                                case "Bandhan CA": c.LedgerBy = LedgerBy.BandhanCA; break;


                                case "BHARAT QR": c.LedgerBy = LedgerBy.BHARATQR; break;
                                case "EDCBandhan": c.LedgerBy = LedgerBy.EDCBandhan; break;
                                case "EDCEASYTAP": c.LedgerBy = LedgerBy.EDCEASYTAP; break;

                                case "EDCSBI": c.LedgerBy = LedgerBy.EDCSBI; break;
                                case "SBI CC": c.LedgerBy = LedgerBy.SBICC; break;
                                case "IDBI CA": c.LedgerBy = LedgerBy.IDBICA; break;


                                default:
                                    c.LedgerBy = LedgerBy.Others;
                                    c.Naration = c.Naration + " #By: " + tempstr; break;

                            }

                            tempstr = (workSheet.Cells[i, 3].Value ?? string.Empty).ToString();
                            switch (tempstr)
                            {
                                case "Cash": c.LedgerTo = LedgerTo.Cash; break;
                                case "POSSale": c.LedgerTo = LedgerTo.POSSale; break;
                                case "CashSales": c.LedgerTo = LedgerTo.CashSales; break;
                                case "Suspense": c.LedgerTo = LedgerTo.Suspense; break;
                                case "TailorBook": c.LedgerTo = LedgerTo.TailorBook; break;
                                default:
                                    c.LedgerTo = LedgerTo.Suspense;
                                    c.Naration = c.Naration + " #To: " + tempstr;
                                    break;

                            }
                            tempstr = (workSheet.Cells[i, 5].Value ?? string.Empty).ToString();
                            switch (tempstr)
                            {
                                case "Payment": c.VoucherType = VoucherType.Payment; break;
                                case "Receipt": c.VoucherType = VoucherType.Reciept; break;
                                case "Contra": c.VoucherType = VoucherType.Contra; break;

                                case "Debit Note": c.VoucherType = VoucherType.DebitNote; break;
                                case "Credit Note": c.VoucherType = VoucherType.CreditNote; break;
                                case "JV": c.VoucherType = VoucherType.JV; break;
                                default:
                                    c.VoucherType = VoucherType.JV;
                                    c.Naration = c.Naration + " #VCType: " + tempstr;
                                    break;
                            }


                            addList.Add(c);
                            xo++;
                        }
                        db.ImportBookEntries.AddRange(addList);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return UploadReturns.Error;
                    }

                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
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

        public UploadReturns UploadBankStatment(AprajitaRetailsContext db, IFormFile FileUpload, int AccountNumberId, UploadSetting settings, string SheetName = "Sheet1")
        {
            if (FileUpload != null)
            {
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;

                    string pathToExcelFile = Path.GetTempPath() + filename;
                    using (var stream = new FileStream(pathToExcelFile, FileMode.Create))
                    {
                        FileUpload.CopyTo(stream);
                    }

                    try
                    {
                        FileInfo file = new FileInfo(pathToExcelFile);

                        using ExcelPackage package = new ExcelPackage(file);
                        ExcelWorksheet workSheet = package.Workbook.Worksheets[SheetName];
                        int totalRows = workSheet.Dimension.Rows;

                        if (totalRows > settings.EndRow) totalRows = settings.EndRow;

                        List<BankStatement> addList = new List<BankStatement>();

                        int xo = 0;
                        string tempstr = "";
                        for (int i = settings.StartRow; i <= totalRows; i++)
                        {
                            BankStatement c = new BankStatement
                            {
                                OnDateValue=(DateTime)workSheet.Cells[i, settings.ColSetting.ValueDateCol].GetValue<DateTime>(),
                                OnDateTranscation = (DateTime)workSheet.Cells[i, settings.ColSetting.ValueDateCol].GetValue<DateTime>(),
                                WithdrawalAmount = (decimal)workSheet.Cells[i, settings.ColSetting.OutCol].GetValue<decimal>(),
                                DepositAmount = (decimal)workSheet.Cells[i, settings.ColSetting.InCol].GetValue<decimal>(),
                                AccountNumberId=AccountNumberId, 
                                Balance = (decimal)workSheet.Cells[i, settings.ColSetting.BalCol].GetValue<decimal>(),
                                ChequeNumber=(workSheet.Cells[i, settings.ColSetting.ChequeNumberCol].Value ?? string.Empty).ToString(),
                                TransactionRemarks = (workSheet.Cells[i, settings.ColSetting.TransCol].Value ?? string.Empty).ToString(),
                                Remark=Remark.Uploaded
                            };
                            addList.Add(c);
                            xo++;
                        }
                        db.BankStatements.AddRange(addList);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return UploadReturns.Error;
                    }

                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
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

    public class UploadSetting
    {
        public int StartRow { get; set; }
        public int StartCol { get; set; }

        public int EndRow { get; set; }
        public int EndCol { get; set; }

        public int TotalNoOfRow { get; set; }
        public int TotalNoOfCol { get; set; }

        public int TotalRowNumber { get; set; }
        public int TotalColNumber { get; set; }

        public ColSetting ColSetting { get; set; }


    }
    public class ColSetting
    {
        public int ValueDateCol { get; set; }
        public int TransDateCol { get; set; }
        public int ChequeNumberCol { get; set; }
        public int TransCol { get; set; }
        public int OutCol { get; set; }
        public int InCol { get; set; }
        public int BalCol { get; set; }

    }
    public class PostUpload
    {
        public void ProcessBookEntry(AprajitaRetailsContext db, VoucherType voucherType, int StoreId)
        {
            switch (voucherType)
            {
                case VoucherType.Payment:
                    ProcessPaymentVoucher(db, StoreId);
                    break;
                case VoucherType.Reciept:
                    ProcessRecieptVoucher(db, StoreId);
                    break;
                case VoucherType.Contra:
                    ProcessContraVoucher(db, StoreId);
                    break;
                case VoucherType.DebitNote:
                    break;
                case VoucherType.CreditNote:
                    break;
                case VoucherType.JV:
                    break;
                default:
                    break;
            }
        }

        private void ProcessPaymentVoucher(AprajitaRetailsContext db, int StoreId)
        {
            var dataList = db.ImportBookEntries.Where(c => !c.IsConsumed && c.VoucherType == VoucherType.Payment);





        }
        private void ProcessRecieptVoucher(AprajitaRetailsContext db, int StoreId)
        {
            var dataList = db.ImportBookEntries.Where(c => !c.IsConsumed && c.VoucherType == VoucherType.Reciept);
            foreach (var item in dataList)
            {
                if (item.LedgerTo == LedgerTo.Cash)
                {
                    switch (item.LedgerBy)
                    {
                        case LedgerBy.EXPUNDEF:
                            PettyCashExpense cashExpense = new PettyCashExpense
                            {
                                Amount = item.Amount,
                                ExpDate = item.OnDate,
                                EmployeeId = 1//Alok Emp Id,
                                ,
                                Particulars = item.Naration,
                                Remarks = "AutoUpload",
                                PaidTo = item.Naration,
                                StoreId = StoreId

                            };
                            item.IsConsumed = true;
                            db.ImportBookEntries.Update(item);
                            db.PettyCashExpenses.Add(cashExpense);

                            break;
                        case LedgerBy.Suspense:
                            if (item.Naration.Contains("AMIT JEE"))
                            {
                                CashPayment cash = new CashPayment
                                {
                                    Amount = item.Amount,
                                    StoreId = StoreId,
                                    PaymentDate = item.OnDate,
                                    PaidTo = item.Naration,
                                    SlipNo = "AutoUpload",
                                    TranscationModeId = 1
                                };
                                item.IsConsumed = true;
                                db.CashPayments.Add(cash);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("RAJ MISTRY"))
                            {
                                CashPayment cash = new CashPayment
                                {
                                    Amount = item.Amount,
                                    StoreId = StoreId,
                                    PaymentDate = item.OnDate,
                                    PaidTo = item.Naration,
                                    SlipNo = "AutoUpload",
                                    TranscationModeId = 2
                                };
                                item.IsConsumed = true;
                                db.CashPayments.Add(cash);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("BHUTU"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "BHUTU MISTRY",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("WORKSHOP"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "WORKSHOP RENT",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("ADVERTISMENT RM: MIKEING"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "ADVERTISMENT RM: MIKEING",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("ADVERTISMENT"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "ADVERTISMENT",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("BIKASH PATWARI"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "BIKASH PATWARI",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("SALARY"))
                            {
                                SalaryPayment salary2 = new SalaryPayment
                                {
                                    EmployeeId = 0,
                                    Amount = item.Amount,
                                    PaymentDate = item.OnDate,
                                    PayMode = PayModes.Cash,
                                    SalaryComponet = SalaryComponet.Others,
                                    StoreId = StoreId,
                                    Details = "AutoUpload #" + item.Naration,
                                    SalaryMonth = item.OnDate.Year.ToString()
                                };

                                if (item.Naration.Contains("ALOK")) salary2.EmployeeId = 1;
                                else if (item.Naration.Contains("SANJEEV")) salary2.EmployeeId = 2;
                                else if (item.Naration.Contains("RAMREKH")) salary2.EmployeeId = 10;
                                else if (item.Naration.Contains("MUKESH")) salary2.EmployeeId = 3;
                                else if (item.Naration.Contains("SOURAV")) salary2.EmployeeId = 6;
                                else if (item.Naration.Contains("SANTOSH")) salary2.EmployeeId = 8;
                                else if (item.Naration.Contains("ANIL")) salary2.EmployeeId = 11;
                                else salary2.EmployeeId = 4;


                                item.IsConsumed = true;
                                db.ImportBookEntries.Update(item);
                                db.SalaryPayments.Add(salary2);
                            }
                            else if (item.Naration.Contains("RAMREKH"))
                            {
                                if (item.Naration.Contains("RAMREKH RM: SALARY"))
                                {
                                    SalaryPayment salary1 = new SalaryPayment
                                    {
                                        EmployeeId = 10,//TODO: For ramrek
                                        Amount = item.Amount,
                                        PaymentDate = item.OnDate,
                                        PayMode = PayModes.Cash,
                                        SalaryComponet = SalaryComponet.Others,
                                        StoreId = StoreId,
                                        Details = "AutoUpload #" + item.Naration,
                                        SalaryMonth = item.OnDate.Year.ToString()
                                    };
                                    item.IsConsumed = true;
                                    db.ImportBookEntries.Update(item);
                                    db.SalaryPayments.Add(salary1);
                                }
                                else
                                {
                                    PettyCashExpense cashExpense1 = new PettyCashExpense
                                    {
                                        Amount = item.Amount,
                                        ExpDate = item.OnDate,
                                        EmployeeId = 1//Alok Emp Id,
                               ,
                                        Particulars = item.Naration,
                                        Remarks = "AutoUpload",
                                        PaidTo = item.Naration,
                                        StoreId = StoreId

                                    };
                                    item.IsConsumed = true;
                                    db.ImportBookEntries.Update(item);
                                    db.PettyCashExpenses.Add(cashExpense1);
                                }

                            }
                            else if (item.Naration.Contains("ANUBHA"))
                            {
                                CashPayment cash3 = new CashPayment
                                {
                                    Amount = item.Amount,
                                    StoreId = StoreId,
                                    PaymentDate = item.OnDate,
                                    PaidTo = item.Naration,
                                    SlipNo = "AutoUpload",
                                    TranscationModeId = 2
                                };
                                item.IsConsumed = true;
                                db.CashPayments.Add(cash3);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("PETROL") || item.Naration.Contains("PettyCash RM:") || item.Naration.Contains("VERNA") || item.Naration.Contains("NEWSPAPER") || item.Naration.Contains("NET") || item.Naration.Contains("TILES MISTRY"))
                            {
                                PettyCashExpense cashExpense2 = new PettyCashExpense
                                {
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,//ALOK EMP ID

                                    Particulars = item.Naration,
                                    Remarks = "AutoUpload",
                                    PaidTo = item.Naration,
                                    StoreId = StoreId

                                };
                                item.IsConsumed = true;
                                db.ImportBookEntries.Update(item);
                                db.PettyCashExpenses.Add(cashExpense2);
                            }
                            else if (item.Naration.StartsWith("MUKESH(STAFF)"))
                            {
                                SalaryPayment salary1 = new SalaryPayment
                                {
                                    EmployeeId = 3,//TODO: For Mukesh
                                    Amount = item.Amount,
                                    PaymentDate = item.OnDate,
                                    PayMode = PayModes.Cash,
                                    SalaryComponet = SalaryComponet.Others,
                                    StoreId = StoreId,
                                    Details = "AutoUpload #" + item.Naration,
                                    SalaryMonth = item.OnDate.Year.ToString()
                                };
                                item.IsConsumed = true;
                                db.ImportBookEntries.Update(item);
                                db.SalaryPayments.Add(salary1);
                            }
                            else if (item.Naration.StartsWith("ALOK(Staff)"))
                            {
                                SalaryPayment salary1 = new SalaryPayment
                                {
                                    EmployeeId = 1,//TODO: For Mukesh
                                    Amount = item.Amount,
                                    PaymentDate = item.OnDate,
                                    PayMode = PayModes.Cash,
                                    SalaryComponet = SalaryComponet.Others,
                                    StoreId = StoreId,
                                    Details = "AutoUpload #" + item.Naration,
                                    SalaryMonth = item.OnDate.Year.ToString()
                                };
                                item.IsConsumed = true;
                                db.ImportBookEntries.Update(item);
                                db.SalaryPayments.Add(salary1);
                            }
                            else if (item.Naration.StartsWith("SANJEEV(STAFF)"))
                            {
                                SalaryPayment salary1 = new SalaryPayment
                                {
                                    EmployeeId = 2,//TODO: For Sanjeev
                                    Amount = item.Amount,
                                    PaymentDate = item.OnDate,
                                    PayMode = PayModes.Cash,
                                    SalaryComponet = SalaryComponet.Others,
                                    StoreId = StoreId,
                                    Details = "AutoUpload #" + item.Naration,
                                    SalaryMonth = item.OnDate.Year.ToString()
                                };
                                item.IsConsumed = true;
                                db.ImportBookEntries.Update(item);
                                db.SalaryPayments.Add(salary1);
                            }
                            else if (item.Naration.StartsWith("SOURAV(STAFF)"))
                            {
                                SalaryPayment salary1 = new SalaryPayment
                                {
                                    EmployeeId = 6,//TODO: For SOURAV(STAFF)
                                    Amount = item.Amount,
                                    PaymentDate = item.OnDate,
                                    PayMode = PayModes.Cash,
                                    SalaryComponet = SalaryComponet.Others,
                                    StoreId = StoreId,
                                    Details = "AutoUpload #" + item.Naration,
                                    SalaryMonth = item.OnDate.Year.ToString()
                                };
                                item.IsConsumed = true;
                                db.ImportBookEntries.Update(item);
                                db.SalaryPayments.Add(salary1);
                            }
                            else if (item.Naration.Contains("MUKESH(HOME)"))
                            {
                                CashPayment cash = new CashPayment
                                {
                                    Amount = item.Amount,
                                    StoreId = StoreId,
                                    PaymentDate = item.OnDate,
                                    PaidTo = item.Naration,
                                    SlipNo = "AutoUpload",
                                    TranscationModeId = 1
                                };
                                item.IsConsumed = true;
                                db.CashPayments.Add(cash);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("TV INSTALMENT RM"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "TV INSTALMENT RM:Mukesh",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("GOPI"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "GOPI",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("ELECTRIC BILL"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "ELECTRIC BILL",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("TELEPHONE"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "TELEPHONE BILL",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("Expenses RM:"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "UpdatePartyName",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("FLOWER"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "FLOWER",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("JAFAR MASTER"))
                            {
                                SalaryPayment salarys = new SalaryPayment
                                {
                                    EmployeeId = 0,//TODO: For Zafar
                                    Amount = item.Amount,
                                    PaymentDate = item.OnDate,
                                    PayMode = PayModes.Cash,
                                    SalaryComponet = SalaryComponet.Others,
                                    StoreId = StoreId,
                                    Details = "AutoUpload #" + item.Naration,
                                    SalaryMonth = item.OnDate.Year.ToString()
                                };
                                item.IsConsumed = true;
                                db.ImportBookEntries.Update(item);
                                db.SalaryPayments.Add(salarys);
                            }
                            else if (item.Naration.Contains("JAFAR RM"))
                            {
                                Expense expense = new Expense
                                {
                                    StoreId = StoreId,
                                    Amount = item.Amount,
                                    ExpDate = item.OnDate,
                                    EmployeeId = 1,
                                    PaidTo = "Zafar",
                                    Particulars = item.Naration,
                                    PaymentDetails = "Cash",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUpload"
                                };
                                item.IsConsumed = true;
                                db.Expenses.Add(expense);
                                db.ImportBookEntries.Update(item);
                            }
                            else if (item.Naration.Contains("BULLET SHOWROOM RM:"))
                            {
                                Payment pay = new Payment
                                {
                                    Amount = item.Amount,
                                    PayDate = item.OnDate,
                                    PaymentDetails = item.Naration,
                                    PaymentPartry = "BULLET SHOWROOM Dumka",
                                    PaymentSlipNo = "AUTOUPLOAD",
                                    PayMode = PaymentModes.Cash,
                                    Remarks = "AutoUPLOAD",
                                    StoreId = StoreId
                                };
                                item.IsConsumed = true;
                                db.Payments.Add(pay);
                                db.ImportBookEntries.Update(item);
                            }

                            break;
                        case LedgerBy.Zafar:
                            SalaryPayment salary = new SalaryPayment
                            {
                                EmployeeId = 0,//TODO: For Zafar
                                Amount = item.Amount,
                                PaymentDate = item.OnDate,
                                PayMode = PayModes.Cash,
                                SalaryComponet = SalaryComponet.Others,
                                StoreId = StoreId,
                                Details = "AutoUpload #" + item.Naration,
                                SalaryMonth = item.OnDate.Year.ToString()
                            };
                            item.IsConsumed = true;
                            db.ImportBookEntries.Update(item);
                            db.SalaryPayments.Add(salary);

                            break;
                        default:
                            break;
                    }
                }

            }
        }
        private int ProcessContraVoucher(AprajitaRetailsContext db, int StoreId)
        {
            var dataList = db.ImportBookEntries.Where(c => !c.IsConsumed && c.VoucherType == VoucherType.Contra);
            foreach (var item in dataList)
            {
                if (item.LedgerTo == LedgerTo.Cash)
                {
                    if (item.Naration.Contains("Bank Deposit "))
                    {
                        BankDeposit deposit = new BankDeposit
                        {
                            Amount = item.Amount,
                            Details = item.Naration,
                            DepoDate = item.OnDate,
                            PayMode = BankPayModes.Cash,
                            StoreId = StoreId,
                            Remarks = "AutoAdded"
                        };
                        switch (item.LedgerBy)
                        {
                            case LedgerBy.AmitKumar:
                                deposit.AccountNumberId = 12;
                                deposit.Remarks = deposit.Remarks + " #LedBy: Amit Kumar";
                                break;
                            case LedgerBy.BandhanCA:
                                deposit.AccountNumberId = 3;
                                break;

                            case LedgerBy.HDFCCA:
                                deposit.AccountNumberId = 11;
                                break;
                            case LedgerBy.ICICIBankCA:
                                deposit.AccountNumberId = 6;
                                break;
                            case LedgerBy.IDBICA:
                                deposit.AccountNumberId = 10;
                                break;
                            case LedgerBy.Others:
                                deposit.AccountNumberId = 12;
                                deposit.Remarks = deposit.Remarks + " #LedBy: Others";
                                break;
                            case LedgerBy.SBICC:
                                deposit.AccountNumberId = 1;
                                break;
                            case LedgerBy.Suspense:
                                deposit.AccountNumberId = 12;
                                deposit.Remarks = deposit.Remarks + " #LedBy: Suspense";
                                break;

                            default:
                                break;
                        }
                        item.IsConsumed = true;
                        db.BankDeposits.Add(deposit);
                        db.ImportBookEntries.Update(item);
                    }


                }

            }
            return db.SaveChanges();
        }


        private void ProcessEDCReciept(AprajitaRetailsContext db, int StoreId) { }
        private void ProcessSuspensesReciept(AprajitaRetailsContext db, int StoreId) { }

    }
}
