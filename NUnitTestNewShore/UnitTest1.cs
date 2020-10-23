using NewShoreTest.ExternalAPIs.VivaAirAPI.Handler.Implementation;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;
using NUnit.Framework;
using System.Linq;
using System;
using System.Diagnostics;

namespace UnitTestNewShore
{
    [TestFixture]
    public class UnitTest1
    {

        private VivaAirApiImplementation _vivaAirApiImplementation;

        [SetUp]
        public void SetUp()
        {
            _vivaAirApiImplementation = new VivaAirApiImplementation();

        }

        [Test]
        public void NewShoreTest_GetFlightsFromApi_ObtieneUnVueloDeLaApi_DevuelveVuelos()
        {
            string FlightOrigin = "BOG";
            string FlightDestination = "PEI";
            string FlightDate = "2020-11-20";

            var response = _vivaAirApiImplementation.GetFlightsFromApi(FlightOrigin,
                FlightDestination, FlightDate).Result.First();

            VivaAirApiResponse respuesta = new VivaAirApiResponse()
            {
                DepartureStation = "BOG",
                DepartureDate = new DateTime(2020, 11, 20, 11, 00, 00),
                ArrivalStation = "PEI",
                Currency = "COP",
                Price = (decimal)598814.0000,
                FlightNumber = "1244"
            };

            Assert.AreEqual(response.ArrivalStation, respuesta.ArrivalStation);
            Assert.AreEqual(response.DepartureStation, respuesta.DepartureStation);
            Assert.AreEqual(response.DepartureDate, respuesta.DepartureDate);
            Assert.AreEqual(response.Currency, respuesta.Currency);
            Assert.AreEqual(response.Price, respuesta.Price);
            Assert.AreEqual(response.FlightNumber, respuesta.FlightNumber);
        }

        [Test]
        public void NewShoreTest_GetFlightsFromApi_ObtieneUnVueloDeLaApi_DevuelveError()
        {

            string FlightOrigin = "";
            string FlightDestination = "";
            string FlightDate = "2020-11-20";

            var response = _vivaAirApiImplementation.GetFlightsFromApi(FlightOrigin,
                FlightDestination, FlightDate).Result;

            Debug.WriteLine(response);

            Assert.Null(response);
        }
    }
}
