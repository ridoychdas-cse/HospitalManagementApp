using System;
using System.Collections.Generic;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.DAL
{
    public class PackageTypeGetway
    {
        private readonly string _connestionString = DataManager.ConnectionString();

        public int Save(PackageType aPackageType)
        {
            string query = "Insert into PackageTypes Values('" + aPackageType.Name + "', '" + aPackageType.ShortName +
                           "', '" + aPackageType.Description + "')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connestionString);
            return rowAffected;
        }

        public int Update(int id, PackageType aPackageType)
        {
            string query = "Update PackageTypes SET Name='" + aPackageType.Name + "', ShortName='" +
                           aPackageType.ShortName + "', Description='" + aPackageType.Description + "' WHERE Id='" + id +
                           "'";
            return DataManager.ExecuteNonQuery(query, _connestionString);
        }

        public int Delete(int id)
        {
            string query = "Delete From PackageTypes Where Id='" + id + "'";
            return DataManager.ExecuteNonQuery(query, _connestionString);
        }

        public List<PackageType> GetAllPackageTypes()
        {
            PackageType aPackageType = null;
            var packageTypeList = new List<PackageType>();

            string query = "Select * FROM PackageTypes";

            var reader = DataManager.SqlDataReader(query, _connestionString);
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

            var reader = DataManager.SqlDataReader(query, _connestionString);
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

            var reader = DataManager.SqlDataReader(query, _connestionString);
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
    }
}