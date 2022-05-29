using Business.Abstract;
using Core.Utilities.Results;
using System;

namespace Business.Concrete
{
    public class FindexScoreManager : IFindexScoreService
    {
        private readonly ICustomerService _customerService;

        public FindexScoreManager(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IDataResult<int> GetCustomerFindexScore(int customerId)
        {
            var customerResult = IsCustomerIdExist(customerId);
            if (customerResult.Success)
            {
                //Simulated
                Random random = new Random();
                int randomFindexScore = Convert.ToInt16(random.Next(0, 1900));
                return new SuccessDataResult<int>(randomFindexScore);
            }

            return new ErrorDataResult<int>(-1, customerResult.Message);
        }

        private IResult IsCustomerIdExist(int customerId)
        {
            var result = _customerService.GetById(customerId);
            if (result.Success)
            {
                return new SuccessResult();
            }

            return new ErrorResult(result.Message);
        }
    }
}
