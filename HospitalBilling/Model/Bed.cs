namespace HospitalBilling.Models
{
    public class Bed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WardId { get; set; }
        public string WardName { get; set; }
        public decimal PriceDaily { get; set; }

        public bool Status { get; set; }
    }
}