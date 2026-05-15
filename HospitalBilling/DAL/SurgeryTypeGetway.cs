using System;
using System.Collections.Generic;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.DAL
{
    public class SurgeryTypeGetway
    {

        private readonly string _connectionString = DataManager.ConnectionString();

        internal int Save(SurgeryType aSurgeryType)
        {
            string query = "INSERT INTO SurgeryTypes VALUES ('" + aSurgeryType.Name + "', '" + aSurgeryType.ShortName + "', '" +
                           aSurgeryType.Description + "')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Update(SurgeryType aSurgeryType)
        {
            string query = "UPDATE SurgeryTypes SET Name='" + aSurgeryType.Name + "', ShortName='" +
                           aSurgeryType.ShortName + "', Description='" + aSurgeryType.Description + "' WHERE Id='" +
                           aSurgeryType.Id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Delete(int id)
        {
            string query = "DELETE FROM SurgeryTypes WHERE Id='" + id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

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
    }
}