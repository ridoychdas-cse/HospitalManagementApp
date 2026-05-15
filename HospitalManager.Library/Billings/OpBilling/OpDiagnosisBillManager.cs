
using HospitalModels.Library.Billings;
using HospitalRepository.Library.Billings.OpBilling;
using System.Collections.Generic;

namespace HospitalManager.Library.Billings.OpBilling
{
    public class OpDiagnosisBillManager
    {
        private readonly OpDiagnosisBillRepository _opDiagnosisBillRepository;

        public OpDiagnosisBillManager()
        {
            _opDiagnosisBillRepository = new OpDiagnosisBillRepository();
        }

        public List<DiagnosisBillMst> GetDiagnosisBillMstsByPatientIdAndType(int patientId, string patientType)
        {
            return _opDiagnosisBillRepository.GetDiagnosisBillMstsByPatientIdAndType(patientId, patientType);
        }

        public DiagnosisBillMst GetDiagnosisBillMstsById(int id)
        {
            return _opDiagnosisBillRepository.GetDiagnosisBillMstsById(id);
        }

        public DiagnosisBillMst GetDiagnosisBillMstsByBillNo(string billNo)
        {
            return _opDiagnosisBillRepository.GetDiagnosisBillMstsByBillNo(billNo);
        }

        public List<DiagnosisBillDtl> GetDiagnosisBillDtlsByMstId(int diagnosisBillMstId)
        {
            return _opDiagnosisBillRepository.GetDiagnosisBillDtlsByMstId(diagnosisBillMstId);
        }


    }
}
