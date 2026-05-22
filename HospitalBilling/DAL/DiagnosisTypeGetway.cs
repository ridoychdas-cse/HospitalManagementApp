using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class DiagnosisTypeGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        #region Diagnosistype Setup Save, Update, Delete
        internal int Save(DiagnosisType aDiagnosisType)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveDiagnosisTypeInfo]", LoadParametersInputData(aDiagnosisType, ActionType.Save), _connectionString);

        }
        internal int Update(DiagnosisType aDiagnosisType)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateDiagnosisTypeInfo]", LoadParametersInputData(aDiagnosisType, ActionType.Update), _connectionString);

        }
        internal int Delete(DiagnosisType aDiagnosisType)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteDiagnosisTypeInfoById]", LoadParametersInputData(aDiagnosisType, ActionType.Delete), _connectionString);
        }

        internal SqlParameter[] LoadParametersInputData(DiagnosisType aDiagnosisType, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aDiagnosisType.Id));
            }

            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", aDiagnosisType.Name));
                parameters.Add(new SqlParameter("@strShortName", aDiagnosisType.ShortName));
                parameters.Add(new SqlParameter("@strDetails", aDiagnosisType.Details));
            }

            return parameters.ToArray();
        }
        #endregion

        #region DiagnosisType info get
        internal List<DiagnosisType> GetAllDiagnosisTypes()
        {
            DiagnosisType aDiagnosisType = null;
            var diagnosisList = new List<DiagnosisType>();

            string query = "SELECT * FROM DiagnosisTypes ORDER BY Name ASC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aDiagnosisType = new DiagnosisType();
                    aDiagnosisType.Id = Convert.ToInt32(reader["Id"]);
                    aDiagnosisType.Name = reader["Name"].ToString();
                    aDiagnosisType.ShortName = reader["ShortName"].ToString();
                    aDiagnosisType.Details = reader["Details"].ToString();

                    diagnosisList.Add(aDiagnosisType);
                }
            }
            reader.Close();
            return diagnosisList;
        }
        // search
        internal List<DiagnosisType> GetDiagnosisTypeByNameOrShortName(string searchInput)
        {
            DiagnosisType aDiagnosisType = null;
            var diagnosisList = new List<DiagnosisType>();

            string query = "SELECT * FROM DiagnosisTypes WHERE Name LIKE '%" + searchInput + "%' Or ShortName LIKE '%" + searchInput + "%' Order By Name ASC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aDiagnosisType = new DiagnosisType();
                    aDiagnosisType.Id = Convert.ToInt32(reader["Id"]);
                    aDiagnosisType.Name = reader["Name"].ToString();
                    aDiagnosisType.ShortName = reader["ShortName"].ToString();
                    aDiagnosisType.Details = reader["Details"].ToString();

                    diagnosisList.Add(aDiagnosisType);
                }
            }
            reader.Close();
            return diagnosisList;
        }
        internal DiagnosisType GetDiagnosisTypeById(int id)
        {
            DiagnosisType aDiagnosisType = null;

            string query = "SELECT * FROM DiagnosisTypes WHERE Id='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aDiagnosisType = new DiagnosisType();
                aDiagnosisType.Id = Convert.ToInt32(reader["Id"]);
                aDiagnosisType.Name = reader["Name"].ToString();
                aDiagnosisType.ShortName = reader["ShortName"].ToString();
                aDiagnosisType.Details = reader["Details"].ToString();

            }
            reader.Close();
            return aDiagnosisType;
        }
        internal DiagnosisType GetDiagnosisTypeByName(string name)
        {
            DiagnosisType aDiagnosisType = null;

            string query = "SELECT * FROM DiagnosisTypes WHERE Name='" + name + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aDiagnosisType = new DiagnosisType();
                aDiagnosisType.Id = Convert.ToInt32(reader["Id"]);
                aDiagnosisType.Name = reader["Name"].ToString();
                aDiagnosisType.ShortName = reader["ShortName"].ToString();
                aDiagnosisType.Details = reader["Details"].ToString();

            }
            reader.Close();
            return aDiagnosisType;
        }
        public DataTable GetAllUOM()
        {
            var connnection = DataManager.ConnectionString();
            string query = "select * from UOM where active =1";
            DataTable data=DataManager.ExecuteQuery(connnection,query,"UOM");
            return data;
        }
        #endregion
    }
}