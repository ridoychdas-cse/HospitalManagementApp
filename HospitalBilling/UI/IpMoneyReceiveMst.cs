
namespace HospitalBilling.UI
{
    class IpMoneyReceiveMst
    {
        public int PatientId { get; set; }

        public string PatientType { get; set; }

        public string PaymentType { get; set; }

        public int PayAmount { get; set; }

        public int AdvanceAmount { get; set; }

        public int SpecialDiscount { get; set; }

        public int IPVat { get; set; }
    }
}
