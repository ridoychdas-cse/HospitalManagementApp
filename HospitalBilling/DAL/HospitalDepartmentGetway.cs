using HospitalBilling.BLL;
using HospitalBilling.Enum;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class HospitalDepartmentGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        #region HospitalDepartment Info Insert Update Delete
        internal int Save(HospitalDepartment objhospitalDepartment)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_SaveHospitalDepartment]", LoadParametersInputData(objhospitalDepartment, ActionType.Save), _connectionString);
        }
        internal int Update(HospitalDepartment objhospitalDepartment)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateHospitalDepartment]", LoadParametersInputData(objhospitalDepartment, ActionType.Update), _connectionString);
        }
        internal int Delete(HospitalDepartment objhospitalDepartment)
        {
            return DataManager.ExecuteNonQuerySP("[dbo].[Sp_UpdateHospitalDepartment]", LoadParametersInputData(objhospitalDepartment,ActionType.Delete), _connectionString);

        }
        internal SqlParameter[] LoadParametersInputData(HospitalDepartment aHospitalDepartment,ActionType actionType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if(actionType==ActionType.Update|| actionType==ActionType.Delete)
            {
                parameters.Add(new SqlParameter("@intId", aHospitalDepartment.Id));
            }
            if(actionType == ActionType.Save || actionType == ActionType.Update)
            {
                parameters.Add(new SqlParameter("@strName", aHospitalDepartment.Name));
                parameters.Add(new SqlParameter("@strShortName", aHospitalDepartment.ShortName));
                parameters.Add(new SqlParameter("@strDetails", aHospitalDepartment.Details));
               
            }
            return parameters.ToArray();
        }
        #endregion

        #region HospitalDepartment Info Get
        internal List<HospitalDepartment> GetAllDepartment()
        {
            HospitalDepartment aHospitalDepartment = null;
            var departmentList = new List<HospitalDepartment>();

            string query = "SELECT * FROM HospitalDepartments ORDER BY Id DESC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aHospitalDepartment = new HospitalDepartment();
                    aHospitalDepartment.Id = Convert.ToInt32(reader["Id"]);
                    aHospitalDepartment.Name = reader["Name"].ToString();
                    aHospitalDepartment.ShortName = reader["ShortName"].ToString();
                    aHospitalDepartment.Details = reader["Details"].ToString();
                    departmentList.Add(aHospitalDepartment);
                }
                
            }
            reader.Close();
            return departmentList;
        }
        internal HospitalDepartment GetDepartmentByName(string name)
        {
            HospitalDepartment aHospitalDepartment = null;

            string query = "SELECT * FROM HospitalDepartments WHERE Name='" + name + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aHospitalDepartment=new HospitalDepartment();
                aHospitalDepartment.Id = Convert.ToInt32(reader["Id"]);
                aHospitalDepartment.Name = reader["Name"].ToString();
                aHospitalDepartment.ShortName = reader["ShortName"].ToString();

                aHospitalDepartment.Details = reader["Details"].ToString();
            }
            reader.Close();
            return aHospitalDepartment;
        }
        internal HospitalDepartment GetDepartmentByShortName(string shortName)
        {
            HospitalDepartment aHospitalDepartment = null;

            string query = "SELECT * FROM HospitalDepartments WHERE ShortName='" + shortName + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aHospitalDepartment = new HospitalDepartment();
                aHospitalDepartment.Id = Convert.ToInt32(reader["Id"]);
                aHospitalDepartment.Name = reader["Name"].ToString();
                aHospitalDepartment.ShortName = reader["ShortName"].ToString();

                aHospitalDepartment.Details = reader["Details"].ToString();
            }
            reader.Close();
            return aHospitalDepartment;
        }
        internal HospitalDepartment GetDepartmentById(int id)
        {
            HospitalDepartment aHospitalDepartment = null;

            string query = "SELECT * FROM HospitalDepartments WHERE Id='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aHospitalDepartment = new HospitalDepartment();
                aHospitalDepartment.Id = Convert.ToInt32(reader["Id"]);
                aHospitalDepartment.Name = reader["Name"].ToString();
                aHospitalDepartment.ShortName = reader["ShortName"].ToString();

                aHospitalDepartment.Details = reader["Details"].ToString();
            }
            reader.Close();
            return aHospitalDepartment;
        }
        internal List<HospitalDepartment> GetDepartmentByNameOrShortName(string searchInput)
        {
            HospitalDepartment aHospitalDepartment = null;
            var departmentList = new List<HospitalDepartment>();

            string query = "SELECT * FROM HospitalDepartments WHERE Name LIKE '%" + searchInput + "%' OR ShortName LIKE '%" + searchInput + "%'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aHospitalDepartment = new HospitalDepartment();
                    aHospitalDepartment.Id = Convert.ToInt32(reader["Id"]);
                    aHospitalDepartment.Name = reader["Name"].ToString();
                    aHospitalDepartment.ShortName = reader["ShortName"].ToString();

                    aHospitalDepartment.Details = reader["Details"].ToString();
                    departmentList.Add(aHospitalDepartment);
                }

            }
            reader.Close();
            return departmentList;
        }
        #endregion
    }
}