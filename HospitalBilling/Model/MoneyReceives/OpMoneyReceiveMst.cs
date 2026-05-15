
namespace HospitalBilling.Models.MoneyReceives
{
    public class OpMoneyReceiveMst
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientType { get; set; }
        public string PaymentType { get; set; }
        public int DiagnosisBillMstId { get; set; }
        public decimal PayAmount { get; set; }

    }
}