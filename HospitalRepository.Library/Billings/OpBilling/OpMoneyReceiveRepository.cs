using HospitalModels.Library.Billings.Op;
using HospitalRepository.Library.DataManagers;
using System.Data;
using System.Data.SqlClient;

namespace HospitalRepository.Library.Billings.OpBilling
{
    public class OpMoneyReceiveRepository
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        // MoneyReceiveMstSave
        public int MoneyReceiveMstSave(OpMoneyReceiveMst moneyReceiveMst)
        {
            string query =
                "INSERT INTO MoneyReceiveMst(PatientId, PatientType, DiagnosisBillMstId, PayAmount, AdvanceAmount, SpecialDiscount) VALUES('" +
                moneyReceiveMst.PatientId + "', '" + moneyReceiveMst.PatientType + "', '" + moneyReceiveMst.DiagnosisBillMstId + "', '" + moneyReceiveMst.PayAmount +
                "', '" + moneyReceiveMst.AdvanceAmount + "', '" + moneyReceiveMst.SpecialDiscount + "')";

            const string query2 = "SELECT TOP(1) Id FROM MoneyReceiveMst ORDER BY Id DESC";

            return DataManager.Transaction(_connectionString, query, query2);
        }


        //moneyReceiveDtl by Cash
        public int MoneyReceiveDtlSaveByCash(OpMoneyReceiveDtl moneyReceiveDtl)
        {
            string query =
                "INSERT INTO MoneyReceiveDtl(MoneyReceiveMstId, PayMethode, EntryDate, MoneyReceiveBy) VALUES('" +
                moneyReceiveDtl.MoneyReceiveMstId + "', '" + moneyReceiveDtl.PayMethode + "', '" +
                moneyReceiveDtl.EntryDate + "', '" + moneyReceiveDtl.MoneyReceiveBy + "')";

            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        // moneyReceiveDtl by Bank
        public int MoneyReceiveDtlSaveByBank(OpMoneyReceiveDtl moneyReceiveDtl)
        {
            string query =
                "INSERT INTO MoneyReceiveDtl(MoneyReceiveMstId, PayMethode, EntryDate, MoneyReceiveBy, BankName, ChequeNo, ChequeDate) VALUES('" +
                moneyReceiveDtl.MoneyReceiveMstId + "', '" + moneyReceiveDtl.PayMethode + "', '" +
                moneyReceiveDtl.EntryDate + "', '" + moneyReceiveDtl.MoneyReceiveBy + "', '" + moneyReceiveDtl.BankName +
                "', '" + moneyReceiveDtl.ChequeNo + "', '" + moneyReceiveDtl.ChequerDate + "')";

            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        public DataTable Input(string SerchName)
        {
            string connectionString = DataManager.ConnectionString();
            SqlConnection sqlCon = new SqlConnection(connectionString);
            string query = "Select  PatientId,Name from [OutdoorPatients] where PatientId+'-'+Name ='" + SerchName + "'";
            DataTable dt = DataManager.ExecuteQuery(connectionString, query, "[OutdoorPatients]");
            return dt;
        }
    }
}
