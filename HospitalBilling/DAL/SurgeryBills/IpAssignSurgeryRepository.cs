using HospitalBilling.BLL;
using HospitalBilling.Model.SurgeryBills;
using HospitalModels.Library.Billings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalBilling.DAL.SurgeryBills
{
    public class IpAssignSurgeryRepository
    {

        private readonly string _connectionString;
        // private readonly string _connectionString = DataManager.ConnectionString();

        public IpAssignSurgeryRepository()
        {
            _connectionString = DataManager.ConnectionString();
        }

        public int Save(SurgeryBillMst surgeryBillMst, List<SurgeryBillDtl> surgeryBillDtls)
        {
            int rowAffected = 0;
            string query1 = "INSERT INTO SurgeryBillMst VALUES('" + surgeryBillMst.PatientId + "', '" +
                            surgeryBillMst.PatientType + "', '" + surgeryBillMst.BillNo + "', '" + surgeryBillMst.SpecialDiscount + "', '" +
                            surgeryBillMst.Vat + "', '" + surgeryBillMst.TotalPayableAmount + "', '" + surgeryBillMst.EntryDate + "')";

            const string query2 = "SELECT TOP(1) Id FROM SurgeryBillMst ORDER BY Id DESC";

            int mstId = DataManager.Transaction(_connectionString, query1, query2);

            foreach (var value in surgeryBillDtls)
            {
                string query = "INSERT INTO SurgeryBillDtl ( SurgeryBillMstId, SurgeryTypeId, SurgeryId, Price, Discount, PayableAmount, DeliveryDate, ReferenceId) VALUES('" + mstId + "',  '" +
                               value.SurgeryTypeId + "','" + value.SurgeryId + "','" + value.Price + "', '" +
                               value.Discount + "', '" + value.PayableAmount + "', '" + value.DeliveryDate + "', '" + value.Reference + "')";
                rowAffected = DataManager.ExecuteNonQuery(query, _connectionString);
            }
            if (rowAffected > 0)
            {
                return mstId;
            }
            return rowAffected;
        }

        // Auto BillNo
        public string GetAutoBillNumber()
        {
            string billNo = "";
            int idAutoIncrement = 0;
            string query = "SELECT TOP(1) BillNo FROM SurgeryBillMst ORDER BY Id DESC";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (!reader.HasRows)
            {
                billNo = "001";
            }
            else
            {
                reader.Read();
                idAutoIncrement = Convert.ToInt32(reader["BillNo"]);
                idAutoIncrement++;
                billNo = "00" + idAutoIncrement.ToString();
            }
            return billNo;
        }


        // Check Unique BillNo
        public string BillNoUniqueCheck(string billNo)
        {
            string query = "SELECT BillNo FROM SurgeryBillMst WHERE BillNo='" + billNo + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);
            if (reader.HasRows)
            {
                reader.Read();
                string dbbillNo = reader["BillNo"].ToString();
                return dbbillNo;
            }
            return "";
        }



        internal MrParticular GetTotalSurgeryBill(int patientId)
        {
            var diagnosisBillMsts = GetSurgeryBillMstsByPatientIdAndType(patientId, "IP");
            if (diagnosisBillMsts != null)
            {
                var totalBill = diagnosisBillMsts.Sum(c => c.TotalPayableAmount);

                var mrParticular = new MrParticular()
                {
                    Particular = "Surgery",
                    Amount = totalBill
                };
                return mrParticular;
            }
            return null;
        }


        public List<SurgeryBillMst> GetSurgeryBillMstsByPatientIdAndType(int patientId, string patientType)
        {
            var surgeryBillMstList = new List<SurgeryBillMst>();

            string query = "SELECT * FROM SurgeryBillMst WHERE PatientId='" + patientId + "' AND PatientType='" + patientType + "'";
            var reader = DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var surgeryBillMst = new SurgeryBillMst()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PatientId = reader["PatientId"].ToString(),
                        PatientType = reader["PatientType"].ToString(),
                        BillNo = reader["BillNo"].ToString(),
                        SpecialDiscount = Convert.ToDecimal(reader["SpecialDiscount"]),
                        Vat = Convert.ToDecimal(reader["Vat"]),
                        TotalPayableAmount = Convert.ToDecimal(reader["TotalPayableAmount"]),
                        EntryDate = Convert.ToDateTime(reader["EntryDate"])
                    };
                    surgeryBillMstList.Add(surgeryBillMst);
                }
            }
            reader.Close();
            return surgeryBillMstList;
        }

  
    }
}