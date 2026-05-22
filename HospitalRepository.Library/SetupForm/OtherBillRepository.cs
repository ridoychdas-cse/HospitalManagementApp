using HospitalModels.Library.Billings;
using HospitalRepository.Library.DataManagers;
using HospitalRepository.Library.Enum;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalRepository.Library.SetupForm
{
    public class OtherBillRepository
    {
        private readonly string _connectionString = DataManagers.DataManager.ConnectionString();

        #region Other Bill Info Insert Update Delete
        public int Save(OtherBillType otherBill)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveOtherBillType]", LoadParametersInputData(otherBill, ActionType.Save), _connectionString);

        }
        public int Update(OtherBillType otherBill)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateOtherBillType]", LoadParametersInputData(otherBill, ActionType.Update), _connectionString);

        }
        public int Delete(OtherBillType otherBill)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteOtherBillType]", LoadParametersInputData(otherBill, ActionType.Delete), _connectionString);

        }
        internal SqlParameter[] LoadParametersInputData(OtherBillType otherBill, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", otherBill.Id));
            }

            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", otherBill.Name));
                parameters.Add(new SqlParameter("@blnStatus", otherBill.Status));
            }

            return parameters.ToArray();
        }

        #endregion

        #region Other Bill Info Get
        public IEnumerable<OtherBillType> GetAllOtherBills()
        {
            var billList = new List<OtherBillType>();

            string query = "SELECT * FROM OtherBillType WHERE Status='" + true + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var otherBill = new OtherBillType()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Status = Convert.ToBoolean(reader["Status"])
                    };

                    billList.Add(otherBill);
                }
            }
            reader.Close();
            return billList;
        }
        public OtherBillType GetOtherBillById(int id)
        {
            OtherBillType otherBill = null;

            string query = "SELECT * FROM OtherBillType WHERE Status='" + true + "' AND Id='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                otherBill = new OtherBillType()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Status = Convert.ToBoolean(reader["Status"])
                };


            }
            reader.Close();
            return otherBill;
        }
        #endregion
    }
}
