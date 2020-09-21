using System.Collections.Generic;
public static class Constants
{
    public const string AdministratorRole = "Administrator";
    public const string UserRole = "User";
    public const int MAX_TAGS = 10;   //TODO: increase tag size

    public const string STOREID = "_StoreId";
    public const string STORECODE = "StoreCode";
    public const string EMPID = "_EMPID";
    public const string USERNAME = "_UserName";
}
public enum EmpType { Salesman, StoreManager, HouseKeeping, Owner, Accounts, TailorMaster, Tailors, TailoringAssistance, Others }

public enum Genders { Male, Female, TransGender }
public enum Units { Meters, Nos, Pcs, Packets, NoUnit }
public enum TaxType {  GST, SGST, CGST, IGST, VAT , CST}

//public enum SalePayMode { Cash, Card, Mix }//TODO: check update based on data present
public enum SalePayMode { Cash, Card, Mix, SR }//TODO: check update based on data present
public enum Sizes { S, M, L, XL, XXL, XXXL, T28, T30, T32, T34, T36, T38, T40, T41, T42, T44, T46, T48 }

public enum ProductCategorys { Fabric, ReadyMade, Accessiories, Tailoring, Trims, PromoItems, Coupons, GiftVouchers, Others }

public enum CardModes { DebitCard, CreditCard, AmexCard }

public enum CardTypes { Visa, MasterCard, Mastro, Amex, Dinners, Rupay, }

public enum LedgerType { Credit, Debit, Income, Expenses, Assests, Bank, Loan, Purchase, Sale, Vendor, Customer }

public enum VPayModes { CA, DC, CC, Mix, Wal, CRD, OTH }


// Aprajita Retails Context

public enum PayModes { Cash, Card, RTGS, NEFT, IMPS, Wallets, Cheques, DemandDraft, Points, Others, Coupons, MixPayments };
public enum PaymentModes { Cash, Card, RTGS, NEFT, IMPS, Wallets, Cheques, DemandDraft, Others };
public enum AttUnits { Present, Absent, HalfDay, Sunday, Holiday, StoreClosed };
public enum SalaryComponet { NetSalary, LastPcs, WOWBill, SundaySalary, Incentive, Others, Advance }
public enum BankPayModes { Cash, Card, Cheques, RTGS, NEFT, IMPS, Wallets, Others }



public enum UploadTypes { Purchase, SaleRegister, SaleItemWise, InWard, Customer,Attendance }

public static class UploadType
{

    public static List<string> Types = new List<string> { "Purchase", "SaleItemWise", "SaleRegister", "InWard", "Customer" };
}
public enum UploadReturns { Success, Error, FileNotFound, NotExcelType, ImportNotSupported }
public  enum LoginRole { Admin, StoreManager, Salesman, Accountant, RemoteAccountant, Member, PowerUser };


public enum LedgerEntryType { Expenses, Payment, Reciept, Salary, AdvacePayment, AdvaceReciept, ArvindLimited, Others }
public enum AccountType
{
    Saving, Current, CashCredit, OverDraft, Others

}


public enum VoucherType { Payment, Reciept, Contra, DebitNote, CreditNote, JV }
public enum LedgerTo { CashSales, POSSale, Cash, TailorBook, Suspense }
public enum Head { Sale, HDFCCA, TailorBook, BikashPatwari, Sanjeev, Zafar, Suspense, IDBICA, ICICIBankCA, BandhanCA, SBIOD, SBICC, AmitKumar, Others }
public enum LedgerBy { AmitKumar, Cash, BandhanCA, BHARATQR, EDCBandhan, EDCEASYTAP, EDCHDFC, EDCICICI, EDCSBI, EXPUNDEF, HDFCCA, ICICIBankCA, IDBICA, Others, SBICC, Suspense, Zafar }



//public enum Genders { Male, Female, TransGender }
//public enum Units { Nos, Meters, PCs, Packets }
//public enum TaxType { GST, SGST, CGST, IGST, VAT }



//public enum Sizes { S, M, L, XL, XXL, XXXL, T28, T30, T32, T34, T36, T38, T40, T41, T42, T44, T46, T48 }

//public enum ProductCategorys { Fabric, ReadyMade, Accessiories, Tailoring, Trims, PromoItems, Coupons, GiftVouchers, Others }

//public enum CardModes { DebitCard, CreditCard, AmexCard }

//public enum CardTypes { Visa, MasterCard, Mastro, Amex, Dinners, Rupay, }

//public enum LedgerType { Credit, Debit, Income, Expenses, Assests, Bank, Loan, Purchase, Sale, Vendor, Customer }

//public enum VPayModes { CA, DC, CC, Mix, Wal, CRD, OTH }


//// Aprajita Retails Context

//public enum PayModes { Cash, Card, RTGS, NEFT, IMPS, Wallets, Cheques, DemandDraft, Points, Others, Coupons, MixPayments };
//public enum PaymentModes { Cash, Card, RTGS, NEFT, IMPS, Wallets, Cheques, DemandDraft, Others };
//public enum AttUnits { Present, Absent, HalfDay, Sunday };
//public enum SalaryComponet { NetSalary, LastPcs, WOWBill, SundaySalary, Incentive, Others }
//public enum BankPayModes { Cash, Card, Cheques, RTGS, NEFT, IMPS, Wallets, Others }



//public enum UploadTypes { Purchase, SaleRegister, SaleItemWise, InWard, Customer }


//public class UploadType
//{

//    public static List<string> Types = new List<string> { "Purchase", "SaleItemWise", "SaleRegister", "InWard", "Customer" };
//}