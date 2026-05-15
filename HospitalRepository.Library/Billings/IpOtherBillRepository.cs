
using HospitalModels.Library.Billings;
using HospitalRepository.Library.DataManagers;
using System;
using System.Collections.Generic;

namespace HospitalRepository.Library.Billings
{
    public class IpOtherBillRepository
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        public int Save(OtherBill otherBill)
        {
            string query = "INSERT INTO OtherBills VALUES('" + otherBill.PatientId + "', '" + otherBill.PatientType +
                           "', '" + otherBill.OtherBillId + "', '" + otherBill.Price + "', '" + otherBill.EntryDate +
                           "')";

            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        public int Update(int id, OtherBill otherBill)
        {
            string query = "UPDATE OtherBills SET OtherBillId='" + otherBill.OtherBillId + "', Price='" +
                           otherBill.Price + "' EntryDate='" + otherBill.EntryDate + "' WHERE Id='" + id + "'";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        public List<OtherBill> GetAllOtherBillsByPatientIdAndType(int pateintId, string patientType)
        {
            var billList = new List<OtherBill>();

            string query =
                "select t1.Id, t1.PatientId, t1.PatientType, t1.OtherBillId, t2.Name as OtherBillName, t1.Price, t1.EntryDate from otherbills as t1 left join OtherBillType as t2 on t1.OtherBillId=t2.Id WHERE t1.PatientId='" +
                pateintId + "' AND t1.PatientType='" + patientType + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var otherBill = new OtherBill();

                    otherBill.Id = Convert.ToInt32(reader["Id"]);
                    otherBill.PatientId = Convert.ToInt32(reader["PatientId"]);
                    otherBill.PatientType = reader["PatientType"].ToString();
                    otherBill.OtherBillId = Convert.ToInt32(reader["OtherBillId"]);
                    otherBill.OtherBillName = reader["OtherBillName"].ToString();
                    otherBill.Price = Convert.ToDecimal(reader["Price"]);
                    otherBill.EntryDate = Convert.ToDateTime(reader["EntryDate"]);


                    billList.Add(otherBill);
                }
            }
            reader.Close();
            return billList;
        }

        // Get Total Bill
        public decimal GetTotalOtherBill(int patientId, string patientType)
        {
            decimal totalBill = 0;
            string query =
                "select t1.Id, t1.PatientId, t1.PatientType, t1.OtherBillId, t2.Name as OtherBillName, t1.Price, t1.EntryDate from otherbills as t1 left join OtherBillType as t2 on t1.OtherBillId=t2.Id WHERE t1.PatientId='" +
                patientId + "' AND t1.PatientType='" + patientType + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var otherBill = new OtherBill()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        PatientType = reader["PatientType"].ToString(),
                        OtherBillId = Convert.ToInt32(reader["OtherBillId"]),
                        OtherBillName = reader["OtherBillName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        EntryDate = Convert.ToDateTime(reader["EntryDate"])
                    };

                    totalBill += otherBill.Price;
                }
            }
            reader.Close();
            return totalBill;
        }

        public OtherBill GetOtherBillById(int id)
        {
            OtherBill otherBill = null;

            string query =
                "select t1.Id, t1.PatientId, t1.PatientType, t1.OtherBillId, t2.Name as OtherBillName, t1.Price, t1.EntryDate from otherbills as t1 left join OtherBillType as t2 on t1.OtherBillId=t2.Id WHERE t1.Id='" + id + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    otherBill = new OtherBill()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        PatientType = reader["PatientType"].ToString(),
                        OtherBillId = Convert.ToInt32(reader["OtherBillId"]),
                        OtherBillName = reader["OtherBillName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        EntryDate = Convert.ToDateTime(reader["EntryDate"])
                    };
                }
            }
            reader.Close();
            return otherBill;
        }


    }
}
