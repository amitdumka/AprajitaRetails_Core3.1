using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//http://tallydll.com/
namespace AprajitaRetails.Ops.Tally.doc
{
    public class temp
    {
    }
    public partial class VoucherCreate //: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void requestTally(string pGroupType)
        {
            //WebRequest Request = WebRequest.Create(TallyUrl.Text);
            //string exportxml = null;
            //int Amt = int.Parse(VCHAmount.Text);

            //if (pGroupType == "")
            //{
            //    exportxml = "<ENVELOPE>" +
            //                      "<HEADER>" +
            //                          "<TALLYREQUEST>Import Data</TALLYREQUEST>" +
            //                      "</HEADER>" +
            //                      "<BODY>" +
            //                          "<IMPORTDATA>" +
            //                              "<REQUESTDESC>" +
            //                                  "<REPORTNAME>Vouchers</REPORTNAME>" +
            //                                  "<STATICVARIABLES>" +
            //                                      "<SVCURRENTCOMPANY>##SVCURRENTCOMPANY</SVCURRENTCOMPANY>" +
            //                                  "</STATICVARIABLES>" +
            //                              "</REQUESTDESC>" +
            //                              "<REQUESTDATA>" +
            //                                  "<TALLYMESSAGE xmlns:UDF='TallyUDF'>" +
            //                                      "<VOUCHER VCHTYPE='Payment' ACTION='Create' OBJVIEW='Accounting Voucher View'>" +
            //                                          "<DATE>" + "11-Jun-2014" + "</DATE>" + "\r\n" +
            //                                          "<VOUCHERTYPENAME>Payment</VOUCHERTYPENAME>" +
            //                                          "<VOUCHERNUMBER>1</VOUCHERNUMBER>" +
            //                                          "<PARTYLEDGERNAME>" + "Cash" + "</PARTYLEDGERNAME>" +//VchCashBankLed.Text 
            //                                          "<PERSISTEDVIEW>Accounting Voucher View</PERSISTEDVIEW>" +
            //                                          "<EFFECTIVEDATE>" + "11-Jun-2014" + "</EFFECTIVEDATE>" +
            //                                          "<ALLLEDGERENTRIES.LIST>" +
            //                                              "<LEDGERNAME>" + "Expenses" + "</LEDGERNAME>" +// VCHLedger.Text 
            //                                              "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" +
            //                                              "<AMOUNT>" + 500 * -1 + "</AMOUNT>" +//(Amt * -1) 
            //                                          "</ALLLEDGERENTRIES.LIST>" +
            //                                          "<ALLLEDGERENTRIES.LIST>" +
            //                                              "<LEDGERNAME>" + "Cash" + "</LEDGERNAME>" +//VchCashBankLed.Text 
            //                                              "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" +
            //                                              "<AMOUNT>" + 500 + "</AMOUNT>" +//VCHAmount.Text 
            //                                          "</ALLLEDGERENTRIES.LIST>" +
            //                                      "</VOUCHER>" +
            //                                  "</TALLYMESSAGE>" +
            //                              "</REQUESTDATA>" +
            //                          "</IMPORTDATA>" +
            //                      "</BODY>" +
            //                  "</ENVELOPE>";
            //    Response.Write("<script LANGUAGE='JavaScript' >alert(''" + exportxml + "'')</script>");
            //}
            //else
            //{
            //    exportxml = "<ENVELOPE>" +
            //              "<HEADER>" +
            //                  "<VERSION>1</VERSION>" +
            //                  "<TALLYREQUEST>Export</TALLYREQUEST>" +
            //                  "<TYPE>Collection</TYPE>" +
            //                  "<ID>FilteredLedgers</ID>" +
            //              "</HEADER>" +
            //              "<BODY>" +
            //                  "<DESC>" +
            //                      "<TDL>" +
            //                          "<TDLMESSAGE>" +
            //                              "<COLLECTION NAME='FilteredLedgers' ISMODIFY='No'>" +
            //                                  "<SOURCECOLLECTION>Ledger</SOURCECOLLECTION>" +
            //                                  "<FETCH>Name</FETCH>" +
            //                                  "<FILTER>" + pGroupType + "</FILTER>" +
            //                              "</COLLECTION>" +
            //                              "<SYSTEM TYPE='Formulae' NAME='PartyExpense Filter' ISMODIFY='No'>" +
            //                                "$$IsLedOfGrp:$Name:$$GroupSundryCreditors OR $$IsLedOfGrp:$Name:$$GroupIndirectExpenses OR $$IsLedOfGrp:$Name:$$GroupDirectExpenses</SYSTEM>" +
            //                              "<SYSTEM TYPE='Formulae' NAME='BankCashFilter' ISMODIFY='No'>" +
            //                                "$$IsLedOfGrp:$Name:$$GroupBank OR $$IsLedOfGrp:$Name:$$GroupBankOD OR $$IsLedOfGrp:$Name:$$GroupCash</SYSTEM>" +
            //                          "</TDLMESSAGE>" +
            //                     "</TDL>" +
            //                  "</DESC>" +
            //              "</BODY>" +
            //          "</ENVELOPE>";
            //}


            //Byte[] bytesToWrite = Encoding.ASCII.GetBytes(exportxml);

            //Request.Method = "POST";
            //Request.ContentLength = bytesToWrite.Length;
            //Request.ContentType = "text/xml";

            //Stream newStream = Request.GetRequestStream();
            //newStream.Write(bytesToWrite, 0, bytesToWrite.Length);
            //newStream.Close();

            //HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
            //Stream dataStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(dataStream);

            //string responseFromServer = reader.ReadToEnd();
            //string xmlresponse = responseFromServer;

            //XmlDocument xd = new XmlDocument();
            //xd.LoadXml(xmlresponse);

            //XmlNodeList xmlNameList = xd.SelectNodes("NAME");

            //if (pGroupType == "PartyExpenseFilter")
            //{
            //    DropDownList1.Items.Clear();

            //    for (int i = 0; i < (xmlNameList.Count - 1); i++)
            //    {
            //        DropDownList1.Items.Add(xmlNameList.Item(i).InnerText.ToString());
            //    }
            //}

            //if (pGroupType == "BankCashFilter")
            //{
            //    DropDownList2.Items.Clear();

            //    for (int i = 0; i < (xmlNameList.Count - 1); i++)
            //    {
            //        DropDownList2.Items.Add(xmlNameList.Item(i).InnerText.ToString());
            //    }
            //}

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //requestTally("");
            //DropDownList1.Text = "";
            //DropDownList2.Text = "";
            //VCHAmount.Text = "";
            //DropDownList1.Focus();
        }
    }
}


/*
 * #1
 * <p>&nbsp;WebRequest Request = WebRequest.Create(TallyUrl.Text);<br />
string exportxml = null;<br />
int Amt = int.Parse(VCHAmount.Text);</p>

<p>exportxml = &quot;&lt;ENVELOPE&gt;&quot; +<br />
&quot;&lt;HEADER&gt;&quot; +<br />
&quot;&lt;TALLYREQUEST&gt;Export Data&lt;/TALLYREQUEST&gt;&quot; +<br />
&quot;&lt;/HEADER&gt;&quot; +<br />
&quot;&lt;BODY&gt;&quot; +<br />
. . .<br />
. . .<br />
&quot;&lt;/BODY&gt;&quot; +<br />
&quot;&lt;/ENVELOPE&gt;&quot;;<br />
}<br />

Byte[] bytesToWrite = Encoding.ASCII.GetBytes(exportxml);</p>

<p>Request.Method = &quot;POST&quot;;<br />
Request.ContentLength = bytesToWrite.Length;<br />
Request.ContentType = &quot;text/xml&quot;;</p>

<p>Stream newStream = Request.GetRequestStream();<br />
newStream.Write(bytesToWrite, 0, bytesToWrite.Length);<br />
newStream.Close();</p>

<p>HttpWebResponse response = (HttpWebResponse)Request.GetResponse();<br />
Stream dataStream = response.GetResponseStream();<br />
StreamReader reader = new StreamReader(dataStream);</p>

<p>string responseFromServer = reader.ReadToEnd();<br />
string xmlresponse = responseFromServer;</p>

<p>XmlDocument xd = new XmlDocument();<br />
xd.LoadXml(xmlresponse);</p>
 */

/*
 * #2
 * WebRequest Request = WebRequest.Create(TallyUrl.Text);
string exportxml = null;
int Amt = int.Parse(VCHAmount.Text);

exportxml = "<ENVELOPE>" +
"<HEADER>" +
"<TALLYREQUEST>Export Data</TALLYREQUEST>" +
"</HEADER>" +
"<BODY>" +
. . .
. . .
"</BODY>" +
"</ENVELOPE>";
}
Byte[] bytesToWrite = Encoding.ASCII.GetBytes(exportxml);

Request.Method = "POST";
Request.ContentLength = bytesToWrite.Length;
Request.ContentType = "text/xml";

Stream newStream = Request.GetRequestStream();
newStream.Write(bytesToWrite, 0, bytesToWrite.Length);
newStream.Close();

HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
Stream dataStream = response.GetResponseStream();
StreamReader reader = new StreamReader(dataStream);

string responseFromServer = reader.ReadToEnd();
string xmlresponse = responseFromServer;

XmlDocument xd = new XmlDocument();
xd.LoadXml(xmlresponse);
 */

