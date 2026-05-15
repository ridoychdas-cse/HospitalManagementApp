
using System;

namespace HospitalModels.Library.Billings.Op
{
    public class OpMoneyReceiveDtl
    {
        public int Id { get; set; }
        public int MoneyReceiveMstId { get; set; }
        public string PayMethode { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public string ChequerDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string MoneyReceiveBy { get; set; }
    }
}
