using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [SecuredOperation("brand.add, admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNameExists(brand.Name));
            if (result != null)
            {
                return result;
            }

            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdded);
        }

        [SecuredOperation("brand.update, admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandExists(brand.Id));
            if (result != null)
            {
                return result;
            }

            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }

        [SecuredOperation("brand.delete, admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandExists(brand.Id));
            if (result != null)
            {
                return result;
            }

            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == id));
        }

        private IResult CheckIfBrandNameExists(string brandName)
        {
            if (string.IsNullOrEmpty(brandName))
            {
                return new ErrorResult(Messages.BrandNotFound);
            }

            var result = _brandDal.GetAll(b => b.Name.ToLower() == brandName.ToLower()).Any();
            if (result)
            {
                return new ErrorResult(Messages.BrandNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfBrandExists(int brandId)
        {
            if (brandId == 0)
            {
                return new ErrorResult(Messages.BrandNotFound);
            }

            var result = _brandDal.GetAll(b => b.Id == brandId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.BrandNotFound);
            }
            return new SuccessResult();
        }
    }
}
