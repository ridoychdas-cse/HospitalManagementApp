
using HospitalModels.Library.Billings;
using HospitalRepository.Library.DataManagers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalRepository.Library.Billings
{
    public class IpPaymentHistoryRepository
    {
        private readonly string _connectionString = DataManager.ConnectionString();


        public List<IpMoneyReceiveMst> GetAllIpMoneyReceiveMstsByPatientIdAndType(int patientId, string patientType)
        {
            string query = "SELECT * FROM MoneyReceiveMst WHERE PatientId='" + patientId + "' AND PatientType='" +
                           patientType + "'";

            var moneyReceiveMstList = new List<IpMoneyReceiveMst>();

            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var moneyReceiveMst = new IpMoneyReceiveMst()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        PatientType = reader["PatientType"].ToString(),
                        PayAmount = Convert.ToDecimal(reader["PayAmount"]),
                        AdvanceAmount = Convert.ToDecimal(reader["AdvanceAmount"]),
                        SpecialDiscount = Convert.ToDecimal(reader["SpecialDiscount"])
                    };

                    moneyReceiveMstList.Add(moneyReceiveMst);
                }
            }
            reader.Close();
            return moneyReceiveMstList;
        }


        public IpMoneyReceiveDtl GetIpMoneyReceiveDtlsByMoneyReceiveMstId(int moneyReceiveMstId)
        {
            IpMoneyReceiveDtl moneyReceiveDtl = null;
            string query = "SELECT * FROM MoneyReceiveDtl WHERE MoneyReceiveMstId='" + moneyReceiveMstId + "'";

            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    moneyReceiveDtl = new IpMoneyReceiveDtl()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        MoneyReceiveMstId = Convert.ToInt32(reader["MoneyReceiveMstId"]),
                        PayMethode = reader["PayMethode"].ToString(),
                        BankName = reader["BankName"].ToString(),
                        ChequeNo = reader["ChequeNo"].ToString(),
                        ChequerDate = reader["ChequeDate"].ToString(),
                        EntryDate = Convert.ToDateTime(reader["EntryDate"]),
                        MoneyReceiveBy = reader["MoneyReceiveBy"].ToString()
                    };
                }
            }
            reader.Close();
            return moneyReceiveDtl;
        }

        // payment Calclution
        public decimal TotalPaymentByPatientIdandPatientType(int patientId, string patientType)
        {
            decimal totalPaid = 0;
            var moneyReceiveMsts = GetAllIpMoneyReceiveMstsByPatientIdAndType(patientId, patientType);
            if (moneyReceiveMsts != null)
            {
                totalPaid += moneyReceiveMsts.Sum(moneyReceiveMst => (moneyReceiveMst.PayAmount + moneyReceiveMst.AdvanceAmount));
            }
            return totalPaid;
        }



        // Get Discount Informaton
        public decimal TotalSpecialDiscountByPatientIdAndType(int patientId, string patientType)
        {
            decimal totalDiscount = 0;
            var moneReceiveMsts = GetAllIpMoneyReceiveMstsByPatientIdAndType(patientId, patientType);
            if (moneReceiveMsts != null)
            {
                totalDiscount += moneReceiveMsts.Sum(moneReceiveMst => (moneReceiveMst.SpecialDiscount));
            }
            return totalDiscount;
        }
    }
}
