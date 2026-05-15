using HospitalModels.Library.Billings;
using System.Collections.Generic;

namespace HospitalBilling.Report.Models
{
    public class IpMoneyReceiveReportModel
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }

        public List<MrParticular> MrParticulars { get; set; }

        public decimal TotalBill { get; set; }
        public decimal TotalDiscount { get; set; }

        public decimal PreviesTotalPaid { get; set; }

        public decimal NowPay { get; set; }

        public decimal TotalDue { get; set; }
    }


}