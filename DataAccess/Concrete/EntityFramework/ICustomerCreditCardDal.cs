using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public interface ICustomerCreditCardDal : IEntityRepository<CustomerCreditCard>
    {
    }
}
