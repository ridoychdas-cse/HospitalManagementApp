using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Data;

namespace HospitalBilling.DAL
{
    public class OutdoorPatientGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal int Save(OutdoorPatient aOutdoorPatient)
        {
            string query = "INSERT INTO OutdoorPatients VALUES ('" + aOutdoorPatient.PatientId + "','" + aOutdoorPatient.Name + "', '" +
                           aOutdoorPatient.PhoneNo + "', '" + aOutdoorPatient.Gender + "', '" + aOutdoorPatient.Age +
                           "', '" + aOutdoorPatient.EntryDate + "', '" + aOutdoorPatient.DepartmentId + "', '" +
                           aOutdoorPatient.DoctorId + "', '" + aOutdoorPatient.ReferenceById + "','"+aOutdoorPatient.DivisionId+"','"+aOutdoorPatient.DistrictId+"','"+aOutdoorPatient.ThanaId+"')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Update(OutdoorPatient aOutdoorPatient)
        {
            string query = "UPDATE OutdoorPatients SET PatientId='" + aOutdoorPatient.PatientId + "', Name='" + aOutdoorPatient.Name + "', PhoneNo='" +
                           aOutdoorPatient.PhoneNo + "', Gender='" + aOutdoorPatient.Gender + "', Age='" +
                           aOutdoorPatient.Age + "', EntryDate='" + aOutdoorPatient.EntryDate + "', DepartmentId='" +
                           aOutdoorPatient.DepartmentId + "', DoctorId='" + aOutdoorPatient.DoctorId +
                           "', ReferenceById='" + aOutdoorPatient.ReferenceById + "',DivisionId='" + aOutdoorPatient.DivisionId + "',DistrictId='" + aOutdoorPatient.DistrictId + "',ThanaId='" + aOutdoorPatient.ThanaId + "' WHERE Id='" + aOutdoorPatient.Id + "'";
                         
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Delete(int id)
        {
            string query = "DELETE FROM OutdoorPatients WHERE Id='" + id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }


        internal OutdoorPatient GetPatientByPatientId(string patientId)
        {
            OutdoorPatient aOutdoorPatient = null;

            string query = "SELECT * FROM OutdoorPatients WHERE PatientId='" + patientId + "' ";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aOutdoorPatient = new OutdoorPatient();
                    aOutdoorPatient.Id = Convert.ToInt32(reader["Id"]);
                    aOutdoorPatient.PatientId = reader["PatientId"].ToString();
                    aOutdoorPatient.Name = reader["Name"].ToString();
                    aOutdoorPatient.PhoneNo = reader["PhoneNo"].ToString();
                    aOutdoorPatient.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
                }
            }
            reader.Close();

            return aOutdoorPatient;
        }

