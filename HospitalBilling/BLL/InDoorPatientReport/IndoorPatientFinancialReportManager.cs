using HospitalBilling.DAL.InDoorPatientReport;
using HospitalBilling.DAL.OutdoorPatientReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HospitalBilling.BLL.InDoorPatientReport
{
    public class IndoorPatientFinancialReportManager
    {
        IndoorPatientFinancialReportGetway _IndoorPatientFinancialReportGetway = new IndoorPatientFinancialReportGetway();
        public DataTable GetData(string startDate, string endDate, string BillNo, string PatientId, string DiagnosisTypeId, string DiagnosisId, string ReportType)
        {
            return _IndoorPatientFinancialReportGetway.GetData(startDate, endDate, BillNo, PatientId, DiagnosisTypeId, DiagnosisId, ReportType);
        }

       
    }
}