using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorNameExists(color.Name));
            if (result != null)
            {
                return result;
            }

            _colorDal.Add(color);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorExists(color.Id));
            if (result != null)
            {
                return result;
            }

            _colorDal.Update(color);
            return new SuccessResult();
        }

        public IResult Delete(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorExists(color.Id));
            if (result != null)
            {
                return result;
            }

            _colorDal.Delete(color);
            return new SuccessResult();
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

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
