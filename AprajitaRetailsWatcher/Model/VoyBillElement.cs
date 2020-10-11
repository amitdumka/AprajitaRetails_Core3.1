namespace AprajitaRetailsWatcher.Model
{
    public class VoyBillElement
    {
        public string TYPE {get;set;}// "TYPE";
        public string BILL_NUMBER {get;set;}// "BILL_NUMBER";
        public string BILLING_TIME {get;set;}// "BILLING_TIME";
        public string BILL_AMOUNT {get;set;}// "BILL_AMOUNT";
        public string BILL_GROSS_AMOUNT {get;set;}// "BILL_GROSS_AMOUNT";
        public string BILL_DISCOUNT {get;set;}// "BILL_DISCOUNT";
        public string LINE_ITEMS {get;set;}// "LINE_ITEMS";
        public string LINE_ITEM {get;set;}// "LINE_ITEM";
        public string LINE_ITEM_TYPE {get;set;}// "LINE_ITEM_TYPE";
        public string SERIAL {get;set;}// "SERIAL";
        public string ITEM_CODE {get;set;}// "ITEM_CODE";
        public string QTY {get;set;}// "QTY";
        public string RATE {get;set;}// "RATE";
        public string VALUE {get;set;}// "VALUE";
        public string DISCOUNT_VALUE {get;set;}// "DISCOUNT_VALUE";
        public string AMOUNT {get;set;}// "AMOUNT";
        public string DESCRIPTION {get;set;}// "DESCRIPTION";

        public string CUSTOMER {get;set;}// "CUSTOMER";
        public string CUSTOMERNAME {get;set;}// "NAME";
        public string MOBILE {get;set;}// "MOBILE";

        public string PAYMENT_MODE {get;set;}// "PAYMENT_MODE";
        public string PAYMENT_DETAIL {get;set;}// "PAYMENT_DETAIL";
        public string PAYMENT {get;set;}// "PAYMENT";
        public string MODE {get;set;}// "MODE";
        public string PAYVALUE {get;set;}// "VALUE";
        public string NOTES {get;set;}// "NOTES";

        public string ATTRIBUTES {get;set;}// "ATTRIBUTES";
        public string ATTRIBUTE {get;set;}// "ATTRIBUTE";
        public string NAME {get;set;}// "NAME";
        public string VALUES {get;set;}// "VALUE";

        //BILL TYPE SERVICE
        public string PAYMODE_TYPE_DETAILS {get;set;}// "PAYMODE_TYPE_DETAILS";

        public string PAYMODE_VALUE {get;set;}// "PAYMODE_VALUE";
        public string PAYMODE_DETAILS {get;set;}// "PAYMODE_DETAILS";
        public string BILL_STORE_ID {get;set;}// "BILL_STORE_ID";
    }

}
