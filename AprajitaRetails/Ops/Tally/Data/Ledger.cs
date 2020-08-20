using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace AprajitaRetails.Ops.Tally.Data
{
    /*
     * Helper XML File to different things
     * https://www.rtslink.com/articles/tally-xml-tags-export/ 
     */

    /*
     * Notes: 
     * DupModify specifies that the current Opening Balance should be modified with the new one that is being imported. 
     * DupIgnoreCombine specifies that the ledger if exists need to be ignored.
     * DupCombine specifies the system to combine both the Opening Balances. Ideally, this option is used when Data pertaining to Group Companies are merged together into a single company. 
     * 
     * I have Used DupCombine
     */
    /*
     * Excute
     * Tags used for sending a request to Execute an action from Tally.ERP 9.
     * <HEADER> contains the following: 
     *              Tag <TALLYREQUEST> must contain value Execute
     *              Tag <TYPE> must contain value TDLAction and
     *              Tag <ID> should contain the Name of the TDL Action
     *              
     * <ENVELOPE>
           <HEADER>
                    <VERSION>1</VERSION>
                    <TALLYREQUEST>Execute</TALLYREQUEST>
                    <TYPE>TDLAction</TYPE>
                    <ID>Sync</ID>
           </HEADER>
         </ENVELOPE>
     * 
     * In the above XML request, <HEADER> describes the expected result.
     * The value of the Tag <TALLYREQUEST> is Execute which indicates that some action needs to be executed in Tally.      
     * The value of the Tag <TYPE> is TDLAction which indicates that some TDLAction has to be executed in Tally.
     * The value of the Tag <ID> must be a TDL Action Name. Any action which needs to be exe­cuted in Tally can be specified within this Tag.
     */

    public enum Action { Create, Alter, Drop, Delete };
    public enum ResponseMessage { CREATED, ALTERED, LASTVCHID, LASTMID, COMBINED, IGNORED, ERRORS }


    public class TallyAction
    {
        const string header = "<ENVELOPE> < HEADER >  < VERSION > 1 </ VERSION > < TALLYREQUEST > Import </ TALLYREQUEST > < TYPE > Data </ TYPE > < ID > All Masters </ ID </ HEADER >" +
                "< BODY > < DESC >< STATICVARIABLES >< IMPORTDUPS >@@DUPCOMBINE </ IMPORTDUPS ></ STATICVARIABLES ></ DESC >" +
                "< DATA >< TALLYMESSAGE >";
        const string footer = " </ TALLYMESSAGE ></ DATA ></ BODY ></ ENVELOPE > ";

        public string AddLedger(Ledger ledger)
        {
            string ActionMessage = $"{header} <LEDGER NAME='{ledger.LedgerName}' Action ='Create' >< NAME >{ledger.LedgerName}</ NAME >< PARENT >{ledger.ParentName} </ PARENT >< OPENINGBALANCE > {ledger.OpenningBalance}</ OPENINGBALANCE ></ LEDGER >{footer} ";
            return ActionMessage;
        }

        public string AddGroup(Group group)
        {
            string ActionMessage = $"<GROUP NAME = '{group.GroupName}' Action = 'Create' >< NAME > {group.GroupName}</ NAME >< PARENT >{group.ParentName} </ PARENT ></ GROUP >";
            return $"{header} {ActionMessage}{footer} ";

        }
        public string AddLedgerWithGroup(Ledger ledger, Group group)
        {
            string ActionMessage1 = $"<GROUP NAME = '{group.GroupName}' Action = 'Create' >< NAME > {group.GroupName}</ NAME >< PARENT >{group.ParentName} </ PARENT ></ GROUP >";
            string ActionMessage2 = $"<LEDGER NAME='{ledger.LedgerName}' Action ='Create' >< NAME >{ledger.LedgerName}</ NAME >< PARENT >{ledger.ParentName} </ PARENT >< OPENINGBALANCE > {ledger.OpenningBalance}</ OPENINGBALANCE ></ LEDGER >";
            return $"{ header}{ActionMessage1 }{ ActionMessage2}{ footer}";
        }
    }



    public class Ledger
    {
        public string LedgerName { get; set; }
        public string ParentName { get; set; }
        public decimal OpenningBalance { get; set; }
        public Action Action { get; set; }

    }
    public class Group
    {
        public string GroupName { get; set; }
        public string ParentName { get; set; }
        public Action Action { get; set; }
    }
}
/*
 * <LEDGER NAME="ICICI" Action = "Create">
 *      <NAME>ICICI</NAME>
 *      <PARENT>Bank Accounts</PARENT>
 *      <OPENINGBALANCE>-12500</OPENINGBALANCE>
 * </LEDGER>
 * 
 * <GROUP NAME=" Bangalore Debtors" Action = "Create">
 *      <NAME>Bangalore Debtors</NAME>
 *      <PARENT>Sundry Debtors</PARENT>
 * </GROUP>
 * 
 * <LEDGER NAME="RK Builders Pvt Ltd" Action = "Create">
 *      <NAME>RK Builders Pvt Ltd</NAME>
 *      <PARENT>Bangalore Debtors</PARENT>
 *      <OPENINGBALANCE>-1000</OPENINGBALANCE>
 * </LEDGER>
 */

/*
 * XML Response received From Tally.

<RESPONSE>
           <CREATED>2</CREATED>
           <ALTERED>0</ALTERED>
           <LASTVCHID>0</LASTVCHID>
           <LASTMID>0</LASTMID>
           <COMBINED>0</COMBINED>
           <IGNORED>0</IGNORED>
           <ERRORS>0</ERRORS>
</RESPONSE>
 */
