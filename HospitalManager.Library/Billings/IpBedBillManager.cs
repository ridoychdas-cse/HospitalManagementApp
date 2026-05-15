using HospitalRepository.Library.Billings;

namespace HospitalManager.Library.Billings
{
    public class IpBedBillManager
    {
        private readonly IpBedBillRepository _bedBillRepository;


        public IpBedBillManager()
        {
            _bedBillRepository = new IpBedBillRepository();
        }

        // get bed bill by patient table id
        public decimal GetTotalBedBill(int patientId)
        {
            return _bedBillRepository.GetTotalBedBill(patientId);
        }
    }
}
