using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewShoreTest.DataBaseAccessObject.Handler.Interfaces;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;
using NewShoreTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoreTest.DataBaseAccessObject.Handler.Implementation
{
    [Route("api/flights")]
    public class DataBaseImplementation : DataBaseInterface
    {
        private Context.NewShoreContext db;
        private readonly ILogger<DataBaseImplementation> _logger;

        public DataBaseImplementation(Context.NewShoreContext context, ILogger<DataBaseImplementation> logger)
        {
            db = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<FlightModel> SaveFlight(VivaAirApiResponse flight)
        {
            try
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
                _logger.LogInformation("El vuelo ha sido guardado con exito");
                return newFlight;
            }catch (Exception ex)
            {
                _logger.LogError("El vuelo no puede ser guardado" + ex.ToString());
                return null;

            }
        }
    }
}
