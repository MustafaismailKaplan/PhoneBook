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
    public class GetPersonContactHandler : AsyncRequestHandler<GetPersonContactCommand>
    {
        private readonly PhoneBookContext _dbContext;
        private readonly ILogger<GetPersonContactCommand> _logger;
        private readonly IBusPublisher _busPublisher;

        public GetPersonContactHandler(PhoneBookContext dbContext,
                                          ILogger<GetPersonContactCommand> logger,
                                          IBusPublisher busPublisher)
        {
            _logger = logger;
            _busPublisher = busPublisher;
            _dbContext = dbContext;
        }

        protected override async Task<PersonGet> Handle(GetPersonContactCommand command, CancellationToken cancellationToken)
        {
            PersonGet personGet = new PersonGet();
            var response = await _dbContext.Persons.FirstOrDefaultAsync(s => s.Id == command.Id);
            if (response == null)
                return personGet;

            var contactDetails = await _dbContext.ContactDetail.Where(s => s.PersonId == response.Id).ToListAsync();
            List<ContactDetailGet> contactDetailsList = new List<ContactDetailGet>();
            foreach (var item in contactDetails)
            {
                var detail = new ContactDetailGet()
                {
                    ContactType = item.ContactType,
                    Value = item.Value,
                    Id = item.Id
                };
                contactDetailsList.Add(detail);
            }

            personGet.Id = response.Id;
            personGet.FirstName = response.FirstName;
            personGet.LastName = response.LastName;
            personGet.Company = response.Company;
            personGet.ContactDetails = contactDetailsList;

            return personGet;


        }
    }
}
