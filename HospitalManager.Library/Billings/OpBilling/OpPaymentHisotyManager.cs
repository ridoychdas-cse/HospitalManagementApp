using HospitalModels.Library.Billings;
using HospitalModels.Library.Billings.Op;
using HospitalRepository.Library.Billings.OpBilling;
using System.Collections.Generic;

namespace HospitalManager.Library.Billings.OpBilling
{
    public class OpPaymentHisotyManager
    {
        private readonly OpPaymentHisotyRepository _opPaymentHisotyRepository;

        public OpPaymentHisotyManager()
        {
            _opPaymentHisotyRepository = new OpPaymentHisotyRepository();
        }

        // diagnosis bill history by diagnosis bill mstId
        public List<OpMoneyReceiveMst> GetAllPaymentHistoryByDiagnosisBillMstId(int diagnosisMstId)
        {
            return _opPaymentHisotyRepository.GetAllPaymentHistoryByDiagnosisBillMstId(diagnosisMstId);
        }

        public List<DiagnosisBillDtl> GetAllDiagonisHistoryByDiagnosisBillMstId(int diagnosisMstId)
        {
            return _opPaymentHisotyRepository.GetAllDiagonisHistoryByDiagnosisBillMstId(diagnosisMstId);
        }

        
        public OpMoneyReceiveDtl GetOpMoneyReceiveDtlsByMoneyReceiveMstId(int moneyReceiveMstId)
        {
            return _opPaymentHisotyRepository.GetOpMoneyReceiveDtlsByMoneyReceiveMstId(moneyReceiveMstId);
        }


    }
}
