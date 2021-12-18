using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.Api.Data;
using PhoneBook.Api.Events;
using Shared.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Api.Commands.Handlers
{
    public class DeletePersonCommandHandler : AsyncRequestHandler<DeletePersonCommand>
    {
        private readonly PhoneBookContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public DeletePersonCommandHandler(PhoneBookContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _dbContext.Persons.FirstOrDefaultAsync(s => s.Id == command.PersonId);


            if (person != null)
            {
                var contactDetails = await _dbContext.ContactDetail.Where(s => s.PersonId == command.PersonId).ToListAsync();
                foreach (var item in contactDetails)
                {
                    _dbContext.ContactDetail.Remove(item);
                }

                _dbContext.Persons.Remove(person);

                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : Person deleted.");

            await _busPublisher.PublishAsync(new PersonDeleted(person.Id, person.FirstName, person.LastName,
                 person.Company), null);

        }
    }
}
