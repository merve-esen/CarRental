using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        IDataResult<int> Rent(RentPaymentRequestModel rentPaymentRequest);
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IDataResult<Rental> GetById(int id);
        IDataResult<List<Rental>> GetAllByCarId(int carId);
        IResult CheckReturnDateByCarId(int carId);
        IResult IsRentable(Rental rental);
    }
}
