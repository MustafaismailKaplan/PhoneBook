using MediatR;
using PhoneBook.Api.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Commands
{
    public class GetPersonContactCommand : IRequest<PersonGet>
    {
        public Guid Id { get; set; }
        public GetPersonContactCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
