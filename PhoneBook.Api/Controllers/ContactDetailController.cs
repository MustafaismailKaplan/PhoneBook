using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/contactdetail")]
    [ApiController]
    public class ContactDetailController : ControllerBase
    {
        private readonly PhoneBookContext _dbContext;
        private readonly IMediator _mediator;


        public ContactDetailController(PhoneBookContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        [HttpGet("{personId}/personContact")]
        public async Task<IActionResult> GetPersonId(Guid personId)
        {
            if (personId == default)
                return BadRequest();

            var response = await _dbContext.ContactDetail.Where(s => s.PersonId == personId).ToListAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default)
                return BadRequest();

            var response = await _dbContext.ContactDetail.FirstOrDefaultAsync(s => s.Id == id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetList()
        {
            return Ok(await _dbContext.ContactDetail.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> CreateContactDetail(CreateContactDetailCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactDetail(Guid id)
        {
            var command = new DeleteContactDetailCommand
            {
                Id = id
            };
            return Ok(await _mediator.Send(command));
        }
    }
}
