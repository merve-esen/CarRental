using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [NotMapped]
    public class Customer : User, IEntity
    {
        public int UserId { get; set; }
        public string CompanyName { get; set; }
    }
}
