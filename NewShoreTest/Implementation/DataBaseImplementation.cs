using Microsoft.AspNetCore.Mvc;
using NewShoreTest.Interfaces;
using NewShoreTest.Models;
using NewShoreTest.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoreTest.Implementation
{
    [Route("api/flights")]
    public class DataBaseImplementation : IDataBase
    {
        private Context.NewShoreContext db;

        public DataBaseImplementation(Context.NewShoreContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<FlightModel> SaveFlight(ResponseApi flight)
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
    }
}
