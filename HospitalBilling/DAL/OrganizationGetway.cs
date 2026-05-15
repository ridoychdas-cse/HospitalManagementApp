using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class OrganizationGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();


        public int Save(Organization organization)
        {
            string query =
                "INSERT INTO Organization(Name, ShortName, PhoneNo, Email, Address, OrganizationSpeech, Description, Status, Image) VALUES('" +
                organization.Name + "', '" + organization.ShortName + "', '" + organization.PhoneNo + "', '" +
                organization.Email + "', '" + organization.Address + "', '" + organization.OrganizationSpeech + "', '" +
                organization.Description + "', '" + true + "',  CONVERT(VARBINARY(MAX),'" + organization.Image + "'))";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        public int Update(Organization organization, int id)
        {
            string query = "UPDATE Organization SET Name='" + organization.Name + "', ShortName='" +
                           organization.ShortName + "', PhoneNo='" + organization.PhoneNo + "', Email='" +
                           organization.PhoneNo + "', Address='" + organization.Address + "', OrganizationSpeech='" +
                           organization.OrganizationSpeech + "', Description='" + organization.Description +
                           "'WHERE Id='" + id + "'";

            return DataManager.ExecuteNonQuery(query, _connectionString);
        }


        public int DeleteUpdate(int id)
        {
            string query = "UPDATE Organization SET Status='" + false + "' WHERE Id='" + id + "'";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        public List<Organization> GetOrganizationsList()
        {
            var orgList = new List<Organization>();
            Organization aOrganization = null;

            string query = "SELECT * FROM Organization WHERE Status='" + true + "' Order By Id DESC";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aOrganization = new Organization
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    ShortName = reader["ShortName"].ToString(),
                    PhoneNo = reader["PhoneNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    OrganizationSpeech = reader["OrganizationSpeech"].ToString(),
                    Description = reader["Description"].ToString()
                };

                string image = reader["Image"].ToString();
                if (image != "")
                {
                    aOrganization.Image = DataManager.ConvertStringToBytes(image);
                }

                orgList.Add(aOrganization);
            }
            reader.Close();
            return orgList;
        }

        internal Organization GetOrganizationById(int id)
        {
            Organization aOrganization = null;

            string query = "SELECT * FROM Organization WHERE Status='" + true + "' and Id='" + id + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aOrganization = new Organization
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    ShortName = reader["ShortName"].ToString(),
                    PhoneNo = reader["PhoneNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    OrganizationSpeech = reader["OrganizationSpeech"].ToString(),
                    Description = reader["Description"].ToString()
                };
            }
            reader.Close();
            return aOrganization;
        }

        internal Organization GetOrganizations()
        {
            Organization aOrganization = null;

            string query = "SELECT * FROM Organization Order By Id DESC";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aOrganization = new Organization
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    ShortName = reader["ShortName"].ToString(),
                    PhoneNo = reader["PhoneNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    OrganizationSpeech = reader["OrganizationSpeech"].ToString(),
                    Description = reader["Description"].ToString()
                };
            }
            reader.Close();
            return aOrganization;
        }


        public  byte[] GetGlLogo(string Id)
        {
            byte[] img = null;
          
            SqlConnection myConnection = new SqlConnection(_connectionString);
            string Query = "select Image from Organization where Id='"+Id+"'";
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand(Query, myConnection);
            object maxValue = myCommand.ExecuteScalar();
            myConnection.Close();
            if (maxValue != System.DBNull.Value)
            {
                img = (byte[])maxValue;
            }
            return img;
        }

        public byte[] GetImageById(int id)
        {
            //var connection = new SqlConnection(_connectionString);
            //var command = new SqlCommand("spGetImage", connection) { CommandType = CommandType.StoredProcedure };
            //var pramImage = new SqlParameter()
            //{
            //    ParameterName = "@id",
            //    Value = id
            //};

            //command.Parameters.Add(pramImage);

            //connection.Open();
            //var image = (byte[])command.ExecuteScalar();

            //return image;

            byte[] img = null;

            SqlConnection myConnection = new SqlConnection(_connectionString);
            string Query = "select Image from Organization where Id='" + id + "'";
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand(Query, myConnection);
            object maxValue = myCommand.ExecuteScalar();
            myConnection.Close();
            if (maxValue != System.DBNull.Value)
            {
                img = (byte[])maxValue;
            }
            return img;
        }
    }
}