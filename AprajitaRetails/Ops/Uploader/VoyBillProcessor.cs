using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.JsonData;
using AprajitaRetails.Models.Voy;
using AprajitaRetails.Ops.TAS.Mails;
using AprajitaRetails.Ops.Triggers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Uploader
{
    public class ServerReturn
    {
        public bool Error { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }

    public class VoyBillProcessor
    {
        public static async Task<ServerReturn> ProcessVoyInvoiceXML(Bill invoice, AprajitaRetailsContext db)
        {
            ServerReturn @return = new ServerReturn { Error = false, Success = false, ErrorMessage = "", SuccessMessage = "" };

            try
            {
                if (IsInvoiceExits(invoice.bill_number, db))
                {
                    @return.Error = true; @return.ErrorMessage = "Invoice Already added!";
                    return @return;
                }

                VBInvoice vBInvoice = ToSaleInvoice(invoice, db);
                db.VBInvoices.Add(vBInvoice);
                int crt = await db.SaveChangesAsync();

                if (crt > 0)
                {
                    @return.Success = true;
                    @return.SuccessMessage = $"Record Added Successful.#InvoiceId={vBInvoice.VBInvoiceId} on @{DateTime.Now}";
                   
                    AddUpdateCustomer(db, invoice, vBInvoice.StoreId);

                    int invNo = (int?)db.DailySales.Where(c => c.InvNo == vBInvoice.InvoiceNumber).Select(c => c.DailySaleId).FirstOrDefault() ?? 0;
                    if (invNo <= 0)
                    {

                        DailySale sale = AddDailySale(vBInvoice);
                        db.DailySales.Add(sale);
                        if (db.SaveChanges() > 0)
                        {
                            @return.SuccessMessage = $"Record Added Successful.#InvoiceId={vBInvoice.VBInvoiceId} and DailySale also on @{DateTime.Now}";
                            new SalesManager().OnInsert(db, sale);
                        }
                        else
                        {
                            @return.Error = true;
                            @return.ErrorMessage = "Failed to save the DailySale Record!.";
                        }
                    }


                }
                else
                {
                    @return.Error = true;
                    @return.ErrorMessage = "Failed to save the VBInvoice and DailySale record!.";
                }


                MyMail.SendEmail("Invoice Upload", JsonConvert.SerializeObject(invoice), "amitnarayansah@gmail.com");

                return @return;
            }
            catch (Exception ex)
            {
                @return.Error = true;
                @return.ErrorMessage = $"Error Occurred.\t#{ex.Message}";
                MyMail.SendEmail("Invoice Upload Error", @return.ErrorMessage, "amitnarayansah@gmail.com");
                return @return;
            }
        }
        private static void AddSaleInvoice(AprajitaRetailsContext db, VBInvoice inv)
        {


        }
        private static bool IsInvoiceExits(string invNumber, AprajitaRetailsContext db)
        {
            int id = (int?)db.VBInvoices.Where(c => c.InvoiceNumber == invNumber).Select(c => c.VBInvoiceId).FirstOrDefault() ?? 0;

            if (id > 0) return true; else return false;
        }
        private static VBInvoice ToSaleInvoice(Bill invoice, AprajitaRetailsContext db)
        {
            var bill = invoice;

            VBInvoice vB = new VBInvoice
            {
                InvoiceNumber = bill.bill_number,
                BillType = bill.type,
                Tailoring = IsTailoringBill(bill.Custom_fields.field_details.tailoring_req),
                CustomerMobile = bill.customer.mobile.ToString(),
                CustomerName = bill.customer.name,
                BillAmount = (decimal)bill.bill_amount,
                BillGrossAmount = (decimal)bill.bill_gross_amount,
                DiscountAmount = bill.bill_discount
            };

            vB.OnDate = DateTime.Parse(bill.billing_time).Date;
            vB.StoreId = GetStoreId(db, bill.bill_store_id);

            List<VBPaymentDetail> details = new List<VBPaymentDetail>();
            foreach (var item in bill.Payment_Mode.Payment_detail.payment)
            {
                VBPaymentDetail vB1 = new VBPaymentDetail { Amount = item.value, Mode = item.mode };
                if (String.IsNullOrEmpty((string)item.notes))
                {
                    vB1.Notes = "";
                }
                else
                {
                    vB1.Notes = item.notes.ToString();
                }
                details.Add(vB1);
            }

            vB.VBPaymentDetails = details;
            List<VBLineItem> items = new List<VBLineItem>();
            foreach (var item in bill.line_items.line_item)
            {
                VBLineItem line = new VBLineItem
                {
                    Amount = item.value,
                    DiscountAmount = item.discount_value,
                    ItemCode = item.item_code,
                    LineItemType = item.line_item_type,
                    Qty = (double)item.qty,
                    Rate = item.rate,
                    SerialNo = Int16.Parse(item.serial),
                    LineTotalAmount = item.amount
                };
                items.Add(line);
            }
            vB.VBLineItems = items;
            return vB;
        }

        private static int GetStoreId(AprajitaRetailsContext db, string storeCode)
        {
            if (storeCode.Contains("JHC"))
            {
                storeCode = storeCode.Replace("JHC", "JH");
            }
            int id = db.Stores.Where(c => c.StoreCode == storeCode).Select(c => c.StoreId).FirstOrDefault();
            return id;
        }

        private static bool IsTailoringBill(string mode)
        {
            if (mode == "N")
                return false;
            else if (mode == "Y")
                return true;
            else
                return false;
        }

        private static DailySale AddDailySale(VBInvoice inv)
        {
            DailySale sale = new DailySale
            {
                InvNo = inv.InvoiceNumber,
                IsTailoringBill = inv.Tailoring,
                IsManualBill = false,
                Amount = inv.BillGrossAmount,
                SaleDate = inv.OnDate,
                StoreId = inv.StoreId,
                IsDue = false,
                IsSaleReturn = false,
                UserName = "AutoAdded",
                Remarks = "Auto Added # Enter Correct Salesman",
                IsMatchedWithVOy = true,
                CashAmount = 0,
                PayMode = PayMode.Cash,
                SalesmanId = 3
            };

            if (inv.BillType.Contains("RETURN"))
            {
                sale.IsSaleReturn = true;
            }
            var pd = inv.VBPaymentDetails.First();
            if (pd.Mode == "CA")
            {
                sale.CashAmount = inv.BillGrossAmount;
                sale.PayMode = PayMode.Cash;
            }
            else if (pd.Mode == "CRD")
            {
                sale.CashAmount = 0;
                sale.PayMode = PayMode.Card;
            }
            else if (pd.Mode == "MIX")
            {
                sale.CashAmount = 0;
                sale.PayMode = PayMode.MixPayments;
            }

            return sale;

            // Here check for Payment mode and do
        }


        private static void AddUpdateCustomer(AprajitaRetailsContext db, Bill Invo, int StoreId)
        {
            var cust = Invo.customer;

            var customer = db.Customers.Where(c => c.MobileNo == cust.mobile).FirstOrDefault();
            if (customer != null) {

                customer.TotalAmount += Invo.bill_gross_amount;
                customer.NoOfBills++;
                db.Update(customer);
            }
            else
            {

                Areas.Voyager.Models.Customer newCust = new Areas.Voyager.Models.Customer
                {
                    Age = 18,
                    City = "Dumka",
                    CreatedDate = DateTime.Today,
                    DateOfBirth = DateTime.Today,
                    Gender = Gender.Male,
                    MobileNo = cust.mobile,
                    NoOfBills = 1,
                    TotalAmount = Invo.bill_gross_amount
                };
                var name = cust.name.Split(" ");

                newCust.FirstName = name[0];
                if (name.Length > 1)
                    newCust.LastName = name[1];
                else newCust.LastName = name[0];
                db.Customers.Add(newCust);
                //TODO: Add City Name from Store for Default.
            }
            db.SaveChanges();

        }

    }
}