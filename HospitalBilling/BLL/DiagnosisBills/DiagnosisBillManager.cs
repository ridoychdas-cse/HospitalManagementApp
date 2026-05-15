using HospitalBilling.DAL.DiagnosisBills;
using HospitalBilling.DAL.MoneyReceives;
using HospitalBilling.Models.DiagnosisBills;
using System.Collections.Generic;
using System.Linq;

namespace HospitalBilling.BLL.DiagnosisBills
{
    public class DiagnosisBillManager
    {
        private readonly DiagnosisBillGetway _diagnosisBillGetway;
        private readonly MoneyRecieveGetway _moneyRecieveGetway;

        public DiagnosisBillManager()
        {
            _diagnosisBillGetway = new DiagnosisBillGetway();
            _moneyRecieveGetway = new MoneyRecieveGetway();
        }

        // Store ViewState Value
        public List<DiagnosisBillDtl> GetAllSelectedDiagnosis(DiagnosisBillDtl diagnosisBill, object p)
        {
            var diagnosisBillDtlList = (List<DiagnosisBillDtl>)p;
            diagnosisBillDtlList.Add(diagnosisBill);
            return diagnosisBillDtlList;
        }

        // Save DiagnosisBillMst and DiagnosisBillDtls
        internal int Save(DiagnosisBillMst diagnosisBillMst, List<DiagnosisBillDtl> diagnosisBillDtls)
        {
            return _diagnosisBillGetway.Save(diagnosisBillMst, diagnosisBillDtls);
        }

        // auto billNo
        internal string GetAutoBillNumber()
        {
            return _diagnosisBillGetway.GetAutoBillNumber();
        }


        // Check Unique BillNo
        internal bool BillNoIsExist(string billNo)
        {
            bool isBillNoExist = false;
            var abillNo = _diagnosisBillGetway.BillNoUniqueCheck(billNo);
            if (abillNo != "")
            {
                isBillNoExist = true;
            }
            return isBillNoExist;
        }


        // Get Total Diagnosis Bill Info By BillNo (DiagnossiBillMst Info)
        internal DiagnosisBillMst GetDiagnosisBillMstBillInfoByBillNo(string billNo)
        {
            return _diagnosisBillGetway.GetDiagnosisBillMstBillInfoByBillNo(billNo);
        }


        // Get DiagnsoisBill  detils info
        internal List<DiagnosisBillDtl> GetDiagnosisBillDtlsByMstId(int mstId)
        {
            return _diagnosisBillGetway.GetDiagnosisBillDtlsByMstId(mstId);
        }


        // Outdoor patient all Diagnosis billNo List
        internal List<string> GetAllBillNoList(int patientId, string patientType)
        {
            return _diagnosisBillGetway.GetAllBillNoList(patientId, patientType);
        }


        // Indoor patient Total PayableAmountby BillNoList
        internal decimal GetIPTotalPayableAmountByBillList(List<string> billList)
        {
            var list = billList;
            return list.Select(value => _diagnosisBillGetway.GetDiagnosisBillMstBillInfoByBillNo(value)).Select(diagnosisMst => diagnosisMst.TotalPayableAmount).Sum();
        }

        // Outdoor Patient Diagnosis Bill Payment and Due Calclutation 
        internal OpDiagnosisBillDto OutdoorPatientDiagnosisBillHistory(string billNo)
        {
            OpDiagnosisBillDto opDiagnosisBillDto = null;
            var diagnosisBillMsts = GetDiagnosisBillMstBillInfoByBillNo(billNo);
            if (diagnosisBillMsts != null)
            {
                opDiagnosisBillDto = new OpDiagnosisBillDto();
                var mstId = diagnosisBillMsts.Id;
                var payAmount = _moneyRecieveGetway.GetOpDiagnosisPaymentByDiagnosisMstId(mstId);

                opDiagnosisBillDto.Id = diagnosisBillMsts.Id;
                opDiagnosisBillDto.PatientId = diagnosisBillMsts.PatientId;
                opDiagnosisBillDto.PatientType = diagnosisBillMsts.PatientType;
                opDiagnosisBillDto.BillNo = diagnosisBillMsts.BillNo;
                opDiagnosisBillDto.TotalPayableAmount = diagnosisBillMsts.TotalPayableAmount;
                opDiagnosisBillDto.Vat = diagnosisBillMsts.Vat;
                opDiagnosisBillDto.TotalPayAmount = payAmount;
                opDiagnosisBillDto.DueAmount = opDiagnosisBillDto.TotalPayableAmount -
                                               opDiagnosisBillDto.TotalPayAmount;
            }

            return opDiagnosisBillDto;
        }



        // Outdoor Patient All Bill Payment and due Calclutor by Bill List
        internal List<OpDiagnosisBillDto> OpAllDiagnosisBillHistory(int patientId, string patientType)
        {
            var opDiagnosisBillDtoList = new List<OpDiagnosisBillDto>();
            var billList = GetAllBillNoList(patientId, patientType);
            if (billList != null)
            {
                foreach (var value in billList)
                {
                    var opDiagnosisBilldto = OutdoorPatientDiagnosisBillHistory(value);

                    opDiagnosisBillDtoList.Add(opDiagnosisBilldto);
                }
            }
            return opDiagnosisBillDtoList;
        }

        public DiagnosisBillMst GetDiagnosisBillMstById(int id)
        {
            return _diagnosisBillGetway.GetDiagnosisBillMstById(id);
        }

        internal decimal GetDiagnosisBillTotalNetPriceByDiagnosisBillMst(int id)
        {
            return _diagnosisBillGetway.GetDiagnosisBillTotalNetPriceByDiagnosisBillMst(id);
        }
    }
}