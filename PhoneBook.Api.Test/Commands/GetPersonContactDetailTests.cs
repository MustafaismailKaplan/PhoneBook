using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using PhoneBook.Api.Events;

namespace PhoneBook.Api.Test.Commands
{

    using static Testing;
    public class GetPersonContactDetailTests
    {

        [Test]
        public async Task ShouldGetPersonContactDetail()
        {
            var personCommand = new CreatePersonCommand()
            {
                Company = "Test Company",
                FirstName = "Test Mustafa",
                LastName = "Test Last Name"
            };

            await SendAsync(personCommand);

            await SendAsync(new CreateContactDetailCommand()
            {
                PersonId = personCommand.Id,
                ContactType = ContactType.Address,
                Value = "Test Address"
            });

            await SendAsync(new CreateContactDetailCommand()
            {
                PersonId = personCommand.Id,
                ContactType = ContactType.Email,
                Value = "test@test.com"
            });

            await SendAsync(new CreateContactDetailCommand()
            {
                PersonId = personCommand.Id,
                ContactType = ContactType.Phone,
                Value = "05056856295"
            });

            var personContactDetailList = await SendAsync(new GetPersonContactCommand
            {
                Id = personCommand.Id
            });

            personContactDetailList.Should().NotBeNull();
            personContactDetailList.ContactDetails.Should().HaveCount(3);


        }

    }
}
