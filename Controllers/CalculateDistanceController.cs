using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace DistanceApi.Controllers
{
    [ApiController]
    public class CalculateDistanceController : ControllerBase
    {
        public CalculateDistanceController(ApplicationDbContext dbContext,
            DataPreloader dataPreloader,
            ILogger<CalculateDistanceController> logger)
        {
            DbContext = dbContext;
            DataPreloader = dataPreloader;
            Logger = logger;

            DataPreloader.Seed();
        }

        public ApplicationDbContext DbContext { get; }
        public DataPreloader DataPreloader { get; }
        public ILogger<CalculateDistanceController> Logger { get; }

        [HttpGet]
        [Route("cities")]
        public ActionResult<IEnumerable<DistanceApi.ServiceModels.City>> Get()
        {
            return DbContext.Cities.Select(x => new DistanceApi.ServiceModels.City
            {
                Name = x.Name,
                Latitude = x.Location.Y,
                Longitude = x.Location.X
            }).ToList();
        }

        [HttpGet]
        [Route("calculatedistance/{fromCity}/{toCity}")]
        public ActionResult<double> Get(string fromCity, string toCity)
        {
            var from = DbContext.Cities.First(x => x.Name == fromCity);
            var to = DbContext.Cities.First(x => x.Name == toCity);
            var distance = from.Location.ProjectTo(2855).Distance(to.Location.ProjectTo(2855));

            return new ActionResult<double>(distance/1000);
        }
    }
}