using CarsalesApp.Contexts;
using CarsalesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsalesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnqueriesController : Controller
    {
        private readonly VehicleContext _context;
        private readonly ILogger<EnqueriesController> _logger;

        // constructor
        // use constructor dependency injection here
        public EnqueriesController(VehicleContext context, ILogger<EnqueriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<controller>
        [HttpGet]
        public ActionResult<List<Enquery>> Get()
        {
            // egar load the referenced vehicle
            return _context.Enqueries.Include(enquery => enquery.Vehicle).ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetEnquery")]
        public ActionResult<Enquery> GetById(long id)
        {
            _logger.LogInformation("Getting enquery {ID}", id);

            var enquery = _context.Enqueries.Find(id);
            if(enquery == null)
            {
                _logger.LogWarning("GetById({ID}) NOT FOUND", id);
                return NotFound();
            }

            return enquery;
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(Enquery enquery)
        {
            _context.Add(enquery);
            _context.SaveChanges();

            return CreatedAtRoute("GetEnquery", new { id = enquery.Id }, enquery);
        }
    }
}
