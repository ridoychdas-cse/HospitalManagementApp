using HospitalBilling.BLL;
using System;
using System.Data.SqlClient;

namespace HospitalBilling.UI
{
    public partial class DatabaseBackupForm : System.Web.UI.Page
    {
        private readonly string _connectionString = DataManager.ConnectionString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void dbBackupButton_Click(object sender, EventArgs e)
        {
            var connection = new SqlConnection(_connectionString);

            const string backupDrive = "E:\\HospitalBackup";

            if (!System.IO.Directory.Exists(backupDrive))
            {
                System.IO.Directory.CreateDirectory(backupDrive);
            }

            try
            {
                connection.Open();
                var command = new SqlCommand("backup database HospitalBillingDb to disk='" + backupDrive + "\\" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".Bak'", connection);
                command.ExecuteNonQuery();
                connection.Close();
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Database backup Succes');", true);
            }
            catch (Exception ex)
            {

                messageLabel.Text = "Error Occured During DB backup process !<br>" + ex.ToString();
            }
        }
    }
}