using HospitalBilling.BLL;
using System;

namespace HospitalBilling.DAL.IndoorPatientBills
{
    public class IpBillGetway
    {
        private readonly string _connectionString;

        public IpBillGetway()
        {
            _connectionString = DataManager.ConnectionString();
        }


        //public IpBillInfo GetIndoorPatientAllBillInfo(int patientId, string patientType)
        //{

        //}


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
                    totalPayableAmount += dailyCharge * Convert.ToDecimal(totalDay);
                }
            }
            totalPayableAmount = Convert.ToDecimal(totalPayableAmount.ToString("N0"));
            return totalPayableAmount;
        }


        public decimal GetTotalDiagnosisBillByPatientId(int patientId)
        {
            decimal totalBill = 0;
            string query = "Select* from DiagnosisBillMst where patientId='" + patientId + "' and patientType='IP'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    decimal totalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]);
                    totalBill += totalPayableAmount;
                }
            }
            return totalBill;
        }


        public decimal GetIndoorPatientPamentHistory(int patientId)
        {
            string query = "SELECT * FROM MoneyReceiveMst WHERE PatientId='" + patientId + "' and PatientType='IP'";

            int totalPayment = 0;
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int payAmount = Convert.ToInt32(reader["PayAmount"]);
                    int advanceAmount = Convert.ToInt32(reader["AdvanceAmount"]);
                    int specialDiscount = Convert.ToInt32(reader["SpecialDiscount"]);

                    totalPayment += (payAmount + advanceAmount + specialDiscount);
                }
            }
            return totalPayment;
        }


        // Diagnosis bill and BedBill
        public IpBill IndoorPatientBillHistory(int patientId, string patientType)
        {
            var ipBill = new IpBill
            {
                DiagosisBill = GetTotalDiagnosisBillByPatientId(patientId),
                BedBill = GetTotalBedBill(patientId),
                TotalPayAmount = GetIndoorPatientPamentHistory(patientId),
            };
            ipBill.TotalBill = ipBill.DiagosisBill + ipBill.BedBill;
            ipBill.TotalDue = (ipBill.DiagosisBill + ipBill.BedBill) - ipBill.TotalPayAmount;
            return ipBill;
        }


        internal int PayAmountSave(Models.IndoorPatientBills.IpPayment ipPayment)
        {
            string query = "INSERT INTO MoneyReceiveMst(PatientId, PatientType, PayAmount, AdvanceAmount, SpecialDiscount, IPVat) VALUES('" + ipPayment.PatientId + "','" + ipPayment.PatientType + "','" + ipPayment.PayAmount + "', '" + ipPayment.AdvanceAmount + "', '" + ipPayment.SpacialDiscount + "', '" + ipPayment.Vat + "')";

            return DataManager.ExecuteNonQuery(query, _connectionString);
        }
    }

    public class IpBill
    {
        public decimal? DiagosisBill { get; set; }
        public decimal? BedBill { get; set; }

        public decimal? TotalPayAmount { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? SpecialDiscount { get; set; }
        public decimal? Vat { get; set; }
        public decimal? TotalDue { get; set; }

        public decimal? TotalBill { get; set; }
    }
}