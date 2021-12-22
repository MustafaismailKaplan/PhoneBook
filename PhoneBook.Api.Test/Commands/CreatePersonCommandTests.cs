using FluentAssertions;
using NUnit.Framework;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PhoneBook.Api.Test.Commands
{
    using static Testing;
   public class CreatePersonCommandTests : TestBase
    {

        [Test]
        public async Task ShouldCreatePersonCommand()
        {
            var personCommand = new CreatePersonCommand()
            {
                Company = "TestCompany",
                FirstName = "TestName",
                LastName = "TestLastName"
            };

            await SendAsync(personCommand);

            var person = await FindAsync<Person>(personCommand.Id);

            person.Should().NotBeNull();
            person.Id.Should().Be(personCommand.Id);
            person.FirstName.Should().Be(personCommand.FirstName);
            person.LastName.Should().Be(personCommand.LastName);
            person.Company.Should().Be(personCommand.Company);
        }
    }
}
