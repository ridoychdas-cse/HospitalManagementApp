using HospitalBilling.DAL.OutdoorPatientReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HospitalBilling.BLL.OutDoorPatientReport
{
    public class OutdoorPatientFinancialReportManager
    {
        OutdoorPatientFinancialReportGetway _outdoorPatientFinancialReportGetway = new OutdoorPatientFinancialReportGetway();
        public DataTable GetData(string startDate, string endDate, string BillNo, string PatientId, string DiagnosisTypeId, string DiagnosisId, string ReportType)
        {
            return _outdoorPatientFinancialReportGetway.GetData( startDate,  endDate,  BillNo,  PatientId,  DiagnosisTypeId,  DiagnosisId,  ReportType);
        }
    }
}