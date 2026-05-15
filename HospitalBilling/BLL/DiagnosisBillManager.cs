using System.Collections.Generic;
using System.Data;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class DiagnosisBillManager
    {
        private readonly DiagnosisBillGetway _diagnosisBillGetway=new DiagnosisBillGetway();
        internal DataTable GetAssignDiagnosisList(DiagnosisBillDtl aDiagnosisBillDtl, object p)
        {
            DataTable dt = (DataTable) p;
            dt.NewRow();
            dt.Rows.Add(aDiagnosisBillDtl.BillingId,aDiagnosisBillDtl.DiagnosisTypeId, aDiagnosisBillDtl.DiagnosisTypeName, aDiagnosisBillDtl.DiagnosisId, aDiagnosisBillDtl.DianosisName,
                aDiagnosisBillDtl.Price, aDiagnosisBillDtl.Discount, aDiagnosisBillDtl.TotalPrice, aDiagnosisBillDtl.DeliveryDate.ToShortDateString());
            return dt;
        }

        internal string GetAutoBillNumber()
        {
            return _diagnosisBillGetway.GetAutoBillNumber();
        }

        public DiagnosisBillMr GetDiagnosisBillMrByBillingId(string billingId)
        {
            return _diagnosisBillGetway.GetDiagnosisBillMrByBillingId(billingId);
        }

        internal int Save(DiagnosisBillMst aDiagnosisBillMst, DataTable diagnosisList )
        {
            return _diagnosisBillGetway.Save(aDiagnosisBillMst, diagnosisList);
        }

        public int SaveDiagnosisBillMr(DiagnosisBillMr aDiagnosisBillMr)
        {
            return _diagnosisBillGetway.SaveDiagnosisBillMr(aDiagnosisBillMr);
        }

        public int UpdateDiagnosisBillMr(DiagnosisBillMr aDiagnosisBillMr)
        {
            return _diagnosisBillGetway.UpdateDiagnosisBillMr(aDiagnosisBillMr);
        }

        internal List<DiagnosisBillMr> GetDuiBillListByPatientId(int id, string patientType)
        {
            return _diagnosisBillGetway.GetDuiBillListByPatientId(id, patientType);
        }

        internal DiagnosisBillMr GetBillInformationById(int id)
        {
            return _diagnosisBillGetway.GetBillInformationById(id);
        }

        public DataTable GetTotalDiagnosisBillSummery(string startDate, string endDate, string type)
        {
            return _diagnosisBillGetway.GetTotalDiagnosisBillSummery(startDate, endDate, type);
        }
    }
}