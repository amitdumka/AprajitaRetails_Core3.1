//using zkemkeeper;
/*
 * https://stackoverflow.com/questions/31377437/zkemkeeper-dll-support-which-biometric-devices
 * https://usermanual.wiki/Document/Biometric20Device20SDK20Programmers20Guide.1906605638/help
 * https://www.codeproject.com/Questions/711973/Using-Zkemkeeper-dll-from-SDK-for-Biometric-scanne
 * https://www.google.com/search?sxsrf=ALeKk008bhLtDV1pFn2o32ZNEvQDyERDxg:1599915944796&q=Zkemkeeper+sample+code+c%23&sa=X&ved=2ahUKEwiPmb2s1-PrAhVBXn0KHT8jCzYQ1QIwDHoECA4QBA&biw=1920&bih=937
 * https://github.com/nrubiano/ZK.Biometric/wiki/ZK-SDK-Installation
 * https://www.toolbox.com/tech/programming/question/connectiong-and-reading-data-from-bio-metric-device-050415/
 * https://stackoverflow.com/questions/35666583/how-to-connect-attendance-punching-machine-using-zkemkeeper-in-c
 * https://www.academia.edu/31586469/Zkemkeeper_User_Manual
 * https://www.youtube.com/watch?v=G246YLNqeh4
 * 
 */

//namespace AprajitaRetails.Ops.Helpers.BioMetric
//{
//    public class BioMetricDevice
//    {
//        public string Id { get; set; }
//        public string MachineNo { get; set; }
//        public string MachineIP { get; set; }
//        public string PortNo { get; set; }
//        public string Remark { get; set; }
//        public string Tuser { get; set; }
//        public string Tdate { get; set; }
//        public string Status { get; set; }
//        public int DwMachineNumber { get; set; }
//        public string DwEnrollNumber { get; set; }
//        public int DwVerifyMode { get; set; }
//        public int DwInOutMode { get; set; }
//        public int DwYear { get; set; }
//        public int DwMonth { get; set; }
//        public int DwDay { get; set; }
//        public int DwHour { get; set; }
//        public int DwMinute { get; set; }
//        public int DwSecond { get; set; }
//        public int DwWorkcode { get; set; }
//        public string UserId { get; set; }
//        public string Name { get; set; }
//        public int FingerIndex { get; set; }
//        public string FingerImage { get; set; }
//        public int Privilege { get; set; }
//        public string Passwords { get; set; }
//        public bool Enabled { get; set; }
//        public int Flag { get; set; }
//        public string Fromdate { get; set; }
//        public string Todate { get; set; }
//    }
//    public class BioMetricHelper
//    {

//        public static bool ConnectDevice(string ipAddress, int portno)
//        {
//            CZKEM axCZKEM1 = new CZKEM();
//            try
//            {
//                return axCZKEM1.Connect_Net(ipAddress, portno);
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }

//        public void ReadBioMetricData(string ipAddress, int portno)
//        {
//            CZKEM axCZKEM1 = new CZKEM();
//            try
//            {
//                bool bIsConnected = axCZKEM1.Connect_Net(ipAddress, portno);   // 4370 is port no of attendance machine
//                if (bIsConnected == true)
//                {
//                    // MessageBox.Show("Device Connected Successfully");

