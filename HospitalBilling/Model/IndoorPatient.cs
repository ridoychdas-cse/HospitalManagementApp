using System;

namespace HospitalBilling.Models
{
    public class IndoorPatient
    {
        // patient basic information
        public int Id { get; set; }
        public int BillNoId { get; set; }
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public int BloodId { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public byte[] Image { get; set; }
        public DateTime EntryDate { get; set; }
        public string Remark { get; set; }
        public int? DivisinId { get; set; }
        public int? DistrictId { get; set; }
        public int? ThanaId { get; set; }

        // Gurdine Information
        public string GName { get; set; }
        public string GPhoneNo { get; set; }
        public string GGender { get; set; }
        public string GAge { get; set; }
        public string Relation { get; set; }
        public string GAddress { get; set; }


        // Consultant and Reference
        public int? ConsultantId { get; set; }
        public int? ReferenceById { get; set; }
        public int? StatusId { get; set; }



        public string BloodName { get; set; }

        public string ConsultantName { get; set; }

        public string ReferenceByName { get; set; }

        public string StatusName { get; set; }

        public string TypeWisePatientId { get; set; }
    }
}