using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HospitalBilling.BLL
{
    public class DropDown
    {
        public static bool PopulationDropDownList(DropDownList ddl, string TableName, string query, string TextField, string ValueField)
        {
            ddl.DataSource = null;
            String connectionString = DataManager.ConnectionString();
            SqlConnection myConnection = new SqlConnection(connectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter(query, myConnection);
            DataSet ds = new DataSet();
            myAdapter.Fill(ds, TableName);
            DataTable Dt = ds.Tables[TableName];
            if (Dt.Rows.Count == 0)
            {
                return false;
            }
            ddl.Items.Clear();
            ddl.DataSource = Dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
            return true;
        }

        
    }
}