//                }
//                else
//                {
//                    // MessageBox.Show("Device Not Connect");
//                }
//            }   
//            catch(Exception ex)
//            {
//                // MessageBox.Show(ex.Message.ToString());
//            }
//        }
//    }
//}
/*
 * 
 * 
 * public zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();
private void btndownload_Click(object sender, EventArgs e)
{
    bool bIsConnected = axCZKEM1.Connect_Net(ip_address_of_your_machine, 4370);   // 4370 is port no of attendance machine
    if (bIsConnected == true)
    {
        bool delete = axCZKEM1.ClearGLog(dwMachineNumber);
        if (delete == true)
        {
            MessageBox.Show("Deleted.....");
        }
        if (delete == false)
        {
            MessageBox.Show("No Log Found To Delete.....");
        }
    }
}
 * public zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();
private void btnconnect_Click(object sender, EventArgs e)
{
    try
    {
        bool bIsConnected = axCZKEM1.Connect_Net(ip_address_of_your_machine, 4370);   // 4370 is port no of attendance machine
        if (bIsConnected == true)
        {
            MessageBox.Show("Device Connected Successfully");
        }
        else
        {
            MessageBox.Show("Device Not Connect");
        }
    }

    Catch( (Exception ex)
    {
        MessageBox.Show(ex.Message.ToString());
    }
}

private bool IsRead = false;
string dwEnrollNumber;
int dwVerifyMode, dwInOutMode, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond,           dwWorkcode, dwMachineNumber;
public zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();

public class ClsMachineBL
{
    public String DownloadDataFromBiomatrix(ClsMachineML prp)
    {
        try
        {
            string constr = CommonConnection.ConStr;
            con = new SqlConnection(constr);
            con.Open();
            cmd = new SqlCommand("Prc_InsertDatafromBiomatrix", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@dwMachineNumber", prp.dwMachineNumber);
            cmd.Parameters.AddWithValue("@dwEnrollNumber", prp.dwEnrollNumber);
            cmd.Parameters.AddWithValue("@dwVerifyMode", prp.dwVerifyMode);
            cmd.Parameters.AddWithValue("@dwInOutMode", prp.dwInOutMode);
            cmd.Parameters.AddWithValue("@dwYear", prp.dwYear);
            cmd.Parameters.AddWithValue("@dwMonth", prp.dwMonth);
            cmd.Parameters.AddWithValue("@dwDay", prp.dwDay);
            cmd.Parameters.AddWithValue("@dwHour", prp.dwHour);
            cmd.Parameters.AddWithValue("@dwMinute", prp.dwMinute);
            cmd.Parameters.AddWithValue("@dwSecond", prp.dwSecond);
            cmd.Parameters.AddWithValue("@dwWorkcode", prp.dwWorkcode);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            BL.clsCommon objerr = new BL.clsCommon();
            objerr.InesrtError("Error IS " + ex.Message + "_" + ex.StackTrace);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        return result;
    }
}

public class ClsMachineML
{
    public string Id { get; set; }
    public string MachineNo { get; set; }
    public string MachineIP { get; set; }
    public string PortNo { get; set; }
    public string Remark { get; set; }
    public string Tuser { get; set; }
    public string Tdate { get; set; }
    public string Status { get; set; }
    public int  dwMachineNumber { get; set; }
    public string dwEnrollNumber { get; set; }
    public int dwVerifyMode { get; set; }
    public int dwInOutMode { get; set; }
    public int dwYear { get; set; }
    public int dwMonth { get; set; }
    public int dwDay { get; set; }
    public int dwHour { get; set; }
    public int dwMinute { get; set; }
    public int dwSecond { get; set; }
    public int dwWorkcode { get; set; }
    public string User_Id { get; set; }
    public string Name { get; set; }
    public int Finger_Index { get; set; }
    public string Finger_Image { get; set; }
    public int Privilege { get; set; }
    public string Passwords { get; set; }
    public bool Enabled { get; set; }
    public int Flag { get; set; }
    public string  Fromdate { get; set; }
    public string Todate { get; set; }
}

private void btndownload_Click(object sender, EventArgs e)
{
     ClsMachineBL obj = new ClsMachineBL();
     ClsMachineML prp = new ClsMachineML();

    try
    {
        if (cbmachine.Text == "" || cbmachine.Text == "Select")
        {
            MessageBox.Show("Please Select Machine");
            cbmachine.Focus();
            return;
        }

        progressBar1.Visible = true;
          bool bIsConnected = axCZKEM1.Connect_Net(ip_address_of_your_machine, 4370);   // 4370 is port no of attendance machine
        if (bIsConnected == true)
        {
            IsRead = axCZKEM1.ReadGeneralLogData(dwMachineNumber);
            if (IsRead == true)
            {
                progressBar1.Maximum = 100;
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                while (axCZKEM1.SSR_GetGeneralLogData(dwMachineNumber, out dwEnrollNumber, out dwVerifyMode, out dwInOutMode, out dwYear, out                                dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkcode))
                {
                    prp.dwDay = dwDay;
                    prp.dwEnrollNumber = dwEnrollNumber;
                    prp.dwHour = dwHour;
                    prp.dwInOutMode = dwInOutMode;
                    prp.dwMachineNumber = dwMachineNumber;
                    prp.dwMinute = dwMinute;
                    prp.dwMonth = dwMonth;
                    prp.dwSecond = dwSecond;
                    prp.dwVerifyMode = dwVerifyMode;
                    prp.dwWorkcode = dwWorkcode;
                    prp.dwYear = dwYear;
                    string add = obj.DownloadDataFromBiomatrix(prp);
                    progressBar1.PerformStep();
                }

                string export = obj.ExportToAttendance(prp);
                MessageBox.Show("Attendance Downloaded Successfully");
                progressBar1.Visible = false;
            }
            else
            {
                MessageBox.Show("No Log Found....");
                progressBar1.Visible = false;
            }
        }
        else
        {
            MessageBox.Show("Device Not Connected");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message.ToString());
    }
} 
//internal Axzkemkeeper.AxCZKEM AxCZKEM1;
    //public Axzkemkeeper.AxCZKEM axCZKEM1 = new Axzkemkeeper.AxCZKEM();
    //public zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();
    //public delegate int DecompressMCX(int hComp,IntPtr in, uint in_len, IntPtr out, ref uint out_len, bool eod);

    public zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();

private void Essl_Connect(string IpAddress, int MachineNo)
    {
        try
        {
            bConn = axCZKEM1.Connect_Net(IpAddress.Trim(), 8080);
            //bConn = axCZKEM1.Connect_Net(IpAddress.Trim(), 4370);
            if (bConn == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Connected')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Not Connected')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Error Message", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
 */
