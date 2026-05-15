using HospitalModels.Library.Billings;
using HospitalRepository.Library.DataManagers;
using System;
using System.Collections.Generic;

namespace HospitalRepository.Library.IpAssignDiagnosis
{
    public class IpAssignDiagnosisRepository
    {
     private readonly string _connectionString;
       // private readonly string _connectionString = DataManager.ConnectionString();

        public IpAssignDiagnosisRepository()
        {
            _connectionString = DataManager.ConnectionString();
        }

        // Save DiagnosisBillMst and DiagnosisBillDtl
        public int Save(DiagnosisBillMst diagnosisBillMst, List<DiagnosisBillDtl> diagnosisBillDtls)
        {
            int rowAffected = 0;
            string query1 = "INSERT INTO DiagnosisBillMst VALUES('" + diagnosisBillMst.PatientId + "', '" +
                            diagnosisBillMst.PatientType + "', '" + diagnosisBillMst.BillNo + "', '" + diagnosisBillMst.SpecialDiscount + "', '" +
                            diagnosisBillMst.Vat + "', '" + diagnosisBillMst.TotalPayableAmount + "', '" + diagnosisBillMst.EntryDate + "')";

            const string query2 = "SELECT TOP(1) Id FROM DiagnosisBillMst ORDER BY Id DESC";

            int mstId = DataManager.Transaction(_connectionString, query1, query2);

            foreach (var value in diagnosisBillDtls)
            {
                string query = "INSERT INTO DiagnosisBillDtl ( DiagnosisBillMstId, DiagnosisTypeId, DiagnosisId, Price, Discount, PayableAmount, DeliveryDate, ReferenceId) VALUES('" + mstId + "',  '" +
                               value.DiagnosisTypeId + "','" + value.DiagnosisId + "','" + value.Price + "', '" +
                               value.Discount + "', '" + value.PayableAmount + "', '" + value.DeliveryDate + "', '" + value.Reference + "')";
                rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            }
            if (rowAffected > 0)
            {
                return mstId;
            }
            return rowAffected;
        }

        // Auto BillNo
        public string GetAutoBillNumber()
        {
            string billNo = "";
            int idAutoIncrement = 0;
            string query = "SELECT TOP(1) BillNo FROM DiagnosisBillMst ORDER BY Id DESC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (!reader.HasRows)
            {
                billNo = "001";
            }
            else
            {
                reader.Read();
                idAutoIncrement = Convert.ToInt32(reader["BillNo"]);
                idAutoIncrement++;
                billNo = "00" + idAutoIncrement.ToString();
            }
            return billNo;
        }


        // Check Unique BillNo
        public string BillNoUniqueCheck(string billNo)
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
    }
}
