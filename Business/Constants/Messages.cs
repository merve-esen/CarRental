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
        public static string CarRented = "Araba kiralandı";
        public static string RentalUndeliveredCar = "Araba teslim edilmedi";
        public static string RentalNotAvailable = "Araba seçilen tarihler arasında kiralanabilir değil";

        // CarImage
        public static string CarImageAdded = "Araba resmi eklendi";
        public static string CarImageLimitAchieved = "Bir araba için en fazla 5 resim yüklenebilir";
        public static string CarImageNotFound = "Araba resmi bulunamadı";
        public static string CarImageUpdated = "Araba resmi güncellendi";

        // Authorization
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatası";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
    }
}
