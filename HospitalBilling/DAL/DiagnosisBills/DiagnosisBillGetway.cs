using HospitalBilling.BLL;
using HospitalBilling.Models.DiagnosisBills;
using System;
using System.Collections.Generic;

namespace HospitalBilling.DAL.DiagnosisBills
{
    public class DiagnosisBillGetway
    {
        private readonly string _connectionString;

        public DiagnosisBillGetway()
        {
            _connectionString = DataManager.ConnectionString();
        }

        // Save DiagnosisBillMst and DiagnosisBillDtl
        internal int Save(DiagnosisBillMst diagnosisBillMst, List<DiagnosisBillDtl> diagnosisBillDtls)
        {
            int rowAffected = 0;
            string query1 = "INSERT INTO DiagnosisBillMst VALUES('" + diagnosisBillMst.PatientId + "', '" +
                            diagnosisBillMst.PatientType + "', '" + diagnosisBillMst.BillNo + "', '" + diagnosisBillMst.SpecialDiscount + "', '" +
                            diagnosisBillMst.Vat + "', '" + diagnosisBillMst.TotalPayableAmount + "', '" + diagnosisBillMst.EntryDate + "')";

            const string query2 = "SELECT TOP(1) Id FROM DiagnosisBillMst ORDER BY Id DESC";

            int mstId = DataManager.Transaction(_connectionString, query1, query2);

            foreach (var value in diagnosisBillDtls)
            {
                string query = "INSERT INTO DiagnosisBillDtl VALUES('" + mstId + "',  '" +
                               value.DiagnosisTypeId + "','" + value.DiagnosisId + "','" + value.Price + "', '" +
                               value.Discount + "', '" + value.PayableAmount + "', '" + value.DeliveryDate + "')";
                rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            }
            if (rowAffected > 0)
            {
                return mstId;
            }
            return rowAffected;
        }

        // Auto BillNo
        internal string GetAutoBillNumber()
        {
            string billNo = "";
            int idAutoIncrement = 0;
            string query = "SELECT TOP(1) BillNo FROM DiagnosisBillMst ORDER BY Id DESC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (!reader.HasRows)
            {
                billNo = "1001";
            }
            else
            {
                reader.Read();
                idAutoIncrement = Convert.ToInt32(reader["BillNo"]);
                idAutoIncrement++;
                billNo = idAutoIncrement.ToString();
            }
            return billNo;
        }


        // Check Unique BillNo
        internal string BillNoUniqueCheck(string billNo)
        {
            string query = "SELECT BillNo FROM DiagnosisBillMst WHERE BillNo='" + billNo + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                string dbbillNo = reader["BillNo"].ToString();
                return dbbillNo;
            }
            return "";
        }


        // Get Total Diagnosis Bill Info By BillNo (DiagnossiBillMst Info)
        internal DiagnosisBillMst GetDiagnosisBillMstBillInfoByBillNo(string billNo)
        {
            DiagnosisBillMst diagnosisBillMst = null;
            string query = "SELECT * FROM DiagnosisBillMst WHERE BillNo='" + billNo + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                diagnosisBillMst = new DiagnosisBillMst();
                diagnosisBillMst.Id = Convert.ToInt32(reader["Id"]);
                diagnosisBillMst.PatientId = Convert.ToInt32(reader["PatientId"]);
                diagnosisBillMst.PatientType = reader["PatientType"].ToString();
                diagnosisBillMst.BillNo = reader["BillNo"].ToString();
                diagnosisBillMst.SpecialDiscount = Convert.ToDecimal(reader["SpecialDiscount"]);
                diagnosisBillMst.Vat = Convert.ToDecimal(reader["Vat"]);
                diagnosisBillMst.TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]);
                diagnosisBillMst.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
            }
            reader.Close();

            return diagnosisBillMst;
        }

        // Outdoor patient all Diagnosis billNo List
        internal List<string> GetAllBillNoList(int patientId, string patientType)
        {
            var billNoList = new List<string>();

            string query = "SELECT * FROM DiagnosisBillMst WHERE PatientId='" + patientId + "' AND PatientType='" +
                           patientType + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string billNo = reader["BillNo"].ToString();

                    billNoList.Add(billNo);
                }
            }
            return billNoList;
        }

        // Get DiagnsoisBill  detils info
        internal List<DiagnosisBillDtl> GetDiagnosisBillDtlsByMstId(int mstId)
        {
            DiagnosisBillDtl diagnosisBillDtl = null;
            var diagnsosiBillDtls = new List<DiagnosisBillDtl>();
            string query =
                "select t1.Id, t1.DiagnosisBillMstId, t1.DiagnosisTypeId, t1.DiagnosisId, t2.Name as DiagnosisName, t1.Price, t1.Discount, t1.PayableAmount, t1.DeliveryDate from DiagnosisBillDtl as t1 left join Diagnosis as t2 on t1.DiagnosisId= t2.Id WHERE t1.DiagnosisBillMstId='" +
                mstId + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    diagnosisBillDtl = new DiagnosisBillDtl();
                    diagnosisBillDtl.Id = Convert.ToInt32(reader["Id"]);
                    diagnosisBillDtl.DiagnosisBillMstId = Convert.ToInt32(reader["DiagnosisBillMstId"]);
                    diagnosisBillDtl.DiagnosisTypeId = Convert.ToInt32(reader["DiagnosisTypeId"]);
                    diagnosisBillDtl.DiagnosisId = Convert.ToInt32(reader["DiagnosisId"]);
                    diagnosisBillDtl.DiagnosisName = reader["DiagnosisName"].ToString();
                    diagnosisBillDtl.Price = Convert.ToDecimal(reader["Price"]);
                    diagnosisBillDtl.Discount = Convert.ToDecimal(reader["Discount"]);
                    diagnosisBillDtl.PayableAmount = Convert.ToDecimal(reader["PayableAmount"]);
                    diagnosisBillDtl.DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]);

                    diagnsosiBillDtls.Add(diagnosisBillDtl);
                }
            }
            reader.Close();
            return diagnsosiBillDtls;
        }





        internal DiagnosisBillMst GetDiagnosisBillMstById(int id)
        {
            DiagnosisBillMst diagnosisBillMst = null;
            string query = "SELECT * FROM DiagnosisBillMst WHERE Id='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                diagnosisBillMst = new DiagnosisBillMst();
                diagnosisBillMst.Id = Convert.ToInt32(reader["Id"]);
                diagnosisBillMst.PatientId = Convert.ToInt32(reader["PatientId"]);
                diagnosisBillMst.PatientType = reader["PatientType"].ToString();
                diagnosisBillMst.BillNo = reader["BillNo"].ToString();
                diagnosisBillMst.SpecialDiscount = Convert.ToDecimal(reader["SpecialDiscount"]);
                diagnosisBillMst.Vat = Convert.ToDecimal(reader["Vat"]);
                diagnosisBillMst.TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]);
                diagnosisBillMst.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
            }
            reader.Close();

            return diagnosisBillMst;
        }

        internal decimal GetDiagnosisBillTotalNetPriceByDiagnosisBillMst(int id)
        {
            decimal netPrice = 0;
            string query = "SELECT * FROM DiagnosisBillDtl WHERE DiagnosisBillMstId='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader["PayableAmount"].ToString() != "")
                    {
                        netPrice += Convert.ToDecimal(reader["PayableAmount"]);
                    }
                }
            }
            reader.Close();
            return netPrice;
        }


    }
}