/*
 * #3
 * <ENVELOPE>
          <HEADER>
                    <VERSION>1</VERSION>
                    <TALLYREQUEST>Import</TALLYREQUEST>
                    <TYPE>Data</TYPE>
                    <ID>All Masters</ID
          </HEADER>
          <BODY>
                    <DESC>
                             <STATICVARIABLES>
                                        <IMPORTDUPS>@@DUPCOMBINE</IMPORTDUPS>
                             </STATICVARIABLES>
                    </DESC>
                    <DATA>
                             <TALLYMESSAGE>
                                        <LEDGER NAME="ICICI" Action = "Create">
                                                  <NAME>ICICI</NAME>
                                                  <PARENT>Bank Accounts</PARENT>
                                                  <OPENINGBALANCE>-12500</OPENINGBALANCE>
                                        </LEDGER>
                                        <GROUP NAME=" Bangalore Debtors" Action = "Create">
                                                   <NAME>Bangalore Debtors</NAME>
                                                   <PARENT>Sundry Debtors</PARENT>
                                        </GROUP>
                                        <LEDGER NAME="RK Builders Pvt Ltd" Action = "Create">
                                                   <NAME>RK Builders Pvt Ltd</NAME>
                                                   <PARENT>Bangalore Debtors</PARENT>
                                                   <OPENINGBALANCE>-1000</OPENINGBALANCE>
                                        </LEDGER>
                             </TALLYMESSAGE>
                    </DATA>
          </BODY>
</ENVELOPE>

In the above XML Request, Create action is used. Any of the following system formulae can be used to choose the required behavior in case the system encounters a ledger with the same name. The behavior is for the treatment of the Opening Balance which is being imported.
DupModify specifies that the current Opening Balance should be modified with the new one that is being imported.
DupIgnoreCombine specifies that the ledger if exists need to be ignored.
DupCombine specifies the system to combine both the Opening Balances. Ideally, this option is used when Data pertaining to Group Companies are merged together into a single company. 
On processing the above request for importing ledgers, the requested ledgers are created in Tally. 

XML Response received From Tally.

<RESPONSE>
           <CREATED>2</CREATED>
           <ALTERED>0</ALTERED>
           <LASTVCHID>0</LASTVCHID>
           <LASTMID>0</LASTMID>
           <COMBINED>0</COMBINED>
           <IGNORED>0</IGNORED>
           <ERRORS>0</ERRORS>
</RESPONSE>

The above XML Response is a log of masters created, altered, combined, ignored or not imported due to some errors. It also contains information pertaining to the last Master ID imported. For Alteration and Deletion of Masters, the Object action needs to be Alter or Delete respectively.

For instance, in the above example,

<LEDGER NAME="ICICI" Action = "Alter">
          <NAME>HDFC</NAME>

Name of an existing ledger ICICI will get altered to HDFC.

In case of Deletion, following line suffices

<LEDGER NAME="ICICI" Action = "Delete">
 
Execute
 
Tags used for sending a request to Execute an action from Tally.ERP 9.
<HEADER> contains the following:
Tag <TALLYREQUEST> must contain value Execute
Tag <TYPE> must contain value TDLAction and
Tag <ID> should contain the Name of the TDL Action
Example : Request for Executing Synchronization in Tally

<ENVELOPE>
           <HEADER>
                    <VERSION>1</VERSION>
                    <TALLYREQUEST>Execute</TALLYREQUEST>
                    <TYPE>TDLAction</TYPE>
                    <ID>Sync</ID>
           </HEADER>
</ENVELOPE>

In the above XML request, <HEADER> describes the expected result.
The value of the Tag <TALLYREQUEST> is Execute which indicates that some action needs to be executed in Tally.
The value of the Tag <TYPE> is TDLAction which indicates that some TDLAction has to be executed in Tally.
The value of the Tag <ID> must be a TDL Action Name. Any action which needs to be exe­cuted in Tally can be specified within this Tag.
 */


/*
 * how to get vocher list of specified type
 * Tally Request XML for Exporting Sales Vouchers
 * <ENVELOPE>
<HEADER>
<VERSION>1</VERSION>
<TALLYREQUEST>Export</TALLYREQUEST>
<TYPE>Collection</TYPE>
<ID>
List of All Sales Vouchers</ID>
</HEADER>
<BODY>
<DESC>
<STATICVARIABLES>
<SVCURRENTCOMPANY>#CompanyName</SVCURRENTCOMPANY>
<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>
<SVFROMDATE TYPE="Date">fromdate</SVFROMDATE>
<SVTODATE TYPE="Date">todate 23:30:00</SVTODATE>
</STATICVARIABLES>
<TDL>
<TDLMESSAGE>
<COLLECTION NAME="List of All Sales Vouchers" ISMODIFY="No" ISFIXED="No" ISINITIALIZE="No" ISOPTION="No" ISINTERNAL="No">
<TYPE>Voucher</TYPE>
<FETCH>ADDRESS.LIST,DATE,PARTYNAME,LEDGERENTRIES.LIST</FETCH>
<FILTERS>TYPOFSALES</FILTERS>
</COLLECTION>
<SYSTEM TYPE="Formulae" NAME="TYPOFSALES">
$VOUCHERTYPENAME contains "INVOICE" </SYSTEM>
</TDLMESSAGE>
</TDL>
</DESC>
</BODY>
</ENVELOPE>
$VOUCHERTYPENAME is the name of your Tally Sales Voucher Type
 
 */

/*
 * How to filer xml response based on vocher Number
 * Tally Request XML for Exporting Voucher filter by specific voucher number
 * <ENVELOPE>
<HEADER>
<VERSION>1</VERSION>
<TALLYREQUEST>Export</TALLYREQUEST>
<TYPE>Collection</TYPE>
<ID>List of All Sales Vouchers</ID>
</HEADER>
<BODY>
<DESC>

<STATICVARIABLES>
<SVCURRENTCOMPANY>#company</SVCURRENTCOMPANY>
<SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>
<SVFROMDATE TYPE="Date">#fromdate</SVFROMDATE>
<SVTODATE TYPE="Date">#todate</SVTODATE>

</STATICVARIABLES>

<TDL>
<TDLMESSAGE>

<COLLECTION NAME="List of All Sales Vouchers" ISMODIFY="No" ISFIXED="No" ISINITIALIZE="No" ISOPTION="No" ISINTERNAL="No">
<TYPE>Voucher</TYPE>

<FETCH>ADDRESS.LIST,BASICBUYERADDRESS.LIST,DATE,
CLASSNAME,PARTYNAME,PERSISTEDVIEW,
BASICDUEDATEOFPYMT,LEDGERENTRIES.LIST</FETCH>

<FILTERS>TYPOFSALES</FILTERS>


</COLLECTION>

<SYSTEM TYPE="Formulae" NAME="TYPOFSALES">
$VOUCHERTYPENAME equal to "#TALLYVOUCHERTYPE" AND $VOUCHERNUMBER equal to "#TALLYVCHNO"
</SYSTEM>


</TDLMESSAGE>
</TDL>
</DESC>
</BODY>
</ENVELOPE>
 * 
 */

/*
 * aler credit list and no of days
 * 
 * <ENVELOPE>
<HEADER>
<TALLYREQUEST>Import Data</TALLYREQUEST>
</HEADER>
<BODY>
<IMPORTDATA>
<REQUESTDESC>
<REPORTNAME>All Masters</REPORTNAME>
<STATICVARIABLES>
     <SVCURRENTCOMPANY>#Company</SVCURRENTCOMPANY>
    </STATICVARIABLES>
</REQUESTDESC>
<REQUESTDATA>
<TALLYMESSAGE xmlns:UDF="TallyUDF">

<LEDGER NAME="#LEDGERNAME" ACTION="Alter">

<CREDITLIMIT>#CREDITLIMIT</CREDITLIMIT>
<BILLCREDITPERIOD>#CREDITDAYS</BILLCREDITPERIOD>
</LEDGER>

</TALLYMESSAGE>
</REQUESTDATA>
</IMPORTDATA>
</BODY>
</ENVELOPE>
 */


/*
 * I finally got a solution, using the following code I could insert into Tally from C#.net

try
            {
           
            xmlstc = xmlstc + "<VOUCHER VCHTYPE="+"\""+"Receipt"+"\" ACTION="+"\""+"Create"+"\">";
            xmlstc = "<ENVELOPE>";
            xmlstc = xmlstc + "<HEADER>" ;
            xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>" ;
            xmlstc = xmlstc + "</HEADER>";
            xmlstc = xmlstc + "<BODY>";
            xmlstc = xmlstc + "<IMPORTDATA>";
            xmlstc = xmlstc + "<REQUESTDESC>";
            xmlstc = xmlstc + "<REPORTNAME>Vouchers</REPORTNAME>";
            xmlstc = xmlstc + "<STATICVARIABLES>" ;
            xmlstc = xmlstc + "<SVCURRENTCOMPANY>" + "##SVCURRENTCOMPANY" + "</SVCURRENTCOMPANY>" ;
            xmlstc = xmlstc + "</STATICVARIABLES>";
            xmlstc = xmlstc + "</REQUESTDESC>";
            
            xmlstc = xmlstc + "<REQUESTDATA>";
            
           
            strVchNumber = txtVhrNo.Text;
            //strDate = "01/04/2020";
            strDate = dtpDate.Value.Date.ToShortDateString();
            strNarration = txtName.Text;
            strAmount = txtAmount.Text;
            
            xmlstc = xmlstc + "<TALLYMESSAGE >";
            xmlstc = xmlstc + "<VOUCHER VCHTYPE=" + "\"" + "Receipt" + "\" ACTION=" + "\"" + "Create" + "\">";
            xmlstc = xmlstc + "<VOUCHERNUMBER>" + strVchNumber + "</VOUCHERNUMBER>" ;
            xmlstc = xmlstc + "<DATE>" + strDate + "</DATE>";
            xmlstc = xmlstc + "<EFFECTIVEDATE>" + strDate + "</EFFECTIVEDATE>";
            xmlstc = xmlstc + "<NARRATION>" + strNarration + "</NARRATION>";
            xmlstc = xmlstc + "<VOUCHERTYPENAME>" + strVchType + "</VOUCHERTYPENAME>";
    
              //Credit Ledger
                            xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>";
                            xmlstc = xmlstc + "<LEDGERNAME>" + "Tution Fees - D&apos;Ring" + "</LEDGERNAME>" ;
                            xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>";
                            xmlstc = xmlstc + "<AMOUNT>" + strAmount + "</AMOUNT>";
                            xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" ;
            
             //Debit Ledger
                            xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" ;
                            xmlstc = xmlstc + "<LEDGERNAME>" + "Tution Fees - D&apos;Ring" + "</LEDGERNAME>" ;
                            xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>";
                            xmlstc = xmlstc + "<AMOUNT>-" + strAmount + "</AMOUNT>";
                            xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>";
                        
            xmlstc = xmlstc + "</VOUCHER>";
            xmlstc = xmlstc + "</TALLYMESSAGE>";
            xmlstc = xmlstc + "</REQUESTDATA>";
            xmlstc = xmlstc + "</IMPORTDATA>";
            xmlstc = xmlstc + "</BODY>";
            xmlstc = xmlstc + "</ENVELOPE>";

            

            string result = "";
            

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9000");
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = xmlstc.Length;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            streamWriter.Write(xmlstc);
            MessageBox.Show("Data inserted into Tally sucessfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.StackTrace);
            }
            finally
            {
                streamWriter.Close();
            }
 */

