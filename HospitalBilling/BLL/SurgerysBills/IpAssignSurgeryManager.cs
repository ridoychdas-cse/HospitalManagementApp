using HospitalBilling.DAL.SurgeryBills;
using HospitalBilling.Model.SurgeryBills;
using HospitalModels.Library.Billings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalBilling.BLL.SurgerysBills
{
    public class IpAssignSurgeryManager
    {
        private readonly IpAssignSurgeryRepository _assignSurgeryRepository;

        public IpAssignSurgeryManager()
        {
            _assignSurgeryRepository = new IpAssignSurgeryRepository();
        }

        // Store ViewState Value
        public List<SurgeryBillDtl> GetAllSelectedSurgery(SurgeryBillDtl surgeryBill, object p)
        {
            var surgeryBillDtlList = (List<SurgeryBillDtl>)p;
            surgeryBillDtlList.Add(surgeryBill);
            return surgeryBillDtlList;
        }


        // Save DiagnosisBillMst and DiagnosisBillDtl
        public int Save(SurgeryBillMst surgeryBillMst, List<SurgeryBillDtl> surgeryBillDtls)
        {
            return _assignSurgeryRepository.Save(surgeryBillMst, surgeryBillDtls);
        }

        // Auto BillNo
        public string GetAutoBillNumber()
        {
            return _assignSurgeryRepository.GetAutoBillNumber();
        }


        // Check Unique BillNo
        public bool BillNoUniqueCheck(string billNo)
        {
            bool isBillNoExist = false;
            var abillNo = _assignSurgeryRepository.BillNoUniqueCheck(billNo);
            if (abillNo != "")
            {
                isBillNoExist = true;
            }
            return isBillNoExist;
        }

        public MrParticular GetTotalSurgeryBill(int patientId)
        {
            return _assignSurgeryRepository.GetTotalSurgeryBill(patientId);
        }
    }
}