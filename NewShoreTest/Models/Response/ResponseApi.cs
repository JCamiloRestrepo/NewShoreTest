using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoreTest.Models.Response
{
    public class ResponseApi
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureDate { get; set; }
        public string Transport { get; set; }          
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
