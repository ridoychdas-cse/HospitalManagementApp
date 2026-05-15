using System;

namespace HospitalBilling.Models
{
    public class OutdoorPatient
    {
        public int Id { get; set; }
        public int BillNoId { get; set; }
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public DateTime EntryDate { get; set; }
        public int? DepartmentId { get; set; }
        public string   DepartmentName { get; set; }
        public int? DoctorId { get; set; }
        public string DoctioName { get; set; }
        public int? ReferenceById { get; set; }
        public string ReferenceByName { get; set; }
        public string TypeWisePatientId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? ThanaId { get; set; }

    }
}