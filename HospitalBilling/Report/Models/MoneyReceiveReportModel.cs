using HospitalBilling.Models;

namespace HospitalBilling.Report.Models
{
    public class MoneyReceiveReportModel
    {
        public MoneyReceiveReportModel()
        {
            Patient = new OutdoorPatient();
            OpBillInfoReportModel = new OpBillInfoReportModel();
        }
        public OutdoorPatient Patient { get; set; }
        public OpBillInfoReportModel OpBillInfoReportModel { get; set; }
    }
}