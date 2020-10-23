using Microsoft.AspNetCore.Mvc;
using NewShoreTest.DataBaseAccessObject.Handler.Interfaces;
using NewShoreTest.ExternalAPIs.VivaAirAPI.Response;
using NewShoreTest.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 * Class to implement the methods related to the DB
 * This class implements the DBInterface
 **/
namespace NewShoreTest.DataBaseAccessObject.Handler.Implementation
{
    [Route("api/flights")]
    public class DataBaseImplementation : DataBaseInterface
    {
        private readonly Context.NewShoreContext db;

        public DataBaseImplementation(Context.NewShoreContext context)
        {
            db = context;
        }

        /**
         * Method to save flights in DB
         * It receives as parameter a flight from API,
         * returns a flight with a model from the flights table
         **/

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
                return newFlight;
            }
            catch
            {
                return null;
            }
        }

        /**
         * Method to get all the flights from DB
         * returns a list with all saved flights
         **/
        [HttpGet("db")]
        public IEnumerable<FlightModel> GetFlights()
        {
            try
            {
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
                return lst;
            }
            catch
            {
                return null;
            }
        }


    }

}

