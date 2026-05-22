
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

        #region Other Bill Info Insert Update Delete
        public int Save(OtherBillType otherBill)
        {
            return _otherBillRepository.Save(otherBill);
        }
        public int Update(OtherBillType otherBill)
        {
            return _otherBillRepository.Update(otherBill);
        }
        public int Delete(OtherBillType otherBill)
        {
            return _otherBillRepository.Delete(otherBill);
        }
        #endregion
        #region Other Bill Info Get
        public IEnumerable<OtherBillType> GetAllOtherBills()
        {
            return _otherBillRepository.GetAllOtherBills();
        }

        public OtherBillType GetOtherBillById(int id)
        {
            return _otherBillRepository.GetOtherBillById(id);
        }
        #endregion
    }
}
