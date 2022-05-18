using Core.Entities.Abstract;
using System;

namespace Entities.DTOs
{
    public class RentalDetailDto: IDto
    {
        public int RentalId { get; set; }
        public string BrandName { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
