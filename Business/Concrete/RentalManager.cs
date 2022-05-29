using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;
        IPaymentService _paymentService;
        ICreditCardService _creditCardService;
        IFindexScoreService _findexScoreService;

        public RentalManager(IRentalDal rentalDal, ICarService carService, IPaymentService paymentService, ICreditCardService creditCardService, IFindexScoreService findexScoreService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _paymentService = paymentService;
            _creditCardService = creditCardService;
            _findexScoreService = findexScoreService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental)
        {
            Rental lastRental = _rentalDal.GetLastRentalByCarId(rental.CarId);
            if (lastRental == null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.CarRented);
            }
            else if (lastRental.ReturnDate != DateTime.Parse("1.1.0001 00:00:00"))
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.CarRented);
            }
            else
            {
                return new ErrorResult(Messages.ReturnDateIsNull);
            }
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        [SecuredOperation("admin,rental.all,rental.rent,customer")]
        [ValidationAspect(typeof(RentPaymentRequestValidator))]
        [TransactionScopeAspect]
        public IDataResult<int> Rent(RentPaymentRequestModel rentPaymentRequest)
        {
            //Get Customer Findex Score
            var customerFindexScoreResult = _findexScoreService.GetCustomerFindexScore(rentPaymentRequest.CustomerId);
            if (!customerFindexScoreResult.Success)
            {
                return new ErrorDataResult<int>(-1, customerFindexScoreResult.Message);
            }

            //Get CreditCard
            var creditCardResult = _creditCardService.Get(rentPaymentRequest.CardNumber, rentPaymentRequest.ExpireYear, rentPaymentRequest.ExpireMonth, rentPaymentRequest.Cvc, rentPaymentRequest.CardHolderFullName.ToUpper());

            List<Rental> verifiedRentals = new List<Rental>();
            decimal totalAmount = 0;

            if (creditCardResult.Success)
            {
                //Verify Rental
                var rental = rentPaymentRequest.Rental;
                var car = _carService.GetById(rental.CarId);
                if (car == null)
                {
                    return new ErrorDataResult<int>(-1, Messages.CarNotFound);
                }

                if (customerFindexScoreResult.Data < car.Data.MinFindexScore)
                {
                    return new ErrorDataResult<int>(-1, Messages.InsufficientFindexScore);
                }

                verifiedRentals.Add(rental);

                //Get Amount
                var carDailyPrice = _carService.GetById(rental.CarId).Data.DailyPrice;
                var rentalPeriod = GetRentalPeriod(rental.RentDate, (DateTime)rental.ReturnDate);
                var amount = carDailyPrice * rentalPeriod;
                totalAmount += amount;

                //Pay
                var creditCard = creditCardResult.Data;
                var paymentResult = _paymentService.Pay(creditCard, rentPaymentRequest.CustomerId, rentPaymentRequest.Amount);

                //Verify payment
                if (paymentResult.Success && paymentResult.Data != -1)
                {
                    //Add rentals on db
                    foreach (var verifiedRental in verifiedRentals)
                    {
                        verifiedRental.PaymentId = paymentResult.Data;

                        //Add Rental
                        var rentalAddResult = Add(verifiedRental);

                        //Check Rental
                        if (!rentalAddResult.Success)
                        {
                            return new ErrorDataResult<int>(-1, rentalAddResult.Message);
                        }
                    }
                    return new SuccessDataResult<int>(paymentResult.Data, Messages.RentalSuccessful);
                }
                return new ErrorDataResult<int>(-1, paymentResult.Message);
            }
            return new ErrorDataResult<int>(-1, creditCardResult.Message);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id));
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAllByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId));
        }

        public IResult CheckReturnDateByCarId(int carId)
        {
            var result = _rentalDal.GetAll(x => x.CarId == carId && x.ReturnDate == null);
            if (result.Count > 0) return new ErrorResult(Messages.RentalUndeliveredCar);

            return new SuccessResult();
        }

        public IResult IsRentable(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId);

            if (result.Any(r =>
                r.ReturnDate >= rental.RentDate &&
                r.RentDate <= rental.ReturnDate
            )) return new ErrorResult(Messages.RentalNotAvailable);

            return new SuccessResult();
        }

        private int GetRentalPeriod(DateTime rentDate, DateTime returnDate)
        {
            return (Convert.ToInt32((returnDate - rentDate).TotalDays));
        }
    }
}
