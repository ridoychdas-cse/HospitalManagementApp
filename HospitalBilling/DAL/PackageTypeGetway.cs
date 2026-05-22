using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class PackageTypeGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        #region PackageType Info Save, Update, Delete
        public int Save(PackageType aPackageType)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SavePackageTypeInfo]", LoadParametersInputData(aPackageType, ActionType.Save), _connectionString);
        }
        public int Update( PackageType aPackageType)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdatePackageTypeInfo]", LoadParametersInputData(aPackageType, ActionType.Update), _connectionString);
        }
        public int Delete(PackageType aPackageType)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_DeletePackageTypeInfoById]", LoadParametersInputData(aPackageType, ActionType.Delete), _connectionString);
        }

        internal SqlParameter[] LoadParametersInputData(PackageType aPackageType, ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (actionType == ActionType.Update || actionType == ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aPackageType.Id));
            }

            if (actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", aPackageType.Name));
                parameters.Add(new SqlParameter("@strShortName", aPackageType.ShortName));
                parameters.Add(new SqlParameter("@strDescription", aPackageType.Description));
            }

            return parameters.ToArray();
        }
        #endregion

        #region PackageType Info Get
        public List<PackageType> GetAllPackageTypes()
        {
            PackageType aPackageType = null;
            var packageTypeList = new List<PackageType>();

            string query = "Select * FROM PackageTypes";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aPackageType=new PackageType();
                    aPackageType.Id = Convert.ToInt32(reader["Id"]);
                    aPackageType.Name = reader["Name"].ToString();
                    aPackageType.ShortName = reader["ShortName"].ToString();
                    aPackageType.Description = reader["Description"].ToString();

                    packageTypeList.Add(aPackageType);
                }
            }
            reader.Close();
            return packageTypeList;
        }
        public PackageType GetAllPackageTypesById(int id)
        {
            PackageType aPackageType = null;


            string query = "Select * FROM PackageTypes Where Id='" + id + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aPackageType = new PackageType();
                aPackageType.Id = Convert.ToInt32(reader["Id"]);
                aPackageType.Name = reader["Name"].ToString();
                aPackageType.ShortName = reader["ShortName"].ToString();
                aPackageType.Description = reader["Description"].ToString();


            }
            reader.Close();
            return aPackageType;
        }
        public PackageType GetAllPackageTypesByName(string name)
        {
            PackageType aPackageType = null;


            string query = "Select * FROM PackageTypes Where Name='"+name+"'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                
                    aPackageType = new PackageType();
                    aPackageType.Id = Convert.ToInt32(reader["Id"]);
                    aPackageType.Name = reader["Name"].ToString();
                    aPackageType.ShortName = reader["ShortName"].ToString();
                    aPackageType.Description = reader["Description"].ToString();

                
            }
            reader.Close();
            return aPackageType;
        }
        #endregion
    }
}