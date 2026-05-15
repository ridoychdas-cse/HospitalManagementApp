using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HospitalBilling.DAL
{
    public class WardGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        #region Ward Information Insert Update Delete
        internal int Save(Ward aWard)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveWardInfo]", LoadParametersInputData(aWard, ActionType.Save), _connectionString);
        }
        internal int Update(Ward aWard)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateWardInfo]", LoadParametersInputData(aWard, ActionType.Update), _connectionString);
        }
        internal int Delete(Ward aWard)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteWardInfoById]", LoadParametersInputData(aWard, ActionType.Delete), _connectionString);

        }
        internal SqlParameter[] LoadParametersInputData(Ward aWard, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aWard.Id));
            }
            if (actionType == ActionType.Save|| actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", aWard.Name));
                parameters.Add(new SqlParameter("@intDepartmentId", aWard.DepartmentId));
                parameters.Add(new SqlParameter("@intFloorId", aWard.FloorId));
                parameters.Add(new SqlParameter("@strWardFor", aWard.WardFor));
                parameters.Add(new SqlParameter("@intRoomTypeId", aWard.RoomTypeId));
                parameters.Add(new SqlParameter("@strDetails", aWard.Details));
            }
            return parameters.ToArray();
        }
        #endregion

        #region Get Ward Information
        internal List<Ward> GetAllWardList()
        {
            Ward aWard = null;
            var wardList = new List<Ward>();

            string query = "select t1.Id, t1.Name, t1.DepartmentId, t2.Name as DepartmentName, t1.FloorId, t3.Name as FloorName, t1.WardFor, t1.RoomTypeId, t4.Name as RoomTypeName, t1.Details from Wards as t1 left join  HospitalDepartments as t2 on t2.Id=t1.DepartmentId left join Floors as t3 on t3.Id=t1.FloorId left join RoomTypes as t4 on t4.Id=t1.RoomTypeId";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aWard = new Ward();
                    aWard.Id = Convert.ToInt32(reader["Id"]);
                    aWard.Name = reader["Name"].ToString();
                    aWard.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                    aWard.DepartmentName = reader["DepartmentName"].ToString();
                    aWard.FloorId = Convert.ToInt32(reader["FloorId"]);
                    aWard.FloorName = reader["FloorName"].ToString();
                    aWard.WardFor = reader["WardFor"].ToString();
                    aWard.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                    aWard.RoomTypeName = reader["RoomTypeName"].ToString();
                    aWard.Details = reader["Details"].ToString();
                    wardList.Add(aWard);
                }
            }
            reader.Close();
            return wardList;
        }
        internal Ward GetWardById(int id)
        {
            Ward aWard = null;

            string query = "select t1.Id, t1.Name, t1.DepartmentId, t2.Name as DepartmentName, t1.FloorId, t3.Name as FloorName, t1.WardFor, t1.RoomTypeId, t4.Name as RoomTypeName, t1.Details from Wards as t1 left join  HospitalDepartments as t2 on t2.Id=t1.DepartmentId left join Floors as t3 on t3.Id=t1.FloorId left join RoomTypes as t4 on t4.Id=t1.RoomTypeId  WHERE t1.Id='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aWard=new Ward();
                aWard.Id = Convert.ToInt32(reader["Id"]);
                aWard.Name = reader["Name"].ToString();
                aWard.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                aWard.DepartmentName = reader["DepartmentName"].ToString();
                aWard.FloorId = Convert.ToInt32(reader["FloorId"]);
                aWard.FloorName = reader["FloorName"].ToString();
                aWard.WardFor = reader["WardFor"].ToString();
                aWard.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                aWard.RoomTypeName = reader["RoomTypeName"].ToString();
                aWard.Details = reader["Details"].ToString();
            }
            reader.Close();
            return aWard;
        }
        internal Ward GetWardByName(string name)
        {
            Ward aWard = null;

            string query = "select t1.Id, t1.Name, t1.DepartmentId, t2.Name as DepartmentName, t1.FloorId, t3.Name as FloorName, t1.WardFor, t1.RoomTypeId, t4.Name as RoomTypeName, t1.Details from Wards as t1 left join  HospitalDepartments as t2 on t2.Id=t1.DepartmentId left join Floors as t3 on t3.Id=t1.FloorId left join RoomTypes as t4 on t4.Id=t1.RoomTypeId  WHERE t1.Name='" + name + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aWard = new Ward();
                aWard.Id = Convert.ToInt32(reader["Id"]);
                aWard.Name = reader["Name"].ToString();
                aWard.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                aWard.DepartmentName = reader["DepartmentName"].ToString();
                aWard.FloorId = Convert.ToInt32(reader["FloorId"]);
                aWard.FloorName = reader["FloorName"].ToString();
                aWard.WardFor = reader["WardFor"].ToString();
                aWard.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                aWard.RoomTypeName = reader["RoomTypeName"].ToString();
                aWard.Details = reader["Details"].ToString();
            }
            reader.Close();
            return aWard;
        }
        internal bool ChackWardByNameAndFloorId(string name, int floorId)
        {
            bool isNameExist = false;
            string query = "SELECT * FROM Wards WHERE Name='" + name + "' AND FloorId='" + floorId + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                isNameExist = true;
            }
            return isNameExist;
        }
        // for search
        internal List<Ward> GetAllWardList(string name)
        {
            Ward aWard = null;
            var wardList = new List<Ward>();

            string query = "select t1.Id, t1.Name, t1.DepartmentId, t2.Name as DepartmentName, t1.FloorId, t3.Name as FloorName, t1.WardFor, t1.RoomTypeId, t4.Name as RoomTypeName, t1.Details from Wards as t1 left join  HospitalDepartments as t2 on t2.Id=t1.DepartmentId left join Floors as t3 on t3.Id=t1.FloorId left join RoomTypes as t4 on t4.Id=t1.RoomTypeId WHERE t1.Name LIKE '%" + name + "%'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aWard = new Ward();
                    aWard.Id = Convert.ToInt32(reader["Id"]);
                    aWard.Name = reader["Name"].ToString();
                    aWard.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                    aWard.DepartmentName = reader["DepartmentName"].ToString();
                    aWard.FloorId = Convert.ToInt32(reader["FloorId"]);
                    aWard.FloorName = reader["FloorName"].ToString();
                    aWard.WardFor = reader["WardFor"].ToString();
                    aWard.RoomTypeId = Convert.ToInt32(reader["RoomTypeId"]);
                    aWard.RoomTypeName = reader["RoomTypeName"].ToString();
                    aWard.Details = reader["Details"].ToString();
                    wardList.Add(aWard);
                }
            }
            reader.Close();
            return wardList;
        }
        #endregion
    }
}