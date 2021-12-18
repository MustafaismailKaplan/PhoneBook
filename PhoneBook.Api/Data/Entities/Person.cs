using System;
using System.Collections.Generic;
namespace PhoneBook.Api.Data.Entities
{
    public class Person : BaseEntity
    {
        public Person() {
        }
        public Person(List<ContactDetail> contactDetails, string firstName, string lastName, string company)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            ContactDetails = contactDetails;
        }

        public void ContactDetailAdd(ContactDetail contactDetails)
        {
            if (contactDetails == null || contactDetails.Value == null)
            {
                throw new ArgumentNullException(nameof(contactDetails));
            }

            ContactDetails.Add(contactDetails);
        }

        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Company { get;  set; }
        public IList<ContactDetail> ContactDetails { get;  set; }
    }
}
