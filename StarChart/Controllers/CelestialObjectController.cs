using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // field NOT a property so no get; set; required
                                                        // this doesn't get exposed outside for others when they create objects of that type

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id: int}")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if (celestialObject == null)
            {
                return NotFound();
            }
            celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObject = _context.CelestialObjects.Where(c => c.Name == name).ToList();
            if (!celestialObject.Any())
                return NotFound();
            foreach (var ce in celestialObject)
            {
                ce.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == ce.Id).ToList();
            }
            return Ok(celestialObject);
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();
            foreach (var ce in celestialObjects)
            {
                ce.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == ce.Id).ToList();
            }
            return Ok(celestialObjects);
        }
    }
}
