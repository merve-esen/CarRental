using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int id);
        IDataResult<List<Rental>> GetAllByCarId(int carId);
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IDataResult<int> Rent(RentPaymentRequestModel rentPaymentRequest);
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        IResult IsRentable(Rental rental);
        IResult CheckReturnDateByCarId(int carId);
    }
}
