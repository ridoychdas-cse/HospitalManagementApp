using System;

namespace HospitalBilling.Models
{
    public class DiagnosisBillMst
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientType { get; set; }
        public DateTime EntryDate { get; set; }

    }

    public class DiagnosisBillMr
    {
        public int Id { get; set; }
        public int MstId { get; set; }
        public string BillingId { get; set; }
        public decimal TotalNetPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }



        public decimal TotalPayAmount { get; set; }
    }
}