using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace HospitalBilling.DAL
{
    public class DiagnosisTypeGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal int Save(DiagnosisType aDiagnosisType)
        {
            string query = "INSERT INTO DiagnosisTypes VALUES('" + aDiagnosisType.Name + "', '" +
                           aDiagnosisType.ShortName + "', '" + aDiagnosisType.Details + "')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Update(DiagnosisType aDiagnosisType)
        {
            string query = "UPDATE DiagnosisTypes SET Name='" + aDiagnosisType.Name + "', ShortName='" +
                           aDiagnosisType.ShortName + "', Details='" + aDiagnosisType.Details + "' WHERE Id='" +
                           aDiagnosisType.Id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Delete(int id)
        {
            string query = "DELETE FROM DiagnosisTypes WHERE Id='" + id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

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
    }
}