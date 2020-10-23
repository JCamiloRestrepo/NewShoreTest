using Microsoft.AspNetCore.Mvc;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;
using NewShoreTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/**
   * Interface of the methods to implement in DB
 **/

namespace NewShoreTest.DataBaseAccessObject.Handler.Interfaces
{
    public interface DataBaseInterface
    {
        [HttpPost]
        Task<FlightModel> SaveFlight(VivaAirApiResponse flight);

        [HttpGet("db")]
        IEnumerable<FlightModel> GetFlights();

    }
}