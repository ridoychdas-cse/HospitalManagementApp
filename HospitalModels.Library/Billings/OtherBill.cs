using System;

namespace HospitalModels.Library.Billings
{
    public class OtherBill
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientType { get; set; }
        public int OtherBillId { get; set; }
        public decimal Price { get; set; }
        public DateTime EntryDate { get; set; }
        public string OtherBillName { get; set; }
    }
}
