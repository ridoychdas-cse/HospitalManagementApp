
using HospitalRepository.Library.DataManagers;
using System;

namespace HospitalRepository.Library.Billings
{
    public class IpBedBillRepository
    {
        private readonly string _connectionString = DataManagers.DataManager.ConnectionString();


        // get bed bill by patient table id
        public decimal GetTotalBedBill(int patientId)
        {
            decimal totalPayableAmount = 0;

            string query =
                "	SELECT dbo.PatientBedInfo.Id, dbo.PatientBedInfo.RoomType, dbo.PatientBedInfo.WardId, dbo.Wards.Name as WardName, dbo.PatientBedInfo.BedId, dbo.Beds.Name as BedName, dbo.PatientBedInfo.BedPrice, dbo.PatientBedInfo.CabinId, dbo.Cabins.Name as CabinName, dbo.PatientBedInfo.CabinPrice, dbo.PatientBedInfo.AdmitDate, dbo.PatientBedInfo.ReleseDate FROM dbo.PatientBedInfo left join dbo.Wards on dbo.Wards.Id=dbo.PatientBedInfo.WardId left join dbo.Beds on dbo.Beds.Id=dbo.PatientBedInfo.BedId left join dbo.Cabins on dbo.Cabins.Id=dbo.PatientBedInfo.CabinId WHERE PatientId='" +
                patientId + "' ";

            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string roomType = reader["RoomType"].ToString();
                    DateTime admitDate = Convert.ToDateTime(reader["AdmitDate"]);
                    string relese = reader["ReleseDate"].ToString();

                    // relese date calclutation
                    DateTime releseDate;
                    if (relese != "")
                    {
                        releseDate = Convert.ToDateTime(reader["ReleseDate"]);
                    }
                    else
                    {
                        releseDate = DateTime.Now;
                    }

                    decimal dailyCharge;
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




                    double totalDay = (releseDate - admitDate).TotalDays;
                    totalDay = Convert.ToDouble(totalDay.ToString("N0"));
                    if (totalDay == 0)
                        totalDay++;
                    totalPayableAmount += dailyCharge * Convert.ToDecimal(totalDay);
                }
            }
            totalPayableAmount = Convert.ToDecimal(totalPayableAmount.ToString("N0"));
            return totalPayableAmount;
        }
    }



}
