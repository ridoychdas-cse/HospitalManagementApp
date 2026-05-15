using System;

namespace HospitalModels.Library.Billings
{
    public class DiagnosisBillMst
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string PatientType { get; set; }
        public string BillNo { get; set; }
        public decimal SpecialDiscount { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
