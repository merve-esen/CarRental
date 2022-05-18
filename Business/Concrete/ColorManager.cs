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
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [SecuredOperation("color.add, admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorNameExists(color.Name));
            if (result != null)
            {
                return result;
            }

            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }

        [SecuredOperation("color.update, admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorExists(color.Id));
            if (result != null)
            {
                return result;
            }

            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        [SecuredOperation("color.delete, admin")]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorExists(color.Id));
            if (result != null)
            {
                return result;
            }

            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == id));
        }

        private IResult CheckIfColorNameExists(string colorName)
        {
            if (string.IsNullOrEmpty(colorName))
            {
                return new ErrorResult(Messages.ColorNotFound);
            }

            var result = _colorDal.GetAll(c => c.Name.ToLower() == colorName.ToLower()).Any();
            if (result)
            {
                return new ErrorResult(Messages.ColorNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfColorExists(int colorId)
        {
            if (colorId == 0)
            {
                return new ErrorResult(Messages.ColorNotFound);
            }

            var result = _colorDal.GetAll(c => c.Id == colorId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.ColorNotFound);
            }
            return new SuccessResult();
        }
    }
}
