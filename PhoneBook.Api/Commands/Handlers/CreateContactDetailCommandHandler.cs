using MediatR;
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
    public class CreateContactDetailCommandHandler : AsyncRequestHandler<CreateContactDetailCommand>
    {
        private readonly PhoneBookContext _dbContext;
        private readonly ILogger<CreateContactDetailCommandHandler> _logger;
        private readonly IBusPublisher _busPublisher;

        public CreateContactDetailCommandHandler(PhoneBookContext dbContext,
                                          ILogger<CreateContactDetailCommandHandler> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task Handle(CreateContactDetailCommand command, CancellationToken cancellationToken)
        {
            
            _dbContext.ContactDetail.Add(new Data.Entities.ContactDetail
            {
                Id = command.Id,
                Value=command.Value,
                ContactType=command.ContactType,
                PersonId=command.PersonId
            });

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"[Local Transaction] : ContactDetail created.");

            await _busPublisher.PublishAsync(new ContactDetailCreated(command.Id, command.ContactType, command.Value,
                 command.PersonId), null);
        }
    }
}
