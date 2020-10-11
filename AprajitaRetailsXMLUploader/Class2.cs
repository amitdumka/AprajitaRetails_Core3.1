using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetailsWatcher.Model.JSONData
{

    public class Rootobject
    {
        public Xml xml { get; set; }
        public Root root { get; set; }
    }

    public class Xml
    {
        public string version { get; set; }
    }

    public class Root
    {
        public Bill bill { get; set; }
    }

    public class Bill
    {
        public string type { get; set; }
        public string bill_number { get; set; }
        public string billing_time { get; set; }
        public string WalletPaymentId { get; set; }
        public string WalletPaymentWalletId { get; set; }
        public string bill_client_id { get; set; }
        public string bill_store_id { get; set; }
        public string bill_amount { get; set; }
        public string bill_gross_amount { get; set; }
        public string balancepoints_add { get; set; }
        public string bill_discount { get; set; }
        public Line_Items line_items { get; set; }
        public Customer customer { get; set; }
        public Custom_Fields Custom_fields { get; set; }
        public Payment_Mode Payment_Mode { get; set; }
    }

    public class Line_Items
    {
        public Line_Item[] line_item { get; set; }
    }

    public class Line_Item
    {
        public string line_item_type { get; set; }
        public string serial { get; set; }
        public string item_code { get; set; }
        public string qty { get; set; }
        public string rate { get; set; }
        public string value { get; set; }
        public string discount_value { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
    }

    public class Customer
    {
        public string name { get; set; }
        public string mobile { get; set; }
    }

    public class Custom_Fields
    {
        public Field_Details field_details { get; set; }
    }

    public class Field_Details
    {
        public string field_name { get; set; }
        public string tailoring_req { get; set; }
    }

    public class Payment_Mode
    {
        public Payment_Detail Payment_detail { get; set; }
    }

    public class Payment_Detail
    {
        public Payment payment { get; set; }
    }

    public class Payment
    {
        public string mode { get; set; }
        public string value { get; set; }
        public string notes { get; set; }
        public Attributes attributes { get; set; }
    }

    public class Attributes
    {
        public Attribute attribute { get; set; }
    }

    public class Attribute
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}