/*
 * This is the code which I used to insert into tally

xmlstc = "<ENVELOPE>";
                    xmlstc = xmlstc + "<HEADER><TALLYREQUEST>Import Data</TALLYREQUEST></HEADER>";
                    xmlstc = xmlstc + "<BODY><IMPORTDATA><

<div id=":1su">REQUESTDESC><REPORTNAME>All Masters</REPORTNAME>";
                    xmlstc = xmlstc + "<STATICVARIABLES><SVEXPORTFORMAT>$$SYSNAME:XML</SVEXPORTFORMAT> ";
                    xmlstc = xmlstc + "<SVCURRENTCOMPANY>##SVCURRENTCOMPANY</SVCURRENTCOMPANY>";
                    xmlstc = xmlstc + "<IMPORTDUPS>@@DUPIGNORECOMBINE</IMPORTDUPS>";
                    xmlstc = xmlstc + "</STATICVARIABLES></REQUESTDESC>";
                    xmlstc = xmlstc + "<REQUESTDATA>";

                    xmlstc = xmlstc + "<TALLYMESSAGE><LEDGER>";
                    xmlstc = xmlstc + "<NAME.LIST>";
                    if (strLedgerName != "")
                    {
                        xmlstc = xmlstc + "<NAME>" + strLedgerName + "</NAME>";
                    }
                    if (strLedgerId != "")
                    {
                        xmlstc = xmlstc + "<NAME>" + strLedgerId + "</NAME>";
                    }
                    xmlstc = xmlstc + "</NAME.LIST>";
                    xmlstc = xmlstc + "<ADDRESS.LIST>";
                    if (strAddress1 != "")
                    {
                        xmlstc = xmlstc + "<ADDRESS>" + strAddress1 + "</ADDRESS>";
                    }
                    if (strAddress1 != "")
                    {
                        xmlstc = xmlstc + "<ADDRESS>" + strAddress1 + "</ADDRESS>";
                    }
                    if (strAddress1 != "")
                    {
                        xmlstc = xmlstc + "<ADDRESS>" + strAddress1 + "</ADDRESS>";
                    }
                    xmlstc = xmlstc + "</ADDRESS.LIST>";
                    xmlstc = xmlstc + "<MAILINGNAME.LIST >";
                    xmlstc = xmlstc + "<MAILINGNAME>Supplier</MAILINGNAME>";
                    xmlstc = xmlstc + "</MAILINGNAME.LIST>";
                    xmlstc = xmlstc + "<CURRENCYNAME>aed</CURRENCYNAME>";
                    if (strEmailId != "")
                    {
                        xmlstc = xmlstc + "<EMAIL>" + strEmailId + "</EMAIL>";
                    }
                    //xmlstc = xmlstc + "<PARENT>SUNDRY CREDITORS</PARENT>";
                    xmlstc = xmlstc + "<PARENT>SUNDRY DEBTORS</PARENT>";
                    xmlstc = xmlstc + "<BILLCREDITPERIOD>30 Days</BILLCREDITPERIOD>";
                    if (strPhone != "")
                    {
                        xmlstc = xmlstc + "<LEDGERPHONE>" + strPhone + "</LEDGERPHONE>";
                    }
                    if (strFax != "")
                    {
                        xmlstc = xmlstc + "<LEDGERFAX>" + strFax + "</LEDGERFAX>";
                    }
                    if (strCntPerson != "")
                    {
                        xmlstc = xmlstc + "<LEDGERCONTACT>" + strCntPerson + "</LEDGERCONTACT>";
                    }
                    xmlstc = xmlstc + "<ISBILLWISEON>Yes</ISBILLWISEON>";
                    xmlstc = xmlstc + "<ISCOSTCENTRESON>No</ISCOSTCENTRESON>";
                    xmlstc = xmlstc + "<ISINTERESTON>No</ISINTERESTON>";
                    xmlstc = xmlstc + "<ISCONDENSED>No</ISCONDENSED>";
                    xmlstc = xmlstc + "<AFFECTSSTOCK>No</AFFECTSSTOCK>";
                    xmlstc = xmlstc + "<FORPAYROLL>No</FORPAYROLL>";

                    xmlstc = xmlstc + "<CREDITLIMIT>" + strCreditlimit + "</CREDITLIMIT>";
                    xmlstc = xmlstc + "</LEDGER></TALLYMESSAGE>";
                    xmlstc = xmlstc + "</REQUESTDATA>";
                    xmlstc = xmlstc + "</IMPORTDATA></BODY></ENVELOPE>";


                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:" + Connection.TallyPort);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentLength = xmlstc.Length;
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                    streamWriter.Write(xmlstc);
                    streamWriter.Close();</div>
 */


