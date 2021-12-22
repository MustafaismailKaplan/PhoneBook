using FluentAssertions;
using NUnit.Framework;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PhoneBook.Api.Test.Commands
{

    using static Testing;

    public class CreateContactDetailCommandTests : TestBase
    { 

        [Test]
        public async Task ShouldCreateContactDetail()
        {
            var personCommand = new CreatePersonCommand()
            {
                Company = "Test Company",
                FirstName = "Test Mustafa",
                LastName = "Test Last Name"
            };

             await SendAsync(personCommand);

            var contactDetailCommand = new CreateContactDetailCommand()
            {
                PersonId = personCommand.Id,
                ContactType = ContactType.Address,
                Value = "Test Address"
            }; 

             _ = await SendAsync(contactDetailCommand);

            var contactDetail = await FindAsync<ContactDetail>(contactDetailCommand.Id);

             contactDetail.Should().NotBeNull();
             contactDetail!.PersonId.Should().Be(personCommand.Id);
             contactDetail.ContactType.Should().Be(contactDetailCommand.ContactType);
             contactDetail.Value.Should().Be(contactDetailCommand.Value);
        }
    }
}