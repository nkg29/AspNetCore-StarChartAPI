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
    }
}