/*
 * This is the code for posting purchase details into tally

xmlstc = "<ENVELOPE>"+"\r\n";
                        xmlstc = xmlstc + "<HEADER>" + "\r\n";
                        xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc = xmlstc + "</HEADER>" + "\r\n";
                        xmlstc = xmlstc + "<BODY>" + "\r\n";
                        xmlstc = xmlstc + "<IMPORTDATA>" + "\r\n";
                        xmlstc = xmlstc + "<REQUESTDESC>" + "\r\n";
                        xmlstc = xmlstc + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc = xmlstc + "<STATICVARIABLES>" + "\r\n";
                        xmlstc = xmlstc + "<SVCURRENTCOMPANY>##SVCURRENTCOMPANY</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
                        xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
                        xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
                        xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc = xmlstc + "<VOUCHER VCHTYPE=" + "\"" + "Purchase" + "\" >" + "\r\n";
                        xmlstc = xmlstc + "<DATE>" + strGRNDate + "</DATE>" + "\r\n";
                        xmlstc = xmlstc + "<VOUCHERTYPENAME>Purchase</VOUCHERTYPENAME>" + "\r\n";
                        xmlstc = xmlstc + "<VOUCHERNUMBER>" + strVoucherNo + "</VOUCHERNUMBER>" + "\r\n";
                        xmlstc = xmlstc + "<REFERENCE>" + strGRNNo + "</REFERENCE>" + "\r\n";
                        xmlstc = xmlstc + "<PARTYLEDGERNAME>" + strSupplierName + "</PARTYLEDGERNAME>" + "\r\n";
                        xmlstc = xmlstc + "<PARTYNAME>" + strSupplierName + "</PARTYNAME>" + "\r\n";
                        xmlstc = xmlstc + "<EFFECTIVEDATE>" + strGRNDate + "</EFFECTIVEDATE>" + "\r\n";
                        xmlstc = xmlstc + "<ISINVOICE>Yes</ISINVOICE>" + "\r\n";
                        xmlstc = xmlstc + "<INVOICEORDERLIST.LIST>" + "\r\n";
                        xmlstc = xmlstc + "<BASICORDERDATE>" + strGRNDate + "</BASICORDERDATE>" + "\r\n";
                        xmlstc = xmlstc + "<BASICPURCHASEORDERNO>" + strPurchaseOrder + "</BASICPURCHASEORDERNO>" + "\r\n";
                        xmlstc = xmlstc + "</INVOICEORDERLIST.LIST>" + "\r\n";
                        xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc = xmlstc + "<LEDGERNAME>" + strSupplierName + "</LEDGERNAME>" + "\r\n";
                        xmlstc = xmlstc + "<GSTCLASS/>" + "\r\n";
                        xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + "\r\n";
                        xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                        xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                        xmlstc = xmlstc + "<ISPARTYLEDGER>Yes</ISPARTYLEDGER>" + "\r\n";
                        xmlstc = xmlstc + "<AMOUNT>" + strGRN + "</AMOUNT>" + "\r\n";
                        xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>" + "\r\n";
                        xmlstc = xmlstc + "<NAME>" + strGRNNo + "</NAME>" + "\r\n";
                        xmlstc = xmlstc + "<BILLCREDITPERIOD>30 Days</BILLCREDITPERIOD>" + "\r\n";
                        xmlstc = xmlstc + "<BILLTYPE>New Ref</BILLTYPE>" + "\r\n";
                        xmlstc = xmlstc + "<AMOUNT>" + strGRN + "</AMOUNT>" + "\r\n";
                        xmlstc = xmlstc + "</BILLALLOCATIONS.LIST>" + "\r\n";
                        xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc = xmlstc + "<LEDGERNAME>Purchase</LEDGERNAME>" + "\r\n";
                        xmlstc = xmlstc + "<GSTCLASS/>" + "\r\n";
                        xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + "\r\n";
                        xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                        xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                        xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>" + "\r\n";
                        xmlstc = xmlstc + "<AMOUNT>" + strGRNValueNtv + "</AMOUNT>" + "\r\n";
                        xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc = xmlstc + "</VOUCHER>" + "\r\n";
                        xmlstc = xmlstc + "</TALLYMESSAGE>" + "\r\n";
                        xmlstc = xmlstc + "</REQUESTDATA>" + "\r\n";
                        xmlstc = xmlstc + "</IMPORTDATA>" + "\r\n";
                        xmlstc = xmlstc + "</BODY>" + "\r\n";
                        xmlstc = xmlstc + "</ENVELOPE>" + "\r\n";

                        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:" + Connection.TallyPort);
                        httpWebRequest.Method = "POST";
                        httpWebRequest.ContentLength = xmlstc.Length;
                        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                        streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                        streamWriter.Write(xmlstc);
                        streamWriter.Close();


And this is the code for posting sales details use two functions  (for credit note and Invoice)

private void Invoice(string custName,string InvDate,string InvNo,string InvAmt,string SalesOrderNo,string Discount,string Surcharge1,string Surcharge2,string Surcharge3,double NetAmount)
        {
            try
            {
                xmlstc = "<ENVELOPE>" + "\r\n";
                xmlstc = xmlstc + "<HEADER>" + "\r\n";
                xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                xmlstc = xmlstc + "</HEADER>" + "\r\n";
                xmlstc = xmlstc + "<BODY>" + "\r\n";
                xmlstc = xmlstc + "<IMPORTDATA>" + "\r\n";
                xmlstc = xmlstc + "<REQUESTDESC>" + "\r\n";
                xmlstc = xmlstc + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                xmlstc = xmlstc + "<STATICVARIABLES>" + "\r\n";
                xmlstc = xmlstc + "<SVCURRENTCOMPANY>##SVCURRENTCOMPANY</SVCURRENTCOMPANY>" + "\r\n";
                xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
                xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
                xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                xmlstc = xmlstc + "<VOUCHER VCHTYPE=" + "\"" + "Sales" + "\" ACTION=" + "\"" + "Create" + "\" >" + "\r\n";
                xmlstc = xmlstc + "<DATE>"+InvDate+"</DATE>" + "\r\n";
                xmlstc = xmlstc + "<VOUCHERTYPENAME>Sales</VOUCHERTYPENAME>" + "\r\n";
                xmlstc = xmlstc + "<VOUCHERNUMBER>"+InvNo+"</VOUCHERNUMBER>" + "\r\n";
                xmlstc = xmlstc + "<REFERENCE>Ref</REFERENCE>" + "\r\n";
                xmlstc = xmlstc + "<PARTYLEDGERNAME>"+custName+"</PARTYLEDGERNAME>" + "\r\n";
                xmlstc = xmlstc + "<EFFECTIVEDATE>"+InvDate+"</EFFECTIVEDATE>" + "\r\n";
                xmlstc = xmlstc + "<ISINVOICE>Yes</ISINVOICE>" + "\r\n";
                xmlstc = xmlstc + "<INVOICEORDERLIST.LIST>" + "\r\n";
                xmlstc = xmlstc + "<BASICORDERDATE>" + InvDate + "</BASICORDERDATE>" + "\r\n";
                xmlstc = xmlstc + "<BASICPURCHASEORDERNO>" + SalesOrderNo + "</BASICPURCHASEORDERNO>" + "\r\n";
                xmlstc = xmlstc + "</INVOICEORDERLIST.LIST>" + "\r\n";
                xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                xmlstc = xmlstc + "<LEDGERNAME>"+custName+"</LEDGERNAME>" + "\r\n";
                xmlstc = xmlstc + "<GSTCLASS/>" + "\r\n";
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + "\r\n";
                xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                xmlstc = xmlstc + "<ISPARTYLEDGER>Yes</ISPARTYLEDGER>" + "\r\n";
                xmlstc = xmlstc + "<AMOUNT>-"+NetAmount+"</AMOUNT>" + "\r\n";
                xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>" + "\r\n";
                xmlstc = xmlstc + "<NAME>" + InvNo + "</NAME>" + "\r\n";
                xmlstc = xmlstc + "<BILLCREDITPERIOD>30 Days</BILLCREDITPERIOD>" + "\r\n";
                xmlstc = xmlstc + "<BILLTYPE>New Ref</BILLTYPE>" + "\r\n";
                xmlstc = xmlstc + "<AMOUNT>-" + NetAmount + "</AMOUNT>" + "\r\n";
                xmlstc = xmlstc + "</BILLALLOCATIONS.LIST>" + "\r\n";
                xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                xmlstc = xmlstc + "<LEDGERNAME>Sales</LEDGERNAME>" + "\r\n";
                xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + "\r\n";
                xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>" + "\r\n";
                xmlstc = xmlstc + "<AMOUNT>"+InvAmt+"</AMOUNT>" + "\r\n";
                xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                if (Discount != "0.00")
                {
                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERNAME>" + Connection.DiscountTxt + "</LEDGERNAME>" + "\r\n";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                    xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>" + "\r\n";
                    xmlstc = xmlstc + "<AMOUNT>-" + Discount + "</AMOUNT>" + "\r\n";
                    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                }
                if (Surcharge1 != "0.00")
                {
                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERNAME>" + Connection.Surchage1 + "</LEDGERNAME>" + "\r\n";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                    xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>" + "\r\n";
                    xmlstc = xmlstc + "<AMOUNT>" + Surcharge1 + "</AMOUNT>" + "\r\n";
                    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                }
                if (Surcharge2 != "0.00")
                {
                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERNAME>" + Connection.Surchage2 + "</LEDGERNAME>" + "\r\n";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                    xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>" + "\r\n";
                    xmlstc = xmlstc + "<AMOUNT>" + Surcharge2 + "</AMOUNT>" + "\r\n";
                    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                }
                if (Surcharge3 != "0.00")
                {
                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERNAME>" + Connection.Surchage3 + "</LEDGERNAME>" + "\r\n";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                    xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>" + "\r\n";
                    xmlstc = xmlstc + "<AMOUNT>" + Surcharge3 + "</AMOUNT>" + "\r\n";
                    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                }
                xmlstc = xmlstc + "</VOUCHER>" + "\r\n";
                xmlstc = xmlstc + "</TALLYMESSAGE>" + "\r\n";
                xmlstc = xmlstc + "</REQUESTDATA>" + "\r\n";
                xmlstc = xmlstc + "</IMPORTDATA>" + "\r\n";
                xmlstc = xmlstc + "</BODY>" + "\r\n";
                xmlstc = xmlstc + "</ENVELOPE>" + "\r\n";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:" + Connection.TallyPort);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = xmlstc.Length;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                streamWriter.Write(xmlstc);
                streamWriter.Close();
                AddcountInv++;

                //string result;
                //HttpWebResponse objResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                //{
                //    result = sr.ReadToEnd();
                //    sr.Close();
                //}
                //MessageBox.Show(result);
               

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, ex.StackTrace);
            }
        }

        private void CreditNote(string custName, string InvDate, string InvNo, string InvAmt, string SalesOrderNo)
        {
            try
            {
                 xmlstc = "<ENVELOPE>" + "\r\n";
                 xmlstc = xmlstc + "<HEADER>" + "\r\n";
                 xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                 xmlstc = xmlstc + "</HEADER>" + "\r\n";
                 xmlstc = xmlstc + "<BODY>" + "\r\n";
                 xmlstc = xmlstc + "<IMPORTDATA>" + "\r\n";
                 xmlstc = xmlstc + "<REQUESTDESC>" + "\r\n";
                 xmlstc = xmlstc + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                 xmlstc = xmlstc + "<STATICVARIABLES>" + "\r\n";
                 xmlstc = xmlstc + "<SVCURRENTCOMPANY>##SVCURRENTCOMPANY</SVCURRENTCOMPANY>" + "\r\n";
                 xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
                 xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
                 xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
                 xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                 xmlstc = xmlstc + "<VOUCHER VCHTYPE=" + "\"" + "Sales" + "\" ACTION=" + "\"" + "Create" + "\" >" + "\r\n";
                 xmlstc = xmlstc + "<DATE>" + InvDate + "</DATE>" + "\r\n";
                 xmlstc = xmlstc + "<VOUCHERTYPENAME>Credit Note</VOUCHERTYPENAME>" + "\r\n";
                 xmlstc = xmlstc + "<VOUCHERNUMBER>" + InvNo + "</VOUCHERNUMBER>" + "\r\n";
                 xmlstc = xmlstc + "<REFERENCE>Ref</REFERENCE>" + "\r\n";
                 xmlstc = xmlstc + "<PARTYLEDGERNAME>" + custName + "</PARTYLEDGERNAME>" + "\r\n";
                 xmlstc = xmlstc + "<EFFECTIVEDATE>" + InvDate + "</EFFECTIVEDATE>" + "\r\n";
                 xmlstc = xmlstc + "<ISINVOICE>Yes</ISINVOICE>" + "\r\n";
                 xmlstc = xmlstc + "<INVOICEORDERLIST.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<BASICORDERDATE>" + InvDate + "</BASICORDERDATE>" + "\r\n";
                 xmlstc = xmlstc + "<BASICPURCHASEORDERNO>" + SalesOrderNo + "</BASICPURCHASEORDERNO>" + "\r\n";
                 xmlstc = xmlstc + "</INVOICEORDERLIST.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<LEDGERNAME>" + custName + "</LEDGERNAME>" + "\r\n";
                 xmlstc = xmlstc + "<GSTCLASS/>" + "\r\n";
                 xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + "\r\n";
                 xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                 xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                 xmlstc = xmlstc + "<ISPARTYLEDGER>Yes</ISPARTYLEDGER>" + "\r\n";
                 xmlstc = xmlstc + "<AMOUNT>" + InvAmt + "</AMOUNT>" + "\r\n";
                 xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<NAME>" + InvNo + "</NAME>" + "\r\n";
                 xmlstc = xmlstc + "<BILLCREDITPERIOD>30 Days</BILLCREDITPERIOD>" + "\r\n";
                 xmlstc = xmlstc + "<BILLTYPE>New Ref</BILLTYPE>" + "\r\n";
                 xmlstc = xmlstc + "<AMOUNT>" + InvAmt + "</AMOUNT>" + "\r\n";
                 xmlstc = xmlstc + "</BILLALLOCATIONS.LIST>" + "\r\n";
                 xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<LEDGERNAME>Sales</LEDGERNAME>" + "\r\n";
                 xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + "\r\n";
                 xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                 xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                 xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>" + "\r\n";
                 xmlstc = xmlstc + "<AMOUNT>-" + InvAmt + "</AMOUNT>" + "\r\n";
                 xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                 xmlstc = xmlstc + "</VOUCHER>" + "\r\n";
                 xmlstc = xmlstc + "</TALLYMESSAGE>" + "\r\n";
                 xmlstc = xmlstc + "</REQUESTDATA>" + "\r\n";
                 xmlstc = xmlstc + "</IMPORTDATA>" + "\r\n";
                 xmlstc = xmlstc + "</BODY>" + "\r\n";
                 xmlstc = xmlstc + "</ENVELOPE>" + "\r\n";

                 HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:" + Connection.TallyPort);
                 httpWebRequest.Method = "POST";
                 httpWebRequest.ContentLength = xmlstc.Length;
                 httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                 streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                 streamWriter.Write(xmlstc);
                 streamWriter.Close();
                 AddcountCrdNt++;

                 //string result;
                 //HttpWebResponse objResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                 //using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                 //{
                 //    result = sr.ReadToEnd();
                 //    sr.Close();
                 //}
                 //MessageBox.Show(result);
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, ex.StackTrace);
            }
        }
 */

/*
 * https://www.c-sharpcorner.com/article/reading-and-writing-xml-in-C-Sharp/
 * reading writing xml file
 */

