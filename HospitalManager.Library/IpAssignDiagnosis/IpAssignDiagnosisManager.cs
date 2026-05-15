using HospitalModels.Library.Billings;
using HospitalRepository.Library.DataManagers;
using HospitalRepository.Library.IpAssignDiagnosis;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HospitalManager.Library.IpAssignDiagnosis
{
    public class IpAssignDiagnosisManager
    {
        private readonly IpAssignDiagnosisRepository _assignDiagnosisRepository;

        public IpAssignDiagnosisManager()
        {
            _assignDiagnosisRepository = new IpAssignDiagnosisRepository();
        }

        // Store ViewState Value
        public List<DiagnosisBillDtl> GetAllSelectedDiagnosis(DiagnosisBillDtl diagnosisBill, object p)
        {
            var diagnosisBillDtlList = (List<DiagnosisBillDtl>)p;
            diagnosisBillDtlList.Add(diagnosisBill);
            return diagnosisBillDtlList;
        }


        // Save DiagnosisBillMst and DiagnosisBillDtl
        public int Save(DiagnosisBillMst diagnosisBillMst, List<DiagnosisBillDtl> diagnosisBillDtls)
        {
            return _assignDiagnosisRepository.Save(diagnosisBillMst, diagnosisBillDtls);
        }

        // Auto BillNo
        public string GetAutoBillNumber()
        {
            return _assignDiagnosisRepository.GetAutoBillNumber();
        }


        // Check Unique BillNo
        public bool BillNoUniqueCheck(string billNo)
        {
            bool isBillNoExist = false;
            var abillNo = _assignDiagnosisRepository.BillNoUniqueCheck(billNo);
            if (abillNo != "")
            {
                isBillNoExist = true;
            }
            return isBillNoExist;
        }

        public static DataTable Input(string selectedText)
        {
            string connectionString = DataManager.ConnectionString();
            SqlConnection sqlCon = new SqlConnection(connectionString);
            string query = "Select  PatientId,Name from IndoorPatients where PatientId+'-'+Name ='"+selectedText+"'";
            DataTable dt = DataManager.ExecuteQuery(connectionString, query, "IndoorPatients");
            return dt;
        }
    }
}
