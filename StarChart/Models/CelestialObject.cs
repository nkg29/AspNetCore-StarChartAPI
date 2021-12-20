using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace StarChart.Models
{
    public class CelestialObject
    {
        public int Id;
        [Required] public string Name;
        public int? OrbitedObjectId;
        [NotMapped] public List<CelestialObject> Satellites;
        public TimeSpan OrbitalPeriod;
    }
}
