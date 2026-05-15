using HospitalModels.Library.Billings;
using HospitalRepository.Library.Billings;
using System.Collections.Generic;

namespace HospitalManager.Library.Billings
{
    public class IpPaymentHistoryManager
    {
        private readonly IpPaymentHistoryRepository _ipPaymentHistoryRepository;

        public IpPaymentHistoryManager()
        {
            _ipPaymentHistoryRepository = new IpPaymentHistoryRepository();
        }

        public List<IpMoneyReceiveMst> GetAllIpMoneyReceiveMstsByPatientIdAndType(int patientId, string patientType)
        {
            return _ipPaymentHistoryRepository.GetAllIpMoneyReceiveMstsByPatientIdAndType(patientId, patientType);
        }

        public IpMoneyReceiveDtl GetIpMoneyReceiveDtlsByMoneyReceiveMstId(int moneyReceiveMstId)
        {
            return _ipPaymentHistoryRepository.GetIpMoneyReceiveDtlsByMoneyReceiveMstId(moneyReceiveMstId);
        }

        // payment Calclution
        public decimal TotalPaymentByPatientIdandPatientType(int patientId, string patientType)
        {
            return _ipPaymentHistoryRepository.TotalPaymentByPatientIdandPatientType(patientId, patientType);
        }

        // Get Discount Informaton
        public decimal TotalSpecialDiscountByPatientIdAndType(int patientId, string patientType)
        {
            return _ipPaymentHistoryRepository.TotalSpecialDiscountByPatientIdAndType(patientId, patientType);
        }
    }
}
