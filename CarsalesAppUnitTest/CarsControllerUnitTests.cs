using System;
using System.Collections.Generic;
using AutoMapper;
using CarsalesApp.Contexts;
using CarsalesApp.Controllers;
using CarsalesApp.Models;
using CarsalesApp.Models.Automapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CarsalesAppUnitTest
{
    public class CarsControllerUnitTests
    {
        // some hard-coded car data for testing purpose
        const String Make = "Holden";
        const String Model = "Commodore";
        const int Year = 2018;
        const double DriveawayPrice = 30000;
        const double ExcludingGovernmentChargePrice = 28000;
        const String ContactName = "Jessic Jones";
        const String ContactEmail = "jjones@gmail.com";
        const String ContactPhone = "0432333333";

        const String Comment = "This is a very solid car";
        const String ABN = "8888888";

        // create a cars controller object from scratch
        public static CarsController CreateCarsController()
        {
            // db context
            var optionsBuilder = new DbContextOptionsBuilder<VehicleContext>();
            DbContextOptions<VehicleContext> contextOptions = optionsBuilder.UseInMemoryDatabase("TestVehicleSaleDatabase").Options;
            VehicleContext context = new VehicleContext(contextOptions);

            // automapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<VehicleAutoMapperProfile>());
            IMapper mapper = config.CreateMapper();

            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<CarsController> logger = loggerFactory.CreateLogger<CarsController>();

            CarsController carsController = new CarsController(context, mapper, logger);

            return carsController;
        }

        // create a new car with hardcoded attributes
        public static IActionResult CreateNewCar()
        {
            // get a cars controller object
            CarsController controller = CreateCarsController();

            Car car = new Car
            {
                SaleType = VehicleSaleType.DEALER,

                Make = Make,
                Model = Model,
                Year = Year,

                DriveAwayPrice = DriveawayPrice,
                ExcludingGovernmentChargesPrice = ExcludingGovernmentChargePrice,

                AdvertisedPriceType = AdvertisedPriceType.DAP,

                ContactName = ContactName,
                ContactEmail = ContactEmail,
                ContactPhone = ContactPhone,

                Comment = Comment,
                ABN = ABN
            };

            return controller.Create(car);
        }

        [Fact]
        public void Cars_Get()
        {
            // get a cars controller object
            CarsController controller = CreateCarsController();

            // Act
            var result = controller.Get();

            // Assert
            var resultData = result.Should().BeOfType<ActionResult<List<VehicleBaseDTO>>>().Subject;
            resultData.Value.Should().BeAssignableTo<List<VehicleBaseDTO>>();
        }

        [Fact]
        public void Cars_Get_Specific()
        {
            // create a new car
            var car = (Car)(((CreatedAtRouteResult)CreateNewCar()).Value);

            // get a cars controller object
            CarsController controller = CreateCarsController();

            // Act
            var result = controller.GetById(car.Id);

            // Assert
            var resultData = result.Should().BeOfType<ActionResult<VehicleBaseDTO>>().Subject;
            var createdCar = resultData.Value.Should().BeAssignableTo<VehicleBaseDTO>().Subject;
            createdCar.Id.Should().Be(car.Id);
        }

        [Fact]
        public void Cars_Create()
        {
            // Act
            var result = CreateNewCar();

            // Assert
            var resultData = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var newCar = resultData.Value.Should().BeOfType<Car>().Subject;
            newCar.Make.Should().Be(Make);
            newCar.Model.Should().Be(Model);
            newCar.Year.Should().Be(Year);
        }

        [Fact]
        public void Cars_Update()
        {
            // the new drive away price
            const double newDriveawayPrice = 35000;

            // create a new car
            var result = CreateNewCar();

            // get the id
            long Id = ((Car)((CreatedAtRouteResult)result).Value).Id;

            // updated with a different driveaway price
            Car updatedCar = new Car
            {
                SaleType = VehicleSaleType.DEALER,

                Make = Make,
                Model = Model,
                Year = Year,

                DriveAwayPrice = newDriveawayPrice,
                ExcludingGovernmentChargesPrice = 30000,

                AdvertisedPriceType = AdvertisedPriceType.DAP,

                ContactName = "Jessic Jones",
                ContactEmail = "jjones@gmail.com",
                ContactPhone = "0432333333",

                Comment = "This is a very solid car",
                ABN = "8888888"
            };

            // get an instance of the cars controller
            var controller = CreateCarsController();

            // Act
            var updateResult = controller.Update(Id, updatedCar);

            // Assert
            updateResult.Should().BeOfType<NoContentResult>();

            var car = (VehicleBaseDTO)controller.GetById(Id).Value;
            car.AdvertisedPrice.Should().Be(newDriveawayPrice);
        }

        [Fact]
        public void Cars_Delete()
        {
            // create a new car
            var result = CreateNewCar();

            // get the id
            long Id = ((Car)((CreatedAtRouteResult)result).Value).Id;

            // get an instance of the controller
            var controller = CreateCarsController();

            // Act
            var deleteResult = controller.Delete(Id);

            // Assert
            deleteResult.Should().BeOfType<NoContentResult>();

            var foundResult = controller.GetById(Id).Result;
            foundResult.Should().BeOfType<NotFoundResult>();
        }
    }
}
