using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewShoreTest.Models.Response;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace NewShoreTest.Controllers
{
    [Route("api/flights")] 
    public class FlightsController : ControllerBase
    {
        private Context.NewShoreContext db;
        
        public HttpClient Client { get; }

        public FlightsController(Context.NewShoreContext context, HttpClient client)
        {
            db = context;
            Client = client;
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

            respuesta = JsonSerializer.Deserialize<List<ResponseApi>>(responseStream);

            Debug.WriteLine("hae", respuesta.ToString());

            return respuesta;
        }

        [HttpGet]
        public async Task<IEnumerable<ResponseApi>> Flight()
        {
            var response = await GetFlightsFromApi("MDE", "CTG", "2020-01-01");

            
            //List<ResponseApi> lst = (from d in db.Flights
            //                         select new FlightModel
            //                         {
            //                             Id = d.Id,
            //                             DepartureDate = d.DepartureDate,
            //                             DepartureStation = d.DepartureStation,
            //                             ArrivalStation = d.ArrivalStation,
            //                             Transport = d.Transport,
            //                             Price = d.Price,
            //                             Currency = d.Currency,
            //                             FkTransporte = d.FkTransporte,

            //                         }).ToList();
            
            return response;
        }

    }
}
