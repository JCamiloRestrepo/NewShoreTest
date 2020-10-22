using Microsoft.AspNetCore.Mvc;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;
using NewShoreTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoreTest.DataBaseAccessObject.Handler.Interfaces
{
    public interface DataBaseInterface
    {
       [HttpPost]
       Task<FlightModel> SaveFlight(VivaAirApiResponse flight);
        
    }
}