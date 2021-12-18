using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;
using PhoneBook.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PhoneBookContext _dbContext;
        private readonly IMediator _mediator;


        public PersonsController(PhoneBookContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == default)
                return BadRequest();

            var command = new GetPersonContactCommand
            {
                Id = id
            };


            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult> GetList()
        {
            return Ok(await _dbContext.Persons.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> CreatePerson(CreatePersonCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var command = new DeletePersonCommand
            {
                PersonId = id
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{personId}/phoneNumbers")]
        public async Task<IActionResult> GetPhoneNumbersByPersonId(Guid personId)
        {
            if (personId == default)
                return BadRequest();

            var response = await _dbContext.ContactDetail.Where(x => x.PersonId == personId & x.ContactType == ContactType.Phone).FirstOrDefaultAsync();

            return Ok(response.Value);

        }
    }
}
