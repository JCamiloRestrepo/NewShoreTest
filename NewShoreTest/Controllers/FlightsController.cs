using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewShoreTest.Context;
using NewShoreTest.Models;

namespace NewShoreTest.Controllers
{
    [Route("api/flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly NewShoreContext _context;
        private readonly IWebHostEnvironment _environment;

        private DbSet<FlightModel> _flightsDbSet;

        public FlightsController(NewShoreContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _flightsDbSet = context.Set<FlightModel>();
        }

        [HttpGet]
        public async Task<ActionResult<List<FlightModel>>> GetFlights()
        {
            return await _flightsDbSet
                .Include(f => f.Transport)
                .ToListAsync();
        }

        [HttpGet]
        public IActionResult Get()
        {
            using(NewShoreContext db = new NewShoreContext())
            {
                var lst = db.Flights.ToList();
                return Ok(lst);
            }
        }
    }
}