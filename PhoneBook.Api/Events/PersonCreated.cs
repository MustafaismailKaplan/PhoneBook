using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Events
{
    public class PersonCreated : IEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }

        public PersonCreated(Guid id, string name, string surname, string companyName)
        {
            Id = id;
            FirstName = name;
            LastName = surname;
            Company = companyName;
        }
    }
}
