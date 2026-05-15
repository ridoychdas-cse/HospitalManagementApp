using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;

namespace HospitalBilling.DAL
{
    public class IndoorPatientBillGetway
    {
        private readonly string _connectionString = DataManager.ConnectionString();

        // save in BedBillMr
        public int SaveBedBillMr(BedBill aBedBill)
        {
            string query = "INSERT INTO BedBillMR VALUES('" + aBedBill.PatientId + "', '" + aBedBill.PayAmount + "')";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }


        // update in bedbillMr
        public int UpdatebedBillMr(int id, BedBill aBedBill)
        {
            string query = "UPDATE BedBillMR SET TotalPayAmount='" + aBedBill.PayAmount + "' WHERE PatientId='" + id +
                           "'";
            return DataManager.ExecuteNonQuery(query, _connectionString);
        }


        // get bed bill by patient table id
        public decimal GetTotalBedBill(int id)
        {
            string roomType;
            decimal dailyCharge, totalPayableAmount = 0;
            double totalDay;
            DateTime admitDate, releseDate;

            string query =
                "	SELECT dbo.PatientBedInfo.Id, dbo.PatientBedInfo.RoomType, dbo.PatientBedInfo.WardId, dbo.Wards.Name as WardName, dbo.PatientBedInfo.BedId, dbo.Beds.Name as BedName, dbo.PatientBedInfo.BedPrice, dbo.PatientBedInfo.CabinId, dbo.Cabins.Name as CabinName, dbo.PatientBedInfo.CabinPrice, dbo.PatientBedInfo.AdmitDate, dbo.PatientBedInfo.ReleseDate FROM dbo.PatientBedInfo left join dbo.Wards on dbo.Wards.Id=dbo.PatientBedInfo.WardId left join dbo.Beds on dbo.Beds.Id=dbo.PatientBedInfo.BedId left join dbo.Cabins on dbo.Cabins.Id=dbo.PatientBedInfo.CabinId WHERE PatientId='" +
                id + "' ";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    roomType = reader["RoomType"].ToString();
                    admitDate = Convert.ToDateTime(reader["AdmitDate"]);
                    string relese = reader["ReleseDate"].ToString();

                    // relese date calclutation
                    if (relese != "")
                    {
                        releseDate = Convert.ToDateTime(reader["ReleseDate"]);
                    }
                    else
                    {
                        releseDate = DateTime.Now;
                    }

                    if (roomType == "Ward")
                    {
                        dailyCharge = Convert.ToDecimal(reader["BedPrice"]);

                    }
                    else
                    {
                        dailyCharge = Convert.ToDecimal(reader["CabinPrice"]);
                    }


                    // day calclutation by Hour
                    int admiTime = admitDate.Hour;
                    int releseTime = releseDate.Hour;

                    if (admiTime < 12)
                    {
                        admitDate = admitDate.AddDays(-1);
                    }

                    if (releseTime >= 12)
                    {
                        releseDate = releseDate.AddDays(1);
                    }
                    else
                    {
                        releseDate = releseDate.AddDays(-1);
                    }




                    totalDay = (releseDate - admitDate).TotalDays;
                    totalDay = Convert.ToDouble(totalDay.ToString("N0"));
                    if (totalDay == 0)
                        totalDay++;
                    totalPayableAmount += dailyCharge * Convert.ToDecimal(totalDay);
                }
            }
            totalPayableAmount = Convert.ToDecimal(totalPayableAmount.ToString("N0"));
            return totalPayableAmount;
        }

        public BedBill GetTotalPaymentHistory(int id)
        {
            BedBill aBedBill = new BedBill();

            string query = "SELECT * FROM BedBillMr WHERE PatientId='" + id + "'";

            var reder = DataManager.SqlDataReader(query, _connectionString);
            if (reder.HasRows)
            {
                reder.Read();
                aBedBill = new BedBill();
                aBedBill.TotalPayAmount = Convert.ToDecimal(reder["TotalPayAmount"]);
            }
            reder.Close();
            return aBedBill;
        }



        public List<IndoorPatientBedBill> GetAllBedBillByPatientId(int id)
        {

            IndoorPatientBedBill aIndoorPatientBedBill = null;
            var indoorPatientBedBillList = new List<IndoorPatientBedBill>();

            string query =
                "	SELECT dbo.PatientBedInfo.Id, dbo.PatientBedInfo.RoomType, dbo.PatientBedInfo.WardId, dbo.Wards.Name as WardName, dbo.PatientBedInfo.BedId, dbo.Beds.Name as BedName, dbo.PatientBedInfo.BedPrice, dbo.PatientBedInfo.CabinId, dbo.Cabins.Name as CabinName, dbo.PatientBedInfo.CabinPrice, dbo.PatientBedInfo.AdmitDate, dbo.PatientBedInfo.ReleseDate FROM dbo.PatientBedInfo left join dbo.Wards on dbo.Wards.Id=dbo.PatientBedInfo.WardId left join dbo.Beds on dbo.Beds.Id=dbo.PatientBedInfo.BedId left join dbo.Cabins on dbo.Cabins.Id=dbo.PatientBedInfo.CabinId WHERE PatientId='" +
                id + "' ";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    aIndoorPatientBedBill = new IndoorPatientBedBill();
                    aIndoorPatientBedBill.Id = Convert.ToInt32(reader["Id"]);
                    aIndoorPatientBedBill.RoomType = reader["RoomType"].ToString();
                    if (aIndoorPatientBedBill.RoomType == "Ward")
                    {
                        aIndoorPatientBedBill.BedNo = reader["BedName"].ToString();
                        aIndoorPatientBedBill.DailyCharge = Convert.ToDecimal(reader["BedPrice"]);
                    }
                    else
                    {
                        aIndoorPatientBedBill.BedNo = reader["CabinName"].ToString();
                        aIndoorPatientBedBill.DailyCharge = Convert.ToDecimal(reader["CabinPrice"]);
                    }

                    aIndoorPatientBedBill.AdmitDate = reader["AdmitDate"].ToString();
                    string releseDate = reader["ReleseDate"].ToString();
                    if (releseDate != "")
                    {
                        aIndoorPatientBedBill.ReleseDate = releseDate;
                    }
                    else
                    {
                        aIndoorPatientBedBill.ReleseDate = "Continue";
                    }

                    indoorPatientBedBillList.Add(aIndoorPatientBedBill);
                }
            }
            reader.Close();
            return indoorPatientBedBillList;
        }



        public BedBill GetBedBillMrHistory(int id)
        {
            BedBill aBedBill = null;

            string query = "SELECT * FROM BedBillMR WHERE PatientId='" + id + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();

                aBedBill = new BedBill();
                aBedBill.Id = Convert.ToInt32(reader["Id"]);
                aBedBill.PatientId = Convert.ToInt32(reader["PatientId"]);
                aBedBill.TotalPayAmount = Convert.ToDecimal(reader["TotalPayAmount"]);


            }
            reader.Close();
            return aBedBill;
        }

    }
}