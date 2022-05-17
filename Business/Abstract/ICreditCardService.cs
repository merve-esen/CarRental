using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IDataResult<CreditCard> Get(string cardNumber, string expireYear, string expireMonth, string cvc, string cardHolderFullName);
        IDataResult<CreditCard> GetById(int creditCardId);
        IResult Validate(CreditCard creditCard);
        IResult Update(CreditCard creditCard);
    }
}
