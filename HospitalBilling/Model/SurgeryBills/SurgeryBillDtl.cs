using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalBilling.Model.SurgeryBills
{
    [Serializable]
    public class SurgeryBillDtl
    {

        public int Id { get; set; }
        public int SurgeryBillMstId { get; set; }


        public int SurgeryTypeId { get; set; }
        public string SurgeryTypeName { get; set; }


        public int SurgeryId { get; set; }
        public string SurgeryName { get; set; }

        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal PayableAmount { get; set; }
        public DateTime DeliveryDate { get; set; }

        public int? Reference { get; set; }
    }
}