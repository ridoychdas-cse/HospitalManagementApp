
namespace HospitalModels.Library.Billings.Op
{
    public class OpMoneyReceiveMst
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public string PatientType { get; set; }

        public int DiagnosisBillMstId { get; set; }

        public decimal PayAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal SpecialDiscount { get; set; }
    }
}
