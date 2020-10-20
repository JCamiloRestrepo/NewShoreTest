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
        private Context.NewShoreContext db;

        public HttpClient Client { get; }

        public FlightsController(Context.NewShoreContext context)
        {
            db = context;
            Client = new HttpClient();
        }

        public async Task<IEnumerable<ResponseApi>> GetFlightsFromApi(string FlightOrigin, string FlightDestination, string FlightDate)
        {
            List<ResponseApi> respuesta = new List<ResponseApi>();
            var requestObject = new
            {
                Origin = FlightOrigin,
                Destination = FlightDestination,
                From = FlightDate
            };
            var requestJson = new StringContent(
                JsonSerializer.Serialize(requestObject),
                Encoding.UTF8,
                "application/json");

            var httpResponse =
                await Client.PostAsync("http://testapi.vivaair.com/otatest/api/values", requestJson);

            httpResponse.EnsureSuccessStatusCode();
            var responseStream = await httpResponse.Content.ReadAsStringAsync();
            responseStream = responseStream.Substring(1, responseStream.Length - 2).Replace("\\", "");
            respuesta = JsonSerializer.Deserialize<List<ResponseApi>>(responseStream);

            return respuesta;
        }

        [HttpGet]
        public async Task<IEnumerable<ResponseApi>> Flight(
            [FromQuery(Name = "origin")] string origin,
            [FromQuery(Name = "destination")] string destination,
            [FromQuery(Name = "from")] string from
            )
        {
            var response = await GetFlightsFromApi(origin, destination, from);

            return response;
        }
        
        [HttpPost]
        public async Task<FlightModel> SaveFlight([FromBody] ResponseApi flight)
        {
            FlightModel newFlight = new FlightModel()
            {
                DepartureStation = flight.DepartureStation,
                DepartureDate = flight.DepartureDate,
                ArrivalStation = flight.ArrivalStation,
                Currency = flight.Currency,
                Price = flight.Price,
                Transport = new TransportModel()
                {
                    FlightNumber = flight.FlightNumber
                }
            };
            db.Flights.Add(newFlight);
            await db.SaveChangesAsync();
            return newFlight;
        }

    }
}
