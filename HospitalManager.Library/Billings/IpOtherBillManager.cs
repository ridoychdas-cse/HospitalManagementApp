using HospitalModels.Library.Billings;
using HospitalRepository.Library.Billings;
using System.Collections.Generic;

namespace HospitalManager.Library.Billings
{
    public class IpOtherBillManager
    {
        private readonly IpOtherBillRepository _ipOtherBillRepository;

        public IpOtherBillManager()
        {
            _ipOtherBillRepository = new IpOtherBillRepository();
        }


        public int Save(OtherBill otherBill)
        {
            return _ipOtherBillRepository.Save(otherBill);
        }

        public int Update(int id, OtherBill otherBill)
        {
            return _ipOtherBillRepository.Update(id, otherBill);
        }

        public OtherBill GetOtherBillById(int id)
        {
            return _ipOtherBillRepository.GetOtherBillById(id);
        }
        public List<OtherBill> GetAllOtherBillsByPatientIdAndType(int pateintId, string patientType)
        {
            return _ipOtherBillRepository.GetAllOtherBillsByPatientIdAndType(pateintId, patientType);
        }


        // Get Total Bill By PatientId and Type
        public decimal GetTotalOtherBill(int patientId, string patientType)
        {
            return _ipOtherBillRepository.GetTotalOtherBill(patientId, patientType);
        }
    }
}
