using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        // Car
        public static string CarAdded = "Araba eklendi.";
        public static string CarNameMinTwoCharacters = "Araba ismi minimum 2 karakter olmalıdır.";
        public static string DailyPriceBiggerThanZero = "Fiyat 0'dan büyük olmalıdır.";
        public static string ReturnDateIsNull = "Araba teslim edilmediği için kiralanamaz.";

        // Rental
        public static string CarRented = "Araba kiralandı.";

        // CarImage
        public static string CarImageAdded = "Araba resmi eklendi";
        public static string CarImageLimitAchieved = "Bir araba için en fazla 5 resim yüklenebilir";
        public static string CarImageNotFound = "Araba resmi bulunamadı";
        public static string CarImageUpdated = "Araba resmi güncellendi";
    }
}
