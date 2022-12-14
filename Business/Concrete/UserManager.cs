using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            if (!String.IsNullOrEmpty(user.Email))
            {
                IResult result = BusinessRules.Run(CheckIfEmailExists(user.Email));
                if (!result.Success)
                {
                    _userDal.Add(user);
                    return new SuccessResult();
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return new ErrorResult(Messages.MissingInformation);
            }
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            IResult result = BusinessRules.Run(CheckIfUserExists(user.Id));
            if (result != null)
            {
                return result;
            }

            _userDal.Update(user);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult UpdateProfile(User user)
        {
            IResult result = BusinessRules.Run(CheckIfUserExists(user.Id));
            if (result != null)
            {
                return result;
            }

            var updatedUser = _userDal.Get(u => u.Id == user.Id && u.Email == user.Email);
            if (updatedUser == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            _userDal.Update(updatedUser);
            return new SuccessResult();
        }

        private IResult CheckIfUserExists(int userId)
        {
            if (userId == 0)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var result = _userDal.GetAll(u => u.Id == userId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            return new SuccessResult();
        }

        private IResult CheckIfEmailExists(string email)
        {
            var result = _userDal.GetAll(u => u.Email == email).Any();
            if (!result)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            return new SuccessResult();
        }
    }
}
