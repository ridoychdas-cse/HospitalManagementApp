using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace HospitalBilling.Models
{
    public class CommonModel
    {
        public List<WardFor> GetAllWardFors()
        {
            var wardFor = new List<WardFor>()
            {
                new WardFor(){Id = "M", Name = "Male"},
                new WardFor(){Id = "F", Name = "Female"},
            };
            return wardFor;
        }
    }

    public class WardFor
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}