/*
 * Solution 2
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
public partial class CashManager_SampleTally : System.Web.UI.Page
{
protected void Page_Load(object sender, EventArgs e)
{

}
public void LedgerCreateXml(string ledgerName, string parentName, string openingBalance) // request xml and response for ledger creation
{
try
{
String xmlstc = "";
xmlstc = "<envelope>\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<tallyrequest>Import Data\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<importdata>\r\n";
xmlstc = xmlstc + "<requestdesc>\r\n";
xmlstc = xmlstc + "<reportname>All Masters\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<requestdata>\r\n";
xmlstc = xmlstc + "<tallymessage xmlns:udf=" + " \""="" +="" "tallyudf"="" "\"="">\r\n";

xmlstc = xmlstc + "<ledger name=" + " \""="" +="" ledgername="" "\"="" action=" + " "create"="">\r\n";
xmlstc = xmlstc + "<name>" + ledgerName + "\r\n";
xmlstc = xmlstc + "<parent>" + parentName + "\r\n";
xmlstc = xmlstc + "<openingbalance>" + openingBalance + "\r\n";
xmlstc = xmlstc + "<isbillwiseon>Yes\r\n";
xmlstc = xmlstc + "\r\n";

xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
String xml = xmlstc;
Values lResponse = SendReqst(xml);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}

catch (Exception ex)
{
lblResult.Text = ex.Message;
}
}
public void LedgeGetXml()
{
try
{
String xmlstc = "";
xmlstc = "<envelope>\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<tallyrequest>Export\r\n";
xmlstc = xmlstc + "<type>Collection\r\n";
xmlstc = xmlstc + "<id>All Ledgers\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<desc>\r\n";

xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
String xml = xmlstc;
Values lResponse = SendReqst(xml);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}

catch (Exception ex)
{
lblResult.Text = ex.Message;
}
}
public void LedgeGetXml2()
{
try
{
String xmlstc = "";
xmlstc = "<envelope>\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<tallyrequest>Export\r\n";
xmlstc = xmlstc + "<type>Collection\r\n";
xmlstc = xmlstc + "<id>FilteredLedgers\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<desc>\r\n";
xmlstc = xmlstc + "<tdl>\r\n";
xmlstc = xmlstc + "<tdlmessage>\r\n";
xmlstc = xmlstc + "<collection name="\"FilteredLedgers\"" ismodify="\"No\"">\r\n";
xmlstc = xmlstc + "<sourcecollection>Ledger\r\n";
xmlstc = xmlstc + "<fetch>Name\r\n";
xmlstc = xmlstc + "<filter>PartyExpenseFilter\r\n";
xmlstc = xmlstc + "\r\n";

xmlstc = xmlstc + "<system type="\"Formulae\"" name="\"PartyExpenseFilter\"" ismodify="\"No\"">\r\n";
xmlstc = xmlstc + "$$IsLedOfGrp:$Name:$$GroupSundryCreditors OR $$IsLedOfGrp:$Name:$$GroupIndirectExpenses";
xmlstc = xmlstc + "\r\n";

xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
String xml = xmlstc;
Values lResponse = SendReqst(xml);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}

catch (Exception ex)
{
lblResult.Text = ex.Message;
}
}
private Values CreatePaymentReceiptXML(Values v)
{
String xmlstc = "";
xmlstc = "<envelope>";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "<tallyrequest>Import Data";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "<importdata>";
xmlstc = xmlstc + "<requestdesc>";
xmlstc = xmlstc + "<reportname>Vouchers";
xmlstc = xmlstc + "<staticvariables>";
//xmlstc = xmlstc + "<svcurrentcompany>" + "##SVCURRENTCOMPANY" + "";
xmlstc = xmlstc + "<svcurrentcompany>" + v.Company + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";

xmlstc = xmlstc + "<requestdata>";

xmlstc = xmlstc + "<tallymessage>";
xmlstc = xmlstc + "<voucher vchtype=" + " \""="" +="" v.strvchtype="" "\"="" action=" + " "create"="">";
xmlstc = xmlstc + "<vouchernumber>" + v.strVchNumber + "";
xmlstc = xmlstc + "<date>" + v.strDate + "";
xmlstc = xmlstc + "<effectivedate>" + v.strDate + "";
xmlstc = xmlstc + "<narration>" + v.strNarration + "";
xmlstc = xmlstc + "<vouchertypename>" + v.strVchType + "";

//1st Entry in Voucher
xmlstc = xmlstc + "<allledgerentries.list>";
xmlstc = xmlstc + "<ledgername>" + v.strVoucherEntryName1 + "";
xmlstc = xmlstc + "<isdeemedpositive>" + v.strISDEEMEDPOSITIVE1 + "";
xmlstc = xmlstc + "<amount>" + v.strAmount1 + "";
xmlstc = xmlstc + "";

//2nd Entry in Voucher
xmlstc = xmlstc + "<allledgerentries.list>";
xmlstc = xmlstc + "<ledgername>" + v.strVoucherEntryName2 + "";
xmlstc = xmlstc + "<isdeemedpositive>" + v.strISDEEMEDPOSITIVE2 + "";
xmlstc = xmlstc + "<amount>" + v.strAmount2 + "";
xmlstc = xmlstc + "";

xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";

v.StrResponse = xmlstc;

return v;
}
private Values CreateDeleteXML(Values v)
{
String xmlstc = "";
xmlstc = "<envelope>";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "<tallyrequest>Import Data";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "<importdata>";
xmlstc = xmlstc + "<requestdesc>";
xmlstc = xmlstc + "<reportname>Vouchers";
xmlstc = xmlstc + "<staticvariables>";
//xmlstc = xmlstc + "<svcurrentcompany>" + "##SVCURRENTCOMPANY" + "";
xmlstc = xmlstc + "<svcurrentcompany>" + v.Company + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";

xmlstc = xmlstc + "<requestdata>";

xmlstc = xmlstc + "<tallymessage>"; //VCHTYPE=" + "\"" + v.strVchType + "\" it shud be included in cashmanager
xmlstc = xmlstc + "<voucher date=" + " \""="" +="" v.strdate="" "\"="" action=" + " "delete"="" tagname=" + " "master="" id"="" tagvalue=" + " v.intmasterid.tostring()="">";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";

v.StrResponse = xmlstc;

return v;
}
public Values SendReqst(string pWebRequstStr)
{
Values ro = new Values();

try
{
String lTallyLocalHost = "http://localhost:9000";
HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(lTallyLocalHost);
httpWebRequest.Method = "POST";
httpWebRequest.ContentLength = (long)pWebRequstStr.Length;
httpWebRequest.ContentType = "application/x-www-form-urlencoded";
StreamWriter lStrmWritr = new StreamWriter(httpWebRequest.GetRequestStream());
lStrmWritr.Write(pWebRequstStr);
lStrmWritr.Close();
HttpWebResponse lhttpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
Stream lreceiveStream = lhttpResponse.GetResponseStream();

StreamReader lStreamReader = new StreamReader(lreceiveStream, Encoding.UTF8);
string lResponseStr = lStreamReader.ReadToEnd();
XmlDocument doc= new XmlDocument();
doc.LoadXml(lResponseStr);
doc.Save("Server.xml");

DataSet ds = new DataSet();
ds.ReadXml("Server.xml");
ro.dsResponse = ds;
ro.StrResponse = lResponseStr;

lhttpResponse.Close();
lStreamReader.Close();

}
catch (Exception)
{
throw;
}

return ro;
}

protected void btnCreateVoucher_Click(object sender, EventArgs e)
{
string ledgerName = "Expense House";
string parentName = "Indirect Expenses";
string openingBalance = "100";

LedgerCreateXml(ledgerName, parentName, openingBalance);
}
protected void btnInsertPayment_Click(object sender, EventArgs e)
{
try
{
Values v = new Values();
v.Company = "TestVNR";
v.strVchNumber = "2";
v.strDate = "20200601";//DateTime.Now.ToString("");
v.strNarration = "My narration" + DateTime.Now.ToString();
v.strAmount1 = "-1200";
v.strAmount2 = "1200";
v.strVchType = "Payment";
v.strVoucherEntryName1 = "Expense House";
v.strVoucherEntryName2 = "Cash";
v.strISDEEMEDPOSITIVE1 = "Yes";
v.strISDEEMEDPOSITIVE2 = "No";

Values xml = CreatePaymentReceiptXML(v); ;

Values lResponse = SendReqst(xml.StrResponse);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}
catch (Exception ex)
{
lblResult.Text = ex.Message;
}
finally
{

}
}
protected void btnInsertReceipt_Click(object sender, EventArgs e)
{
try
{
Values v = new Values();
v.Company = "TestVNR";
v.strVchNumber = "10";
v.strDate = "20200601";//DateTime.Now.ToString(""); If educational mode date shud be 01 or 31
v.strNarration = "My receipt narration" + DateTime.Now.ToString();
v.strAmount1 = "1200";
v.strAmount2 = "-1200";
v.strVchType = "Receipt";
v.strVoucherEntryName1 = "Expense House";
v.strVoucherEntryName2 = "Cash";
v.strISDEEMEDPOSITIVE1 = "No";
v.strISDEEMEDPOSITIVE2 = "Yes";

Values xml = CreatePaymentReceiptXML(v); ;

Values lResponse = SendReqst(xml.StrResponse);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}
catch (Exception ex)
{
lblResult.Text = ex.Message;
}
finally
{

}
}
protected void btnDelete_Click(object sender, EventArgs e)
{
try
{
Values v = new Values();
v.Company = "TestVNR";
v.strDate = "20170611";//DateTime.Now.ToString("");
//v.strNarration = "My receipt narration";
//v.strAmount1 = "1200";
//v.strAmount2 = "-1200";
//v.strVchType = "Receipt";
//v.strVoucherEntryName1 = "Expense House";
//v.strVoucherEntryName2 = "Cash";
//v.strISDEEMEDPOSITIVE1 = "No";
//v.strISDEEMEDPOSITIVE2 = "Yes";
v.intMasterId = Convert.ToInt16(txtMasterId.Text);

Values xml = CreateDeleteXML(v); ;

Values lResponse = SendReqst(xml.StrResponse);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;

if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}

}
catch (Exception ex)
{
lblResult.Text = ex.Message;
}
finally
{

}
}
protected void btnLedgerList_Click(object sender, EventArgs e)
{
LedgeGetXml();
}
}

public class Values
{
public DataSet dsResponse;
public string StrResponse;
public string Company;
public string strVchType;
public string strVchNumber;
public string strDate;
public string strNarration;
public string strVoucherEntryName1;
public string strISDEEMEDPOSITIVE1;
public string strAmount1;
public string strVoucherEntryName2;
public string strISDEEMEDPOSITIVE2;
public string strAmount2;
public int intMasterId;

}
 */

/*
 * //Tally will automaticallly create System DSN and user DSN if you add TDL code file in Tally config file/ TDL Configuration in Tally
//"Dsn=TallyODBC_9000;uid=Administrator;pwd=admin"
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;

namespace TallyDataTest
{
    class TallyOdbc
    {
        private static OdbcConnection TallyCollectionConnection = new OdbcConnection("Dsn=TallyODBC_9000;uid=Administrator;pwd=admin");
        public static DataSet ConnectToTally()
        {
            TallyCollectionConnection.Open();
            OdbcCommand TallyCommand = new OdbcCommand("SELECT StockItem.$Name, StockItem.$Parent FROM TESTER.TallyUser.StockItem ORDER BY StockItem.$Name", TallyCollectionConnection);
            OdbcDataAdapter TallyDataAdapter = new OdbcDataAdapter(TallyCommand);
            DataSet TallyDataSet = new DataSet();
            //TallyDataAdapter.Fill(TallyDataSet);
            TallyDataAdapter.Fill(TallyDataSet);
            TallyCollectionConnection.Close();
            return TallyDataSet;            
        }

    }
}
//Similarly data set returned can be used as shown above...

 */

/*
 * class abc{

DataSet ds=new DataSet();

ds=TallyXml.GetTallyData()

}

class TallyXml
    {
        private static string RequestXML;
        private static WebRequest TallyRequest;
        //private static ServerXMLHTTP30 RequestClient=new ServerXMLHTTP30();
        public static DataSet GetTallyData()
        { 
          RequestXML = "<ENVELOPE><HEADER><TALLYREQUEST>Export Data</TALLYREQUEST></HEADER><BODY><EXPORTDATA><REQUESTDESC><REPORTNAME>List of Accounts</REPORTNAME><STATICVARIABLES><SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT><ACCOUNTTYPE>Ledgers</ACCOUNTTYPE></STATICVARIABLES></REQUESTDESC></EXPORTDATA></BODY></ENVELOPE>";

            TallyRequest = WebRequest.Create("http://localhost:9000");
            ((HttpWebRequest)TallyRequest).UserAgent = ".NET Framework Example Client";
            // Set the Method property of the request to POST.
            TallyRequest.Method = "POST";
            // Create POST data and convert it to a byte array.
            string postData = RequestXML;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            TallyRequest.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            TallyRequest.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = TallyRequest.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = TallyRequest.GetResponse();
            // Display the status.
            string Response = (((HttpWebResponse)response).StatusDescription).ToString();
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromTallyServer = reader.ReadToEnd().ToString(); 
            DataSet TallyResponseDataSet = new DataSet();
            TallyResponseDataSet.ReadXml(new StringReader(responseFromTallyServer));
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            byteArray = null;
            response = null;
            responseFromTallyServer = null;
            Response = null;
            dataStream = null;
            // RequestClient.open("Get", "http://localhost:9000/", false, null, null);
            // RequestClient.send(
            // IXMLDOMNode ResponseXml = (IXMLDOMNode)RequestClient.responseXML;






            return TallyResponseDataSet;
        }

    }

         
 */
