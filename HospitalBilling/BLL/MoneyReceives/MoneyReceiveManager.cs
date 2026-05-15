
using HospitalBilling.DAL.MoneyReceives;
using HospitalBilling.Models.MoneyReceives;

namespace HospitalBilling.BLL.MoneyReceives
{
    public class MoneyReceiveManager
    {
        private readonly MoneyRecieveGetway _moneyRecieveGetway;
        public MoneyReceiveManager()
        {
            _moneyRecieveGetway = new MoneyRecieveGetway();
        }

        public int MoneySave(MoneyReceiveMst moneyReceiveMst, MoneyReceiveDtl moneyReceiveDtl)
        {
            return _moneyRecieveGetway.MoneySave(moneyReceiveMst, moneyReceiveDtl);
        }

        // Outdoor Patient Money Receive Save
        public int OutdoorPatientMoneyReceiveSave(OpMoneyReceiveMst opMoneyReceiveMst,
            OpMoneyReceiveDtl opMoneyReceiveDtl)
        {
            return _moneyRecieveGetway.OutdoorPatientMoneyReceiveSave(opMoneyReceiveMst, opMoneyReceiveDtl);
        }


        // Get Outdoor Patient Diagnosis Bill By DiagnosisBillMstId
        public decimal GetOpDiagnosisPaymentByDiagnosisMstId(int diagnosisBillMstId)
        {
            return _moneyRecieveGetway.GetOpDiagnosisPaymentByDiagnosisMstId(diagnosisBillMstId);
        }




        internal int IpMoneyReceiveFormDiagnosisSave(UI.IpMoneyReceiveMst ipMoneyReceiveMst, OpMoneyReceiveDtl opMoneyReceiveDtl)
        {
            return _moneyRecieveGetway.IpMoneyReceiveFormDiagnosisSave(ipMoneyReceiveMst, opMoneyReceiveDtl);
        }




        // get indoor patient all payment
        public decimal GetIpAllPaymentInfo(int patientId, string patientType)
        {
            return _moneyRecieveGetway.GetIpPaymentHistoryByPatientId(patientId, patientType);
        }

        internal int SaveSpecialDiscount(int patientId, string patientType, decimal specialDiscount)
        {
            return _moneyRecieveGetway.SaveSpecialDiscount(patientId, patientType, specialDiscount);
        }
    }



}