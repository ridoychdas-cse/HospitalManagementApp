using System;

namespace HospitalBilling.Models
{
    public class PatientBedInfo
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public string RoomType { get; set; }
        public int? WardId { get; set; }
        public int? BedId { get; set; }
        public decimal? BedPrice { get; set; }
        public int? CabinType { get; set; }
        public int? CabinId { get; set; }
        public decimal? CabinPrice { get; set; }
        public DateTime? AdmitDate { get; set; }
        public DateTime? ReleseDate { get; set; }

        public string BedName { get; set; }

        public string CabinName { get; set; }
    }
}