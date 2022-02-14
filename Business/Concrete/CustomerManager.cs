using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerExists(customer.Id));
            if (result != null)
            {
                return result;
            }

            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerExists(customer.Id));
            if (result != null)
            {
                return result;
            }

            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == id));
        }

        private IResult CheckIfCustomerExists(int customerId)
        {
            if (customerId == 0)
            {
                return new ErrorResult(Messages.CustomerNotFound);
            }

            var result = _customerDal.GetAll(c => c.Id == customerId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.CustomerNotFound);
            }
            return new SuccessResult();
        }
    }
}
