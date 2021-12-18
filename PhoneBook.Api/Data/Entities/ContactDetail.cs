using System;

namespace PhoneBook.Api.Data.Entities
{
    public enum ContactType
    {
        Email, Phone, Address
    }

    public class ContactDetail : BaseEntity
    {
        public ContactDetail() { }
        public ContactDetail(Person person, ContactType contactType, string value)
        {
            Person = person;
            ContactType = contactType;
            Value = value;
        }

        public ContactType ContactType { get;  set; }
        public string Value { get;  set; }
        public Person Person { get; set; }

        public Guid PersonId { get; set; }


    }
}
