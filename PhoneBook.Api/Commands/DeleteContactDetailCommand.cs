using MediatR;
using System;

namespace PhoneBook.Api.Commands
{
    public class DeleteContactDetailCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid ContentDetailId { get; set; }
        public Guid PersonId { get; set; }

        public DeleteContactDetailCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
