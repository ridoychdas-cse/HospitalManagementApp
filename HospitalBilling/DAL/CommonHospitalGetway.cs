using System;
using System.Collections.Generic;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.DAL
{
    public class CommonHospitalGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal List<Floor> GetAllFloors()
        {
            Floor aFloor = null;
            var floorList = new List<Floor>();

            string query = "SELECT * FROM Floors";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aFloor=new Floor();
                    aFloor.Id = Convert.ToInt32(reader["Id"]);
                    aFloor.Name = reader["Name"].ToString();

                    floorList.Add(aFloor);
                }
            }
            reader.Close();
            return floorList;
        }

        internal List<RoomType> GetAllRoomTypes()
        {
            RoomType aRoomType = null;
            var roomTypeList = new List<RoomType>();

            string query = "SELECT * FROM RoomTypes";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aRoomType = new RoomType();
                    aRoomType.Id = Convert.ToInt32(reader["Id"]);
                    aRoomType.Name = reader["Name"].ToString();

                    roomTypeList.Add(aRoomType);
                }
            }
            reader.Close();
            return roomTypeList;
        }

        internal List<BloodGroup> GetallBloodGroups()
        {
            BloodGroup aBloodGroup = null;
            var bloodGroupList = new List<BloodGroup>();

            string query = "SELECT * FROM BloodGroup";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aBloodGroup = new BloodGroup();
                    aBloodGroup.Id = Convert.ToInt32(reader["Id"]);
                    aBloodGroup.Name = reader["Name"].ToString();

                    bloodGroupList.Add(aBloodGroup);
                }
            }
            reader.Close();
            return bloodGroupList;
        }

        internal List<Status> GetStatus()
        {
            Status aStatus = null;
            var statusList = new List<Status>();

            string query = "SELECT * FROM Status";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aStatus = new Status();
                    aStatus.Id = Convert.ToInt32(reader["Id"]);
                    aStatus.Name = reader["Name"].ToString();

                    statusList.Add(aStatus);
                }
            }
            reader.Close();
            return statusList;
        }
    }
}