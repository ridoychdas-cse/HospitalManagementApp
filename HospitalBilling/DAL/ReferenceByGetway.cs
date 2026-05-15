using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;

namespace HospitalBilling.DAL
{
    public class ReferenceByGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal int Save(ReferenceBy aReferenceBy)
        {
            string query = "INSERT INTO ReferenceBy VALUES ('" + aReferenceBy.Name + "', '" + true + "')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Update(ReferenceBy aReferenceBy)
        {
            string query = "UPDATE ReferenceBy SET Name='" + aReferenceBy.Name + "' WHERE Id='" +
                           aReferenceBy.Id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Delete(int id)
        {
            string query = "UPDATE ReferenceBy SET Status='" + false + "' WHERE Id='" + id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

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
    }
}