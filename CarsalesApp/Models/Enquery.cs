using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarsalesApp.Models
{
    public class Enquery
    {
        // Primary Key
        public long Id { get; set; }

        // Foreign Key
        [Required]
        public long VehicleId { get; set; }

        // Navigation property
        public Vehicle Vehicle { get; set; }

        [Required]
        public String FullName { get; set; }
        [Required]
        public String PhoneNo { get; set; }
        [Required]
        public String Postcode { get; set; }
        public String Message { get; set; }
    }
}
