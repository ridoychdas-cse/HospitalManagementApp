using System;

namespace HospitalBilling.Report.Models
{
    public class IpAdvancePaymentReportModel
    {
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string PhoneNo { get; set; }
        public DateTime AdmitDate { get; set; }
        public string RoomType { get; set; }
        public string BedNumber { get; set; }
        public string AdvancePayment { get; set; }

    }
}