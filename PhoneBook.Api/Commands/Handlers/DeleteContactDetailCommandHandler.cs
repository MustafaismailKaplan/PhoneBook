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
    public class DeleteContactDetailCommandHandler : AsyncRequestHandler<DeleteContactDetailCommand>
    {
        private readonly PhoneBookContext _dbContext;
        private readonly ILogger<CreatePersonCommandHandler> _logger;

        public DeleteContactDetailCommandHandler(PhoneBookContext dbContext,
                                          ILogger<CreatePersonCommandHandler> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeleteContactDetailCommand command, CancellationToken cancellationToken)
        {
            var contactDetail = await _dbContext.ContactDetail.FirstOrDefaultAsync(s => s.Id == command.Id);

            if (contactDetail != null)
            {
                

                _dbContext.ContactDetail.Remove(contactDetail);

                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation($"[Local Transaction] : ContactDetail deleted.");

        }
    }
}
