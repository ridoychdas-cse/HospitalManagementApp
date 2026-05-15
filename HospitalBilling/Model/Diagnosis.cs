namespace HospitalBilling.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DiagnosisTypeId { get; set; }
        public string DiagnosisTypeName { get; set; }

        public int UomId { get; set; }
        public string Discription { get; set; }

        public string NormalValue { get; set; }
        public decimal RegularFee{ get; set; }
        public decimal Discount { get; set; }
        public decimal TotalFee { get; set; }
    }
}