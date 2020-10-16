using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.JsonData;
using AprajitaRetails.Models.Voy;
using AprajitaRetails.Ops.TAS.Mails;
using AprajitaRetailsWatcher.Model.XMLData;
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

        public static async Task<ServerReturn> ProcessVoyInvoiceXML(Root invoice, AprajitaRetailsContext db)
        {
            ServerReturn @return = new ServerReturn { Error = false, Success = false, ErrorMessage = "", SuccessMessage = "" };

            //@return.SuccessMessage = "Got it";
            //return @return;

            try
            {
                VBInvoice vBInvoice = ToSaleInvoice (invoice, db);
                db.VBInvoices.Add (vBInvoice);
                int crt = await db.SaveChangesAsync ();

                if ( crt > 0 )
                {
                    @return.Success = true;
                    @return.SuccessMessage = $"Record Added Successful.#InvoiceId={vBInvoice.VBInvoiceId} on @{DateTime.Now}";
                }
                else
                {
                    @return.Error = true;
                    @return.ErrorMessage = "Failed to save the records";
                }
                MyMail.SendEmail ("Invoice Upload", JsonConvert.SerializeObject (vBInvoice), "amitnarayansah@gmail.com");


                return @return;
            }
            catch ( Exception ex )
            {
                @return.Error = true;
                @return.ErrorMessage = $"Error Occurred.#{ex.Message}";
                MyMail.SendEmail ("Invoice Upload Error", @return.ErrorMessage, "amitnarayansah@gmail.com");
                return @return;

            }
        }


        public static async Task<ServerReturn> ProcessVoyInvoiceXML(root invoice, AprajitaRetailsContext db)
        {
            ServerReturn @return = new ServerReturn { Error = false, Success = false, ErrorMessage = "", SuccessMessage = "" };

            try
            {
                VBInvoice vBInvoice = ToSaleInvoice (invoice, db);
                db.VBInvoices.Add (vBInvoice);
                int crt = await db.SaveChangesAsync ();

                if ( crt > 0 )
                {
                    @return.Success = true;
                    @return.SuccessMessage = $"Record Added Successful.#InvoiceId={vBInvoice.VBInvoiceId} on @{DateTime.Now}";
                }
                else
                {
                    @return.Error = true;
                    @return.ErrorMessage = "Failed to save the records";
                }
                MyMail.SendEmail ("Invoice Upload", JsonConvert.SerializeObject (vBInvoice), "amitnarayansah@gmail.com");


                return @return;
            }
            catch ( Exception  ex)
            {
                @return.Error = true;
                @return.ErrorMessage = $"Error Occurred.#{ex.Message}";
                MyMail.SendEmail ("Invoice Upload Error", @return.ErrorMessage, "amitnarayansah@gmail.com");
                return @return;

            }
        }



        private static void AddSaleInvoice(AprajitaRetailsContext db)
        {

        }

        private static VBInvoice ToSaleInvoice(Root invoice, AprajitaRetailsContext db)
        {
            

            var bill = invoice.bill;
            
            VBInvoice vB = new VBInvoice
            {

                InvoiceNumber = bill.bill_number,
                BillType = bill.type,
                Tailoring = IsTailoringBill (bill.Custom_fields.field_details.tailoring_req),
                CustomerMobile = bill.customer.mobile.ToString (),
                CustomerName = bill.customer.name,
                BillAmount = (decimal) bill.bill_amount,
                BillGrossAmount = (decimal) bill.bill_gross_amount,
                DiscountAmount = bill.bill_discount

            };

            vB.OnDate = DateTime.Parse (bill.billing_time).Date;
            vB.StoreId = GetStoreId (db, bill.bill_store_id);

            List<VBPaymentDetail> details = new List<VBPaymentDetail> ();
            foreach ( var item in bill.Payment_Mode.Payment_detail.payment/* .Payment_Mode.Payment_detail*/ )
            {
                VBPaymentDetail vB1 = new VBPaymentDetail { Amount = item.value, Mode = item.mode };
                if ( String.IsNullOrEmpty ((string) item.notes) )
                {
                    vB1.Notes = "";
                }
                else
                {
                    vB1.Notes = item.notes.ToString ();
                }
                details.Add (vB1);

            }

            vB.VBPaymentDetails = details;
            List<VBLineItem> items = new List<VBLineItem> ();
            foreach ( var item in bill.line_items.line_item )
            {
                VBLineItem line = new VBLineItem
                {
                    Amount = item.value,
                    DiscountAmount = item.discount_value,
                    ItemCode = item.item_code,
                    LineItemType = item.line_item_type,
                    Qty = (double) item.qty,
                    Rate = item.rate,
                    SerialNo = Int16.Parse( item.serial),
                    LineTotalAmount = item.amount

                };
                items.Add (line);
            }
            vB.VBLineItems = items;

            return vB;

        }
        private static VBInvoice ToSaleInvoice(root invoice, AprajitaRetailsContext db)
        {
            var bill = invoice.bill;
            VBInvoice vB = new VBInvoice
            {

                InvoiceNumber = bill.bill_number,
                BillType = bill.type,
                Tailoring = IsTailoringBill (bill.Custom_fields.field_details.tailoring_req),
                CustomerMobile = bill.customer.mobile.ToString (),
                CustomerName = bill.customer.name,
                BillAmount = (decimal) bill.bill_amount,
                BillGrossAmount = (decimal) bill.bill_gross_amount,
                DiscountAmount = bill.bill_discount

            };

            vB.OnDate = DateTime.Parse (bill.billing_time).Date;
            vB.StoreId = GetStoreId (db, bill.bill_store_id);

            List<VBPaymentDetail> details = new List<VBPaymentDetail> ();
            foreach ( var item in bill.Payment_Mode.Payment_detail )
            {
                VBPaymentDetail vB1 = new VBPaymentDetail { Amount = item.value, Mode = item.mode };
                if ( String.IsNullOrEmpty ((string) item.notes) )
                {
                    vB1.Notes = "";
                }
                else
                {
                    vB1.Notes = item.notes.ToString ();
                }
                details.Add (vB1);

            }

            vB.VBPaymentDetails = details;
            List<VBLineItem> items = new List<VBLineItem> ();
            foreach ( var item in bill.line_items )
            {
                VBLineItem line = new VBLineItem
                {
                    Amount = item.value,
                    DiscountAmount = item.discount_value,
                    ItemCode = item.item_code,
                    LineItemType = item.line_item_type,
                    Qty = (double) item.qty,
                    Rate = item.rate,
                    SerialNo = item.serial,
                    LineTotalAmount = item.amount

                };
                items.Add (line);
            }
            vB.VBLineItems = items;

            return vB;

        }
        private static int GetStoreId(AprajitaRetailsContext db, string storeCode)
        {
            if ( storeCode.Contains ("JHC") )
            {
                storeCode = storeCode.Replace ("JHC", "JH");
            }
            int id = db.Stores.Where (c => c.StoreCode == storeCode).Select (c => c.StoreId).FirstOrDefault ();
            return id;
        }

        private static bool IsTailoringBill(string mode)
        {
            if ( mode == "N" )
                return false;
            else if ( mode == "Y" )
                return true;
            else
                return false;
        }



        private static void AddDailySale(VBInvoice inv)
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
                Remarks = "Auto Added",
                IsMatchedWithVOy = true
            };

            // Here check for Payment mode and do 
        }
    }
}
