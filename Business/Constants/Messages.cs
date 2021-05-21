using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Araba eklendi.";
        public static string CarNameMinTwoCharacters = "Araba ismi minimum 2 karakter olmalıdır.";
        public static string DailyPriceBiggerThanZero = "Fiyat 0'dan büyük olmalıdır.";
        public static string ReturnDateIsNotNull = "Araba teslim edilmediği için kiralanamaz.";
        public static string CarRented = "Araba kiralandı.";
    }
}
