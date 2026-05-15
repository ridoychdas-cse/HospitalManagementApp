using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class DiagnosisBillGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        /// <summary>
        /// Save Diagnosis Bill In Mst Table 
        /// </summary>
        /// <param name="aDiagnosisBillMst">Mst Table Information</param>
        /// <param name="diagnosisList">Dtl Table Information</param>
        /// <returns>If save Return 1</returns>
        internal int Save(DiagnosisBillMst aDiagnosisBillMst, DataTable diagnosisList)
        {
            int rowAffected = 0;
            string query1 = "INSERT INTO DiagnosisBillMst VALUES('" + aDiagnosisBillMst.PatientId + "', '" +
                                  aDiagnosisBillMst.PatientType + "', '" + aDiagnosisBillMst.EntryDate + "')";

            string query2 = "SELECT TOP(1) Id FROM DiagnosisBillMst ORDER BY Id DESC";

            int mstId = DataManager.Transaction(_connectionString, query1, query2);

            foreach (DataRow value in diagnosisList.Rows)
            {
                string query = "INSERT INTO DiagnosisBillDtl VALUES('" + mstId + "','" + value["BillingId"] + "',  '" +
                    value["DiagnosisTypeId"] + "','" + value["DiagnosisId"] + "','" + value["Price"] + "', '" +
                    value["Discount"] + "', '" + value["TotalPrice"] + "', '" + value["DeliveryDate"] + "')";
                rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            }
            return rowAffected;
        }

        /// <summary>
        /// Save Diagnosis Bill Money Received
        /// </summary>
        /// <param name="aDiagnosisBillMr">Mr Table Information</param>
        /// <returns>If save Return 1</returns>
        public int SaveDiagnosisBillMr(DiagnosisBillMr aDiagnosisBillMr)
        {
            var connection = new SqlConnection(_connectionString);
            string query = "SELECT TOP(1) Id FROM DiagnosisBillMst ORDER BY Id DESC";
            var command = new SqlCommand(query, connection);
            connection.Open();
            int mstId = Convert.ToInt32(command.ExecuteScalar());

            query = "INSERT INTO DiagnosisBillMR VALUES('" + mstId + "', '" + aDiagnosisBillMr.BillingId + "', '" +
                    aDiagnosisBillMr.TotalNetPrice + "', '" + aDiagnosisBillMr.Discount + "', '" + aDiagnosisBillMr.Vat +
                    "', '" + aDiagnosisBillMr.TotalPayableAmount + "', '" + aDiagnosisBillMr.PayAmount + "')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            if (rowAffected > 0)
            {
                DateTime date = DateTime.Now;
                query = "INSERT INTO DiagnosisBillMrDtl VALUES('" + aDiagnosisBillMr.BillingId + "', '" + date + "')";
                rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            }
            return rowAffected;

        }

        public int UpdateDiagnosisBillMr(DiagnosisBillMr aDiagnosisBillMr)
        {
            string query = "UPDATE DiagnosisBillMR SET BillingId='" + aDiagnosisBillMr.BillingId + "', Discount='" +
                           aDiagnosisBillMr.Discount + "', TotalPayableAmount='" + aDiagnosisBillMr.TotalPayableAmount +
                           "', TotalPayAmount='" + aDiagnosisBillMr.TotalPayAmount + "' WHERE BillingId='" + aDiagnosisBillMr.BillingId + "' ";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            if (rowAffected > 0)
            {
                DateTime date = DateTime.Now;
                query = "INSERT INTO DiagnosisBillMrDtl VALUES('" + aDiagnosisBillMr.BillingId + "', '" + date + "')";
                rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            }
            return rowAffected;
        }

        public DiagnosisBillMr GetDiagnosisBillMrByBillingId(string billingId)
        {
            DiagnosisBillMr aDiagnosisBillMr = null;

            string query = "SELECT * FROM DiagnosisBillMR WHERE BillingId='" + billingId + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aDiagnosisBillMr = new DiagnosisBillMr();
                aDiagnosisBillMr.Id = Convert.ToInt32(reader["Id"]);
                aDiagnosisBillMr.BillingId = reader["BillingId"].ToString();
                aDiagnosisBillMr.TotalNetPrice = Convert.ToDecimal(reader["TotalNetPrice"]);
                aDiagnosisBillMr.Discount = Convert.ToDecimal(reader["Discount"]);
                aDiagnosisBillMr.Vat = Convert.ToDecimal(reader["Vat"]);
                aDiagnosisBillMr.TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]);
                aDiagnosisBillMr.PaidAmount = Convert.ToDecimal(reader["TotalPayAmount"]);

            }
            reader.Close();
            return aDiagnosisBillMr;

        }

        internal string GetAutoBillNumber()
        {
            string billNo = "";
            int idAutoIncrement = 0;
            string query = "SELECT TOP(1) BillingId FROM DiagnosisBillDtl ORDER BY Id DESC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (!reader.HasRows)
            {
                billNo = "1001";
            }
            else
            {
                reader.Read();
                idAutoIncrement = Convert.ToInt32(reader["BillingId"]);
                idAutoIncrement++;
                billNo = idAutoIncrement.ToString();
            }
            return billNo;
        }

        internal List<DiagnosisBillMr> GetDuiBillListByPatientId(int id, string patientType)
        {
            DiagnosisBillMr aDiagnosisBillMr = null;
            var billList = new List<DiagnosisBillMr>();

            int Id = 0;
            var patientIdList = new List<int>();
            string query = "SELECT * FROM DiagnosisBillMst WHERE PatientId='" + id + "' AND PatientType='" + patientType + "'";
            var reder = DataManager.SqlDataReader(query, _connectionString);
            if (reder.HasRows)
            {
                while (reder.Read())
                {
                    Id = Convert.ToInt32(reder["Id"]);

                    patientIdList.Add(Id);
                }
            }

            if (patientIdList != null)
            {
                foreach (int value in patientIdList)
                {
                    query = "SELECT * FROM DiagnosisBillMR WHERE MstId='" + value + "' AND TotalPayableAmount!=TotalPayAmount";

                    var reader = DataManager.SqlDataReader(query, _connectionString);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            aDiagnosisBillMr = new DiagnosisBillMr();
                            aDiagnosisBillMr.Id = Convert.ToInt32(reader["Id"]);
                            aDiagnosisBillMr.MstId = Convert.ToInt32(reader["MstId"]);
                            aDiagnosisBillMr.BillingId = reader["BillingId"].ToString();
                            aDiagnosisBillMr.TotalNetPrice = Convert.ToDecimal(reader["TotalNetPrice"]);
                            aDiagnosisBillMr.Discount = Convert.ToDecimal(reader["Discount"]);
                            aDiagnosisBillMr.Vat = Convert.ToDecimal(reader["Vat"]);
                            aDiagnosisBillMr.TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]);
                            aDiagnosisBillMr.TotalPayAmount = Convert.ToDecimal(reader["TotalPayAmount"]);
                            aDiagnosisBillMr.DueAmount = aDiagnosisBillMr.TotalPayableAmount - aDiagnosisBillMr.TotalPayAmount;

                            billList.Add(aDiagnosisBillMr);

                        }
                    }
                    reader.Close();
                }
            }

            return billList;
        }

        internal DiagnosisBillMr GetBillInformationById(int id)
        {
            DiagnosisBillMr aDiagnosisBillMr = null;
            string query = "SELECT * FROM DiagnosisBillMR WHERE Id='" + id + "' ";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aDiagnosisBillMr = new DiagnosisBillMr();
                    aDiagnosisBillMr.Id = Convert.ToInt32(reader["Id"]);
                    aDiagnosisBillMr.MstId = Convert.ToInt32(reader["MstId"]);
                    aDiagnosisBillMr.BillingId = reader["BillingId"].ToString();
                    aDiagnosisBillMr.TotalNetPrice = Convert.ToDecimal(reader["TotalNetPrice"]);
                    aDiagnosisBillMr.Discount = Convert.ToDecimal(reader["Discount"]);
                    aDiagnosisBillMr.Vat = Convert.ToDecimal(reader["Vat"]);
                    aDiagnosisBillMr.TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]);
                    aDiagnosisBillMr.TotalPayAmount = Convert.ToDecimal(reader["TotalPayAmount"]);
                    aDiagnosisBillMr.DueAmount = aDiagnosisBillMr.TotalPayableAmount - aDiagnosisBillMr.TotalPayAmount;

                }
            }
            reader.Close();

            return aDiagnosisBillMr;
        }

        public DataTable GetTotalDiagnosisBillSummery(string startDate, string endDate, string type)
        {
            using (SqlConnection conn = new SqlConnection(DataManager.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_TotalDiagnosisBillSummery", conn);
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
                if (!string.IsNullOrEmpty(type) && type!="All")
                {
                    sqlComm.Parameters.AddWithValue("@PatientType", type);
                }
                else
                {
                    sqlComm.Parameters.AddWithValue("@PatientType", null);
                }
                
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = sqlComm;
                da.Fill(ds, "Sp_TotalDiagnosisBillSummery");
                DataTable dtStdSummery = ds.Tables["Sp_TotalDiagnosisBillSummery"];
                return dtStdSummery;
            }
        }
    }
}