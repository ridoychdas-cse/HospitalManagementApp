namespace HospitalBilling.Models
{
    public class Package
    {
    }

    public class PackageType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }

    public class PackageMst
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int PackageTypeId { get; set; }
        public string PackageTypeName { get; set; }
        public string   Description { get; set; }

        public decimal RegularFee { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalFee { get; set; }
    }

    public class PackageDtl
    {
        public int Id { get; set; }
        public int PackageMstId { get; set; }
        public string ServiceType { get; set; }
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }
    }
}