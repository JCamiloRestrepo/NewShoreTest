using Microsoft.AspNetCore.Mvc;
using NewShoreTest.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoreTest.Interfaces
{

    public interface IApi
    {
        Task<IEnumerable<ResponseApi>> GetFlightsFromApi(string FlightOrigin, string FlightDestination, string FlightDate);

        [HttpPost]
        Task<IEnumerable<ResponseApi>> Flight(string origin, string destination,string from);

    }
}
