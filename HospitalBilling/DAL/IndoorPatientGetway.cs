using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;

namespace HospitalBilling.DAL
{
    public class IndoorPatientGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        /// <summary>
        /// Save Indoor Patient in Database 
        /// </summary>
        /// <param name="aIndoorPatient">Patient and Gurdian Information</param>
        /// <param name="aPatientBedInfo">Patient Bed Information</param>
        /// <returns></returns>
        internal int Save(IndoorPatient aIndoorPatient, PatientBedInfo aPatientBedInfo)
        {
            string query1 =
                "INSERT INTO IndoorPatients (PatientId, Name, Gender, Age, BloodId, PhoneNo, Address, EntryDate, Remark,DivisionId,DistrictId,ThanaId, GName, GPhoneNo, GGender, GAge , Relation, GAddress, ConsultantId, ReferenceById, StatusId, Image) VALUES('" +
                aIndoorPatient.PatientId + "', '" + aIndoorPatient.Name + "', '" + aIndoorPatient.Gender + "', '" +
                aIndoorPatient.Age + "', '" + aIndoorPatient.BloodId + "', '" + aIndoorPatient.PhoneNo + "', '" +
                aIndoorPatient.Address + "', '" + aIndoorPatient.EntryDate + "', '" + aIndoorPatient.Remark + "','" + aIndoorPatient.DivisinId + "','" + aIndoorPatient.DistrictId + "','" + aIndoorPatient.ThanaId + "','" + aIndoorPatient.GName + "', '" + aIndoorPatient.GPhoneNo + "', '" + aIndoorPatient.GGender + "', '" + aIndoorPatient.GAge + "', '" +
                aIndoorPatient.Relation + "', '" + aIndoorPatient.GAddress + "', '" + aIndoorPatient.ConsultantId +
                "', '" + aIndoorPatient.ReferenceById + "', '" + aIndoorPatient.StatusId + "',CONVERT(VARBINARY(MAX),'" + aIndoorPatient.Image + "'))";
               
           

            const string query2 = "SELECT TOP(1) Id FROM IndoorPatients ORDER BY Id DESC";

            int mstId = DataManager.Transaction(_connectionString, query1, query2);

            int rowAffected = SavePatientBedInfo(mstId, aPatientBedInfo);
            return rowAffected;
        }

        /// <summary>
        /// Save Indoor Patient Bed Information
        /// </summary>
        /// <param name="mstId">Patient Table Id</param>
        /// <param name="aPatientBedInfo">Patient Bed Info</param>
        /// <returns></returns>
        internal int SavePatientBedInfo(int mstId, PatientBedInfo aPatientBedInfo)
        {
            string query =
                "INSERT INTO PatientBedInfo (PatientId, RoomType, WardId, BedId, BedPrice, CabinType, CabinId, CabinPrice, AdmitDate) VALUES('" +
                mstId + "', '" + aPatientBedInfo.RoomType + "', '" + aPatientBedInfo.WardId + "', '" +
                aPatientBedInfo.BedId + "', '" + aPatientBedInfo.BedPrice + "', '" + aPatientBedInfo.CabinType + "', '" +
                aPatientBedInfo.CabinId + "', '" + aPatientBedInfo.CabinPrice + "', '" + aPatientBedInfo.AdmitDate +
                "')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            if (rowAffected > 0)
            {
                if (aPatientBedInfo.BedId != null)
                {
                    query = "UPDATE Beds SET Status='" + true + "' WHERE Id='" + aPatientBedInfo.BedId + "'";
                }
                else
                {
                    query = "UPDATE Cabins SET Status='" + true + "' WHERE Id='" + aPatientBedInfo.CabinId + "'";
                }
                rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            }
            return rowAffected;
        }

