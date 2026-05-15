
using HospitalBilling.Model.SurgeryBills;
using HospitalBilling.Models;
using HospitalBilling.Report.OpReport;
using System.Collections.Generic;
using DiagnosisBillDtl = HospitalModels.Library.Billings.DiagnosisBillDtl;
using DiagnosisBillMst = HospitalModels.Library.Billings.DiagnosisBillMst;

namespace HospitalBilling.Report.Models
{
    public class OpDiagnosisAssignReportModel
    {
        public OpDiagnosisAssignReportModel()
        {
            OutdoorPatient = new OutdoorPatient();
            DiagnosisBillMst = new DiagnosisBillMst();
            DiagnosisBillDtls = new List<DiagnosisBillDtl>();
            SurgeryBillDtls=new List<SurgeryBillDtl>();
            OpDiagnosisMoneyReportModel = new OpDiagnosisMoneyReportModel();

        }
        public OutdoorPatient OutdoorPatient { get; set; }
        public DiagnosisBillMst DiagnosisBillMst { get; set; }
        public List<DiagnosisBillDtl> DiagnosisBillDtls { get; set; }

        public List<SurgeryBillDtl> SurgeryBillDtls { get; set; }
        public OpDiagnosisMoneyReportModel OpDiagnosisMoneyReportModel { get; set; }
    }
}