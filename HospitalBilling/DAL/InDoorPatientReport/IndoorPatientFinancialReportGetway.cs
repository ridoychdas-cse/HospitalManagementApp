using HospitalBilling.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HospitalBilling.DAL.InDoorPatientReport
{
    public class IndoorPatientFinancialReportGetway
    {
        public DataTable GetData(string startDate, string endDate, string BillNo, string PatientId, string DiagnosisTypeId, string DiagnosisId, string ReportType)
        {

            using (SqlConnection conn = new SqlConnection(DataManager.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_IndoorPatientFinancialReport", conn);
                if (!string.IsNullOrEmpty(startDate))
                {
                    sqlComm.Parameters.AddWithValue("@startDate", startDate);
                }
                else
                {
                    sqlComm.Parameters.AddWithValue("@startDate", null);
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    sqlComm.Parameters.AddWithValue("@endDate", endDate);
                }
                else
                {
                    sqlComm.Parameters.AddWithValue("@endDate", null);
                }
                if (!string.IsNullOrEmpty(BillNo))
                {
                    sqlComm.Parameters.AddWithValue("@BillNo", BillNo);
                }
                else
                {
                    sqlComm.Parameters.AddWithValue("@BillNo", null);
                }
                if (!string.IsNullOrEmpty(PatientId))
                {
                    sqlComm.Parameters.AddWithValue("@PatientId", PatientId);
                }
                else
                {
                    sqlComm.Parameters.AddWithValue("@PatientId", null);
                    //sqlComm.Parameters.AddWithValue("@ReportType", 1);
                }
                if (!string.IsNullOrEmpty(DiagnosisTypeId))
                {
                    sqlComm.Parameters.AddWithValue("@DiagnosisTypeId", DiagnosisTypeId);
                }
                else
                {
                    sqlComm.Parameters.AddWithValue("@DiagnosisTypeId", null);

                }
                if (!string.IsNullOrEmpty(DiagnosisId))
                {
                    sqlComm.Parameters.AddWithValue("@DiagnosisId", DiagnosisId);
                }
                else
                {
                    sqlComm.Parameters.AddWithValue("@DiagnosisId", null);

                }
                if (!string.IsNullOrEmpty(ReportType))
                {
                    sqlComm.Parameters.AddWithValue("@ReportType", ReportType);
                }
                else
                {
                    sqlComm.Parameters.AddWithValue("@ReportType", null);

                }
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = sqlComm;
                da.Fill(ds, "Sp_IndoorPatientFinancialReport");
                DataTable data = ds.Tables["Sp_IndoorPatientFinancialReport"];
                return data;
            }
        }


       
    }
}