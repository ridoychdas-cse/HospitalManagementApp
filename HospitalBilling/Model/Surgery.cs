namespace HospitalBilling.Models
{
    public class Surgery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int SurgeryTypeId { get; set; }
        public string SurgeryTypeName { get; set; }
        public decimal RegularFee { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalFee { get; set; }
        public string Description { get; set; }
    }
}