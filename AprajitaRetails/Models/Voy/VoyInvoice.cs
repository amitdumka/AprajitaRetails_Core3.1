
//namespace AprajitaRetailsWatcher.Model.XMLData
//{

//    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
//    public partial class root
//    {

//        private rootBill billField;

//        /// <remarks/>
//        public rootBill bill
//        {
//            get
//            {
//                return this.billField;
//            }
//            set
//            {
//                this.billField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class rootBill
//    {

//        private string typeField;

//        private string bill_numberField;

//        private string billing_timeField;

//        private object walletPaymentIdField;

//        private object walletPaymentWalletIdField;

//        private string bill_client_idField;

//        private string bill_store_idField;

//        private ushort bill_amountField;

//        private ushort bill_gross_amountField;

//        private byte balancepoints_addField;

//        private byte bill_discountField;

//        private rootBillLine_item[] line_itemsField;

//        private rootBillCustomer customerField;

//        private rootBillCustom_fields custom_fieldsField;

//        private rootBillPayment_Mode payment_ModeField;

//        /// <remarks/>
//        public string type
//        {
//            get
//            {
//                return this.typeField;
//            }
//            set
//            {
//                this.typeField = value;
//            }
//        }

//        /// <remarks/>
//        public string bill_number
//        {
//            get
//            {
//                return this.bill_numberField;
//            }
//            set
//            {
//                this.bill_numberField = value;
//            }
//        }

//        /// <remarks/>
//        public string billing_time
//        {
//            get
//            {
//                return this.billing_timeField;
//            }
//            set
//            {
//                this.billing_timeField = value;
//            }
//        }

//        /// <remarks/>
//        public object WalletPaymentId
//        {
//            get
//            {
//                return this.walletPaymentIdField;
//            }
//            set
//            {
//                this.walletPaymentIdField = value;
//            }
//        }

//        /// <remarks/>
//        public object WalletPaymentWalletId
//        {
//            get
//            {
//                return this.walletPaymentWalletIdField;
//            }
//            set
//            {
//                this.walletPaymentWalletIdField = value;
//            }
//        }

//        /// <remarks/>
//        public string bill_client_id
//        {
//            get
//            {
//                return this.bill_client_idField;
//            }
//            set
//            {
//                this.bill_client_idField = value;
//            }
//        }

//        /// <remarks/>
//        public string bill_store_id
//        {
//            get
//            {
//                return this.bill_store_idField;
//            }
//            set
//            {
//                this.bill_store_idField = value;
//            }
//        }

//        /// <remarks/>
//        public ushort bill_amount
//        {
//            get
//            {
//                return this.bill_amountField;
//            }
//            set
//            {
//                this.bill_amountField = value;
//            }
//        }

//        /// <remarks/>
//        public ushort bill_gross_amount
//        {
//            get
//            {
//                return this.bill_gross_amountField;
//            }
//            set
//            {
//                this.bill_gross_amountField = value;
//            }
//        }

//        /// <remarks/>
//        public byte balancepoints_add
//        {
//            get
//            {
//                return this.balancepoints_addField;
//            }
//            set
//            {
//                this.balancepoints_addField = value;
//            }
//        }

//        /// <remarks/>
//        public byte bill_discount
//        {
//            get
//            {
//                return this.bill_discountField;
//            }
//            set
//            {
//                this.bill_discountField = value;
//            }
//        }

//        /// <remarks/>
//        [System.Xml.Serialization.XmlArrayItemAttribute("line_item", IsNullable = false)]
//        public rootBillLine_item[] line_items
//        {
//            get
//            {
//                return this.line_itemsField;
//            }
//            set
//            {
//                this.line_itemsField = value;
//            }
//        }

//        /// <remarks/>
//        public rootBillCustomer customer
//        {
//            get
//            {
//                return this.customerField;
//            }
//            set
//            {
//                this.customerField = value;
//            }
//        }

//        /// <remarks/>
//        public rootBillCustom_fields Custom_fields
//        {
//            get
//            {
//                return this.custom_fieldsField;
//            }
//            set
//            {
//                this.custom_fieldsField = value;
//            }
//        }

//        /// <remarks/>
//        public rootBillPayment_Mode Payment_Mode
//        {
//            get
//            {
//                return this.payment_ModeField;
//            }
//            set
//            {
//                this.payment_ModeField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class rootBillLine_item
//    {

//        private string line_item_typeField;

//        private byte serialField;

//        private string item_codeField;

//        private decimal qtyField;

//        private ushort rateField;

//        private ushort valueField;

//        private byte discount_valueField;

//        private decimal amountField;

//        private string descriptionField;

//        /// <remarks/>
//        public string line_item_type
//        {
//            get
//            {
//                return this.line_item_typeField;
//            }
//            set
//            {
//                this.line_item_typeField = value;
//            }
//        }

