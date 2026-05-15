using HospitalModels.Library.Billings;
using HospitalRepository.Library.Billings;
using System.Data;

namespace HospitalManager.Library.Billings
{
    public class IpMoneyReceiveManager
    {
        private readonly IpMoneyReceiveRepository _moneyReceiveRepository;

        public IpMoneyReceiveManager()
        {
            _moneyReceiveRepository = new IpMoneyReceiveRepository();
        }

        public int MoneyReceiveMstSave(IpMoneyReceiveMst moneyReceiveMst)
        {
            return _moneyReceiveRepository.MoneyReceiveMstSave(moneyReceiveMst);
        }

        //moneyReceiveDtl by Cash
        public int MoneyReceiveDtlSaveByCash(IpMoneyReceiveDtl moneyReceiveDtl)
        {
            return _moneyReceiveRepository.MoneyReceiveDtlSaveByCash(moneyReceiveDtl);
        }

        // moneyReceiveDtl by Bank
        public int MoneyReceiveDtlSaveByBank(IpMoneyReceiveDtl moneyReceiveDtl)
        {
            return _moneyReceiveRepository.MoneyReceiveDtlSaveByBank(moneyReceiveDtl);
        }

        public DataTable Input(string SherchName)
        {
            return _moneyReceiveRepository.IpPatienId(SherchName);
        }
    }
}
