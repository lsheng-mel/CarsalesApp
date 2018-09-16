using CarsalesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsalesApp.Contexts
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options)
            : base(options)
        {

        }

        // Use Table Per Hierachy inheritance approach here
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Car> Cars { get; set; }

        public DbSet<Enquery> Enqueries { get; set; }
    }
}
