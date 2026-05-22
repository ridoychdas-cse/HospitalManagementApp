using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class SurgeryGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        #region Surgery Info Save,Update,Delete
        internal int Save(Surgery aSurgery)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveSurgeryInfo]", LoadParametersInputData(aSurgery, ActionType.Save), _connectionString);

        }
        internal int Update(Surgery aSurgery)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateSurgeryInfo]", LoadParametersInputData(aSurgery, ActionType.Update), _connectionString);
        }
        internal int Delete(Surgery aSurgery)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteSurgeryInfoById]", LoadParametersInputData(aSurgery, ActionType.Delete), _connectionString);

        }
        internal SqlParameter[] LoadParametersInputData(Surgery aSurgery, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aSurgery.Id));
            }

            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", aSurgery.Name));
                parameters.Add(new SqlParameter("@strShortName", aSurgery.ShortName));
                parameters.Add(new SqlParameter("@intSurgeryTypeId", aSurgery.SurgeryTypeId));
                parameters.Add(new SqlParameter("@dcmlRegularFee", aSurgery.RegularFee));
                parameters.Add(new SqlParameter("@dcmlDiscount", aSurgery.Discount));
                parameters.Add(new SqlParameter("@dcmlTotalFee", aSurgery.TotalFee));
                parameters.Add(new SqlParameter("@strDescription", aSurgery.Description));
            }

            return parameters.ToArray();
        }
        #endregion


        #region Surgery Info Get
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
        #endregion
    }
}