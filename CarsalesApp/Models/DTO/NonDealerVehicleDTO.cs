using System;

namespace CarsalesApp.Models.DTO
{
    // DTO class from non-dealer advertised vehicle
    public class NonDealerVehicleDTO : VehicleBaseDTO
    {
        // Name & Phone are only available for non-dealer vehicle
        public String ContactName { get; set; }
        public String ContactPhone { get; set; }
    }
}