/*
 * non working code 
 * below option are to upate over this code to makeitwork
 * 
 * public partial class WebForm1 : System.Web.UI.Page
   {
       private static string RequestXML;
       private static WebRequest TallyRequest;
       protected void Page_Load(object sender, EventArgs e)
       {

       }
       public static DataSet ConnectToTally()
       {
           //try
           //{
               RequestXML = "<ENVELOPE><HEADER><TALLYREQUEST>Export Data</TALLYREQUEST></HEADER><BODY><EXPORTDATA><REQUESTDESC><REPORTNAME>List of Products</REPORTNAME><STATICVARIABLES><SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT><PRODUCTTYPE>All Inventory Masters</PRODUCTTTYPE></STATICVARIABLES></REQUESTDESC></EXPORTDATA></BODY></ENVELOPE>";
               TallyRequest = WebRequest.Create("http://localhost:9000");
               ((HttpWebRequest)TallyRequest).UserAgent = ".NET Framework Example Client";
               TallyRequest.Method = "Post";
               string postData = RequestXML;
               byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
               TallyRequest.ContentType = "application/x-www-form-urlencoded";
               TallyRequest.ContentLength = byteArray.Length;
               Stream dataStream = TallyRequest.GetRequestStream();
               dataStream.Write(byteArray, 0, byteArray.Length);
               dataStream.Close();
               WebResponse response = TallyRequest.GetResponse();
               string Response = (((HttpWebResponse)response).StatusDescription).ToString();
               dataStream = response.GetResponseStream();
               StreamReader reader = new StreamReader(dataStream);
               string responseFromTallyServer = reader.ReadToEnd().ToString();
               DataSet TallyResponseDataSet = new DataSet();
               TallyResponseDataSet.ReadXml(new StringReader(responseFromTallyServer));
               reader.Close();
               dataStream.Close();
               response.Close();
               byteArray = null;
               response = null;
               responseFromTallyServer = null;
               Response = null;
               dataStream = null;
               return TallyResponseDataSet;

       }

       protected void btn_Click(object sender, EventArgs e)
       {
           try
           {
               ConnectToTally();
               string xmlstc = string.Empty;
               xmlstc = xmlstc + "<MultipleStockItems=" + "\"" + "All items" + "\" ACTION=" + "\"" + "Create" + "\">";
               xmlstc = "<ENVELOPE>";
               xmlstc = xmlstc + "<HEADER>";
               xmlstc = xmlstc + "<TALLYREQUEST>Export Data</TALLYREQUEST>";
               xmlstc = xmlstc + "</HEADER>";
               xmlstc = xmlstc + "<BODY>";
               xmlstc = xmlstc + "<IMPORTDATA>";
               xmlstc = xmlstc + "<REQUESTDESC>";
               xmlstc = xmlstc + "<REPORTNAME>ListOfProducts</REPORTNAME>";
               xmlstc = xmlstc + "<STATICVARIABLES>";
               xmlstc = xmlstc + "<SVCURRENTCOMPANY>Sphinx</SVCURRENTCOMPANY>";
               xmlstc = xmlstc + "</STATICVARIABLES>";
               xmlstc = xmlstc + "</REQUESTDESC>";

               xmlstc = xmlstc + "<REQUESTDATA>";


             string  item= itm.Text;
             string group = grp.Text;
             string  unit = unt.Text;
            string   quantity = qnt.Text;
            string Rate = rate.Text;
            string msi = "";
               xmlstc = xmlstc + "<TALLYMESSAGE >";
               xmlstc = xmlstc + "<MultipleStockItems=" + "\"" + "All items" + "\" ACTION=" + "\"" + "Create" + "\">";
               xmlstc = xmlstc + "<NameOfItem>" + item + "</NameOfItem>";
               xmlstc = xmlstc + "<under>" + group + "</under>";
               xmlstc = xmlstc + "<units>" + unit + "</units>";
               xmlstc = xmlstc + "<OpeningQuantity>" + quantity + "</OpeningQuantity>";
               xmlstc = xmlstc + "<Rate>" + rate + "</Rate>";
               xmlstc = xmlstc + "<MultipleStockItems>" + msi + "</MultipleStockItems>";


               xmlstc = xmlstc + "</MultipleStockItems>";
               xmlstc = xmlstc + "</TALLYMESSAGE>";
               xmlstc = xmlstc + "</REQUESTDATA>";
               xmlstc = xmlstc + "</IMPORTDATA>";
               xmlstc = xmlstc + "</BODY>";
               xmlstc = xmlstc + "</ENVELOPE>";



               string result = "";


               HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9000");
               httpWebRequest.Method = "POST";
               httpWebRequest.ContentLength = xmlstc.Length;
               httpWebRequest.ContentType = "application/x-www-form-urlencoded";
               StreamWriter streamWriter;
               streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
               streamWriter.Write(xmlstc);
               Response.Write("Data inserted into Tally sucessfully");
               streamWriter.Close();

 */

/*
 * Sol of abve
 * 
 * <MultipleStockItems="All items" ACTION="Create"> invalid
 * <MultipleStockItems SomeAttribute="All items" ACTION="Create"> valid
 * 
 * 
 * You've also got a second MultipleStockItems element nested under the outer MultipleStockItems element, with an empty string as its content. That doesn't look right to me - you'll need to check what the XML is supposed to look like.

You'll probably need to use the correct content type on your web request - an XML string is not "application/x-www-form-urlencoded". You'll need to check the code that receives the data to see what content type it's expecting.

And you'll need to call httpWebRequest.GetResponse() to actually send the request to the server.
 * XDocument xml = new XDocument(
    new XElement("ENVELOPE",
        new XElement("HEADER",
            new XElement("TALLYREQUEST", "Export Data")
        ),
        new XElement("BODY",
            new XElement("IMPORTDATA",
                new XElement("REQUESTDESC",
                    new XElement("REPORTNAME", "ListOfProducts"),
                    new XElement("STATICVARIABLES",
                        new XElement("SVCURRENTCOMPANY", "Sphinx")
                    ) // STATICVARIABLES
                ), // REQUESTDESC
                new XElement("REQUESTDATA",
                    new XElement("TALLYMESSAGE",
                        new XElement("MultipleStockItems",
                            new XAttribute("SomeAttribute", "All items"),
                            new XAttribute("ACTION", "Create"),
                            new XElement("NameOfItem", itm.Text),
                            new XElement("under", grp.Text),
                            new XElement("units", unt.Text),
                            new XElement("OpeningQuantity", qnt.Text),
                            new XElement("Rate", rate.Text),
                            
                            // This one doesn't look right:
                            new XElement("MultipleStockItems", string.Empty) 
                            
                        ) // MultipleStockItems
                    )// TALLYMESSAGE
                ) // REQUESTDATA
            ) // IMPORTDATA
        ) // BODY
    ) // ENVELOPE
);


HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9000");
httpWebRequest.Method = "POST";

// TODO: Use the correct content type:
httpWebRequest.ContentType = "application/x-www-form-urlencoded";

using (Stream requestStream = httpWebRequest.GetRequestStream())
{
    xml.Save(requestStream, SaveOptions.DisableFormatting);
}

// Need to actually send the request:
using (WebResponse response = httpWebRequest.GetResponse())
{
}

Response.Write("Data inserted into Tally sucessfully");

 */

/*
 * void CreateGroup()
{
string strXMLfile = "<ENVELOPE>< HEADER><TALLYREQUEST>Import Data</TALLYREQUEST></HEADER> ";
strXMLfile += " < BODY >< IMPORTDATA >";
strXMLfile += "< REQUESTDESC >< REPORTNAME > All Masters </ REPORTNAME ></ REQUESTDESC >";
strXMLfile += " < DESC >";
strXMLfile += "< STATICVARIABLES >";
strXMLfile += "< SVCURRENTCOMPANY > ##SVCURRENTCOMPANY </ SVCURRENTCOMPANY >";
strXMLfile += "< /STATICVARIABLES >";
strXMLfile += " < /DESC >";
//strXMLfile += "< SVCURRENTCOMPANY >" + "\r\n";
//strXMLfile += "##SVCURRENTCOMPANY" + "\r\n";
//strXMLfile += "</ SVCURRENTCOMPANY>" + "\r\n";
strXMLfile += "< REQUESTDATA >< TALLYMESSAGE xmlns:UDF = 'TallyUDF' >";
strXMLfile += "< GROUP NAME = 'My DebtorsT' ACTION = 'Create' >";
strXMLfile += "< NAME.LIST >< NAME > My DebtorsT </ NAME ></ NAME.LIST >";
strXMLfile += "< PARENT > Sundry Debtors </ PARENT >";
strXMLfile += "< ISSUBLEDGER > No </ ISSUBLEDGER >";
strXMLfile += "< ISBILLWISEON > No </ ISBILLWISEON >";
strXMLfile += "< ISCOSTCENTRESON > No </ ISCOSTCENTRESON >";
strXMLfile += "</ GROUP >";
strXMLfile += "</ TALLYMESSAGE ></ REQUESTDATA ></ IMPORTDATA ></ BODY ></ ENVELOPE > ";
HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9000/");
httpWebRequest.Method = "POST";
httpWebRequest.ContentLength = strXMLfile.Length;
httpWebRequest.ContentType = "application/x-www-form-urlencoded";
StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
streamWriter.Write(strXMLfile);
streamWriter.Close();
}
private static void Invoice(string custName, string InvDate, string InvNo, string InvAmt, double NetAmount)
{
try
{
string xmlstc = "<ENVELOPE>" + "\r\n";
xmlstc = xmlstc + "<HEADER>" + "\r\n";
xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
xmlstc = xmlstc + "</HEADER>" + "\r\n";
xmlstc = xmlstc + "<BODY>" + "\r\n";
xmlstc = xmlstc + "<IMPORTDATA>" + "\r\n";
xmlstc = xmlstc + "<REQUESTDESC>" + "\r\n";
xmlstc = xmlstc + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
xmlstc = xmlstc + "<STATICVARIABLES>" + "\r\n";
xmlstc = xmlstc + "<SVCURRENTCOMPANY>##SVCURRENTCOMPANY</SVCURRENTCOMPANY>" + "\r\n";
xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
xmlstc = xmlstc + "<VOUCHER VCHTYPE=" + "\"" + "Sales" + "\" ACTION=" + "\"" + "Create" + "\" >" + "\r\n";
xmlstc = xmlstc + "<DATE>" + InvDate + "</DATE>" + "\r\n";
xmlstc = xmlstc + "<VOUCHERTYPENAME>Sales</VOUCHERTYPENAME>" + "\r\n";
xmlstc = xmlstc + "<VOUCHERNUMBER>" + InvNo + "</VOUCHERNUMBER>" + "\r\n";
xmlstc = xmlstc + "<REFERENCE>Ref</REFERENCE>" + "\r\n";
xmlstc = xmlstc + "<PARTYLEDGERNAME>" + custName + "</PARTYLEDGERNAME>" + "\r\n";
xmlstc = xmlstc + "<EFFECTIVEDATE>" + InvDate + "</EFFECTIVEDATE>" + "\r\n";
xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
xmlstc = xmlstc + "<LEDGERNAME>" + custName + "</LEDGERNAME>" + "\r\n";
xmlstc = xmlstc + "<AMOUNT>-" + NetAmount + "</AMOUNT>" + "\r\n";
xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
xmlstc = xmlstc + "<LEDGERNAME>Sales</LEDGERNAME>" + "\r\n";
xmlstc = xmlstc + "<AMOUNT>" + InvAmt + "</AMOUNT>" + "\r\n";
xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
xmlstc = xmlstc + "</VOUCHER>" + "\r\n";
xmlstc = xmlstc + "</TALLYMESSAGE>" + "\r\n";
xmlstc = xmlstc + "</REQUESTDATA>" + "\r\n";
xmlstc = xmlstc + "</IMPORTDATA>" + "\r\n";
xmlstc = xmlstc + "</BODY>" + "\r\n";
xmlstc = xmlstc + "</ENVELOPE>" + "\r\n";
HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9000/");
httpWebRequest.Method = "POST";
httpWebRequest.ContentLength = xmlstc.Length;
httpWebRequest.ContentType = "application/x-www-form-urlencoded";
StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
streamWriter.Write(xmlstc);
streamWriter.Close();
//string result;
//HttpWebResponse objResponse = (HttpWebResponse)httpWebRequest.GetResponse();
//using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
//{
// result = sr.ReadToEnd();
// sr.Close();
//}
//MessageBox.Show(result);
}
catch (Exception ex)
{
//MessageBox.Show(ex.Message, ex.StackTrace);
}
}

 */

