
namespace HospitalBilling.Models.DiagnosisBills
{
    public class OpDiagnosisBillDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientType { get; set; }
        public string BillNo { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public decimal TotalPayAmount { get; set; }
        public decimal DueAmount { get; set; }

        public decimal Vat { get; set; }
    }
}