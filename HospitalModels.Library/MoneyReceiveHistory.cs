
using System;

namespace HospitalModels.Library
{
    public class MoneyReceiveHistory
    {
        public DateTime EntryDate { get; set; }
        public string MoneyReceiveBy { get; set; }
        public string PayMethode { get; set; }
        public decimal PayAmount { get; set; }
    }
}
