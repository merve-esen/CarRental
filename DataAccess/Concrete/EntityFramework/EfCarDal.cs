﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cl in context.Colors
                             on c.ColorId equals cl.Id
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 ColorName = cl.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 MinFindexScore = c.MinFindexScore
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}