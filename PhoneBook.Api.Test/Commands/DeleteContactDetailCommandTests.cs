using FluentAssertions;
using NUnit.Framework;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PhoneBook.Api.Test.Commands
{
    using static Testing;
    public class DeleteContactDetailCommandTests
    {

        [Test]
        public async Task ShouldDeleteContactDetailCommand()
        {
            var personCommand = new CreatePersonCommand()
            {
                Company = "Test Company ",
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

            await SendAsync(contactDetailCommand);

            await SendAsync(new DeleteContactDetailCommand
            {
                Id = contactDetailCommand.Id,
                PersonId= personCommand.Id
            });


            var contactDetail = await FindAsync<ContactDetail>(contactDetailCommand.Id);

            contactDetail.Should().BeNull();
        }
    }
}
