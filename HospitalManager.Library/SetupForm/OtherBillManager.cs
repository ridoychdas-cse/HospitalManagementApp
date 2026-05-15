
using HospitalModels.Library.Billings;
using HospitalRepository.Library.SetupForm;
using System.Collections.Generic;

namespace HospitalManager.Library.SetupForm
{
    public class OtherBillManager
    {
        private readonly OtherBillRepository _otherBillRepository;

        public OtherBillManager()
        {
            _otherBillRepository = new OtherBillRepository();
        }

        public int Save(OtherBillType otherBill)
        {
            return _otherBillRepository.Save(otherBill);
        }

        public int Update(int id, OtherBillType otherBill)
        {
            return _otherBillRepository.Update(id, otherBill);
        }

        public int Delete(int id)
        {
            return _otherBillRepository.Delete(id);
        }

        public IEnumerable<OtherBillType> GetAllOtherBills()
        {
            return _otherBillRepository.GetAllOtherBills();
        }

        public OtherBillType GetOtherBillById(int id)
        {
            return _otherBillRepository.GetOtherBillById(id);
        }
    }
}
