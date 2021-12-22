using FluentAssertions;
using NUnit.Framework;
using Reporting.Api.Commands;
using Reporting.Api.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Reporting.Api.Test.Commands
{
    using static Testing;
    public  class CreatePersonReportsByLocationTests
    {
        [Test]
        public async Task ShouldCreatePersonReportsByLocation()
        {
            var reportCommand = new CreatePersonReportsByLocationCommand()
            {
                LocationId = Guid.NewGuid(),
                PersonId = Guid.NewGuid()

        };

            await SendAsync(reportCommand);

            var person = await FindAsync<Report>(reportCommand.Id);

            person.Should().NotBeNull();
            person.Id.Should().Be(reportCommand.Id);
            person.LocationId.Should().Be(reportCommand.LocationId);
            person.PersonId.Should().Be(reportCommand.PersonId);
        }
    }
}
