using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewShoreTest.Context;
using NewShoreTest.Models;

namespace NewShoreTest.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/flights")]
    public class FlightsController
    {
        private readonly NewShoreContext Context;
        private readonly IWebHostEnvironment Environment;
        
        private DbSet<FlightModel> DbSet;

        public FlightsController(NewShoreContext context, IWebHostEnvironment environment)
        {
            Context = context;
            Environment = environment;
            DbSet = context.Set<FlightModel>();
        }
        
        [HttpGet("search")]
        public string SearchFlights()
        {
            var instance = new FlightModel();
            return "OK";
        }
    }
}