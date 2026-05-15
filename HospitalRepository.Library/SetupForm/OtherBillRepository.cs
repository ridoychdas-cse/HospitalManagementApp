using HospitalModels.Library.Billings;
using HospitalRepository.Library.DataManagers;
using System;
using System.Collections.Generic;

namespace HospitalRepository.Library.SetupForm
{
    public class OtherBillRepository
    {
        private readonly string _connectionString = DataManagers.DataManager.ConnectionString();
        public int Save(OtherBillType otherBill)
        {
            string query = "INSERT INTO OtherBillType VALUES('" + otherBill.Name + "', '" + true + "')";
            return DataManagers.DataManager.ExecuteNonQuery(query, _connectionString);
        }

        public int Update(int id, OtherBillType otherBill)
        {
            string query = "UPDATE OtherBillType SET Name='" + otherBill.Name + "' WHERE Id='" + id + "'";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        public int Delete(int id)
        {
            string query = "UPDATE OtherBillType SET Status='" + false + "' WHERE Id='" + id + "'";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }

        public IEnumerable<OtherBillType> GetAllOtherBills()
        {
            var billList = new List<OtherBillType>();

            string query = "SELECT * FROM OtherBillType WHERE Status='" + true + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var otherBill = new OtherBillType()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Status = Convert.ToBoolean(reader["Status"])
                    };

                    billList.Add(otherBill);
                }
            }
            reader.Close();
            return billList;
        }

        public OtherBillType GetOtherBillById(int id)
        {
            OtherBillType otherBill = null;

            string query = "SELECT * FROM OtherBillType WHERE Status='" + true + "' AND Id='" + id + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                otherBill = new OtherBillType()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Status = Convert.ToBoolean(reader["Status"])
                };


            }
            reader.Close();
            return otherBill;
        }
    }
}
