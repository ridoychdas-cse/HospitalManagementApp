namespace HospitalBilling.Models
{
    public class IndoorPatientBedBill
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public string BedNo { get; set; }
        public decimal DailyCharge { get; set; }
        public string AdmitDate { get; set; }
        public string ReleseDate { get; set; }


        // for bill
        public int PatientId { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public decimal TotalPayAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal PayAmount { get; set; }

        public decimal TotalBedBill { get; set; }
    }
}