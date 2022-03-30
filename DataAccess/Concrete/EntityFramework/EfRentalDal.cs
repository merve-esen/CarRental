using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
                return context.Set<Rental>().Where(r => r.CarId == carId).OrderByDescending(r => r.Id).FirstOrDefault();
            }
        }

        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.Id
                             join cs in context.Customers
                             on r.CustomerId equals cs.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             select new RentalDetailDto
                             {
                                 RentalId = r.Id,
                                 BrandName = b.Name,
                                 CustomerFullName = cs.FirstName + " " + cs.LastName,
                                 RentDate = r.RentDate,
                                 ReturnDate = (DateTime)r.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
