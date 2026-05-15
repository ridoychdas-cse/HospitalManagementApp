using System;
using System.Collections.Generic;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.DAL
{
    public class SurgeryGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal int Save(Surgery aSurgery)
        {
            string query = "INSERT INTO Surgerys VALUES('" + aSurgery.Name + "', '" + aSurgery.ShortName + "', '" +
                           aSurgery.SurgeryTypeId + "', '" + aSurgery.RegularFee + "', '" + aSurgery.Discount + "', '" +
                           aSurgery.TotalFee + "', '" + aSurgery.Description + "')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Update(Surgery aSurgery)
        {
            string query = "UPDATE Surgerys SET Name='" + aSurgery.Name + "', ShortName='" + aSurgery.ShortName +
                           "', SurgeryTypeId='" + aSurgery.SurgeryTypeId + "', RegularFee='" + aSurgery.RegularFee +
                           "', Discount='" + aSurgery.Discount + "', TotalFee='" + aSurgery.TotalFee +
                           "', Description='" + aSurgery.Description + "' WHERE Id='" + aSurgery.Id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Delete(int id)
        {
            string query = "DELETE FROM Surgerys WHERE Id='" + id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal List<Surgery> GetAllSurgeryList()
        {
            Surgery aSurgery = null;
            var surgeryList = new List<Surgery>();

            string query =
                "select t1.Id, t1.Name, t1.ShortName, t1.SurgeryTypeId, t2.Name as [SurgeryTypeName], t1.RegularFee, t1.Discount, t1.TotalFee, t1.Description from Surgerys as t1 inner join SurgeryTypes as t2 on t2.Id=t1.SurgeryTypeId ";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aSurgery = new Surgery();

                    aSurgery.Id = Convert.ToInt32(reader["Id"]);
                    aSurgery.Name = reader["Name"].ToString();
                    aSurgery.ShortName = reader["ShortName"].ToString();
                    aSurgery.SurgeryTypeId = Convert.ToInt32(reader["SurgeryTypeId"]);
                    aSurgery.SurgeryTypeName = reader["SurgeryTypeName"].ToString();
                    aSurgery.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                    aSurgery.Discount = Convert.ToInt32(reader["Discount"]);
                    aSurgery.TotalFee = Convert.ToDecimal(reader["TotalFee"]);
                    aSurgery.Description = reader["Description"].ToString();

                    surgeryList.Add(aSurgery);
                }
            }
            reader.Close();
            return surgeryList;
        }
        internal List<Surgery> GetAllSurgeryList(int surgeryTypeId)
        {
            Surgery aSurgery = null;
            var surgeryList = new List<Surgery>();

            string query =
                "select t1.Id, t1.Name, t1.ShortName, t1.SurgeryTypeId, t2.Name as [SurgeryTypeName], t1.RegularFee, t1.Discount, t1.TotalFee, t1.Description from Surgerys as t1 inner join SurgeryTypes as t2 on t2.Id=t1.SurgeryTypeId where t1.SurgeryTypeId='" + surgeryTypeId + "' ";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aSurgery = new Surgery();

                    aSurgery.Id = Convert.ToInt32(reader["Id"]);
                    aSurgery.Name = reader["Name"].ToString();
                    aSurgery.ShortName = reader["ShortName"].ToString();
                    aSurgery.SurgeryTypeId = Convert.ToInt32(reader["SurgeryTypeId"]);
                    aSurgery.SurgeryTypeName = reader["SurgeryTypeName"].ToString();
                    aSurgery.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                    aSurgery.Discount = Convert.ToInt32(reader["Discount"]);
                    aSurgery.TotalFee = Convert.ToDecimal(reader["TotalFee"]);
                    aSurgery.Description = reader["Description"].ToString();

                    surgeryList.Add(aSurgery);
                }
            }
            reader.Close();
            return surgeryList;
        }

        internal Surgery GetSurgeryById(int id)
        {
            Surgery aSurgery = null;

            string query =
                "select t1.Id, t1.Name, t1.ShortName, t1.SurgeryTypeId, t2.Name as [SurgeryTypeName], t1.RegularFee, t1.Discount, t1.TotalFee, t1.Description from Surgerys as t1 inner join SurgeryTypes as t2 on t2.Id=t1.SurgeryTypeId WHERE t1.Id='" +
                id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aSurgery=new Surgery();

                aSurgery.Id = Convert.ToInt32(reader["Id"]);
                aSurgery.Name = reader["Name"].ToString();
                aSurgery.ShortName = reader["ShortName"].ToString();
                aSurgery.SurgeryTypeId = Convert.ToInt32(reader["SurgeryTypeId"]);
                aSurgery.SurgeryTypeName = reader["SurgeryTypeName"].ToString();
                aSurgery.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                aSurgery.Discount = Convert.ToInt32(reader["Discount"]);
                aSurgery.TotalFee = Convert.ToDecimal(reader["TotalFee"]);
                aSurgery.Description = reader["Description"].ToString();
            }
            reader.Close();
            return aSurgery;
        }

        internal List<Surgery> GetSurgeryByTypeId(int surgeryTypeId)
        {
            Surgery aSurgery = null;
            var surgeryList = new List<Surgery>();

            string query =
                "SELECT t1.Id, t1.Name, t1.SurgeryTypeId, t2.Name AS SurgeryTypeName, t1.Description, t1.RegularFee, t1.Discount, t1.TotalFee FROM dbo.Surgerys AS t1 Left JOIN SurgeryTypes AS t2 ON t2.Id = t1.SurgeryTypeId WHERE t1.SurgeryTypeId='" +
                surgeryTypeId + "' ORDER BY t1.Name ASC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aSurgery = new Surgery();
                    aSurgery.Id = Convert.ToInt32(reader["Id"]);
                    aSurgery.Name = reader["Name"].ToString();
                    aSurgery.SurgeryTypeId = Convert.ToInt32(reader["SurgeryTypeId"]);
                    aSurgery.SurgeryTypeName = reader["SurgeryTypeName"].ToString();
                    aSurgery.Description = reader["Description"].ToString();

                    aSurgery.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                    aSurgery.Discount = Convert.ToDecimal(reader["Discount"]);
                    aSurgery.TotalFee = Convert.ToDecimal(reader["TotalFee"]);

                    surgeryList.Add(aSurgery);
                }
            }
            reader.Read();
            return surgeryList;
        }

        internal Surgery GetSurgeryByName(string name)
        {
            Surgery aSurgery = null;

            string query =
                "select t1.Id, t1.Name, t1.ShortName, t1.SurgeryTypeId, t2.Name as [SurgeryTypeName], t1.RegularFee, t1.Discount, t1.TotalFee, t1.Description from Surgerys as t1 inner join SurgeryTypes as t2 on t2.Id=t1.SurgeryTypeId WHERE t1.Name='" +
                name + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aSurgery = new Surgery();

                aSurgery.Id = Convert.ToInt32(reader["Id"]);
                aSurgery.Name = reader["Name"].ToString();
                aSurgery.ShortName = reader["ShortName"].ToString();
                aSurgery.SurgeryTypeId = Convert.ToInt32(reader["SurgeryTypeId"]);
                aSurgery.SurgeryTypeName = reader["SurgeryTypeName"].ToString();
                aSurgery.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                aSurgery.Discount = Convert.ToInt32(reader["Discount"]);
                aSurgery.TotalFee = Convert.ToDecimal(reader["TotalFee"]);
                aSurgery.Description = reader["Description"].ToString();
            }
            reader.Close();
            return aSurgery;
        }
    }
}