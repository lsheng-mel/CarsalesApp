using System;
using System.ComponentModel.DataAnnotations;

namespace CarsalesApp.Models
{
    // This is the base class for all vehicle advertisements including cars, boats, bikes, caravans etc.
    // Note: I am making an assumption that the attributes/properties specified here are common to all vehicle types
    public class Vehicle
    {
        // Primary Key
        public long Id { get; set; }
        
        // advertised sale type
        [Required]
        public VehicleSaleType SaleType { get; set; }

        public bool IsDealerVehicle()
        {
            return SaleType == VehicleSaleType.DEALER;
        }

        // specification data
        [Required]
        public int Year { get; set; }
        [Required]
        public String Make { get; set; }
        [Required]
        public String Model { get; set; }

        // price data
        public double DriveAwayPrice { get; set; }
        public double ExcludingGovernmentChargesPrice { get; set; }
        public double AdvertisedPrice
        {
            // business logic for determining advertised price
            get
            {
                return (AdvertisedPriceType == AdvertisedPriceType.DAP) ? DriveAwayPrice : ExcludingGovernmentChargesPrice;
            }
        }

        // advertise price type
        public AdvertisedPriceType AdvertisedPriceType { get; set; }

        // contact details
        public String ContactName { get; set; }
        public String ContactPhone { get; set; }
        public String ContactEmail { get; set; }

        // comment
        public String Comment { get; set; }

        // dealer ABN
        public String ABN { get; set; }
    }
}
