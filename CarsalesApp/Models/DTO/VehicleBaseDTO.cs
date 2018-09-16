using System;

namespace CarsalesApp.Models
{
    // DTO base for holding data that is common to both dealer vehicle and non-dealer vehicle
    public class VehicleBaseDTO
    {
        // Primary Key
        public long Id { get; set; }

        // advertised sale type
        public VehicleSaleType SaleType { get; set; }

        // specification data
        public int Year { get; set; }
        public String Make { get; set; }
        public String Model { get; set; }

        // price data
        /// <note>
        /// different to the database model class Car, the client only needs to see the advertised price type
        /// </note>
        public double AdvertisedPrice { get; set; }

        // advertise price type
        public AdvertisedPriceType AdvertisedPriceType { get; set; }

        // contact details
        /// <note>
        /// neam & phone are only available for non-dealer vehicle
        /// </note>
        public String ContactEmail { get; set; }

        // comment
        public String Comment { get; set; }
    }
}
