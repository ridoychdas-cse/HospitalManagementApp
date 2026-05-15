using System;
using System.Collections.Generic;
using System.Data;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.DAL
{
    public class PackageGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal int Save(PackageMst aPackageMst, DataTable packageDtl)
        {
            int rowAffected = 0;
            string query1 = "INSERT INTO PackageMst VALUES('" + aPackageMst.Name + "', '" + aPackageMst.ShortName +
                            "', '" + aPackageMst.PackageTypeId + "', '" + aPackageMst.Description + "', '" +
                            aPackageMst.RegularFee + "', '" + aPackageMst.Discount + "', '" + aPackageMst.TotalFee +
                            "')";

            string query2 = "SELECT TOP(1) Id FROM PackageMst ORDER BY Id DESC";

            int mstId = DataManager.Transaction(_connectionString, query1, query2);

            foreach (DataRow value in packageDtl.Rows)
            {
                string query = "INSERT INTO PackageDtl VALUES('" + mstId + "','" + value["ServiceType"] + "', '" +
                               value["ServiceId"] + "')";
                rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            }
            return rowAffected;
        }

        internal int Update(int id, PackageMst aPackageMst, DataTable packageDtl)
        {
            throw new Exception();
        }

        internal int Delete(int id)
        {
            throw new Exception();
        }

        internal List<PackageMst> GetPackageMstsList()
        {
            PackageMst aPackageMst = null;
            var packageMstList = new List<PackageMst>();

            string query =
                "select t1.Id, t1.Name, t1.ShortName, t1.PackageTypeId, t2.Name as PackageName, t1.Description, t1.RegularFee, t1.Discount, t1.TotalFee from PackageMst as t1 left join PackageTypes as t2 on t1.PackageTypeId=t2.Id";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aPackageMst = new PackageMst();
                    aPackageMst.Id = Convert.ToInt32(reader["Id"]);
                    aPackageMst.Name = reader["Name"].ToString();
                    aPackageMst.ShortName = reader["ShortName"].ToString();
                    aPackageMst.PackageTypeId = Convert.ToInt32(reader["PackageTypeId"]);
                    aPackageMst.PackageTypeName = reader["PackageName"].ToString();
                    aPackageMst.Description = reader["Description"].ToString();
                    aPackageMst.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                    aPackageMst.Discount = Convert.ToDecimal(reader["Discount"]);
                    aPackageMst.TotalFee = Convert.ToDecimal(reader["TotalFee"]);

                    packageMstList.Add(aPackageMst);
                }
            }
            reader.Close();
            return packageMstList;
        }

        internal PackageMst GetPackageMstsListById(int id)
        {
            PackageMst aPackageMst = null;

            string query =
                "select t1.Id, t1.Name, t1.ShortName, t1.PackageTypeId, t2.Name as PackageName, t1.Description, t1.RegularFee, t1.Discount, t1.TotalFee from PackageMst as t1 left join PackageTypes as t2 on t1.PackageTypeId=t2.Id WHERE t1.Id='" +
                id + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aPackageMst = new PackageMst();
                aPackageMst.Id = Convert.ToInt32(reader["Id"]);
                aPackageMst.Name = reader["Name"].ToString();
                aPackageMst.ShortName = reader["ShortName"].ToString();
                aPackageMst.PackageTypeId = Convert.ToInt32(reader["PackageTypeId"]);
                aPackageMst.PackageTypeName = reader["PackageName"].ToString();
                aPackageMst.Description = reader["Description"].ToString();
                aPackageMst.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                aPackageMst.Discount = Convert.ToDecimal(reader["Discount"]);
                aPackageMst.TotalFee = Convert.ToDecimal(reader["TotalFee"]);


            }
            reader.Close();
            return aPackageMst;
        }

        internal PackageMst GetPackageMstsListByName(string name)
        {
            PackageMst aPackageMst = null;

            string query =
                "select t1.Id, t1.Name, t1.ShortName, t1.PackageTypeId, t2.Name as PackageName, t1.Description, t1.RegularFee, t1.Discount, t1.TotalFee from PackageMst as t1 left join PackageTypes as t2 on t1.PackageTypeId=t2.Id WHERE t1.Name='" +
                name + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aPackageMst = new PackageMst();
                aPackageMst.Id = Convert.ToInt32(reader["Id"]);
                aPackageMst.Name = reader["Name"].ToString();
                aPackageMst.ShortName = reader["ShortName"].ToString();
                aPackageMst.PackageTypeId = Convert.ToInt32(reader["PackageTypeId"]);
                aPackageMst.PackageTypeName = reader["PackageName"].ToString();
                aPackageMst.Description = reader["Description"].ToString();
                aPackageMst.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                aPackageMst.Discount = Convert.ToDecimal(reader["Discount"]);
                aPackageMst.TotalFee = Convert.ToDecimal(reader["TotalFee"]);

            }
            reader.Close();
            return aPackageMst;
        }

        internal List<PackageDtl> GetAllPackageDtlsListByMstId(int mstId)
        {
            PackageDtl aPackageDtl = null;
            var packageDtlList = new List<PackageDtl>();

            string query =
                "select t1.Id, t1.PackageMstId, t1.ServiceType, t1.ServiceId, CASE WHEN t2.Name IS NULL then t3.Name  else t2.Name END AS[ServiceName]  from PackageDtl t1 left join Diagnosis t2 on t2.Id=t1.ServiceId and t1.ServiceType='Diagnosis' left join Surgerys t3 on t3.Id=t1.ServiceId and t1.ServiceType='Surgery' WHERE t1.PackageMstId='" +
                mstId + "'";
            //string query = "SELECT * FROM PackageDtl WHERE PackageMstId='" + mstId + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aPackageDtl = new PackageDtl();
                    aPackageDtl.Id = Convert.ToInt32(reader["Id"]);
                    aPackageDtl.PackageMstId = Convert.ToInt32(reader["PackageMstId"]);
                    aPackageDtl.ServiceType = reader["ServiceType"].ToString();
                    aPackageDtl.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                    aPackageDtl.ServiceName = reader["ServiceName"].ToString();

                    packageDtlList.Add(aPackageDtl);
                }
            }
            reader.Close();

            return packageDtlList;
        }
    }
}