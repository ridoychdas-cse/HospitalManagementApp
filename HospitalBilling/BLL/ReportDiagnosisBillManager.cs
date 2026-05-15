using System.Collections.Generic;
using HospitalBilling.DAL;
using HospitalBilling.Models;

namespace HospitalBilling.BLL
{
    public class ReportDiagnosisBillManager
    {
        private readonly ReportDiagnosisBillGetway _reportDiagnosisBillGetway=new ReportDiagnosisBillGetway();

        internal List<DiagnosisBillDtl> GetDiagnoisBillDtlsListByIdAndBillNo(int id,string patientType, string billNo)
        {
            return _reportDiagnosisBillGetway.GetDiagnoisBillDtlsListByBillNo(id, patientType,billNo);
        }

        internal DiagnosisBillMr GetDiagnosisBillMrByBillNo(string billNo)
        {
            return _reportDiagnosisBillGetway.GetDiagnosisBillMrByBillNo(billNo);
        }
    }
}