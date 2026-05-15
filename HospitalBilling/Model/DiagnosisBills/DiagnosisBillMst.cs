using System;

namespace HospitalBilling.Models.DiagnosisBills
{
    public class DiagnosisBillMst
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientType { get; set; }
        public string BillNo { get; set; }
        public decimal SpecialDiscount { get; set; }

        public decimal Vat { get; set; }

        public decimal TotalPayableAmount { get; set; }

        public DateTime? EntryDate { get; set; }
    }
}