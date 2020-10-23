using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                _logger.LogInformation("|INFO|" + "Los datos a ingresar en DB son:" 
                   + "\n" + "Origen: " + flight.DepartureStation
                   + "\n" + "Destino: " + flight.ArrivalStation
                   + "\n" + "Fecha: " + flight.DepartureDate
                   + "\n" + "Currency: " + flight.Currency
                   + "\n" + "Precio: " + flight.Price
                   + "\n" + "Numero de vuelo: " + flight.FlightNumber);
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
                _logger.LogInformation("|INFO|" + " El vuelo ha sido guardado con exito");
                return newFlight;
            }catch (Exception ex)
            {
                _logger.LogError("|ERROR|" + "El vuelo no puede ser guardado, datos recibidos :"
                   + "\n" + "Origen: " + flight.DepartureStation
                   + "\n" + "Destino: " + flight.ArrivalStation
                   + "\n" + "Fecha: " + flight.DepartureDate
                   + "\n" + "Currency: " + flight.Currency
                   + "\n" + "Precio: " + flight.Price
                   + "\n" + "Numero de vuelo: " + flight.FlightNumber);
                throw new Exception("Mensaje de error " + ex.Message);
            }
        }
        [HttpGet("db")]
        public IEnumerable<FlightModel> GetFlights()
        {
            try {
                _logger.LogInformation("|INFO|" + " Buscando vuelos en DB");
                List<FlightModel> lst = (from d in db.Flights
                                         select new FlightModel
                                         {
                                             Id = d.Id,
                                             DepartureStation = d.DepartureStation,
                                             DepartureDate = d.DepartureDate,
                                             ArrivalStation = d.ArrivalStation,
                                             FkTransporte = d.FkTransporte,
                                             Transport = d.Transport,
                                             Price = d.Price,
                                             Currency = d.Currency
                                         }).ToList();

                _logger.LogInformation("|INFO|" + " Los vuelos han sido buscados con exito");
                return lst;
            }catch(Exception ex)
            {
                _logger.LogError("|ERROR|" + "Los vuelos no puede ser mostrados :");
                throw new Exception("Mensaje de error " + ex.Message);
            }
            }

       
        
    }
}
