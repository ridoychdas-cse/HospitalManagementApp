using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;
using HospitalBilling.BLL;


[WebService(Namespace = "http://shofthousebd.com/webservices")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class AutoComplete : WebService
{
    public AutoComplete()
    {

    }

    [WebMethod(EnableSession = true)]
    public string[] GetIndoreCode(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        if (Session["user"].ToString() != null)
        {
            DataTable dt = GtIndoreTd(prefixText);

            List<string> items = new List<string>(count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string str = dt.Rows[i][0].ToString();

                items.Add(str);
            }

            return items.ToArray();
        }
        else
            return null;
    }
    public DataTable GtIndoreTd(string strName)
    {
        string strConn = DataManager.ConnectionString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        string query = "select PatientId+'-'+Name from IndoorPatients where upper(PatientId+'-'+Name) like upper('%" + strName + "%') order by Id Desc  ";
        DataTable dt = DataManager.ExecuteQuery(strConn, query, "autoname");
        return dt;
    }



    [WebMethod(EnableSession = true)]
    public string[] GetSponserSearch(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        DataTable dt = GetSponser(prefixText);

        List<string> items = new List<string>(count);
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            string str = dt.Rows[i][0].ToString();

            items.Add(str);
        }

        return items.ToArray();
    }

    private DataTable GetSponser(string strName)
    {
        string strConn = DataManager.ConnectionString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        string query = "select PatientId+'-'+Name from IndoorPatients where upper(PatientId+'-'+Name) like upper('%" + strName + "%') order by Id Desc  ";
        DataTable dt = DataManager.ExecuteQuery(strConn, query, "autoname");
        return dt;
    }


    [WebMethod(EnableSession = true)]
    public string[] GetOutdoorPatient(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        DataTable dt = GetOP(prefixText);

        List<string> items = new List<string>(count);
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            string str = dt.Rows[i][0].ToString();

            items.Add(str);
        }

        return items.ToArray();
    }

    private DataTable GetOP(string strName)
    {
        string strConn = DataManager.ConnectionString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        string query = "select PatientId+'-'+Name from [dbo].[OutdoorPatients] where upper(PatientId+'-'+Name) like upper('%" + strName + "%') order by Id Desc ";
        DataTable dt = DataManager.ExecuteQuery(strConn, query, "autoname");
        return dt;
    }
}
