
using System;

namespace HospitalBilling.ViewModels
{
    public class OpDiagnsoisBillListViewModel
    {
        public int Id { get; set; }
        public string BillNo { get; set; }
        public decimal SpecialDiscount { get; set; }

        public decimal Vat { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public decimal TotalPayAmount { get; set; }
        public decimal TotalDueAmount { get; set; }

        public DateTime EntryDate { get; set; }
    }
}