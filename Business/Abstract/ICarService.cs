using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetById(int id);
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IDataResult<CarDetailDto> GetCarDetailByCarId(int carId);
        IDataResult<List<CarDetailDto>> GetCarsByBrandName(string brandName);
        IDataResult<List<CarDetailDto>> GetCarsByColorName(string colorName);
        IDataResult<List<CarDetailDto>> GetCarsByBrandNameAndColorName(string brandName, string colorName);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IResult AddTransactionalTest(Car car);
    }
}
