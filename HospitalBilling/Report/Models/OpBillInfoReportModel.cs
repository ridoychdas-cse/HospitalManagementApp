
namespace HospitalBilling.Report.Models
{
    public class OpBillInfoReportModel
    {
        public string BillNo { get; set; }

        public string ConsultantFee { get; set; }
        public string TotalNetPrice { get; set; }
        public string SpecialDiscount { get; set; }
        public string Vat { get; set; }
        public string TotalPayableAmount { get; set; }
        public string TotalPay { get; set; }
        public string TotalDue { get; set; }


        public string NowPay { get; set; }
    }
}