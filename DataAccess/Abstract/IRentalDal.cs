using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        Rental GetLastRentalByCarId(int carId);
        List<RentalDetailDto> GetRentalDetails();
    }
}
