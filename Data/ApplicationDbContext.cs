using DistanceApi.DataModels;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace DistanceApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}