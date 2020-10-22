using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;

namespace NewShoreTest.ExternalAPIs.VivaAirAPI.Handler.Interface
{

    public interface VivaAirApiInterface
    {
        Task<IEnumerable<VivaAirApiResponse>> GetFlightsFromApi(string FlightOrigin, string FlightDestination, string FlightDate);

        [HttpPost]
        Task<IEnumerable<VivaAirApiResponse>> Flight(string origin, string destination,string from);

    }
}
