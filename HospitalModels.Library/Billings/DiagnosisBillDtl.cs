
using System;

namespace HospitalModels.Library.Billings
{
    [Serializable]
    public class DiagnosisBillDtl
    {
        public int Id { get; set; }
        public int DiagnosisBillMstId { get; set; }


        public int DiagnosisTypeId { get; set; }
        public string DiagnosisTypeName { get; set; }


        public int DiagnosisId { get; set; }
        public string DiagnosisName { get; set; }

        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal PayableAmount { get; set; }
        public DateTime DeliveryDate { get; set; }

        public int? Reference { get; set; }

        public decimal Amount { get; set; }

        public int Serial { get; set; }

        public string Particular { get; set; }
    }
}
