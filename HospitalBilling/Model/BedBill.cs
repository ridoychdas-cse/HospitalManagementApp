namespace HospitalBilling.Models
{
    public class BedBill
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public decimal TotalPayAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal PayAmount { get; set; }
    }
}