using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class ReferenceByGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        #region ReferenceBy Info Insert Update Delete
        internal int Save(ReferenceBy aReferenceBy)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveReferenceBy]", LoadParametersInputData(aReferenceBy, ActionType.Save), _connectionString);
        }
        internal int Update(ReferenceBy aReferenceBy)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateReferenceBy]", LoadParametersInputData(aReferenceBy, ActionType.Update), _connectionString);

        }
        internal int Delete(ReferenceBy aReferenceBy)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteReferenceBy]", LoadParametersInputData(aReferenceBy, ActionType.Delete), _connectionString);

        }

        internal SqlParameter[] LoadParametersInputData(ReferenceBy aReferenceBy, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aReferenceBy.Id));
            }

            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", aReferenceBy.Name));
            }

            return parameters.ToArray();
        }
        #endregion

        #region ReferenceBy Info Get
        internal List<ReferenceBy> GetAllReferenceByList()
        {
            ReferenceBy aReferenceBy = null;
            var referenceByList = new List<ReferenceBy>();

            string query = "SELECT * FROM ReferenceBy WHERE Status='" + true + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aReferenceBy = new ReferenceBy
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString()
                    };

                    referenceByList.Add(aReferenceBy);
                }
            }
            reader.Close();
            return referenceByList;
        }
        internal ReferenceBy GetReferenceById(int id)
        {
            ReferenceBy aReferenceBy = null;

            string query = "SELECT * FROM ReferenceBy WHERE Id='" + id + "' and Status='" + true + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aReferenceBy = new ReferenceBy();
                aReferenceBy.Id = Convert.ToInt32(reader["Id"]);
                aReferenceBy.Name = reader["Name"].ToString();
            }
            reader.Close();
            return aReferenceBy;
        }
        internal ReferenceBy GetReferanceByName(string name)
        {
            ReferenceBy aReferenceBy = null;

            string query = "SELECT * FROM ReferenceBy WHERE Name='" + name + "' and Status='" + true + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aReferenceBy = new ReferenceBy();
                aReferenceBy.Id = Convert.ToInt32(reader["Id"]);
                aReferenceBy.Name = reader["Name"].ToString();
            }
            reader.Close();
            return aReferenceBy;
        }
        internal int SaveAndGetId(ReferenceBy referenceBy)
        {
            string query = "INSERT INTO ReferenceBy VALUES ('" + referenceBy.Name + "', '" + true + "')";

            string query2 = "SELECT TOP(1) Id FROM ReferenceBy ORDER BY Id DESC";

            int id = DataManager.Transaction(_connectionString, query, query2);
            return id;
        }
        #endregion
    }
}