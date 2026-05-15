using HospitalModels.Library.Billings;
using HospitalRepository.Library.DataManagers;
using System;
using System.Collections.Generic;

namespace HospitalRepository.Library.Billings.OpBilling
{
    public class OpDiagnosisBillRepository
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        public List<DiagnosisBillMst> GetDiagnosisBillMstsByPatientIdAndType(int patientId, string patientType)
        {
            var diagnosisBillMstList = new List<DiagnosisBillMst>();

            string query = "SELECT * FROM DiagnosisBillMst WHERE PatientId='" + patientId + "' AND PatientType='" + patientType + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var diagnosisBillMst = new DiagnosisBillMst()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = reader["PatientId"].ToString(),
                        PatientType = reader["PatientType"].ToString(),
                        BillNo = reader["BillNo"].ToString(),
                        SpecialDiscount = Convert.ToDecimal(reader["SpecialDiscount"]),
                        Vat = Convert.ToDecimal(reader["Vat"]),
                        TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]),
                        EntryDate = Convert.ToDateTime(reader["EntryDate"])
                    };


                    diagnosisBillMstList.Add(diagnosisBillMst);
                }
            }
            reader.Close();
            return diagnosisBillMstList;
        }


        public DiagnosisBillMst GetDiagnosisBillMstsById(int id)
        {
            DiagnosisBillMst diagnosisBillMst = null;

            string query = "SELECT * FROM DiagnosisBillMst WHERE Id='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    diagnosisBillMst = new DiagnosisBillMst()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = reader["PatientId"].ToString(),
                        PatientType = reader["PatientType"].ToString(),
                        BillNo = reader["BillNo"].ToString(),
                        SpecialDiscount = Convert.ToDecimal(reader["SpecialDiscount"]),
                        Vat = Convert.ToDecimal(reader["Vat"]),
                        TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]),
                        EntryDate = Convert.ToDateTime(reader["EntryDate"])
                    };
                }
            }
            reader.Close();
            return diagnosisBillMst;
        }

        public DiagnosisBillMst GetDiagnosisBillMstsByBillNo(string billNo)
        {
            DiagnosisBillMst diagnosisBillMst = null;

            string query = "SELECT * FROM DiagnosisBillMst WHERE BillNo='" + billNo + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    diagnosisBillMst = new DiagnosisBillMst()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = reader["PatientId"].ToString(),
                        PatientType = reader["PatientType"].ToString(),
                        BillNo = reader["BillNo"].ToString(),
                        SpecialDiscount = Convert.ToDecimal(reader["SpecialDiscount"]),
                        Vat = Convert.ToDecimal(reader["Vat"]),
                        TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]),
                        EntryDate = Convert.ToDateTime(reader["EntryDate"])
                    };
                }
            }
            reader.Close();
            return diagnosisBillMst;
        }


        public List<DiagnosisBillDtl> GetDiagnosisBillDtlsByMstId(int diagnosisBillMstId)
        {
            var diagnosisBilldtlList = new List<DiagnosisBillDtl>();

            string query = "select t1.Id, t1.DiagnosisBillMstId, t1.DiagnosisTypeId, t2.Name as DiagnosisTypeName, t1.DiagnosisId, t3.Name as DiagnosisName, t1.Price, t1.Discount, t1.PayableAmount, t1.DeliveryDate from DiagnosisBillDtl as t1 left join DiagnosisTypes as t2 on t1.DiagnosisTypeId=t2.Id left join Diagnosis as t3 on t1.DiagnosisId=t3.Id WHERE t1.DiagnosisBillMstId='" + diagnosisBillMstId + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var diagnosisBillMst = new DiagnosisBillDtl()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        DiagnosisBillMstId = Convert.ToInt32(reader["DiagnosisBillMstId"]),
                        DiagnosisTypeId = Convert.ToInt32(reader["DiagnosisTypeId"]),
                        DiagnosisId = Convert.ToInt32(reader["DiagnosisId"]),
                        Price = Convert.ToInt32(reader["Price"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        PayableAmount = Convert.ToDecimal(reader["PayableAmount"]),
                        DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"])
                    };
                    diagnosisBilldtlList.Add(diagnosisBillMst);
                }
            }
            reader.Close();
            return diagnosisBilldtlList;
        }





    }
}
