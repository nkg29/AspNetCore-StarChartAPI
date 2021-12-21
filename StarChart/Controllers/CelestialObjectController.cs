using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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

        [HttpGet("{id:int}")]
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

        [HttpPost]
        public IActionResult Create([FromBody]CelestialObject c)
        {
            _context.CelestialObjects.Add(c);
            _context.SaveChanges();
            // create at the route value the returned result from it
            return CreatedAtRoute("GetById", new { id = c.Id }, c);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var c = _context.CelestialObjects.Find(id);
            if (c == null)
                return NotFound();
            c.Name = celestialObject.Name;
            c.OrbitalPeriod = celestialObject.OrbitalPeriod;
            c.OrbitedObjectId = celestialObject.OrbitedObjectId;
            _context.CelestialObjects.Update(c);
            _context.SaveChanges();
            return NoContent();
        }
        
        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var c = _context.CelestialObjects.Find(id);
            if (c == null)
                return NotFound();
            c.Name = name;
            _context.CelestialObjects.Update(c);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var c = _context.CelestialObjects.Where(e => e.Id == id);
            if (!c.Any())
                return NotFound();
            _context.CelestialObjects.RemoveRange(c); // remove the whole list
            _context.SaveChanges();
            return NoContent();
        }
    }
}
