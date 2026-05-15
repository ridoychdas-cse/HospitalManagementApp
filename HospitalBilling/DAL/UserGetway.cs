using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;

namespace HospitalBilling.DAL
{
    public class UserGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal int Save(User aUser)
        {
            string query = "INSERT INTO Users VALUES('" + aUser.FirstName + "', '" + aUser.LastName + "', '" +
                           aUser.PhoneNo + "', '" + aUser.UserName.ToLower() + "', '" + aUser.Password + "', '" + aUser.UserRoleId +
                           "', '" + true + "')";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }


        internal int Update(int id, User aUser)
        {
            string query = "UPDATE Users SET FirstName='" + aUser.FirstName + "', LastName='" + aUser.LastName +
                           "', PhoneNo='" + aUser.PhoneNo + "', UserName='" + aUser.UserName.ToLower() + "', Password='" +
                           aUser.Password + "', UserRoleId='" + aUser.UserRoleId + "' WHERE Id='" + id + "'";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        public int Delete(int id)
        {
            string query = "UPDATE Users SET Status='" + false + "' WHERE Id='" + id + "'";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        // get all user
        public List<User> GetAllUserList()
        {
            User aUser = null;
            var userList = new List<User>();

            string query =
                "select dbo.Users.Id, dbo.Users.FirstName,dbo.Users.LastName,dbo.Users.PhoneNo,dbo.Users.UserName,dbo.Users.Password,dbo.Users.UserRoleId, dbo.UserRoles.Name as UserRoleName FROM dbo.Users left join dbo.UserRoles on dbo.Users.UserRoleId=dbo.UserRoles.Id WHERE dbo.Users.Status='" + true + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aUser = new User();
                    aUser.Id = Convert.ToInt32(reader["Id"]);
                    aUser.FirstName = reader["FirstName"].ToString();
                    aUser.LastName = reader["LastName"].ToString();
                    aUser.FullName = aUser.FirstName + " " + aUser.LastName;
                    aUser.PhoneNo = reader["PhoneNo"].ToString();
                    aUser.UserName = reader["UserName"].ToString();
                    aUser.Password = reader["Password"].ToString();
                    aUser.UserRoleId = Convert.ToInt32(reader["UserRoleId"]);
                    aUser.UserRoleName = reader["UserRoleName"].ToString();

                    userList.Add(aUser);
                }
            }
            reader.Close();
            return userList;
        }


