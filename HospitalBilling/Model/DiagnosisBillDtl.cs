using System;

namespace HospitalBilling.Models
{
    public class DiagnosisBillDtl
    {
        public int Id { get; set; }
        public int MstId { get; set; }
        public string BillingId { get; set; }
        public int DiagnosisTypeId { get; set; }
        public string DiagnosisTypeName { get; set; }
        public int DiagnosisId { get; set; }
        public string DianosisName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DeliveryDate { get; set; }

        
    }
}