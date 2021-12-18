using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Data;
using PhoneBook.Api.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly PhoneBookContext _dbContext;

        public LocationsController(PhoneBookContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{locationId}")]
        public async Task<IActionResult> GetLocation(Guid locationId)
        {
            if (locationId == default)
                return BadRequest();

            var response = await _dbContext.ContactDetail.FirstOrDefaultAsync(s => s.Id == locationId);

            return Ok(response);
        }

        [HttpGet("{locationName}/name")]
        public async Task<IActionResult> GetLocationByName(string locationName)
        {
            if (string.IsNullOrEmpty(locationName))
                return BadRequest();

            var response = await _dbContext.ContactDetail.Where(s => s.Value == locationName & s.ContactType == ContactType.Address).ToListAsync();


            return Ok(response);
        }
    }
}
