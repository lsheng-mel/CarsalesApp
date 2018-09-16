using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarsalesApp.Contexts;
using CarsalesApp.Models;
using CarsalesApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsalesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly VehicleContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CarsController> _logger;

        // constructor
        // use constructor dependency injection here to inject the DB context and automapper
        public CarsController(VehicleContext context, IMapper mapper, ILogger<CarsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<VehicleBaseDTO>> Get()
        {
            _logger.LogInformation("Getting all cars");

            List<VehicleBaseDTO> vehicleDTOs = new List<VehicleBaseDTO>();
            foreach(var car in _context.Cars)
            {
                VehicleBaseDTO vehicleDTO;
                if(car.IsDealerVehicle())
                    vehicleDTO = _mapper.Map<DealerVehicleDTO>(car);
                else
                    vehicleDTO = _mapper.Map<NonDealerVehicleDTO>(car);

                vehicleDTOs.Add(vehicleDTO);
            }

            return vehicleDTOs;
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetCar")]
        public ActionResult<VehicleBaseDTO> GetById(long id)
        {
            _logger.LogInformation("Getting car {ID}", id);

            var car = _context.Cars.Find(id);
            if(car == null)
            {
                _logger.LogWarning("GetById({ID}) NOT FOUND", id);
                return NotFound();
            }

            if (car.IsDealerVehicle())
                return _mapper.Map<DealerVehicleDTO>(car);

            return _mapper.Map<NonDealerVehicleDTO>(car);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create(Car car)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vehicles.Add(car);
            _context.SaveChanges();

            return CreatedAtRoute("GetCar", new { id = car.Id }, car);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, Car updatedCar)
        {
            var car = _context.Cars.Find(id);
            if(car == null)
            {
                return NotFound();
            }

            // update the car's attributes
            car.Make = updatedCar.Make;
            car.Model = updatedCar.Model;
            car.Year = updatedCar.Year;

            car.DriveAwayPrice = updatedCar.DriveAwayPrice;
            car.ExcludingGovernmentChargesPrice = updatedCar.ExcludingGovernmentChargesPrice;

            car.ContactName = updatedCar.ContactName;
            car.ContactPhone = updatedCar.ContactPhone;
            car.ContactEmail = updatedCar.ContactEmail;

            car.Comment = updatedCar.Comment;
            car.ABN = updatedCar.ABN;

            _context.Cars.Update(car);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var car = _context.Cars.Find(id);
            if(car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
