using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Handler.Interface;
using System.Diagnostics;

/**
 * Class to implement the methods related to the API
 * This class implements the APIInterface
 **/

namespace NewShoreTest.ExternalAPIs.VivaAirAPI.Handler.Implementation
{
    [Route("api/flights")]
    public class VivaAirApiImplementation : VivaAirApiInterface
    {
        public HttpClient Client { get; }


        public VivaAirApiImplementation()
        {
            Client = new HttpClient();
        }

        /**
         * Method to get flights from externalAPI by method post
         * It receives three parameters sent from user request,
         * returns a list with all the flights found
         **/

        public async Task<IEnumerable<VivaAirApiResponse>> GetFlightsFromApi(string FlightOrigin,
            string FlightDestination, string FlightDate)
        {
            try
            {
                List<VivaAirApiResponse> reply = new List<VivaAirApiResponse>();
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
                reply = JsonSerializer.Deserialize<List<VivaAirApiResponse>>(responseStream);

                return reply;

            }
            catch
            {
                return null;
            }
        }

        /**
        * Method to get flights from externalAPI by method get
        * It receives three parameters sent from user request,
        * returns a list with all the flights found
        **/
        [HttpGet]
        public async Task<IEnumerable<VivaAirApiResponse>> Flight(string origin, string destination, string from)
        {
            try
            {

                var response = await GetFlightsFromApi(origin, destination, from);
                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
