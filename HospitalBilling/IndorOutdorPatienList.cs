using HospitalBilling.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HospitalBilling
{
    class IndorOutdorPatienList
    {

        public static DataTable GetData(string valu)
        {
            DataTable dt = null;
            if (valu=="IN")
            {
                string connectionString = DataManager.ConnectionString();
                SqlConnection sqlCon = new SqlConnection(connectionString);
                string query = " select t1.PatientId as PatienId ,t1.Name as Name ,t1.PhoneNo as PhoneNo,Gender,EntryDate,t2.Name as Docter from [IndoorPatients] as t1  left join Doctors as t2 on t1.ConsultantId=t2.Id";
                 dt = DataManager.ExecuteQuery(connectionString, query, "IndoorPatient");
                
            }
            else if (valu == "OUT")
            {
                string connectionString = DataManager.ConnectionString();
                SqlConnection sqlCon = new SqlConnection(connectionString);
                string query = "select PatientId as PatienId,t1.Name as Name,t1.PhoneNo as PhoneNo,Gender,EntryDate,t2.Name as Docter from OutdoorPatients as t1 Left join Doctors as t2 on t1.DoctorId=t2.Id";

                 dt = DataManager.ExecuteQuery(connectionString, query, "OutdoorPatients");
             
            }
            return dt;
           
        }
    }
}
