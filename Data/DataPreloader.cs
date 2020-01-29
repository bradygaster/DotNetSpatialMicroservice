using System.Linq;
using DistanceApi.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace DistanceApi
{
    public class DataPreloader
    {
        public DataPreloader(ApplicationDbContext dbContext,
            ILogger<DataPreloader> logger)
        {
            DbContext = dbContext;
            Logger = logger;
        }

        public ApplicationDbContext DbContext { get; }
        public ILogger<DataPreloader> Logger { get; }

        private void Reset()
        {
            DbContext.Database.Migrate();
            DbContext.Cities.RemoveRange(DbContext.Cities);
            DbContext.SaveChanges();
        }

        internal void Seed()
        {
            Reset();
            
            // if nothing is here, seed it
            if (DbContext.Cities.Count() == 0)
            {
                SeedPoint("Bellevue", -122.2015, 47.6101);
                SeedPoint("Kent", -122.2348, 47.3809);
                SeedPoint("Kirkland", -122.2060, 47.6769);
                SeedPoint("Redmond", -122.1215, 47.6740);
                SeedPoint("Issaquah", -122.0326, 47.5301);
                SeedPoint("Renton", -122.2079, 47.4797);
                SeedPoint("Sammamish", -122.0356, 47.6163);
                SeedPoint("Seattle", -122.3321, 47.6062);
                SeedPoint("Spokane", -117.4260, 47.6588);
                SeedPoint("Tacoma", -122.4443, 47.2529);
                SeedPoint("Vancouver", -122.6761, 45.6257);

                DbContext.SaveChanges();
            }
        }

        private void SeedPoint(string name, double longitude, double latitude)
        {
            DbContext.Cities.Add(new City
            {
                Name = name,
                Location = new Point(longitude, latitude) { SRID = 4326 }
            });
        }
    }
}