namespace HospitalBilling.Models
{
    public class Cabin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public decimal PriceDaily { get; set; }
        public bool Status { get; set; }
    }
}