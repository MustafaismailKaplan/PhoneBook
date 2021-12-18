using System;
namespace PhoneBook.Api.Data.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
        }
    }
}
