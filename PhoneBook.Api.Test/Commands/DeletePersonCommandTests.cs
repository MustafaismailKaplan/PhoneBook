using FluentAssertions;
using NUnit.Framework;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PhoneBook.Api.Test.Commands
{
    using static Testing;
    public class DeletePersonCommandTests
    {


        [Test]
        public async Task ShouldDeletePersonCommand()
        {
            var personCommand = new CreatePersonCommand()
            {
                Company = "TestCompany",
                FirstName = "TestName",
                LastName = "TestLastName"
            };

            await SendAsync(personCommand);

            await SendAsync(new DeletePersonCommand
            {
                PersonId = personCommand.Id
            });


            var person = await FindAsync<Person>(personCommand.Id);

            person.Should().BeNull();
        }

    }
}