//        /// <remarks/>
//        public byte serial
//        {
//            get
//            {
//                return this.serialField;
//            }
//            set
//            {
//                this.serialField = value;
//            }
//        }

//        /// <remarks/>
//        public string item_code
//        {
//            get
//            {
//                return this.item_codeField;
//            }
//            set
//            {
//                this.item_codeField = value;
//            }
//        }

//        /// <remarks/>
//        public decimal qty
//        {
//            get
//            {
//                return this.qtyField;
//            }
//            set
//            {
//                this.qtyField = value;
//            }
//        }

//        /// <remarks/>
//        public ushort rate
//        {
//            get
//            {
//                return this.rateField;
//            }
//            set
//            {
//                this.rateField = value;
//            }
//        }

//        /// <remarks/>
//        public ushort value
//        {
//            get
//            {
//                return this.valueField;
//            }
//            set
//            {
//                this.valueField = value;
//            }
//        }

//        /// <remarks/>
//        public byte discount_value
//        {
//            get
//            {
//                return this.discount_valueField;
//            }
//            set
//            {
//                this.discount_valueField = value;
//            }
//        }

//        /// <remarks/>
//        public decimal amount
//        {
//            get
//            {
//                return this.amountField;
//            }
//            set
//            {
//                this.amountField = value;
//            }
//        }

//        /// <remarks/>
//        public string description
//        {
//            get
//            {
//                return this.descriptionField;
//            }
//            set
//            {
//                this.descriptionField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class rootBillCustomer
//    {

//        private string nameField;

//        private ulong mobileField;

//        /// <remarks/>
//        public string name
//        {
//            get
//            {
//                return this.nameField;
//            }
//            set
//            {
//                this.nameField = value;
//            }
//        }

//        /// <remarks/>
//        public ulong mobile
//        {
//            get
//            {
//                return this.mobileField;
//            }
//            set
//            {
//                this.mobileField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class rootBillCustom_fields
//    {

//        private rootBillCustom_fieldsField_details field_detailsField;

//        /// <remarks/>
//        public rootBillCustom_fieldsField_details field_details
//        {
//            get
//            {
//                return this.field_detailsField;
//            }
//            set
//            {
//                this.field_detailsField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class rootBillCustom_fieldsField_details
//    {

//        private string field_nameField;

//        private string tailoring_reqField;

//        /// <remarks/>
//        public string field_name
//        {
//            get
//            {
//                return this.field_nameField;
//            }
//            set
//            {
//                this.field_nameField = value;
//            }
//        }

//        /// <remarks/>
//        public string tailoring_req
//        {
//            get
//            {
//                return this.tailoring_reqField;
//            }
//            set
//            {
//                this.tailoring_reqField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class rootBillPayment_Mode
//    {

//        private rootBillPayment_ModePayment[] payment_detailField;

//        /// <remarks/>
//        [System.Xml.Serialization.XmlArrayItemAttribute("payment", IsNullable = false)]
//        public rootBillPayment_ModePayment[] Payment_detail
//        {
//            get
//            {
//                return this.payment_detailField;
//            }
//            set
//            {
//                this.payment_detailField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class rootBillPayment_ModePayment
//    {

//        private string modeField;

//        private decimal valueField;

//        private object notesField;

//        private rootBillPayment_ModePaymentAttributes attributesField;

//        /// <remarks/>
//        public string mode
//        {
//            get
//            {
//                return this.modeField;
//            }
//            set
//            {
//                this.modeField = value;
//            }
//        }

//        /// <remarks/>
//        public decimal value
//        {
//            get
//            {
//                return this.valueField;
//            }
//            set
//            {
//                this.valueField = value;
//            }
//        }

//        /// <remarks/>
//        public object notes
//        {
//            get
//            {
//                return this.notesField;
//            }
//            set
//            {
//                this.notesField = value;
//            }
//        }

//        /// <remarks/>
//        public rootBillPayment_ModePaymentAttributes attributes
//        {
//            get
//            {
//                return this.attributesField;
//            }
//            set
//            {
//                this.attributesField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class rootBillPayment_ModePaymentAttributes
//    {

//        private rootBillPayment_ModePaymentAttributesAttribute attributeField;

//        /// <remarks/>
//        public rootBillPayment_ModePaymentAttributesAttribute attribute
//        {
//            get
//            {
//                return this.attributeField;
//            }
//            set
//            {
//                this.attributeField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.SerializableAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class rootBillPayment_ModePaymentAttributesAttribute
//    {

//        private object nameField;

//        private object valueField;

//        /// <remarks/>
//        public object name
//        {
//            get
//            {
//                return this.nameField;
//            }
//            set
//            {
//                this.nameField = value;
//            }
//        }

//        /// <remarks/>
//        public object value
//        {
//            get
//            {
//                return this.valueField;
//            }
//            set
//            {
//                this.valueField = value;
//            }
//        }
//    }


//}