/*
 * using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
public partial class CashManager_SampleTally : System.Web.UI.Page
{
protected void Page_Load(object sender, EventArgs e)
{

}
public void LedgerCreateXml(string ledgerName, string parentName, string openingBalance) // request xml and response for ledger creation
{
try
{
String xmlstc = "";
xmlstc = "<envelope>\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<tallyrequest>Import Data\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<importdata>\r\n";
xmlstc = xmlstc + "<requestdesc>\r\n";
xmlstc = xmlstc + "<reportname>All Masters\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<requestdata>\r\n";
xmlstc = xmlstc + "<tallymessage xmlns:udf=" + " \""="" +="" "tallyudf"="" "\"="">\r\n";

xmlstc = xmlstc + "<ledger name=" + " \""="" +="" ledgername="" "\"="" action=" + " "create"="">\r\n";
xmlstc = xmlstc + "<name>" + ledgerName + "\r\n";
xmlstc = xmlstc + "<parent>" + parentName + "\r\n";
xmlstc = xmlstc + "<openingbalance>" + openingBalance + "\r\n";
xmlstc = xmlstc + "<isbillwiseon>Yes\r\n";
xmlstc = xmlstc + "\r\n";

xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
String xml = xmlstc;
Values lResponse = SendReqst(xml);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}

catch (Exception ex)
{
lblResult.Text = ex.Message;
}
}
public void LedgeGetXml()
{
try
{
String xmlstc = "";
xmlstc = "<envelope>\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<tallyrequest>Export\r\n";
xmlstc = xmlstc + "<type>Collection\r\n";
xmlstc = xmlstc + "<id>All Ledgers\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<desc>\r\n";

xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
String xml = xmlstc;
Values lResponse = SendReqst(xml);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}

catch (Exception ex)
{
lblResult.Text = ex.Message;
}
}
public void LedgeGetXml2()
{
try
{
String xmlstc = "";
xmlstc = "<envelope>\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<tallyrequest>Export\r\n";
xmlstc = xmlstc + "<type>Collection\r\n";
xmlstc = xmlstc + "<id>FilteredLedgers\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "<desc>\r\n";
xmlstc = xmlstc + "<tdl>\r\n";
xmlstc = xmlstc + "<tdlmessage>\r\n";
xmlstc = xmlstc + "<collection name="\"FilteredLedgers\"" ismodify="\"No\"">\r\n";
xmlstc = xmlstc + "<sourcecollection>Ledger\r\n";
xmlstc = xmlstc + "<fetch>Name\r\n";
xmlstc = xmlstc + "<filter>PartyExpenseFilter\r\n";
xmlstc = xmlstc + "\r\n";

xmlstc = xmlstc + "<system type="\"Formulae\"" name="\"PartyExpenseFilter\"" ismodify="\"No\"">\r\n";
xmlstc = xmlstc + "$$IsLedOfGrp:$Name:$$GroupSundryCreditors OR $$IsLedOfGrp:$Name:$$GroupIndirectExpenses";
xmlstc = xmlstc + "\r\n";

xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "\r\n";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
String xml = xmlstc;
Values lResponse = SendReqst(xml);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}

catch (Exception ex)
{
lblResult.Text = ex.Message;
}
}
private Values CreatePaymentReceiptXML(Values v)
{
String xmlstc = "";
xmlstc = "<envelope>";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "<tallyrequest>Import Data";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "<importdata>";
xmlstc = xmlstc + "<requestdesc>";
xmlstc = xmlstc + "<reportname>Vouchers";
xmlstc = xmlstc + "<staticvariables>";
//xmlstc = xmlstc + "<svcurrentcompany>" + "##SVCURRENTCOMPANY" + "";
xmlstc = xmlstc + "<svcurrentcompany>" + v.Company + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";

xmlstc = xmlstc + "<requestdata>";

xmlstc = xmlstc + "<tallymessage>";
xmlstc = xmlstc + "<voucher vchtype=" + " \""="" +="" v.strvchtype="" "\"="" action=" + " "create"="">";
xmlstc = xmlstc + "<vouchernumber>" + v.strVchNumber + "";
xmlstc = xmlstc + "<date>" + v.strDate + "";
xmlstc = xmlstc + "<effectivedate>" + v.strDate + "";
xmlstc = xmlstc + "<narration>" + v.strNarration + "";
xmlstc = xmlstc + "<vouchertypename>" + v.strVchType + "";

//1st Entry in Voucher
xmlstc = xmlstc + "<allledgerentries.list>";
xmlstc = xmlstc + "<ledgername>" + v.strVoucherEntryName1 + "";
xmlstc = xmlstc + "<isdeemedpositive>" + v.strISDEEMEDPOSITIVE1 + "";
xmlstc = xmlstc + "<amount>" + v.strAmount1 + "";
xmlstc = xmlstc + "";

//2nd Entry in Voucher
xmlstc = xmlstc + "<allledgerentries.list>";
xmlstc = xmlstc + "<ledgername>" + v.strVoucherEntryName2 + "";
xmlstc = xmlstc + "<isdeemedpositive>" + v.strISDEEMEDPOSITIVE2 + "";
xmlstc = xmlstc + "<amount>" + v.strAmount2 + "";
xmlstc = xmlstc + "";

xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";

v.StrResponse = xmlstc;

return v;
}
private Values CreateDeleteXML(Values v)
{
String xmlstc = "";
xmlstc = "<envelope>";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "<tallyrequest>Import Data";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "<importdata>";
xmlstc = xmlstc + "<requestdesc>";
xmlstc = xmlstc + "<reportname>Vouchers";
xmlstc = xmlstc + "<staticvariables>";
//xmlstc = xmlstc + "<svcurrentcompany>" + "##SVCURRENTCOMPANY" + "";
xmlstc = xmlstc + "<svcurrentcompany>" + v.Company + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";

xmlstc = xmlstc + "<requestdata>";

xmlstc = xmlstc + "<tallymessage>"; //VCHTYPE=" + "\"" + v.strVchType + "\" it shud be included in cashmanager
xmlstc = xmlstc + "<voucher date=" + " \""="" +="" v.strdate="" "\"="" action=" + " "delete"="" tagname=" + " "master="" id"="" tagvalue=" + " v.intmasterid.tostring()="">";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";
xmlstc = xmlstc + "";

v.StrResponse = xmlstc;

return v;
}
public Values SendReqst(string pWebRequstStr)
{
Values ro = new Values();

try
{
String lTallyLocalHost = "http://localhost:9000";
HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(lTallyLocalHost);
httpWebRequest.Method = "POST";
httpWebRequest.ContentLength = (long)pWebRequstStr.Length;
httpWebRequest.ContentType = "application/x-www-form-urlencoded";
StreamWriter lStrmWritr = new StreamWriter(httpWebRequest.GetRequestStream());
lStrmWritr.Write(pWebRequstStr);
lStrmWritr.Close();
HttpWebResponse lhttpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
Stream lreceiveStream = lhttpResponse.GetResponseStream();

StreamReader lStreamReader = new StreamReader(lreceiveStream, Encoding.UTF8);
string lResponseStr = lStreamReader.ReadToEnd();
XmlDocument doc= new XmlDocument();
doc.LoadXml(lResponseStr);
doc.Save("Server.xml");

DataSet ds = new DataSet();
ds.ReadXml("Server.xml");
ro.dsResponse = ds;
ro.StrResponse = lResponseStr;

lhttpResponse.Close();
lStreamReader.Close();

}
catch (Exception)
{
throw;
}

return ro;
}

protected void btnCreateVoucher_Click(object sender, EventArgs e)
{
string ledgerName = "Expense House";
string parentName = "Indirect Expenses";
string openingBalance = "100";

LedgerCreateXml(ledgerName, parentName, openingBalance);
}
protected void btnInsertPayment_Click(object sender, EventArgs e)
{
try
{
Values v = new Values();
v.Company = "TestVNR";
v.strVchNumber = "2";
v.strDate = "20200601";//DateTime.Now.ToString("");
v.strNarration = "My narration" + DateTime.Now.ToString();
v.strAmount1 = "-1200";
v.strAmount2 = "1200";
v.strVchType = "Payment";
v.strVoucherEntryName1 = "Expense House";
v.strVoucherEntryName2 = "Cash";
v.strISDEEMEDPOSITIVE1 = "Yes";
v.strISDEEMEDPOSITIVE2 = "No";

Values xml = CreatePaymentReceiptXML(v); ;

Values lResponse = SendReqst(xml.StrResponse);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}
catch (Exception ex)
{
lblResult.Text = ex.Message;
}
finally
{

}
}
protected void btnInsertReceipt_Click(object sender, EventArgs e)
{
try
{
Values v = new Values();
v.Company = "TestVNR";
v.strVchNumber = "10";
v.strDate = "20200601";//DateTime.Now.ToString(""); If educational mode date shud be 01 or 31
v.strNarration = "My receipt narration" + DateTime.Now.ToString();
v.strAmount1 = "1200";
v.strAmount2 = "-1200";
v.strVchType = "Receipt";
v.strVoucherEntryName1 = "Expense House";
v.strVoucherEntryName2 = "Cash";
v.strISDEEMEDPOSITIVE1 = "No";
v.strISDEEMEDPOSITIVE2 = "Yes";

Values xml = CreatePaymentReceiptXML(v); ;

Values lResponse = SendReqst(xml.StrResponse);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;
if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}
}
catch (Exception ex)
{
lblResult.Text = ex.Message;
}
finally
{

}
}
protected void btnDelete_Click(object sender, EventArgs e)
{
try
{
Values v = new Values();
v.Company = "TestVNR";
v.strDate = "20170611";//DateTime.Now.ToString("");
//v.strNarration = "My receipt narration";
//v.strAmount1 = "1200";
//v.strAmount2 = "-1200";
//v.strVchType = "Receipt";
//v.strVoucherEntryName1 = "Expense House";
//v.strVoucherEntryName2 = "Cash";
//v.strISDEEMEDPOSITIVE1 = "No";
//v.strISDEEMEDPOSITIVE2 = "Yes";
v.intMasterId = Convert.ToInt16(txtMasterId.Text);

Values xml = CreateDeleteXML(v); ;

Values lResponse = SendReqst(xml.StrResponse);
string strRes = Server.HtmlEncode(lResponse.StrResponse);
lblResult.Text = strRes;

gv1.DataSource = lResponse.dsResponse;
gv1.DataBind();

DataSet ds = lResponse.dsResponse;

if (!ds.Tables[0].Columns.Contains("LINEERROR"))
{
lblResult.Text = ds.Tables[0].Rows[0]["LASTVCHID"].ToString();
}
else
{
lblResult.Text = ds.Tables[0].Rows[0]["LINEERROR"].ToString(); //LINEERROR
}

}
catch (Exception ex)
{
lblResult.Text = ex.Message;
}
finally
{

}
}
protected void btnLedgerList_Click(object sender, EventArgs e)
{
LedgeGetXml();
}
}

public class Values
{
public DataSet dsResponse;
public string StrResponse;
public string Company;
public string strVchType;
public string strVchNumber;
public string strDate;
public string strNarration;
public string strVoucherEntryName1;
public string strISDEEMEDPOSITIVE1;
public string strAmount1;
public string strVoucherEntryName2;
public string strISDEEMEDPOSITIVE2;
public string strAmount2;
public int intMasterId;

}
 */


