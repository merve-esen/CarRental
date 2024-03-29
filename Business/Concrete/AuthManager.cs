﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Entities.Models;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var result = _userService.Add(user);
            if (result.Success)
            {
                return new SuccessDataResult<User>(user, Messages.UserRegistered);
            }
            else
            {
                return new ErrorDataResult<User>(user, result.Message);
            }
            
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).Data != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            if (accessToken == null)
            {
                return new ErrorDataResult<AccessToken>(accessToken, Messages.AccessTokenNotCreated);
            }
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        [ValidationAspect(typeof(ChangePasswordValidator))]
        public IResult ChangePassword(ChangePasswordModel updatedUser)
        {
            UserForLoginDto checkedUser = new UserForLoginDto
            {
                Email = updatedUser.Email,
                Password = updatedUser.OldPassword
            };
            var loginResult = Login(checkedUser);
            if (loginResult.Success)
            {
                var user = loginResult.Data;
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(updatedUser.NewPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _userService.Update(user);
                return new SuccessResult(Messages.PasswordChanged);
            }

            return new ErrorResult(loginResult.Message);
        }
    }
}
