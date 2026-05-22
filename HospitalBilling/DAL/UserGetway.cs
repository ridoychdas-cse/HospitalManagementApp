using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class UserGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        #region User Info Save,Update,Delete
        internal int Save(User aUser)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveUserInfo]", LoadParametersInputData(aUser, ActionType.Save), _connectionString);

        }
        internal int Update(User aUser)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateUserInfo]", LoadParametersInputData(aUser, ActionType.Update), _connectionString);

        }
        public int Delete(User aUser)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteUserInfo]", LoadParametersInputData(aUser, ActionType.Delete), _connectionString);

        }

        internal SqlParameter[] LoadParametersInputData(User aUser, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aUser.Id));
            }

            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strFirstName", aUser.FirstName));
                parameters.Add(new SqlParameter("@strLastName", aUser.LastName));
                parameters.Add(new SqlParameter("@strPhoneNo", aUser.PhoneNo));
                parameters.Add(new SqlParameter("@strUserName", aUser.UserName));
                parameters.Add(new SqlParameter("@strPassword", aUser.Password));
                parameters.Add(new SqlParameter("@intUserRoleId", aUser.UserRoleId));
            }

            return parameters.ToArray();
        }
        #endregion

        #region User Info Get
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
        #endregion
    }
}