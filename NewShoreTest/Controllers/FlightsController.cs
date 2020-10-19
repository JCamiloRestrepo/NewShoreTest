using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewShoreTest.Models.Response;
using System.Text.Json.Serialization;
using System.Diagnostics;
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

            Debug.WriteLine("hae", respuesta.ToString());

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

    }
    public bool Add(ResponseApi responseApi)
    {

        try
        {
            FlightModel flight = new FlightModel()
            {
                DepartureStation = responseApi.DepartureStation,
                DepartureDate = responseApi.DepartureDate,
                ArrivalStation = responseApi.ArrivalStation,
                Currency = responseApi.Currency,
                Price = responseApi.Price,
                FkTransporte = responseApi.FlightNumber

            };
            //using (NewShoreContext db = new NewShoreContext())
            return true;
        }
        catch
        {
            return false;
        }

    }
}