        // searching
        internal OutdoorPatient GetPatientByPatientIdNamePhoneNo(string serchInput)
        {
            OutdoorPatient aOutdoorPatient = null;

            string query = "SELECT * FROM OutdoorPatients WHERE PatientId='" + serchInput + "' OR Name='" + serchInput +
                           "' Or PhoneNo='" + serchInput + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aOutdoorPatient = new OutdoorPatient();
                    aOutdoorPatient.Id = Convert.ToInt32(reader["Id"]);
                    aOutdoorPatient.PatientId = reader["PatientId"].ToString();
                    aOutdoorPatient.Name = reader["Name"].ToString();
                    aOutdoorPatient.PhoneNo = reader["PhoneNo"].ToString();
                    aOutdoorPatient.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
                    if (reader["ReferenceById"] != null)
                        aOutdoorPatient.ReferenceById = Convert.ToInt32(reader["ReferenceById"]);
                }
            }
            reader.Close();

            return aOutdoorPatient;
        }


        // searching
        public OutdoorPatient GetPatientByBillNo(string serchInput)
        {
            OutdoorPatient aOutdoorPatient = null;

            string query = "select t1.Id as BillNoId,t1.PatientId,t2.Name,t2.PhoneNo,t2.ReferenceById,t2.EntryDate,t2.PatientId as TypeWisePatientId from [dbo].[DiagnosisBillMst] as T1 inner join OutdoorPatients as T2 on t1.PatientId=t2.Id where T1.BillNo='" + serchInput + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aOutdoorPatient = new OutdoorPatient();
                    aOutdoorPatient.BillNoId = Convert.ToInt32(reader["BillNoId"]);
                    aOutdoorPatient.PatientId = reader["PatientId"].ToString();
                    aOutdoorPatient.TypeWisePatientId = reader["TypeWisePatientId"].ToString();
                    aOutdoorPatient.Name = reader["Name"].ToString();
                    aOutdoorPatient.PhoneNo = reader["PhoneNo"].ToString();
                    aOutdoorPatient.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
                    if (reader["ReferenceById"] != null)
                        aOutdoorPatient.ReferenceById = Convert.ToInt32(reader["ReferenceById"]);
                    
                }
            }
            reader.Close();

            return aOutdoorPatient;
        }


        internal string GetAutoPatientId()
        {
            string patientId = "";
            int autoId;
            string query = "SELECT TOP(1) PatientId FROM OutdoorPatients Order BY Id DESC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                patientId = reader["PatientId"].ToString();
                autoId = Convert.ToInt32(patientId) + 1;
                patientId = autoId.ToString();
            }

            if (patientId == "")
            {
                patientId = "1001";
            }
            return patientId;
        }

        internal OutdoorPatient GetPatientById(int id)
        {
            OutdoorPatient aOutdoorPatient = null;

            // string query = "SELECT * FROM OutdoorPatients WHERE Id='" + id + "'";
            string query = "SELECT t1.Id,t1.PatientId,t1.Name,t1.PhoneNo,t1.Gender,t1.Age,t1.EntryDate,t1.DepartmentId,t1.DoctorId,t2.Name as DoctorName,t1.ReferenceById FROM OutdoorPatients t1 left join Doctors t2 on t1.DoctorId=t2.Id WHERE t1.Id='" + id + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aOutdoorPatient = new OutdoorPatient();
                    aOutdoorPatient.Id = Convert.ToInt32(reader["Id"]);
                    aOutdoorPatient.PatientId = reader["PatientId"].ToString();
                    aOutdoorPatient.Name = reader["Name"].ToString();
                    aOutdoorPatient.PhoneNo = reader["PhoneNo"].ToString();
                    aOutdoorPatient.Age = reader["Age"].ToString();
                    aOutdoorPatient.Gender = reader["Gender"].ToString();
                    aOutdoorPatient.DoctioName = reader["DoctorName"].ToString();
                    aOutdoorPatient.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
                }
            }
            reader.Close();

            return aOutdoorPatient;
        }
        internal OutdoorPatient GetPatientByIdUsingReport(int id)
        {
            OutdoorPatient aOutdoorPatient = null;

            // string query = "SELECT * FROM OutdoorPatients WHERE Id='" + id + "'";
            string query = "SELECT t1.Id,t1.PatientId,t1.Name,t1.PhoneNo,t1.Gender,t1.Age,t1.EntryDate,t1.DepartmentId,t1.DoctorId,t2.Name,t2.Name+'\n('+ISNULL(t2.Specialty,'')+'.)' as DoctorName,t1.ReferenceById FROM OutdoorPatients t1 left join Doctors t2 on t1.DoctorId=t2.Id  WHERE t1.Id='" + id + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aOutdoorPatient = new OutdoorPatient();
                    aOutdoorPatient.Id = Convert.ToInt32(reader["Id"]);
                    aOutdoorPatient.PatientId = reader["PatientId"].ToString();
                    aOutdoorPatient.Name = reader["Name"].ToString();
                    aOutdoorPatient.PhoneNo = reader["PhoneNo"].ToString();
                    aOutdoorPatient.Age = reader["Age"].ToString();
                    aOutdoorPatient.Gender = reader["Gender"].ToString();
                    aOutdoorPatient.DoctioName = reader["DoctorName"].ToString();
                    aOutdoorPatient.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
                }
            }
            reader.Close();

            return aOutdoorPatient;
        }

        

        // Get Auto Patient Id
        internal string AutoId()
        {
            string tableName = "OutdoorPatients";
            string autoId = "OP-" + DataManager.AutoId(tableName, _connectionString);
            return autoId;
        }

        internal DataTable GetDiagnosis(string mstId)
        {
            var connectionString = DataManager.ConnectionString();
            string query = "SELECT T2.Id,T2.DiagnosisTypeId,T3.Name AS DiagnosisTypeName,T2.DiagnosisId,T4.Name as DiagnosisName,ISNULL(t2.ResultValue,'') as ResultValue,ISNULL(t4.NormalValue,'0') as NormalValue,ISNULL(t2.ResultValue,'0')+' '+ISNULL(T5.Name,'') as rptResultValue,ISNULL(t4.NormalValue,'0')+' '+ISNULL(T5.Name,'') as rptNormalValue  FROM [DiagnosisBillDtl]  AS T2 INNER JOIN DiagnosisTypes AS T3 ON T2.DiagnosisTypeId=T3.Id INNER JOIN Diagnosis AS T4 ON T2.DiagnosisId=T4.Id Left Join UOM As T5 on t4.UomId=t5.Id where T2.DiagnosisBillMstId='" + mstId + "'";
            return DataManager.ExecuteQuery(connectionString, query, "[DiagnosisBillDtl]");
        }

        internal int UpdateDiagnosisBillResult(int Id,string ResultValue)
        {
            string query = "update DiagnosisBillDtl set ResultValue='"+ResultValue+"' where Id='"+Id+"'";
           
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

    }
}