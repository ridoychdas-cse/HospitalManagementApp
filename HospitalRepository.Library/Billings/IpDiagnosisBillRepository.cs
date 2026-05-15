
using HospitalModels.Library.Billings;
using HospitalRepository.Library.DataManagers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalRepository.Library.Billings
{
    public class IpDiagnosisBillRepository
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


        // Get DiagnosisBill
        public MrParticular GetTotalDiagnosisBillByPatientIdAndPatientType(int patientId, string patientType)
        {
            var diagnosisBillMsts = GetDiagnosisBillMstsByPatientIdAndType(patientId, patientType);
            if (diagnosisBillMsts != null)
            {
                var totalBill = diagnosisBillMsts.Sum(c => c.TotalPayableAmount);

                var mrParticular = new MrParticular()
                {
                    Particular = "Diagnosis",
                    Amount = totalBill
                };
                return mrParticular;
            }
            return null;
        }
    }
}
