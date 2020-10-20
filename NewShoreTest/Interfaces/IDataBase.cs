using Microsoft.AspNetCore.Mvc;
using NewShoreTest.Models;
using NewShoreTest.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoreTest.Interfaces
{
    public interface IDataBase
    {
       [HttpPost]
       Task<FlightModel> SaveFlight(ResponseApi flight);
        
    }
}