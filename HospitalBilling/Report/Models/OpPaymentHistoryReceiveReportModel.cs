using HospitalBilling.Models;
using HospitalModels.Library.Billings;
using System.Collections.Generic;

namespace HospitalBilling.Report.Models
{
    public class OpPaymentHistoryReceiveReportModel
    {
        public OpPaymentHistoryReceiveReportModel()
        {
            OutdoorPatient = new OutdoorPatient();
            OpBillInfoReport = new OpBillInfoReportModel();
            PaymentHistories = new List<IpPaymentHistory>();

        }
        public OutdoorPatient OutdoorPatient { get; set; }
        public OpBillInfoReportModel OpBillInfoReport { get; set; }
        public List<IpPaymentHistory> PaymentHistories { get; set; }
    }
}