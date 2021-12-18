using PhoneBook.Api.Data.Entities;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Events
{
    public class ContactDetailGet : IEvent
    {
        public Guid Id { get; set; }
        public ContactType ContactType { get;  set; }
        public string Value { get;  set; }

        
    }
}
