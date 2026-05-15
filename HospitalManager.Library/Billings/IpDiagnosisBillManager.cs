using HospitalModels.Library.Billings;
using HospitalRepository.Library.Billings;
using System.Collections.Generic;

namespace HospitalManager.Library.Billings
{
    public class IpDiagnosisBillManager
    {
        private readonly IpDiagnosisBillRepository _diagnosisBillRepository;

        public IpDiagnosisBillManager()
        {
            _diagnosisBillRepository = new IpDiagnosisBillRepository();
        }

        public List<DiagnosisBillMst> GetDiagnosisBillMstsByPatientIdAndType(int patientId, string patientType)
        {
            return _diagnosisBillRepository.GetDiagnosisBillMstsByPatientIdAndType(patientId, patientType);
        }

        public List<DiagnosisBillDtl> GetDiagnosisBillDtlsByMstId(int diagnosisBillMstId)
        {
            return _diagnosisBillRepository.GetDiagnosisBillDtlsByMstId(diagnosisBillMstId);
        }


        // Get DiagnosisBill
        public MrParticular GetTotalDiagnosisBillByPatientIdAndPatientType(int patientId, string patientType)
        {
            return _diagnosisBillRepository.GetTotalDiagnosisBillByPatientIdAndPatientType(patientId, patientType);
        }
    }
}
