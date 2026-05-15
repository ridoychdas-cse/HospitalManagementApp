using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HospitalBilling.DAL
{
    public class DiagnosisGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        internal int Save(Diagnosis aDiagnosis)
        {
            string query = "INSERT INTO Diagnosis([Name],[DiagnosisTypeId],[Description],[RegularFee],[Discount],[TotalFee],[NormalValue],[UomId]) VALUES('" + aDiagnosis.Name + "' ,'" + aDiagnosis.DiagnosisTypeId +
                           "', '" + aDiagnosis.Discription + "', '" + aDiagnosis.RegularFee + "', '" +
                           aDiagnosis.Discount + "', '" + aDiagnosis.TotalFee + "','"+aDiagnosis.NormalValue+"','"+aDiagnosis.UomId+"')";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Update(Diagnosis aDiagnosis)
        {
            string query = "UPDATE Diagnosis SET Name='" + aDiagnosis.Name + "' , DiagnosisTypeId='" +
                           aDiagnosis.DiagnosisTypeId + "', Description='" + aDiagnosis.Discription + "', RegularFee='" +
                           aDiagnosis.RegularFee + "', Discount='" + aDiagnosis.Discount + "', TotalFee='" +
                           aDiagnosis.TotalFee + "',NormalValue='"+aDiagnosis.NormalValue+"',UomId='"+aDiagnosis.UomId+"' WHERE Id='" + aDiagnosis.Id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal int Delete(int id)
        {
            string query = "DELETE FROM Diagnosis WHERE Id='" + id + "'";
            int rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            return rowAffected;
        }

        internal List<Diagnosis> GetAllDiagnosesList()
        {
            Diagnosis aDiagnosis = null;
            var diagnosisList = new List<Diagnosis>();

            string query =
                "SELECT t1.Id, t1.Name, t1.DiagnosisTypeId, t2.Name AS DiagnosisTypeName, t1.Description,t1.NormalValue, t1.RegularFee, t1.Discount, t1.TotalFee FROM dbo.Diagnosis AS t1 Left JOIN dbo.DiagnosisTypes AS t2 ON t2.Id = t1.DiagnosisTypeId ORDER BY t2.Name, t1.Name ASC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aDiagnosis = new Diagnosis();
                    aDiagnosis.Id = Convert.ToInt32(reader["Id"]);
                    aDiagnosis.Name = reader["Name"].ToString();
                    aDiagnosis.DiagnosisTypeId = Convert.ToInt32(reader["DiagnosisTypeId"]);
                    aDiagnosis.NormalValue = reader["NormalValue"].ToString();
                    aDiagnosis.DiagnosisTypeName = reader["DiagnosisTypeName"].ToString();
                    aDiagnosis.Discription = reader["Description"].ToString();
                    aDiagnosis.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                    aDiagnosis.Discount = Convert.ToDecimal(reader["Discount"]);
                    aDiagnosis.TotalFee = Convert.ToDecimal(reader["TotalFee"]);


                    diagnosisList.Add(aDiagnosis);
                }
            }
            reader.Read();
            return diagnosisList;
        }

        // search
        internal List<Diagnosis> GetDiagnosesByNameOrType(string searchInput)
        {
            Diagnosis aDiagnosis = null;
            var diagnosisList = new List<Diagnosis>();

            string query =
                "SELECT t1.Id, t1.Name, t1.DiagnosisTypeId, t2.Name AS DiagnosisTypeName, t1.Description, t1.RegularFee, t1.Discount, t1.TotalFee FROM dbo.Diagnosis AS t1 Left JOIN dbo.DiagnosisTypes AS t2 ON t2.Id = t1.DiagnosisTypeId WHERE t1.Name LIKE '%" + searchInput + "%' OR t2.Name LIKE '%" + searchInput + "%' ORDER BY t2.Name, t1.Name ASC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aDiagnosis = new Diagnosis();
                    aDiagnosis.Id = Convert.ToInt32(reader["Id"]);
                    aDiagnosis.Name = reader["Name"].ToString();
                    aDiagnosis.DiagnosisTypeId = Convert.ToInt32(reader["DiagnosisTypeId"]);
                    aDiagnosis.DiagnosisTypeName = reader["DiagnosisTypeName"].ToString();
                    aDiagnosis.Discription = reader["Description"].ToString();
                    aDiagnosis.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                    aDiagnosis.Discount = Convert.ToDecimal(reader["Discount"]);
                    aDiagnosis.TotalFee = Convert.ToDecimal(reader["TotalFee"]);

                    diagnosisList.Add(aDiagnosis);
                }
            }
            reader.Read();
            return diagnosisList;
        }


        internal List<Diagnosis> GetDignosisByTypeId(int diagnosisTypeId)
        {
            Diagnosis aDiagnosis = null;
            var diagnosisList = new List<Diagnosis>();

            string query =
                "SELECT t1.Id, t1.Name, t1.DiagnosisTypeId, t2.Name AS DiagnosisTypeName, t1.Description,t1.NormalValue, t1.RegularFee, t1.Discount, t1.TotalFee FROM dbo.Diagnosis AS t1 Left JOIN dbo.DiagnosisTypes AS t2 ON t2.Id = t1.DiagnosisTypeId WHERE t1.DiagnosisTypeId='" +
                diagnosisTypeId + "' ORDER BY t1.Name ASC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aDiagnosis = new Diagnosis();
                    aDiagnosis.Id = Convert.ToInt32(reader["Id"]);
                    aDiagnosis.Name = reader["Name"].ToString();
                    aDiagnosis.DiagnosisTypeId = Convert.ToInt32(reader["DiagnosisTypeId"]);
                    aDiagnosis.DiagnosisTypeName = reader["DiagnosisTypeName"].ToString();
                    aDiagnosis.Discription = reader["Description"].ToString();
                    aDiagnosis.NormalValue = reader["NormalValue"].ToString();
                    aDiagnosis.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                    aDiagnosis.Discount = Convert.ToDecimal(reader["Discount"]);
                    aDiagnosis.TotalFee = Convert.ToDecimal(reader["TotalFee"]);

                    diagnosisList.Add(aDiagnosis);
                }
            }
            reader.Read();
            return diagnosisList;
        }

        internal Diagnosis GetDiagnosesById(int id)
        {
            Diagnosis aDiagnosis = null;

            string query =
                "SELECT t1.Id, t1.Name, t1.DiagnosisTypeId, t2.Name AS DiagnosisTypeName, t1.Description,t1.NormalValue, t1.RegularFee, t1.Discount, t1.TotalFee,t1.UomId FROM dbo.Diagnosis AS t1 Left JOIN dbo.DiagnosisTypes AS t2 ON t2.Id = t1.DiagnosisTypeId WHERE t1.Id='" +
                id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aDiagnosis = new Diagnosis();
                aDiagnosis.Id = Convert.ToInt32(reader["Id"]);
                aDiagnosis.Name = reader["Name"].ToString();
                aDiagnosis.DiagnosisTypeId = Convert.ToInt32(reader["DiagnosisTypeId"]);
                aDiagnosis.DiagnosisTypeName = reader["DiagnosisTypeName"].ToString();
                aDiagnosis.Discription = reader["Description"].ToString();
                aDiagnosis.NormalValue = reader["NormalValue"].ToString();
                aDiagnosis.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                aDiagnosis.Discount = Convert.ToDecimal(reader["Discount"]);
                aDiagnosis.TotalFee = Convert.ToDecimal(reader["TotalFee"]);
                try
                {
                    aDiagnosis.UomId = Convert.ToInt32(reader["UomId"]);
                }
                catch
                {

                }
                

            }
            reader.Read();
            return aDiagnosis;
        }

        internal Diagnosis GetDiagnosesByName(string name)
        {
            Diagnosis aDiagnosis = null;

            string query =
                "SELECT t1.Id, t1.Name, t1.DiagnosisTypeId, t2.Name AS DiagnosisTypeName, t1.Description,t1.NormalValue, t1.RegularFee, t1.Discount, t1.TotalFee FROM dbo.Diagnosis AS t1 Left JOIN dbo.DiagnosisTypes AS t2 ON t2.Id = t1.DiagnosisTypeId WHERE t1.Name='" +
                name + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                aDiagnosis = new Diagnosis();
                aDiagnosis.Id = Convert.ToInt32(reader["Id"]);
                aDiagnosis.Name = reader["Name"].ToString();
                aDiagnosis.DiagnosisTypeId = Convert.ToInt32(reader["DiagnosisTypeId"]);
                aDiagnosis.DiagnosisTypeName = reader["DiagnosisTypeName"].ToString();
                aDiagnosis.Discription = reader["Description"].ToString();
                aDiagnosis.NormalValue = reader["NormalValue"].ToString();
                aDiagnosis.RegularFee = Convert.ToDecimal(reader["RegularFee"]);
                aDiagnosis.Discount = Convert.ToDecimal(reader["Discount"]);
                aDiagnosis.TotalFee = Convert.ToDecimal(reader["TotalFee"]);

            }
            reader.Read();
            return aDiagnosis;
        }

        internal bool ChackDiagnosisByNameAndType(string name, int typeId)
        {
            bool isNameExist = false;
            string query = "SELECT * FROM Diagnosis WHERE Name='" + name + "', DiagnosisTypeId='" + typeId + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                isNameExist = true;
            }
            return isNameExist;
        }

        public System.Data.DataTable Input(string SerchName)
        {
            string connectionString = DataManager.ConnectionString();
            SqlConnection sqlCon = new SqlConnection(connectionString);
            string query = "Select  PatientId,Name from [OutdoorPatients] where PatientId+'-'+Name ='" + SerchName + "'";
            DataTable dt = DataManager.ExecuteQuery(connectionString, query, "[OutdoorPatients]");
            return dt;
        }
    }
}