        internal IndoorPatient GetIndoorPatientByPatientId(string patientId)
        {
            IndoorPatient aIndoorPatient = null;

            string query =
                "SELECT dbo.IndoorPatients.Id, dbo.IndoorPatients.Image, dbo.IndoorPatients.PatientId, dbo.IndoorPatients.Name, dbo.IndoorPatients.Gender, dbo.IndoorPatients.Age, dbo.IndoorPatients.BloodId, dbo.BloodGroup.Name AS BloodGroupName, dbo.IndoorPatients.PhoneNo, dbo.IndoorPatients.Address, dbo.IndoorPatients.EntryDate, dbo.IndoorPatients.Remark, dbo.IndoorPatients.GName, dbo.IndoorPatients.GPhoneNo, dbo.IndoorPatients.GGender, dbo.IndoorPatients.GAge, dbo.IndoorPatients.Relation, dbo.IndoorPatients.GAddress, dbo.IndoorPatients.ConsultantId, dbo.Doctors.Name AS ConsultantName, dbo.IndoorPatients.ReferenceById, dbo.ReferenceBy.Name AS ReferenceByName, dbo.IndoorPatients.StatusId, dbo.Status.Name AS StatusName FROM dbo.IndoorPatients left join dbo.BloodGroup ON dbo.BloodGroup.Id=dbo.IndoorPatients.BloodId left join dbo.Doctors ON dbo.Doctors.Id=dbo.IndoorPatients.ConsultantId left join dbo.ReferenceBy ON dbo.ReferenceBy.Id=dbo.IndoorPatients.ReferenceById left join dbo.Status ON dbo.Status.Id=dbo.IndoorPatients.StatusId WHERE dbo.IndoorPatients.PatientId='" + patientId + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                reader.Read();
                aIndoorPatient = new IndoorPatient();
                aIndoorPatient.Id = Convert.ToInt32(reader["Id"]);
                aIndoorPatient.PatientId = reader["PatientId"].ToString();
                aIndoorPatient.Name = reader["Name"].ToString();
                aIndoorPatient.Gender = reader["Gender"].ToString();
                aIndoorPatient.Age = reader["Age"].ToString();

                aIndoorPatient.BloodId = Convert.ToInt32(reader["BloodId"].ToString());
                aIndoorPatient.BloodName = reader["BloodGroupName"].ToString();

                aIndoorPatient.PhoneNo = reader["PhoneNo"].ToString();
                aIndoorPatient.Address = reader["Address"].ToString();
                aIndoorPatient.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
                aIndoorPatient.Remark = reader["Remark"].ToString();

                aIndoorPatient.GName = reader["GName"].ToString();
                aIndoorPatient.GPhoneNo = reader["GPhoneNo"].ToString();
                aIndoorPatient.GGender = reader["GGender"].ToString();
                aIndoorPatient.GAge = reader["GAge"].ToString();
                aIndoorPatient.Relation = reader["Relation"].ToString();
                aIndoorPatient.GAddress = reader["GAddress"].ToString();

                aIndoorPatient.ConsultantId = Convert.ToInt32(reader["ConsultantId"].ToString());
                aIndoorPatient.ConsultantName = reader["ConsultantName"].ToString();

                aIndoorPatient.ReferenceById = Convert.ToInt32(reader["ReferenceById"].ToString());
                aIndoorPatient.ReferenceByName = reader["ReferenceByName"].ToString();

                aIndoorPatient.StatusId = Convert.ToInt32(reader["StatusId"]);
                aIndoorPatient.StatusName = reader["StatusName"].ToString();

                string image = reader["Image"].ToString();
                if (image != "")
                {
                    aIndoorPatient.Image = DataManager.ConvertStringToBytes(image);
                }

            }
            reader.Close();
            return aIndoorPatient;
        }

        internal IndoorPatient GetPatientById(int id)
        {
            IndoorPatient aIndoorPatient = null;

            string query =
                "SELECT dbo.IndoorPatients.Id, dbo.IndoorPatients.PatientId, dbo.IndoorPatients.Name, dbo.IndoorPatients.Gender, dbo.IndoorPatients.Age, dbo.IndoorPatients.BloodId, dbo.BloodGroup.Name AS BloodGroupName, dbo.IndoorPatients.PhoneNo, dbo.IndoorPatients.Address, dbo.IndoorPatients.EntryDate, dbo.IndoorPatients.Remark, dbo.IndoorPatients.GName, dbo.IndoorPatients.GPhoneNo, dbo.IndoorPatients.GGender, dbo.IndoorPatients.GAge, dbo.IndoorPatients.Relation, dbo.IndoorPatients.GAddress, dbo.IndoorPatients.ConsultantId, dbo.Doctors.Name AS ConsultantName, dbo.IndoorPatients.ReferenceById, dbo.ReferenceBy.Name AS ReferenceByName, dbo.IndoorPatients.StatusId, dbo.Status.Name AS StatusName FROM dbo.IndoorPatients left join dbo.BloodGroup ON dbo.BloodGroup.Id=dbo.IndoorPatients.BloodId left join dbo.Doctors ON dbo.Doctors.Id=dbo.IndoorPatients.ConsultantId left join dbo.ReferenceBy ON dbo.ReferenceBy.Id=dbo.IndoorPatients.ReferenceById left join dbo.Status ON dbo.Status.Id=dbo.IndoorPatients.StatusId WHERE dbo.IndoorPatients.Id='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                reader.Read();
                aIndoorPatient = new IndoorPatient();
                aIndoorPatient.Id = Convert.ToInt32(reader["Id"]);
                aIndoorPatient.PatientId = reader["PatientId"].ToString();
                aIndoorPatient.Name = reader["Name"].ToString();
                aIndoorPatient.Gender = reader["Gender"].ToString();
                aIndoorPatient.Age = reader["Age"].ToString();

                aIndoorPatient.BloodId = Convert.ToInt32(reader["BloodId"].ToString());
                aIndoorPatient.BloodName = reader["BloodGroupName"].ToString();

                aIndoorPatient.PhoneNo = reader["PhoneNo"].ToString();
                aIndoorPatient.Address = reader["Address"].ToString();
                aIndoorPatient.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
                aIndoorPatient.Remark = reader["Remark"].ToString();

                aIndoorPatient.GName = reader["GName"].ToString();
                aIndoorPatient.GPhoneNo = reader["GPhoneNo"].ToString();
                aIndoorPatient.GGender = reader["GGender"].ToString();
                aIndoorPatient.GAge = reader["GAge"].ToString();
                aIndoorPatient.Relation = reader["Relation"].ToString();
                aIndoorPatient.GAddress = reader["GAddress"].ToString();

                aIndoorPatient.ConsultantId = Convert.ToInt32(reader["ConsultantId"].ToString());
                aIndoorPatient.ConsultantName = reader["ConsultantName"].ToString();

                aIndoorPatient.ReferenceById = Convert.ToInt32(reader["ReferenceById"].ToString());
                aIndoorPatient.ReferenceByName = reader["ReferenceByName"].ToString();

                aIndoorPatient.StatusId = Convert.ToInt32(reader["StatusId"]);
                aIndoorPatient.StatusName = reader["StatusName"].ToString();

            }
            reader.Close();
            return aIndoorPatient;
        }



        internal OutdoorPatient GetPatientByIdUsingReport(int id)
        {
            OutdoorPatient aOutdoorPatient = null;

            // string query = "SELECT * FROM OutdoorPatients WHERE Id='" + id + "'";
            string query = "SELECT t1.Id,t1.PatientId,t1.Name,t1.PhoneNo,t1.Gender,t1.Age,t1.EntryDate,'' as DepartmentId,t1.ConsultantId as DoctorId,t2.Name, t2.Name+' (\n'+ISNULL(t2.Specialty,'')+'.)' as DoctorName,t1.ReferenceById FROM IndoorPatients t1 left join Doctors t2 on t1.ConsultantId=t2.Id  WHERE t1.Id='" + id + "'";

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

        internal IndoorPatient GetPatientByPatientIdNamePhoneNo(string seachInput)
        {
            IndoorPatient aIndoorPatient = null;

            string query = "SELECT * FROM IndoorPatients WHERE StatusId='1' AND PatientId='" + seachInput + "' OR Name='" + seachInput +
                           "' OR PhoneNo='" + seachInput + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                reader.Read();
                aIndoorPatient = new IndoorPatient();
                aIndoorPatient.Id = Convert.ToInt32(reader["Id"]);
                aIndoorPatient.PatientId = reader["PatientId"].ToString();
                aIndoorPatient.Name = reader["Name"].ToString();
                aIndoorPatient.PhoneNo = reader["PhoneNo"].ToString();
                aIndoorPatient.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
            }
            reader.Close();

            return aIndoorPatient;
        }

        internal PatientBedInfo GetBedInfoByPatientId(int patientTableId)
        {
            PatientBedInfo aPatientBedInfo = null;

            string query = "select t1.Id, t1.PatientId, t1.RoomType, t1.WardId, t1.BedId, t2.Name as BedName,t1.BedPrice, t1.CabinType, t1.CabinId, t3.Name as CabinName, t1.CabinPrice, t1.AdmitDate from dbo.PatientBedInfo as t1 left join dbo.Beds as t2 on t2.Id=t1.BedId left join dbo.Cabins as t3 on t3.Id=t1.CabinId WHERE t1.PatientId='" + patientTableId +
                           "' Order by t1.Id DESC";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aPatientBedInfo = new PatientBedInfo();
                aPatientBedInfo.Id = Convert.ToInt32(reader["Id"]);
                aPatientBedInfo.PatientId = Convert.ToInt32(reader["PatientId"]);
                aPatientBedInfo.RoomType = reader["RoomType"].ToString();
                if (aPatientBedInfo.RoomType == "Ward")
                {
                    aPatientBedInfo.WardId = Convert.ToInt32(reader["WardId"]);
                    aPatientBedInfo.BedId = Convert.ToInt32(reader["BedId"]);
                    aPatientBedInfo.BedName = reader["BedName"].ToString();
                    aPatientBedInfo.BedPrice = Convert.ToDecimal(reader["BedPrice"]);
                }
                else
                {
                    aPatientBedInfo.CabinType = Convert.ToInt32(reader["CabinType"]);
                    aPatientBedInfo.CabinId = Convert.ToInt32(reader["CabinId"]);
                    aPatientBedInfo.CabinName = reader["CabinName"].ToString();
                    aPatientBedInfo.CabinPrice = Convert.ToDecimal(reader["CabinPrice"]);
                }

                aPatientBedInfo.AdmitDate = Convert.ToDateTime(reader["AdmitDate"]);
            }
            reader.Close();
            return aPatientBedInfo;
        }

        /// <summary>
        /// Bed Relese
        /// </summary>
        /// <param name="bedTransferId"></param>
        /// <param name="patientId"></param>
        /// <param name="aPatientBedInfo"></param>
        /// <param name="roomType">Ward Or Cabin</param>
        /// <param name="bedId">BedId or CabinId Table </param>
        /// <returns></returns>
        internal int BedRelese(int bedTransferId, int patientId, PatientBedInfo aPatientBedInfo, string roomType, int bedId)
        {
            string query = "UPDATE PatientBedInfo SET ReleseDate='" + aPatientBedInfo.AdmitDate + "' WHERE Id='" +
                           bedTransferId + "' AND PatientId='" + patientId + "'";
            int rowAffeced = DataManager.ExecuteNonQuery(query, _connectionString);
            if (rowAffeced > 0)
            {
                if (roomType == "Ward")
                {
                    query = "UPDATE Beds SET Status='" + false + "' WHERE Id='" + bedId + "'";
                }
                else
                {
                    query = "UPDATE Cabins SET Status='" + false + "' WHERE Id='" + bedId + "'";
                }
                rowAffeced = DataManager.ExecuteNonQuery(query, _connectionString);
            }
            return rowAffeced;
        }

        internal int PatientRelese(int id, int status, DateTime releseDateTime)
        {
            string query = "UPDATE IndoorPatients SET StatusId='" + status + "' WHERE Id='" + id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);

            if (rowAffected > 0)
            {
                query = "SELECT * FROM PatientBedInfo WHERE PatientId='" + id + "'";

                var reader = DataManager.SqlDataReader(query, _connectionString);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int bedTableId = Convert.ToInt32(reader["Id"]);
                        string releseDate = reader["ReleseDate"].ToString();
                        if (releseDate == "")
                        {
                            query = "UPDATE PatientBedInfo SET ReleseDate='" + releseDateTime + "' WHERE Id='" +
                                    bedTableId + "'";
                            rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
                            break;
                        }
                    }
                }
            }
            return rowAffected;
        }

        // auto id
        internal string AutoId()
        {
            string tableName = "IndoorPatients";
            string autoId = "IP-" + DataManager.AutoId(tableName, _connectionString);
            return autoId;
        }

        public int BedRelese(string bedtype, int bedId)
        {
            string query;
            if (bedtype == "Ward")
            {
                query = "UPDATE Beds SET Status='" + false + "' WHERE Id='" + bedId + "'";
            }
            else
            {
                query = "UPDATE Cabins SET Status='" + false + "' WHERE Id='" + bedId + "'";
            }
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

      

        public IndoorPatient GetPatientByBillNo(string serchInput)
        {
            IndoorPatient aOutdoorPatient = null;

            string query = "select t1.Id as BillNoId,t1.PatientId,t2.Name,t2.PhoneNo,t2.ReferenceById,t2.EntryDate,t2.PatientId as TypeWisePatientId from [dbo].[DiagnosisBillMst] as T1 inner join IndoorPatients as T2 on t1.PatientId=t2.Id where T1.BillNo='" + serchInput + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aOutdoorPatient = new IndoorPatient();
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
    }
}