        // get all user
        public List<User> GetUserListBySearchInput(string searchInput)
        {
            User aUser = null;
            var userList = new List<User>();

            string query =
                "select dbo.Users.Id, dbo.Users.FirstName,dbo.Users.LastName,dbo.Users.PhoneNo,dbo.Users.UserName,dbo.Users.Password,dbo.Users.UserRoleId, dbo.UserRoles.Name as UserRoleName FROM dbo.Users left join dbo.UserRoles on dbo.Users.UserRoleId=dbo.UserRoles.Id WHERE dbo.Users.Status='" + true + "' and dbo.Users.UserName like'%" + searchInput + "%' Or dbo.Users.PhoneNo like'%" + searchInput + "%' ";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aUser = new User();
                    aUser.Id = Convert.ToInt32(reader["Id"]);
                    aUser.FirstName = reader["FirstName"].ToString();
                    aUser.LastName = reader["LastName"].ToString();
                    aUser.FullName = aUser.FirstName + " " + aUser.LastName;
                    aUser.PhoneNo = reader["PhoneNo"].ToString();
                    aUser.UserName = reader["UserName"].ToString();
                    aUser.Password = reader["Password"].ToString();
                    aUser.UserRoleId = Convert.ToInt32(reader["UserRoleId"]);
                    aUser.UserRoleName = reader["UserRoleName"].ToString();

                    userList.Add(aUser);
                }
            }
            reader.Close();
            return userList;
        }

        // get user by id
        public User GetUserById(int id)
        {
            User aUser = null;

            string query =
                "select dbo.Users.Id, dbo.Users.FirstName,dbo.Users.LastName,dbo.Users.PhoneNo,dbo.Users.UserName,dbo.Users.Password,dbo.Users.UserRoleId, dbo.UserRoles.Name as UserRoleName FROM dbo.Users left join dbo.UserRoles on dbo.Users.UserRoleId=dbo.UserRoles.Id WHERE dbo.Users.Id='" + id + "' and dbo.Users.Status='" + true + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aUser = new User();
                aUser.Id = Convert.ToInt32(reader["Id"]);
                aUser.FirstName = reader["FirstName"].ToString();
                aUser.LastName = reader["LastName"].ToString();
                aUser.FullName = aUser.FirstName + " " + aUser.LastName;
                aUser.PhoneNo = reader["PhoneNo"].ToString();
                aUser.UserName = reader["UserName"].ToString();
                aUser.Password = reader["Password"].ToString();
                aUser.UserRoleId = Convert.ToInt32(reader["UserRoleId"]);
                aUser.UserRoleName = reader["UserRoleName"].ToString();

            }
            reader.Close();
            return aUser;
        }

        // for searching
        public User GetUserByUserNameOrPhoneNo(string userName, string phoneNo)
        {
            User aUser = null;

            string query =
                "select dbo.Users.Id, dbo.Users.FirstName,dbo.Users.LastName,dbo.Users.PhoneNo,dbo.Users.UserName,dbo.Users.Password,dbo.Users.UserRoleId, dbo.UserRoles.Name as UserRoleName FROM dbo.Users left join dbo.UserRoles on dbo.Users.UserRoleId=dbo.UserRoles.Id WHERE dbo.Users.UserName='" + userName + "' Or dbo.Users.PhoneNo='" + phoneNo + "' and dbo.Users.Status='" + true + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aUser = new User();
                aUser.Id = Convert.ToInt32(reader["Id"]);
                aUser.FirstName = reader["FirstName"].ToString();
                aUser.LastName = reader["LastName"].ToString();
                aUser.FullName = aUser.FirstName + " " + aUser.LastName;
                aUser.PhoneNo = reader["PhoneNo"].ToString();
                aUser.UserName = reader["UserName"].ToString();
                aUser.Password = reader["Password"].ToString();
                aUser.UserRoleId = Convert.ToInt32(reader["UserRoleId"]);
                aUser.UserRoleName = reader["UserRoleName"].ToString();

            }
            reader.Close();
            return aUser;
        }


        // for login
        public User GetUserByUserNameAndPassword(string userName, string password)
        {
            User aUser = null;

            string query =
                "select dbo.Users.Id, dbo.Users.FirstName,dbo.Users.LastName,dbo.Users.PhoneNo,dbo.Users.UserName,dbo.Users.Password,dbo.Users.UserRoleId, dbo.UserRoles.Name as UserRoleName FROM dbo.Users left join dbo.UserRoles on dbo.Users.UserRoleId=dbo.UserRoles.Id WHERE dbo.Users.UserName='" + userName + "' AND dbo.Users.Password='" + password + "' and dbo.Users.Status='" + true + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aUser = new User();
                aUser.Id = Convert.ToInt32(reader["Id"]);
                aUser.FirstName = reader["FirstName"].ToString();
                aUser.LastName = reader["LastName"].ToString();
                aUser.FullName = aUser.FirstName + " " + aUser.LastName;
                aUser.PhoneNo = reader["PhoneNo"].ToString();
                aUser.UserName = reader["UserName"].ToString();
                aUser.Password = reader["Password"].ToString();
                aUser.UserRoleId = Convert.ToInt32(reader["UserRoleId"]);
                aUser.UserRoleName = reader["UserRoleName"].ToString();

            }
            reader.Close();
            return aUser;
        }

        // unique check for UserName
        public User GetUserByUserName(string userName)
        {
            User aUser = null;

            string query =
                "select dbo.Users.Id, dbo.Users.FirstName,dbo.Users.LastName,dbo.Users.PhoneNo,dbo.Users.UserName,dbo.Users.Password,dbo.Users.UserRoleId, dbo.UserRoles.Name as UserRoleName FROM dbo.Users left join dbo.UserRoles on dbo.Users.UserRoleId=dbo.UserRoles.Id WHERE dbo.Users.UserName='" + userName + "' and dbo.Users.Status='" + true + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aUser = new User();
                aUser.Id = Convert.ToInt32(reader["Id"]);
                aUser.FirstName = reader["FirstName"].ToString();
                aUser.LastName = reader["LastName"].ToString();
                aUser.FullName = aUser.FirstName + " " + aUser.LastName;
                aUser.PhoneNo = reader["PhoneNo"].ToString();
                aUser.UserName = reader["UserName"].ToString();
                aUser.Password = reader["Password"].ToString();
                aUser.UserRoleId = Convert.ToInt32(reader["UserRoleId"]);
                aUser.UserRoleName = reader["UserRoleName"].ToString();

            }
            reader.Close();
            return aUser;
        }
    }
}