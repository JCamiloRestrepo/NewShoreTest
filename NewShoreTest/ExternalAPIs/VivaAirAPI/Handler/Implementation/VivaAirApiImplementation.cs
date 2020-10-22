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
using Microsoft.Extensions.Logging;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Handler.Interface;

namespace NewShoreTest.ExternalAPIs.VivaAirAPI.Handler.Implementation
{
    [Route("api/flights")]
    public class VivaAirApiImplementation : VivaAirApiInterface
    {
        public HttpClient Client { get; }
        private readonly ILogger<VivaAirApiImplementation> _logger;

        public VivaAirApiImplementation(ILogger<VivaAirApiImplementation> logger)
        {
            Client = new HttpClient();
            _logger = logger;
        }


        public async Task<IEnumerable<VivaAirApiResponse>> GetFlightsFromApi(string FlightOrigin, 
            string FlightDestination, string FlightDate)
        {
            try
            {
                {
                    List<VivaAirApiResponse> respuesta = new List<VivaAirApiResponse>();
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
                    respuesta = JsonSerializer.Deserialize<List<VivaAirApiResponse>>(responseStream);

                    _logger.LogInformation("Los parametros de busqueda son correctos, vuelos encontrados");
                    return respuesta;

                }
            }catch (Exception ex)
            {
                _logger.LogError("Los parametros de busqueda no son correctos" + ex.Message);
                return null;

            }

        }

        [HttpGet]
        public async Task<IEnumerable<VivaAirApiResponse>> Flight(string origin, string destination, string from)
        {

            {
                var response = await GetFlightsFromApi(origin, destination, from);

                return response;
            }
        }
    }
}
