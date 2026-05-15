using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class CabinGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        #region Cabin Info Insert Update Delete
        internal int Save(Cabin aCabin)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveCabinInfo]", LoadParametersInputData(aCabin, ActionType.Save), _connectionString);
        }
        internal int Update(Cabin aCabin)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateCabinInfo]", LoadParametersInputData(aCabin, ActionType.Update), _connectionString);
        }
        internal int Delete(Cabin aCabin)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteCabinInfoById]", LoadParametersInputData(aCabin, ActionType.Delete), _connectionString);

        }

        internal SqlParameter[] LoadParametersInputData(Cabin aCabin, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aCabin.Id));
            }
            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", aCabin.Name));
                parameters.Add(new SqlParameter("@intRoomTypeId", aCabin.RoomTypeId));
                parameters.Add(new SqlParameter("@intFloorId", aCabin.FloorId));
                parameters.Add(new SqlParameter("@dcmlPriceDaily", aCabin.PriceDaily));
                parameters.Add(new SqlParameter("@bitStatus", aCabin.Status));
            }
            return parameters.ToArray();
        }

        #endregion

        #region Cabin Info Get
        internal List<Cabin> GetAllCabinList()
        {
            Cabin aCabin = null;
            var cabinList = new List<Cabin>();
            string query =
                "select t1.Id, t1.Name, t1.RoomTypeId, t2.Name as RoomTypeName, t1.FloorId, t3.Name as FloorName, t1.PriceDaily from Cabins as t1 left join RoomTypes as t2 on t2.Id=t1.RoomTypeId left join Floors as t3 on t3.Id=t1.FloorId ORDER BY t1.Id DESC";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aCabin = new Cabin();
                    aCabin.Id = Convert.ToInt32(reader["Id"]);
                    aCabin.Name = reader["Name"].ToString();
                    aCabin.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                    aCabin.RoomTypeName = reader["RoomTypeName"].ToString();
                    aCabin.FloorId = Convert.ToInt32(reader["FloorId"]);
                    aCabin.FloorName = reader["FloorName"].ToString();
                    aCabin.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);

                    cabinList.Add(aCabin);
                }
                
            }
            reader.Close();
            return cabinList;
        }
        // search
        internal List<Cabin> GetCabinByNameOrType(string searchInput)
        {
            Cabin aCabin = null;
            var cabinList = new List<Cabin>();
            string query =
                "select t1.Id, t1.Name, t1.RoomTypeId, t2.Name as RoomTypeName, t1.FloorId, t3.Name as FloorName, t1.PriceDaily from Cabins as t1 left join RoomTypes as t2 on t2.Id=t1.RoomTypeId left join Floors as t3 on t3.Id=t1.FloorId WHERE t1.Name LIKE '%" + searchInput + "%'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aCabin = new Cabin();
                    aCabin.Id = Convert.ToInt32(reader["Id"]);
                    aCabin.Name = reader["Name"].ToString();
                    aCabin.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                    aCabin.RoomTypeName = reader["RoomTypeName"].ToString();
                    aCabin.FloorId = Convert.ToInt32(reader["FloorId"]);
                    aCabin.FloorName = reader["FloorName"].ToString();
                    aCabin.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);

                    cabinList.Add(aCabin);
                }

            }
            reader.Close();
            return cabinList;
        }
        // status false
        internal List<Cabin> GetCabinByRoomTypeId(int id)
        {
            Cabin aCabin = null;
            var cabinList = new List<Cabin>();
            string query =
                "select t1.Id, t1.Name, t1.RoomTypeId, t2.Name as RoomTypeName, t1.FloorId, t3.Name as FloorName, t1.PriceDaily from Cabins as t1 left join RoomTypes as t2 on t2.Id=t1.RoomTypeId left join Floors as t3 on t3.Id=t1.FloorId WHERE t1.RoomTypeId='" + id + "' AND t1.Status='"+false+"'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aCabin = new Cabin();
                    aCabin.Id = Convert.ToInt32(reader["Id"]);
                    aCabin.Name = reader["Name"].ToString();
                    aCabin.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                    aCabin.RoomTypeName = reader["RoomTypeName"].ToString();
                    aCabin.FloorId = Convert.ToInt32(reader["FloorId"]);
                    aCabin.FloorName = reader["FloorName"].ToString();
                    aCabin.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);

                    cabinList.Add(aCabin);
                }

            }
            reader.Close();
            return cabinList;
        }
        internal List<Cabin> GetCabinByRoomTypeId()
        {
            Cabin aCabin = null;
            var cabinList = new List<Cabin>();
            string query =
                "select t1.Id,t1.Status, t1.Name, t1.RoomTypeId, t2.Name as RoomTypeName, t1.FloorId, t3.Name as FloorName, t1.PriceDaily from Cabins as t1 left join RoomTypes as t2 on t2.Id=t1.RoomTypeId left join Floors as t3 on t3.Id=t1.FloorId ";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aCabin = new Cabin();
                    aCabin.Id = Convert.ToInt32(reader["Id"]);
                    aCabin.Name = reader["Name"].ToString();
                    aCabin.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                    aCabin.RoomTypeName = reader["RoomTypeName"].ToString();
                    aCabin.FloorId = Convert.ToInt32(reader["FloorId"]);
                    aCabin.FloorName = reader["FloorName"].ToString();
                    aCabin.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);
                    aCabin.Status = Convert.ToBoolean(reader["Status"].ToString());
                    cabinList.Add(aCabin);
                }

            }
            reader.Close();
            return cabinList;
        }
        internal Cabin GetCabinById(int id)
        {
            Cabin aCabin = null;

            string query =
                "select t1.Id, t1.Name, t1.RoomTypeId, t2.Name as RoomTypeName, t1.FloorId, t3.Name as FloorName, t1.PriceDaily from Cabins as t1 left join RoomTypes as t2 on t2.Id=t1.RoomTypeId left join Floors as t3 on t3.Id=t1.FloorId WHERE t1.Id='" +
                id + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aCabin=new Cabin();
                aCabin.Id = Convert.ToInt32(reader["Id"]);
                aCabin.Name = reader["Name"].ToString();
                aCabin.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                aCabin.RoomTypeName = reader["RoomTypeName"].ToString();
                aCabin.FloorId = Convert.ToInt32(reader["FloorId"]);
                aCabin.FloorName = reader["FloorName"].ToString();
                aCabin.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);
            }
            reader.Close();
            return aCabin;
        }
        internal Cabin GetCabinByName(string name)
        {
            Cabin aCabin = null;

            string query =
                "select t1.Id, t1.Name, t1.RoomTypeId, t2.Name as RoomTypeName, t1.FloorId, t3.Name as FloorName, t1.PriceDaily from Cabins as t1 left join RoomTypes as t2 on t2.Id=t1.RoomTypeId left join Floors as t3 on t3.Id=t1.FloorId WHERE t1.Name='" +
                name + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aCabin = new Cabin();
                aCabin.Id = Convert.ToInt32(reader["Id"]);
                aCabin.Name = reader["Name"].ToString();
                aCabin.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                aCabin.RoomTypeName = reader["RoomTypeName"].ToString();
                aCabin.FloorId = Convert.ToInt32(reader["FloorId"]);
                aCabin.FloorName = reader["FloorName"].ToString();
                aCabin.PriceDaily = Convert.ToDecimal(reader["PriceDaily"]);
            }
            reader.Close();
            return aCabin;
        }
        internal bool ChackCabinByNameAndFloorId(string name, int floorId)
        {
            bool isNameExist = false;
            string query = "SELECT * FROM Cabins WHERE Name='" + name + "' AND FloorId='" + floorId + "'";
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