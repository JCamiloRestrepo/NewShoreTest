using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewShoreTest.Models;
using Microsoft.Extensions.Logging;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;
using System;

namespace NewShoreTest.Controllers
{
    [Route("api/flights")]
    public class FlightsController : ControllerBase
    {

        private readonly DataBaseAccessObject.Handler.Interfaces.DataBaseInterface _dataBase;
        private readonly ExternalAPIs.VivaAirAPI.Handler.Interface.VivaAirApiInterface _api;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(DataBaseAccessObject.Handler.Interfaces.DataBaseInterface dataBase,
            ExternalAPIs.VivaAirAPI.Handler.Interface.VivaAirApiInterface api,
            ILogger<FlightsController> logger)
        {
            _dataBase = dataBase;
            _api = api;
            _logger = logger;
        }

        [HttpPost]
        public async Task<FlightModel> SaveFlight([FromBody] VivaAirApiResponse flight)
        {

            try
            {
                _logger.LogInformation("The data to be inserted in the DB are:"
                  + "\n" + "Origen: " + flight.DepartureStation
                  + "\n" + "Destino: " + flight.ArrivalStation
                  + "\n" + "Fecha: " + flight.DepartureDate
                  + "\n" + "Currency: " + flight.Currency
                  + "\n" + "Precio: " + flight.Price
                  + "\n" + "Numero de vuelo: " + flight.FlightNumber);

                FlightModel model = await _dataBase.SaveFlight(flight);
                _logger.LogInformation("The flight has been saved successfully");
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError("Flight cannot be saved, data received :"
                   + "\n" + "Origen: " + flight.DepartureStation
                   + "\n" + "Destino: " + flight.ArrivalStation
                   + "\n" + "Fecha: " + flight.DepartureDate
                   + "\n" + "Currency: " + flight.Currency
                   + "\n" + "Precio: " + flight.Price
                   + "\n" + "Numero de vuelo: " + flight.FlightNumber);
                throw new Exception("Mensaje de error " + ex.Message);
            }
        }


        [HttpGet]
        public async Task<IEnumerable<VivaAirApiResponse>> Flight(
                [FromQuery(Name = "origin")] string origin,
                [FromQuery(Name = "destination")] string destination,
                [FromQuery(Name = "from")] string from)
        {
            try
            {
                _logger.LogInformation("The search parameters received are:"
                    + "\n" + "Origen: " + origin
                    + "\n" + "Destino: " + destination
                    + "\n" + "Fecha: " + from);

                var response = await _api.Flight(origin, destination, from);
                _logger.LogInformation("Search parameters are correct, flights found");

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError("Search parameters are not correct."
                    + "\n" + "Received parameters:" 
                    + "\n" + "Origen: " + origin
                    + "\n" + "Destino: " + destination
                    + "\n" + "Fecha: " + from + "\n" + ex.Message);
                throw new Exception("Mensaje de error" + ex.Message);

            }
        }

        [HttpGet("db")]
        public IEnumerable<FlightModel> GetFlights()
        {
            try
            {

                _logger.LogInformation("Searching for flights in DB");
                IEnumerable<FlightModel> flight = _dataBase.GetFlights();
                _logger.LogInformation("Flights have been successfully searched");
                return flight;
            }
            catch (Exception ex)
            {
                _logger.LogError("Flights cannot be displayed");
                throw new Exception("Mensaje de error " + ex.Message);
            }
        }
    }
}