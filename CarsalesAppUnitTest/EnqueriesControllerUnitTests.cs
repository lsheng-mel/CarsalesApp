using CarsalesApp.Models;
using CarsalesApp.Contexts;
using CarsalesApp.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace CarsalesAppUnitTest
{
    public class EnqueriesControllerUnitTests
    {
        // create a cars controller object from scratch
        private EnqueriesController CreateEnqueriesController()
        {
            // db context
            var optionsBuilder = new DbContextOptionsBuilder<VehicleContext>();
            DbContextOptions<VehicleContext> contextOptions = optionsBuilder.UseInMemoryDatabase("TestVehicleSaleDatabase").Options;
            VehicleContext context = new VehicleContext(contextOptions);

            // logger
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<EnqueriesController> logger = loggerFactory.CreateLogger<EnqueriesController>();

            EnqueriesController ebqueriesController = new EnqueriesController(context, logger);

            return ebqueriesController;
        }

        [Fact]
        public void Enqueries_Test_Create()
        {
            // create a new car
            var car = (Car)((CreatedAtRouteResult)CarsControllerUnitTests.CreateNewCar()).Value;

            // get the controller
            var controller = CreateEnqueriesController();

            // prepare data
            const String Name = "Li Sheng";
            const String Phone = "0432833586";
            const String Postcode = "3172";
            const String MessageText = "Is the price negotiatable?";

            var enquery = new Enquery
            {
                VehicleId = car.Id,
                FullName = Name,
                PhoneNo = Phone,
                Postcode = Postcode,
                Message = MessageText
            };

            // Act
            var result = controller.Create(enquery);

            // Assert
            var resultData = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var createdEnquery = resultData.Value.Should().BeOfType<Enquery>().Subject;
            createdEnquery.VehicleId.Should().Be(car.Id);
            createdEnquery.FullName.Should().Be(Name);
            createdEnquery.PhoneNo.Should().Be(Phone);
            createdEnquery.Postcode.Should().Be(Postcode);
            createdEnquery.Message.Should().Be(MessageText);
        }
    }
}
