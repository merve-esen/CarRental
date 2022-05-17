using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CustomerCreditCard : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CreditCardId { get; set; }
    }
}
