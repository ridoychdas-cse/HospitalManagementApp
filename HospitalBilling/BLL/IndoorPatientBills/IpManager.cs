using HospitalBilling.DAL.IndoorPatientBills;

namespace HospitalBilling.BLL.IndoorPatientBills
{
    public class IpManager
    {
        private readonly IpBillGetway _ipBillGetway;

        public IpManager()
        {
            _ipBillGetway = new IpBillGetway();
        }

        public IpBill IndoorPatientBillHistory(int patientId, string patientType)
        {
            return _ipBillGetway.IndoorPatientBillHistory(patientId, patientType);
        }

        internal int PayAmountSave(Models.IndoorPatientBills.IpPayment ipPayment)
        {
            return _ipBillGetway.PayAmountSave(ipPayment);
        }
    }
}