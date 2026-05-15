
namespace HospitalBilling.Models.IndoorPatientBills
{
    public class IpPayment
    {
        public decimal PayAmount { get; set; }

        public int PatientId { get; set; }

        public string PatientType { get; set; }

        public decimal SpacialDiscount { get; set; }

        public decimal Vat { get; set; }

        public decimal AdvanceAmount { get; set; }
    }
}