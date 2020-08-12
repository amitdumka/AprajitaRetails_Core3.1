﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using AprajitaRetails.Areas.AddressBook.Models;
using AprajitaRetails.Areas.Uploader.Models;

using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace AprajitaRetails.Ops.Uploader
{  //Store Based Changes is made in this class , All function support Store
    public class ExcelUploaders
    {
        public UploadReturns UploadExcel(AprajitaRetailsContext db, UploadTypes UploadType, IFormFile FileUpload, string StoreCode, bool IsVat, bool IsLocal)
        {
            //UploadType = "InWard";
            //List<string> data = new List<string> ();
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
                    // ImportTime = DateTime.Today,
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
            List<ImportInWard> purchaseList = new List<ImportInWard>();
            //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3

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
                    // ImportDate = DateTime.Today,
                });
            }

            db.ImportInWards.AddRange(purchaseList);
            return db.SaveChanges();

            //return purchaseList;
        }

        private int ImportSaleRegister(AprajitaRetailsContext db, string StoreCode, string fileName)
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
            List<ImportSaleRegister> saleList = new List<ImportSaleRegister>();
            //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3
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

            //return purchaseList;
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
                            { // CashSales, POSSale, Cash, TailorBook, Suspense
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
    }
}