using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public Rental GetLastRentalByCarId(int carId)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                return context.Set<Rental>().Where(r => r.CarId == carId).OrderByDescending(r => r.Id).SingleOrDefault();
            }
        }
    }
}
