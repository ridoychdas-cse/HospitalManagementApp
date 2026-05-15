using System;

namespace HospitalBilling.Models.MoneyReceives
{
    public class MoneyReceiveDtl
    {
        public int Id { get; set; }
        public int MoneyReceiveMstId { get; set; }
        public string PayMethode { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal PayAmount { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}