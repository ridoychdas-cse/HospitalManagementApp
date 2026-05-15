using System;
using System.Collections.Generic;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.DAL
{
    public class ReportDiagnosisBillGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal List<DiagnosisBillDtl> GetDiagnoisBillDtlsListByBillNo(int id,string patientType, string billNo)
        {
            // Get DiagnosisBillMst Table Id
            int diagnosisBillMstId = 0;
            var diagnosisBillMstIdList=new List<int>();

            string query = "SELECT Id FROM DiagnosisBillMst WHERE PatientId='" + id + "' AND PatientType='"+patientType+"'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    diagnosisBillMstId = Convert.ToInt32(reader["Id"]);

                    diagnosisBillMstIdList.Add(diagnosisBillMstId);
                }
            }

            // get DiagnsisibillDtl

            DiagnosisBillDtl aDiagnosisBillDtl = null;
            var diagnosisBillDtlsList = new List<DiagnosisBillDtl>();

            if (diagnosisBillMstId==0)
            {
                throw new Exception();
            }
            else
            {
                foreach (var mstId in diagnosisBillMstIdList)
                {
                    query =
                    "select dbo.Diagnosis.Name AS DiagnosisName, dbo.DiagnosisBillDtl.DeliveryDate, dbo.DiagnosisBillDtl.TotalPrice From dbo.DiagnosisBillDtl inner join dbo.Diagnosis ON dbo.DiagnosisBillDtl.DiagnosisId=dbo.Diagnosis.Id WHERE dbo.DiagnosisBillDtl.MstId='" +
                    mstId + "' AND dbo.DiagnosisBillDtl.BillingId='" + billNo + "' ";

                    reader = DataManager.SqlDataReader(query, _connectionString);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            aDiagnosisBillDtl = new DiagnosisBillDtl();
                            aDiagnosisBillDtl.DianosisName = reader["DiagnosisName"].ToString();
                            aDiagnosisBillDtl.DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]);
                            aDiagnosisBillDtl.TotalPrice = Convert.ToDecimal(reader["TotalPrice"]);

                            diagnosisBillDtlsList.Add(aDiagnosisBillDtl);
                        }
                    }
                    reader.Close();
                }
                
            }
            return diagnosisBillDtlsList;
        }

        internal DiagnosisBillMr GetDiagnosisBillMrByBillNo(string billNo)
        {
            DiagnosisBillMr aDiagnosisBillMr=null;

            string query = "SELECT * FROM DiagnosisBillMr WHERE BillingId='" + billNo + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aDiagnosisBillMr=new DiagnosisBillMr();
                aDiagnosisBillMr.TotalNetPrice = Convert.ToDecimal(reader["TotalNetPrice"]);
                aDiagnosisBillMr.Discount = Convert.ToDecimal(reader["Discount"]);
                aDiagnosisBillMr.Vat = Convert.ToDecimal(reader["Vat"]);
                aDiagnosisBillMr.TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]);
                aDiagnosisBillMr.TotalPayAmount = Convert.ToDecimal(reader["TotalPayAmount"]);
                decimal due = aDiagnosisBillMr.TotalPayableAmount - aDiagnosisBillMr.TotalPayAmount;
                aDiagnosisBillMr.DueAmount = due;
            }
            reader.Close();
            return aDiagnosisBillMr;
        }
    }
}