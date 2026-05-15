namespace HospitalBilling.Models
{
    public class Ward
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public string WardFor { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public string Details { get; set; }
    }
}