using HospitalBilling.BLL;
using HospitalBilling.Models.MoneyReceives;
using System;

namespace HospitalBilling.DAL.MoneyReceives
{
    public class MoneyRecieveGetway
    {
        private readonly string _connectionString;

        public MoneyRecieveGetway()
        {
            _connectionString = DataManager.ConnectionString();
        }

        // first time payment
        public int MoneySave(MoneyReceiveMst moneyReceiveMst, MoneyReceiveDtl moneyReceiveDtl)
        {
            string query = "INSERT INTO MoneyReceiveMst VALUES('" + moneyReceiveMst.PatientId + "', '" +
                           moneyReceiveMst.PatientType + "', '" + moneyReceiveMst.PaymentType + "', '" +
                           moneyReceiveMst.DiagnosisBillMstId + "', '" + moneyReceiveMst.PayAmount + "', '" +
                           moneyReceiveMst.AdvanceAmount + "', '" + moneyReceiveMst.Particulars + "', '" +
                           moneyReceiveMst.SpecialDiscount + "', '" + moneyReceiveMst.Vat + "')";

            const string query2 = "SELECT TOP(1) Id FROM MoneyReceiveMst ORDER BY Id DESC";
            int mstId = DataManager.Transaction(_connectionString, query, query2);


            if (mstId > 0)
            {
                query = "INSERT INTO MoneyReceiveDtl VALUES('" + mstId + "', '" + moneyReceiveDtl.PayMethode + "', '" +
                        moneyReceiveDtl.BankName + "', '" + moneyReceiveDtl.ChequeNo + "', '" +
                        moneyReceiveDtl.ChequeDate + "','" + moneyReceiveDtl.EntryDate + "')";

                return DataManager.ExecuteNonQuery(query, _connectionString);
            }

            return 0;
        }

        // Outdoor Patient Money Receive Save
        public int OutdoorPatientMoneyReceiveSave(OpMoneyReceiveMst opMoneyReceiveMst,
            OpMoneyReceiveDtl opMoneyReceiveDtl)
        {
            string query =
                "INSERT INTO MoneyReceiveMst(PatientId, PatientType, DiagnosisBillMstId, PayAmount) VALUES('" +
                opMoneyReceiveMst.PatientId + "', '" + opMoneyReceiveMst.PatientType + "',  '"
                + opMoneyReceiveMst.DiagnosisBillMstId + "', '" + opMoneyReceiveMst.PayAmount + "')";

            const string query2 = "SELECT TOP(1) Id FROM MoneyReceiveMst ORDER BY Id DESC";
            int mstId = DataManager.Transaction(_connectionString, query, query2);


            if (mstId > 0)
            {
                query = "INSERT INTO MoneyReceiveDtl VALUES('" + mstId + "', '" + opMoneyReceiveDtl.PayMethode + "', '" +
                        opMoneyReceiveDtl.BankName + "', '" + opMoneyReceiveDtl.ChequeNo + "', '" +
                        opMoneyReceiveDtl.ChequeDate + "','" + opMoneyReceiveDtl.EntryDate + "')";

                return DataManager.ExecuteNonQuery(query, _connectionString);
            }

            return 0;
        }


        // Get Outdoor Patient Diagnosis Bill By DiagnosisBillMstId
        public decimal GetOpDiagnosisPaymentByDiagnosisMstId(int diagnosisBillMstId)
        {
            int sum = 0;

            string query = "SELECT * FROM MoneyReceiveMst Where DiagnosisBillMstId='" + diagnosisBillMstId + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int payamount = Convert.ToInt32(reader["PayAmount"]);
                    sum += payamount;
                }
            }
            return sum;
        }


        // Indoor patient Bill
        public decimal GetIpPaymentHistoryByPatientId(int patientId, string patientType)
        {
            string query = "SELECT * FROM MoneyReceiveMst WHERE PatientId='" + patientId + "' and PatientType='" +
                           patientType + "'";

            int totalPayment = 0;
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int payAmount = Convert.ToInt32(reader["PayAmount"]);
                    int advanceAmount = Convert.ToInt32(reader["AdvanceAmount"]);
                    int specialDiscount = Convert.ToInt32(reader["SpecialDiscount"]);

                    totalPayment += (payAmount + advanceAmount + specialDiscount);
                }
            }
            return totalPayment;
        }


        internal int IpMoneyReceiveFormDiagnosisSave(UI.IpMoneyReceiveMst ipMoneyReceiveMst, OpMoneyReceiveDtl moneyReceiveDtl)
        {
            string query = "INSERT INTO MoneyReceiveMst(PatientId, PatientType, PaymentType, PayAmount, AdvanceAmount, SpecialDiscount, IPVat) VALUES('" + ipMoneyReceiveMst.PatientId + "', '" +
                           ipMoneyReceiveMst.PatientType + "', '" + ipMoneyReceiveMst.PaymentType + "', '" +
                           ipMoneyReceiveMst.PayAmount + "', '" +
                           ipMoneyReceiveMst.AdvanceAmount + "', '" +
                           ipMoneyReceiveMst.SpecialDiscount + "', '" + ipMoneyReceiveMst.IPVat + "')";

            const string query2 = "SELECT TOP(1) Id FROM MoneyReceiveMst ORDER BY Id DESC";
            int mstId = DataManager.Transaction(_connectionString, query, query2);


            if (mstId > 0)
            {
                query = "INSERT INTO MoneyReceiveDtl VALUES('" + mstId + "', '" + moneyReceiveDtl.PayMethode + "', '" +
                        moneyReceiveDtl.BankName + "', '" + moneyReceiveDtl.ChequeNo + "', '" +
                        moneyReceiveDtl.ChequeDate + "','" + moneyReceiveDtl.EntryDate + "')";

                return DataManager.ExecuteNonQuery(query, _connectionString);
            }

            return 0;
        }


        // special discount indoor patient
        internal int SaveSpecialDiscount(int patientId, string patientType, decimal specialDiscount)
        {
            string query =
                "INSERT INTO MoneyReceiveMst(PatientId, PatientType, PayAmount, AdvanceAmount, SpecialDiscount, IPVat) VALUES('" +
                patientId + "', '" + patientType + "', '" + 0 + "', '" + 0 + "', '" + specialDiscount + "', '" + 0 +
                "')";

            return DataManager.ExecuteNonQuery(query, _connectionString);
        }
    }
}