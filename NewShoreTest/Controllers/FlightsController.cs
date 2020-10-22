using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NewShoreTest.Models;
using Microsoft.Extensions.Logging;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;

namespace NewShoreTest.Controllers
{
    [Route("api/flights")]
    public class FlightsController : ControllerBase
    {

        private readonly  DataBaseAccessObject.Handler.Interfaces.DataBaseInterface _dataBase;
        private readonly  ExternalAPIs.VivaAirAPI.Handler.Interface.VivaAirApiInterface _api;

        public FlightsController(DataBaseAccessObject.Handler.Interfaces.DataBaseInterface dataBase,
            ExternalAPIs.VivaAirAPI.Handler.Interface.VivaAirApiInterface api)
        {
            _dataBase = dataBase;
            _api = api;
        }

        [HttpPost]
        public async Task<FlightModel> SaveFlight([FromBody] VivaAirApiResponse flight)
        {

            FlightModel model = await _dataBase.SaveFlight(flight);
            return model;
        }

        [HttpGet]
        public async Task<IEnumerable<VivaAirApiResponse>> Flight(
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

