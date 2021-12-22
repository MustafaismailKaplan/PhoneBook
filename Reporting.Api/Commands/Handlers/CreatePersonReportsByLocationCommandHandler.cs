using MediatR;
using Microsoft.Extensions.Logging;
using Reporting.Api.Data;
using Reporting.Api.Data.Entity;
using Reporting.Api.Events;
using Shared.MessageHandlers;
using Shared.RabbitMq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reporting.Api.Commands.Handlers
{
    public class CreatePersonReportsByLocationCommandHandler : AsyncRequestHandler<CreatePersonReportsByLocationCommand>
    {
        private readonly ReportDbContext _dbContext;
        private readonly ILogger<CreatePersonReportsByLocationCommand> _logger;

        public CreatePersonReportsByLocationCommandHandler(ReportDbContext dbContext,
                                         ILogger<CreatePersonReportsByLocationCommand> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        protected override async Task Handle(CreatePersonReportsByLocationCommand command, CancellationToken cancellationToken)
        {
            var report = new Report
            {
                Status = ReportStatus.Created,
                Id = Guid.NewGuid(),
                PersonId = command.PersonId,
                LocationId = command.LocationId,
                PersonCount = 0,
                PhoneNumberCount = 0,
                Name = typeof(CreatePersonReportsByLocationCommand).Name
            };

            _dbContext.Reports.Add(report);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"[Local Transaction] : Report is creating");

        }
    }
}
