using PhoneBook.Api.Data.Entities;
using Shared.Messages;
using System;

namespace PhoneBook.Api.Events
{
    public class ContactDetailDeleted : IEvent
    {
        public Guid Id { get; set; }
        public ContactType ContactType { get;  set; }
        public string Value { get;  set; }
        public Guid PersonId { get; set; }


        public ContactDetailDeleted(Guid id, ContactType contactType, string value, Guid personId)
        {
            Id = id;
            ContactType = contactType;
            Value = value;
            PersonId = personId;

        }

    }
}