/*
 * I think excel to tally using c#
 * 
 * private void button1_Click(object sender, EventArgs e)
    {
        string excelfileName = tbXmlView.Text;
        DataSet TallyCollectionDataSet = TallyTest.ConnectToTally(excelfileName);
        MessageBox.Show("finished" + excelfileName);
    }

public static DataSet ConnectToTally( string request)
{
        RequestXML = request;
        TallyRequest = WebRequest.Create("http://localhost:9000/");
        ((HttpWebRequest)TallyRequest).UserAgent = ".NET Framework Example Client";
        // Set the Method property of the request to POST.
        TallyRequest.Method = "POST";
        // Create POST data and convert it to a byte array.
        string postData = RequestXML;
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        // Set the ContentType property of the WebRequest.
        TallyRequest.ContentType = "application/x-www-form-urlencoded";
        // Set the ContentLength property of the WebRequest.
        TallyRequest.ContentLength = byteArray.Length;
        // Get the request stream.
        Stream dataStream = TallyRequest.GetRequestStream();
        // Write the data to the request stream.
        dataStream.Write(byteArray, 0, byteArray.Length);
        // Close the Stream object.
        dataStream.Close();
        // Get the response.
        WebResponse response = TallyRequest.GetResponse();
        // Display the status.
        string Response = (((HttpWebResponse)response).StatusDescription).ToString();
        // Get the stream containing content returned by the server.
        dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.
        StreamReader reader = new StreamReader(dataStream);

        // Read the content.
        string responseFromTallyServer = reader.ReadToEnd().ToString();

        // Display the content.
        string ResponseFromtally=responseFromTallyServer.ToString();

        DataSet TallyResponseDataSet = new DataSet();
        TallyResponseDataSet.ReadXml(new StringReader(responseFromTallyServer));

        // Clean up the streams.
        reader.Close();
        dataStream.Close();
        response.Close();
        byteArray = null;
        response = null;
        responseFromTallyServer = null;
        Response = null;
        dataStream = null;
        RequestClient.open("Get", "http://localhost:9000/", false, null, null);

        IXMLDOMNode ResponseXml = (IXMLDOMNode)RequestClient.responseXML;
        RequestClient.send("helloworld");
        Console.WriteLine(RequestClient.responseText);
        return TallyResponseDataSet;
    }
 */
/*
 * Synchronisation is the process of replicating data between two or more computers using Tally.ERP 9 in a Client – Server environment. Data is transferred from the client to the server and vice versa. Tally.ERP 9 offers control over the frequency of Synchronisation i.e., data can be replicated after each transaction or updated at specific intervals.

Configure Server for Synchronization

To configure the Server Company for Synchronization, follow the steps given below:

Go to Gateway of Tally > F12: Configure > Advanced Configuration

In the Advanced Configuration screen,

Under Client/Server Configuration section, Set the option Tally is acting as to Server, to run Tally.ERP 9 as Sync Server Specify the required port number (e.g. 9009) in the Port field....

Tab down to Tally Sync Configuration section:

Set the option Ignore Clients modified Voucher Type Masters to No, so that modifications made to Voucher Type Masters on the Client are updated on the Server

Set the option Enable Sync Logging to Yes, to generate a Log file which contains synchronized vouchers and is saved in the Tally folder.

Set the option Truncate previous log before Syncing should be set to Yes, to allow the previous log file to be overwritten by the current log file.

Go to end and press Enter to save the details.

For the above changes to take effect, Tally.ERP 9 will prompt you to restart the application as shown:

Press Enter or select Yes to restart Tally.ERP 9 on the Server computer. Tally.ERP 9 will restart and in the Information Panel you can see that it is configured as the Sync Server as shown below.

Detailed reference here.

If the datas are present and you cannot access the sync then there must be restriction in your version
To configure the Server Company for Synchronization, follow the steps given below:
Go to Gateway of Tally > F12: Configure > Advanced Configuration
In the Advanced Configuration screen,
Under Client/Server Configuration section,

Set the option Tally is acting as to Server, to run Tally.ERP 9 as Sync Server
Specify the required port number (e.g. 9009) in the Port field.
Tab down to Tally Sync Configuration section:

Set the option Ignore Clients modified Voucher Type Masters to No, so that modifications made to Voucher Type Masters on the Client are updated on the Server
Set the option Enable Sync Logging to Yes, to generate a Log file which contains synchronized vouchers and is saved in the Tally folder.
Set the option Truncate previous log before Syncing should be set to Yes, to allow the previous log file to be overwritten by the current log file.
Go to end and press Enter to save the details.
For the above changes to take effect, Tally.ERP 9 will prompt you to restart the application as shown:
Press Enter or select Yes to restart Tally.ERP 9 on the Server computer.
Tally.ERP 9 will restart and in the Information Panel you can see that it is configured as the Sync Server as shown below.
2nd Step 
Configure Client for Synchronization

To configure the Client Company for Synchronization, follow the steps given below:

Go to Gateway of Tally > F12: Configure > Advanced Configuration
In the Advanced Configuration screen,

Under Client/Server Configuration section,

Set the option Tally is acting as to Client, to run Tally.ERP 9 as Sync Client
Tab down to Tally Sync Configuration section,
Set the option Ignore Servers modified Voucher Type Masters to No, so that modifications made to Voucher Type Masters on the Server are updated on the Client
Go to end and press Enter to save the details
For the above changes to take effect, Tally.ERP 9 will prompt you to restart the application.
Press Enter or select Yes to restart Tally.ERP 9 on the Client computer
The Tally.ERP 9 is Configured as the Sync Client as shown in the Information panel
3rd Step 
Create Sync Rule on Client

To Create the Sync Rule on the Sync Client for Direct/IP Sync, follow the steps given below:

Select the Company for which the data needs to be synchronized
Go to Gateway of Tally > Import of Data > Synchronization > Client Rules > Create
In the Client Rule Creation screen,
Enter required Sync rule name (e.g. Sync with HO) in the Name of Rule field
Set Use Tally.NET Server to No (By default it will be set to No)
Enter the Static IP Address of the server in the Server URL field along with the Port Number (e.g. 192.168.5.137:9009).
Set Secure Server to Yes or No depending on whether the Server Computer is listening in the Secure mode or not.
Username and Password should be provided when Set Secure Server option is enabled.
Use Compression should be set to Yes if you want to compress the data during the Sync process. This will help to speed up the Sync process.
Select Synchronize in the Type of Rule field.
Enter the name of the Server Company in the Company Name on Server field.
Select Yes for Synchronize Altered Transactions.
Select Yes for Sync over slow connection.
Press Enter to save the Client Rule Creation screen.
Details of other fields

Secure Server

Enter Yes in this field if you are synchronizing to a secure server. Entering Yes here gives you the option to enter your user name and password.
 

User Name - Enter the user name you use to connect to the server.
Password - Enter the password for the specified user name.
Use Compression 

Enter Yes in this field to compress the data that you send to the server. Use this option to reduce the size of data files which synchronize and transfer data.

Synchronize Altered Transactions

To send altered transactions to remote clients set this option to Yes.

 

4th Step 

Establishing a connection from Client

To initiate the connection (handshake) process from Sync Client,

Go to Gateway of Tally > Import of Data > Synchronization > Synchronize

5th Step 

Activate/Enable Sync Rule on Server

During the Handshake process, the Sync Rule is transferred from Client to Server. To Activate/Enable the Sync Rule,

Go to Gateway of Tally > Import of Data > Synchronization > Server Rules > Activate
 

Select the required rule from the List of Rules based on the Client Company Name as shown:
Press Enter to select the Rule
In the Server Rule Activation screen,
Select Is Active to Yes to activate the Rule
Select Yes for Synchronized Altered Transactions, to allow the altered transactions during Sync
 */
