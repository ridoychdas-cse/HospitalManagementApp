namespace HospitalBilling.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public byte[] Image { get; set; }
        public string  PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string OrganizationSpeech { get; set; }
        public string Description { get; set; }
    }
}