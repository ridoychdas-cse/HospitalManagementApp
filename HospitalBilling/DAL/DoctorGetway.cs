using System;
using System.Collections.Generic;
using HospitalBilling.BLL;
using HospitalBilling.Models;

namespace HospitalBilling.DAL
{
    public class DoctorGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal int Save(Doctor aDoctor)
        {
            string query = "INSERT INTO Doctors(Name, PhoneNo, DepartmentId, Specialty,Designation, ProfileBrief) VALUES ('" + aDoctor.Name + "', '" + aDoctor.PhoneNo + "','" +
                           aDoctor.DepartmentId + "', '" + aDoctor.Specialty + "','"+aDoctor.Designation+"', '" +
                           aDoctor.ProfileBrief + "')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Update(Doctor aDoctor)
        {
            string query = "UPDATE Doctors SET Name='" + aDoctor.Name + "', PhoneNo='" + aDoctor.PhoneNo + "', DepartmentId='" + aDoctor.DepartmentId + "',Designation='" + aDoctor.Designation + "',  Specialty='" + aDoctor.Specialty + "', ProfileBrief='" + aDoctor.ProfileBrief + "' WHERE Id='" +
                           aDoctor.Id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Delete(int id)
        {
            string query = "DELETE FROM Doctors WHERE Id='" + id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal List<Doctor> GetAllDoctorList()
        {
            Doctor aDoctor = null;
            var doctorList = new List<Doctor>();

            string query =
                "select t1.Id , t1.Name, t1.PhoneNo, t1.DepartmentId, t2.Name as [DepartmentName], t1.DesignationId, t3.Name as [DesignationName], t1.Specialty, t1.ProfileBrief from Doctors t1  left join HospitalDepartments t2 on t2.Id=t1.DepartmentId left join Designations t3 on t3.Id=t1.DesignationId";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aDoctor = new Doctor();
                    aDoctor.Id = Convert.ToInt32(reader["Id"]);
                    aDoctor.Name = reader["Name"].ToString();
                    aDoctor.PhoneNo = reader["PhoneNo"].ToString();
                    aDoctor.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                    aDoctor.DepartmentName = reader["DepartmentName"].ToString();
                    aDoctor.Specialty = reader["Specialty"].ToString();
                    aDoctor.ProfileBrief = reader["ProfileBrief"].ToString();

                    doctorList.Add(aDoctor);
                }
            }
            reader.Close();
            return doctorList;
        }

        internal Doctor GetDoctorById(int id)
        {
            Doctor aDoctor = null;

            string query = "select t1.Id , t1.Name, t1.PhoneNo, t1.DepartmentId,t1.Designation, t2.Name as DepartmentName, t1.DesignationId, t3.Name as DesignationName, t1.Specialty, t1.ProfileBrief from Doctors as t1  left join HospitalDepartments as t2 on t2.Id=t1.DepartmentId left join Designations as t3 on t3.Id=t1.DesignationId WHERE t1.Id='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aDoctor = new Doctor();
                aDoctor.Id = Convert.ToInt32(reader["Id"]);
                aDoctor.Name = reader["Name"].ToString();
                aDoctor.PhoneNo = reader["PhoneNo"].ToString();
                aDoctor.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                aDoctor.DepartmentName = reader["DepartmentName"].ToString();
                aDoctor.Designation = reader["Designation"].ToString();
                aDoctor.Specialty = reader["Specialty"].ToString();
                aDoctor.ProfileBrief = reader["ProfileBrief"].ToString();
            }
            reader.Close();
            return aDoctor;
        }

        internal Doctor GetDoctorByName(string name)
        {
            Doctor aDoctor = null;

            string query = "select t1.Id , t1.Name, t1.PhoneNo, t1.DepartmentId, t2.Name as DepartmentName, t1.DesignationId, t3.Name as DesignationName, t1.Specialty, t1.ProfileBrief from Doctors as t1  left join HospitalDepartments as t2 on t2.Id=t1.DepartmentId left join Designations as t3 on t3.Id=t1.DesignationId WHERE t1.Name='" + name + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aDoctor = new Doctor();
                aDoctor.Id = Convert.ToInt32(reader["Id"]);
                aDoctor.Name = reader["Name"].ToString();
                aDoctor.PhoneNo = reader["PhoneNo"].ToString();
                aDoctor.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                aDoctor.DepartmentName = reader["DepartmentName"].ToString();
                aDoctor.Specialty = reader["Specialty"].ToString();
                aDoctor.ProfileBrief = reader["ProfileBrief"].ToString();
            }
            reader.Close();
            return aDoctor;
        }
    }
}