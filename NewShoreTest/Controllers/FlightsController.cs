using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewShoreTest.Models.Response;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NewShoreTest.Models;

namespace NewShoreTest.Controllers
{
    [Route("api/flights")]
    public class FlightsController : ControllerBase
    {

        Interfaces.IDataBase _dataBase;
        Interfaces.IApi _api;

        public FlightsController(Interfaces.IDataBase dataBase, Interfaces.IApi api)
        {
            _dataBase = dataBase;
            _api = api;
        }

        [HttpPost]
        public async Task<FlightModel> SaveFlight([FromBody] ResponseApi flight)
        {

            FlightModel model = await _dataBase.SaveFlight(flight);
            return model;
        }

        [HttpGet]
        public async Task<IEnumerable<ResponseApi>> Flight(
            [FromQuery(Name = "origin")] string origin,
            [FromQuery(Name = "destination")] string destination,
            [FromQuery(Name = "from")] string from)
        {
            {
                var response = await _api.Flight(origin, destination, from);

                return response;
            }
        }

    }
}

