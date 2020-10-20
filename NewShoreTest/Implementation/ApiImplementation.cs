using NewShoreTest.Interfaces;
using NewShoreTest.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NewShoreTest.Models;

namespace NewShoreTest.Implementation
{
    [Route("api/flights")]
    public class ApiImplementation : IApi
    {
        public HttpClient Client { get; }

        public ApiImplementation()
        {
            Client = new HttpClient();
        }


        public async Task<IEnumerable<ResponseApi>> GetFlightsFromApi(string FlightOrigin, string FlightDestination, string FlightDate)
        {
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
        }

        [HttpGet]
        public async Task<IEnumerable<ResponseApi>> Flight(string origin, string destination, string from)
        {
            {
                var response = await GetFlightsFromApi(origin, destination, from);

                return response;
            }
        }
    }
}
