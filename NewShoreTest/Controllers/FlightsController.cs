using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewShoreTest.Context;
using NewShoreTest.Models;

namespace NewShoreTest.Controllers
{
    [Route("api/flights")] 
    public class FlightsController : ControllerBase
    {
        private Context.NewShoreContext db;

        public FlightsController(Context.NewShoreContext context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<FlightModel> Flight()
        {
            List<FlightModel> lst = (from d in db.Flights
                                     select new FlightModel
                                     {
                                         Id = d.Id,
                                         DepartureDate = d.DepartureDate,
                                         DepartureStation = d.DepartureStation,
                                         ArrivalStation = d.ArrivalStation,
                                         Transport = d.Transport,
                                         Price = d.Price,
                                         Currency = d.Currency,
                                         FkTransporte = d.FkTransporte,

                                     }).ToList();
            return lst;
                                       
        }

    }
}
