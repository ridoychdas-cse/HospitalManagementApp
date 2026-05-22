using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class SurgeryTypeGetway
    {

        private readonly string _connectionString = DataManager.ConnectionString();

        #region SurgeryType Info Save,Update,Delete
        internal int Save(SurgeryType aSurgeryType)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveSurgeryTypeInfo]", LoadParametersInputData(aSurgeryType, ActionType.Save), _connectionString);

        }
        internal int Update(SurgeryType aSurgeryType)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateSurgeryTypeInfo]", LoadParametersInputData(aSurgeryType, ActionType.Update), _connectionString);

        }
        internal int Delete(SurgeryType aSurgeryType)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteSurgeryTypeInfoById]", LoadParametersInputData(aSurgeryType, ActionType.Delete), _connectionString);

        }
        internal SqlParameter[] LoadParametersInputData(SurgeryType aSurgeryType, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aSurgeryType.Id));
            }

            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", aSurgeryType.Name));
                parameters.Add(new SqlParameter("@strShortName", aSurgeryType.ShortName));
                parameters.Add(new SqlParameter("@strDescription", aSurgeryType.Description));
            }

            return parameters.ToArray();
        }
        #endregion

        #region SurgeryType Info Get
        internal List<SurgeryType> GetAllSurgeryTypesList()
        {
            SurgeryType aSurgeryType=null;
            var surgeryList = new List<SurgeryType>();

            string query = "SELECT * FROM SurgeryTypes";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aSurgeryType = new SurgeryType();
                    aSurgeryType.Id = Convert.ToInt32(reader["Id"]);
                    aSurgeryType.Name = reader["Name"].ToString();
                    aSurgeryType.ShortName = reader["ShortName"].ToString();
                    aSurgeryType.Description = reader["Description"].ToString();

                    surgeryList.Add(aSurgeryType);
                }
            }
            reader.Close();
            return surgeryList;
        }
        internal SurgeryType GetSurgeryTypesById(int id)
        {
            SurgeryType aSurgeryType = null;

            string query = "SELECT * FROM SurgeryTypes WHERE Id='"+id+"'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aSurgeryType = new SurgeryType();
                aSurgeryType.Id = Convert.ToInt32(reader["Id"]);
                aSurgeryType.Name = reader["Name"].ToString();
                aSurgeryType.ShortName = reader["ShortName"].ToString();
                aSurgeryType.Description = reader["Description"].ToString();
            }
            reader.Close();
            return aSurgeryType;
        }
        internal SurgeryType GetSurgeryTypesByName(string name)
        {
            SurgeryType aSurgeryType = null;

            string query = "SELECT * FROM SurgeryTypes WHERE Name='" + name + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aSurgeryType = new SurgeryType();
                aSurgeryType.Id = Convert.ToInt32(reader["Id"]);
                aSurgeryType.Name = reader["Name"].ToString();
                aSurgeryType.ShortName = reader["ShortName"].ToString();
                aSurgeryType.Description = reader["Description"].ToString();
            }
            reader.Close();
            return aSurgeryType;
        }
        #endregion
    }
}