using HospitalBilling.BLL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

[WebService(Namespace = "http://simran.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class AutoComplete1 : WebService
{
    public AutoComplete1()
    {
    }

    //_______________________________________________________________________________

    [WebMethod]
    public string[] GetDiagnosis(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        DataTable dt = GetStudent(prefixText);

        List<string> items = new List<string>(count);
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            string str = dt.Rows[i][0].ToString();

            items.Add(str);
        }

        return items.ToArray();
    }

    public DataTable GetStudent(string strName)
    {
        string strConn = DataManager.ConnectionString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        string query = "SELECT Name  FROM [Diagnosis] where upper(Name) LIKE upper('%" + strName + "%')";

        DataTable dt = DataManager.DataTable(strConn, query, "autoname");
        return dt;
    }



}
