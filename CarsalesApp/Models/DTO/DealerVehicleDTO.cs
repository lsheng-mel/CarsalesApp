using System;

namespace CarsalesApp.Models.DTO
{
    // DTO for dealer advertised vehicles
    public class DealerVehicleDTO : VehicleBaseDTO
    {
        // dealer ABN is only available for dealer vehicle
        public String ABN { get; set; }
    }
}
