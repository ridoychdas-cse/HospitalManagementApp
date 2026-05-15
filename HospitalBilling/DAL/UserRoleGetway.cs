using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;

namespace HospitalBilling.DAL
{
    public class UserRoleGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        public List<UserRole> GetAllUserList()
        {
            var roleList = new List<UserRole>();

            string query = "SELECT * FROM UserRoles WHERE Status='" + true + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var aUserRole = new UserRole
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString()
                    };

                    roleList.Add(aUserRole);
                }
            }
            reader.Close();
            return roleList;
        }

        internal int Save(UserRole role)
        {
            string quey = "INSERT INTO UserRoles VALUES('" + role.Name + "', '" + role.Description + "', '" + true + "')";
            return DataManager.ExecuteNonQuery(quey, _connectionString);
        }

        internal int Update(UserRole role, int id)
        {
            string query = "UPDATE UserRoles SET Name='" + role.Name + "', Description='" + role.Description +
                           "' WHERE Id='" + id + "'";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        internal int Delete(int id)
        {
            string query = "UPDATE UserRoles SET Status='" + false + "' WHERE Id='" + id + "'";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        internal UserRole GetUserRoleById(int id)
        {
            UserRole aUserRole = null;
            string query = "SELECT * FROM UserRoles  WHERE Id='" + id + "' and Status='" + true + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aUserRole = new UserRole
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString()
                };


            }
            reader.Close();
            return aUserRole;
        }
    }
}