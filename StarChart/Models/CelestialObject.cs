using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace StarChart.Models
{
    public class CelestialObject
    {
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        public int? OrbitedObjectId { get; set; }
        [NotMapped] 
        public List<CelestialObject> Satellites { get; set; }
        public TimeSpan OrbitalPeriod { get; set; }
    }
}
