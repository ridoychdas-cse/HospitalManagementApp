using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class UserRoleGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        

        #region User Role Info Save Update Delete
        internal int Save(UserRole role)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveUserRoleInfo]", LoadParametersInputData(role, ActionType.Save), _connectionString);
        }
        internal int Update(UserRole role)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateUserRoleInfo]", LoadParametersInputData(role, ActionType.Update), _connectionString);
        }
        internal int Delete(UserRole role)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeleteUserRoleInfo]", LoadParametersInputData(role, ActionType.Delete), _connectionString);
        }

        internal SqlParameter[] LoadParametersInputData(UserRole userRole, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", userRole.Id));
            }

            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", userRole.Name));
                parameters.Add(new SqlParameter("@strDescription", userRole.Description));
            }

            return parameters.ToArray();
        }
        #endregion


        #region User Role Info Get
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
        #endregion
    }
}