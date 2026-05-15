
using HospitalModels.Library.Billings.Op;
using HospitalRepository.Library.Billings.OpBilling;
using System.Data;

namespace HospitalManager.Library.Billings.OpBilling
{
    public class OpMoneyReceiveManager
    {
        private readonly OpMoneyReceiveRepository _moneyReceiveRepository;

        public OpMoneyReceiveManager()
        {
            _moneyReceiveRepository = new OpMoneyReceiveRepository();
        }

        public int MoneyReceiveMstSave(OpMoneyReceiveMst moneyReceiveMst)
        {
            return _moneyReceiveRepository.MoneyReceiveMstSave(moneyReceiveMst);
        }

        //moneyReceiveDtl by Cash
        public int MoneyReceiveDtlSaveByCash(OpMoneyReceiveDtl moneyReceiveDtl)
        {
            return _moneyReceiveRepository.MoneyReceiveDtlSaveByCash(moneyReceiveDtl);
        }

        // moneyReceiveDtl by Bank
        public int MoneyReceiveDtlSaveByBank(OpMoneyReceiveDtl moneyReceiveDtl)
        {
            return _moneyReceiveRepository.MoneyReceiveDtlSaveByBank(moneyReceiveDtl);
        }


        public DataTable Input(string SerchName)
        {
            return _moneyReceiveRepository.Input(SerchName);
        }
    }
}
