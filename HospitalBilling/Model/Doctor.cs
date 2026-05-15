namespace HospitalBilling.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public string DesignationName { get; set; } 
        public string Specialty { get; set; }
        public string ProfileBrief { get; set; }
        
    }
}