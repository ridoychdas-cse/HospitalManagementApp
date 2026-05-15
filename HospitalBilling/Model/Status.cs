using System.Collections.Generic;

namespace HospitalBilling.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Status> GetAllStatusList()
        {
            var statusList = new List<Status>()
            {
                new Status(){Id = 1, Name = "Active"},
                new Status(){Id = 2, Name = "Inactive"}
            };
            return statusList;
        }
    }
}