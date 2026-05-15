

using System;

namespace HospitalModels.Library.Billings
{
    [Serializable]
    public class IpPaymentHistory
    {
        public string PayType { get; set; }
        public decimal Amount { get; set; }
        public string PayMethode { get; set; }
        public string PayDate { get; set; }
    }
}
