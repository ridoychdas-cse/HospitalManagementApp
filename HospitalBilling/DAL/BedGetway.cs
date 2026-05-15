using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class BedGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        #region Bed Info Insert Update Delete
        internal int Save(Bed aBed)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveBedInfo]", LoadParametersInputData(aBed, ActionType.Save), _connectionString);
        }
        internal int Update(Bed aBed)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateBedInfo]", LoadParametersInputData(aBed, ActionType.Update), _connectionString);

        }
        internal int Delete(Bed aBed)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteBedInfoById]", LoadParametersInputData(aBed, ActionType.Delete), _connectionString);
        }

        internal SqlParameter[] LoadParametersInputData(Bed aBed, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aBed.Id));
            }

            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", aBed.Name));
                parameters.Add(new SqlParameter("@intWardId", aBed.WardId));
                parameters.Add(new SqlParameter("@dcmlPriceDaily", aBed.PriceDaily));
                parameters.Add(new SqlParameter("@strStatus", aBed.Status));
            }

            return parameters.ToArray();
        }
        #endregion

        #region Bed Info Get
        internal List<Bed> GetAllBedList()
        {
            Bed aBed = null;
            var bedList = new List<Bed>();

            string query =
                "select t1.Id, t1.Name, t1.WardId,t2.Name as WardName, t1.PriceDaily, t1.Status From Beds as t1 left join Wards as t2 on t2.Id=t1.WardId ORDER BY t1.Id DESC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aBed = new Bed();
                    aBed.Id = Convert.ToInt32(reader["Id"]);
                    aBed.Name = reader["Name"].ToString();
                    aBed.WardId = Convert.ToInt32(reader["WardId"]);
                    aBed.WardName = reader["WardName"].ToString();
                    aBed.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);
                    aBed.Status = Convert.ToBoolean(reader["Status"]);

                    bedList.Add(aBed);
                }
            }
            reader.Close();
            return bedList;
        }
        // search 
        internal List<Bed> GetbedByNameOrWardNo(string searchInput)
        {
            Bed aBed = null;
            var bedList = new List<Bed>();

            string query =
                "select t1.Id, t1.Name, t1.WardId,t2.Name as WardName, t1.PriceDaily, t1.Status From Beds as t1 left join Wards as t2 on t2.Id=t1.WardId WHERE t1.Name LIKE '%" + searchInput + "%' OR t2.Name LIKE '%" + searchInput + "%' ";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aBed = new Bed();
                    aBed.Id = Convert.ToInt32(reader["Id"]);
                    aBed.Name = reader["Name"].ToString();
                    aBed.WardId = Convert.ToInt32(reader["WardId"]);
                    aBed.WardName = reader["WardName"].ToString();
                    aBed.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);
                    aBed.Status = Convert.ToBoolean(reader["Status"]);

                    bedList.Add(aBed);
                }
            }
            reader.Close();
            return bedList;
        }
        // Get Bed List by Ward Id and Status; Where Status Is False mean it has no Patient
        internal List<Bed> GetBedByWardId(int id)
        {
            Bed aBed = null;
            var bedList = new List<Bed>();

            string query =
                "select t1.Id, t1.Name, t1.WardId,t2.Name as WardName, t1.PriceDaily, t1.Status From Beds as t1 left join Wards as t2 on t2.Id=t1.WardId WHERE t1.WardId='" + id + "' AND t1.Status='" + false + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aBed = new Bed();
                    aBed.Id = Convert.ToInt32(reader["Id"]);
                    aBed.Name = reader["Name"].ToString();
                    aBed.WardId = Convert.ToInt32(reader["WardId"]);
                    aBed.WardName = reader["WardName"].ToString();
                    aBed.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);
                    aBed.Status = Convert.ToBoolean(reader["Status"]);

                    bedList.Add(aBed);
                }
            }
            reader.Close();
            return bedList;
        }
        internal List<Bed> GetBedByWardId()
        {
            Bed aBed = null;
            var bedList = new List<Bed>();

            string query =
                "select t1.Id,t1.Status, t1.Name, t1.WardId,t2.Name as WardName, t1.PriceDaily, t1.Status From Beds as t1 left join Wards as t2 on t2.Id=t1.WardId ";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aBed = new Bed();
                    aBed.Id = Convert.ToInt32(reader["Id"]);
                    aBed.Name = reader["Name"].ToString();
                    aBed.WardId = Convert.ToInt32(reader["WardId"]);
                    aBed.WardName = reader["WardName"].ToString();
                    aBed.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);
                    aBed.Status = Convert.ToBoolean(reader["Status"]);

                    bedList.Add(aBed);
                }
            }
            reader.Close();
            return bedList;
        }
        internal Bed GetBedById(int id)
        {
            Bed aBed = null;

            string query =
                "select t1.Id, t1.Name, t1.WardId,t2.Name as WardName, t1.PriceDaily, t1.Status  From Beds as t1 left join Wards as t2 on t2.Id=t1.WardId WHERE t1.Id='" +
                id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aBed = new Bed();
                aBed.Id = Convert.ToInt32(reader["Id"]);
                aBed.Name = reader["Name"].ToString();
                aBed.WardId = Convert.ToInt32(reader["WardId"]);
                aBed.WardName = reader["WardName"].ToString();
                aBed.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);
                aBed.Status = Convert.ToBoolean(reader["Status"]);

            }
            reader.Close();
            return aBed;
        }
        internal Bed GetBedByName(string name)
        {
            Bed aBed = null;

            string query =
                "select t1.Id, t1.Name, t1.WardId,t2.Name as WardName, t1.PriceDaily, t1.Status  From Beds as t1 left join Wards as t2 on t2.Id=t1.WardId WHERE t1.Name='" +
                name + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aBed = new Bed();
                aBed.Id = Convert.ToInt32(reader["Id"]);
                aBed.Name = reader["Name"].ToString();
                aBed.WardId = Convert.ToInt32(reader["WardId"]);
                aBed.WardName = reader["WardName"].ToString();
                aBed.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);
                aBed.Status = Convert.ToBoolean(reader["Status"]);

            }
            reader.Close();
            return aBed;
        }
        internal bool ChackBadByNameAndWardId(string name, int wardId)
        {
            bool isNameExist = false;
            string query = "SELECT * FROM Beds WHERE Name='" + name + "' AND WardId='" + wardId + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        #endregion

    }
}