using HospitalModels.Library.Billings;
using HospitalModels.Library.Billings.Op;
using HospitalRepository.Library.DataManagers;
using HospitalRepository.Library.Enum;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HospitalRepository.Library.Billings.OpBilling
{
    public class OpMoneyReceiveRepository
    {
        private readonly string _connectionString = DataManager.ConnectionString();


        #region Money Receive Info Save
        // MoneyReceiveMstSave
        public int MoneyReceiveMstSave(OpMoneyReceiveMst moneyReceiveMst)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@intPatientId", moneyReceiveMst.PatientId));
            parameters.Add(new SqlParameter("@strPatientType", moneyReceiveMst.PatientType));
            parameters.Add(new SqlParameter("@intDiagnosisBillMstId", moneyReceiveMst.DiagnosisBillMstId));
            parameters.Add(new SqlParameter("@dcmlPayAmount", moneyReceiveMst.AdvanceAmount));
            parameters.Add(new SqlParameter("@dcmlAdvanceAmount", moneyReceiveMst.AdvanceAmount));
            parameters.Add(new SqlParameter("@dcmlSpecialDiscount", moneyReceiveMst.SpecialDiscount));


            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveMoneyReceiveMst]", parameters.ToArray(), _connectionString);


        }
        //moneyReceiveDtl by Cash
        public int MoneyReceiveDtlSaveByCash(OpMoneyReceiveDtl moneyReceiveDtl)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveMoneyReceiveDtl]", LoadParametersInputData(moneyReceiveDtl, ActionType.Save), _connectionString);
        }
        // moneyReceiveDtl by Bank
        public int MoneyReceiveDtlSaveByBank(OpMoneyReceiveDtl moneyReceiveDtl)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveMoneyReceiveDtl]", LoadParametersInputData(moneyReceiveDtl, ActionType.Save), _connectionString);
        }

        internal SqlParameter[] LoadParametersInputData(OpMoneyReceiveDtl moneyReceiveDtl, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (actionType == ActionType.Save)
            {
                parameters.Add(new SqlParameter("@intMoneyReceiveMstId", moneyReceiveDtl.MoneyReceiveMstId));
                parameters.Add(new SqlParameter("@strPayMethode", moneyReceiveDtl.PayMethode));
                parameters.Add(new SqlParameter("@strBankName", moneyReceiveDtl.BankName));
                parameters.Add(new SqlParameter("@strChequeNo", moneyReceiveDtl.ChequeNo));
                parameters.Add(new SqlParameter("@dtmChequeDate", moneyReceiveDtl.ChequerDate));
                parameters.Add(new SqlParameter("@dtmEntryDate", moneyReceiveDtl.EntryDate));
                parameters.Add(new SqlParameter("@strMoneyReceiveBy", moneyReceiveDtl.MoneyReceiveBy));
            }

            return parameters.ToArray();
        }
        #endregion

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
