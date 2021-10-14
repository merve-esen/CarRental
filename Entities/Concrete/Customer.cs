using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    [NotMapped]
    public class Customer : User, IEntity
    {
        public int UserId { get; set; }
        public string CompanyName { get; set; }
    }
}
