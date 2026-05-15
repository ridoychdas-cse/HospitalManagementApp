
using HospitalModels.Library.Billings;
using HospitalModels.Library.Billings.Op;
using HospitalRepository.Library.DataManagers;
using System;
using System.Collections.Generic;

namespace HospitalRepository.Library.Billings.OpBilling
{
    public class OpPaymentHisotyRepository
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        // diagnosis bill history by diagnosis bill mstId
        public List<OpMoneyReceiveMst> GetAllPaymentHistoryByDiagnosisBillMstId(int diagnosisMstId)
        {
            string query = "SELECT * FROM MoneyReceiveMst WHERE DiagnosisBillMstId='" + diagnosisMstId + "'";

            var moneyReceiveMstList = new List<OpMoneyReceiveMst>();

            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var moneyReceiveMst = new OpMoneyReceiveMst()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        PatientType = reader["PatientType"].ToString(),
                        DiagnosisBillMstId = Convert.ToInt32(reader["DiagnosisBillMstId"]),
                        PayAmount = Convert.ToDecimal(reader["PayAmount"]),
                        AdvanceAmount = Convert.ToDecimal(reader["AdvanceAmount"]),
                        SpecialDiscount = Convert.ToDecimal(reader["SpecialDiscount"])
                    };

                    moneyReceiveMstList.Add(moneyReceiveMst);
                }
            }
            reader.Close();
            return moneyReceiveMstList;
        }

        public List<DiagnosisBillDtl> GetAllDiagonisHistoryByDiagnosisBillMstId(int diagnosisMstId)
        {
            string query = "SELECT ROW_NUMBER() Over (Order by T1.iD) As [Serial],t1.DiagnosisTypeId,t2.Name as DigonosisType,t1.DiagnosisId,t3.Name as Particular,t1.PayableAmount as Amount FROM DiagnosisBillDtl t1 inner join DiagnosisTypes t2 on t1.DiagnosisTypeId=t2.Id inner join Diagnosis t3 on t1.DiagnosisId=t3.Id WHERE DiagnosisBillMstId='" + diagnosisMstId + "'";

            var diagnosisBillDtlList = new List<DiagnosisBillDtl>();

            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var diagnosisBillDtl = new DiagnosisBillDtl()
                    {
                        Serial = Convert.ToInt32(reader["Serial"]),
                        Particular = reader["Particular"].ToString(),
                        Amount = Convert.ToDecimal(reader["Amount"])
                    };

                    diagnosisBillDtlList.Add(diagnosisBillDtl);
                }
            }
            reader.Close();
            return diagnosisBillDtlList;
        }
     

        public OpMoneyReceiveDtl GetOpMoneyReceiveDtlsByMoneyReceiveMstId(int moneyReceiveMstId)
        {
            OpMoneyReceiveDtl moneyReceiveDtl = null;
            string query = "SELECT * FROM MoneyReceiveDtl WHERE MoneyReceiveMstId='" + moneyReceiveMstId + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    moneyReceiveDtl = new OpMoneyReceiveDtl()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        MoneyReceiveMstId = Convert.ToInt32(reader["MoneyReceiveMstId"]),
                        PayMethode = reader["PayMethode"].ToString(),
                        BankName = reader["BankName"].ToString(),
                        ChequeNo = reader["ChequeNo"].ToString(),
                        ChequerDate = reader["ChequeDate"].ToString(),
                        EntryDate = Convert.ToDateTime(reader["EntryDate"]),
                        MoneyReceiveBy = reader["MoneyReceiveBy"].ToString()
                    };
                }
            }
            reader.Close();
            return moneyReceiveDtl;
        }
    }
}
