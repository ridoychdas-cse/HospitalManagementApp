
namespace HospitalBilling.Models.MoneyReceives
{
    public class MoneyReceiveMst
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientType { get; set; }
        public string PaymentType { get; set; }
        public int? DiagnosisBillMstId { get; set; }
        public decimal PayAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public string Particulars { get; set; }
        public decimal SpecialDiscount { get; set; }
        public decimal Vat { get; set; }
    }
}