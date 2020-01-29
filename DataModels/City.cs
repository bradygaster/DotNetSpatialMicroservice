using System.ComponentModel.DataAnnotations.Schema;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace DistanceApi.DataModels
{
    [Table("Cities", Schema = "Application")]
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName="geometry")]
        public Point Location { get; set; }
    }
}