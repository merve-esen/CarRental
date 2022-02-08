using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        // Brand
        public static string BrandAdded = "Marka eklendi";
        public static string BrandUpdated = "Marka güncellendi";
        public static string BrandDeleted = "Marka silindi";
        public static string BrandNameAlreadyExists = "Marka zaten mevcut";
        public static string BrandNotFound = "Marka bulunamadı";

        // Color
        public static string ColorAdded = "Renk eklendi";
        public static string ColorUpdated = "Renk güncellendi";
        public static string ColorDeleted = "Renk silindi";
        public static string ColorNameAlreadyExists = "Renk zaten mevcut";
        public static string ColorNotFound = "Renk bulunamadı";

        // Car
        public static string CarAdded = "Araba eklendi";
        public static string CarUpdated = "Araba güncellendi";
        public static string CarDeleted = "Araba silindi";
        public static string CarNameMinTwoCharacters = "Araba ismi minimum 2 karakter olmalıdır";
        public static string DailyPriceBiggerThanZero = "Fiyat 0'dan büyük olmalıdır";
        public static string ReturnDateIsNull = "Araba teslim edilmediği için kiralanamaz";
        public static string CarNotFound = "Araba bulunamadı";

        // Rental
        public static string CarRented = "Araba kiralandı";
        public static string RentalUndeliveredCar = "Araba teslim edilmedi";
        public static string RentalNotAvailable = "Araba seçilen tarihler arasında kiralanabilir değil";

        // CarImage
        public static string CarImageAdded = "Araba resmi eklendi";
        public static string CarImageUpdated = "Araba resmi güncellendi";
        public static string CarImageDeleted = "Araba resmi silindi";
        public static string CarImageLimitAchieved = "Bir araba için en fazla 5 resim yüklenebilir";
        public static string CarImageNotFound = "Araba resmi bulunamadı";

        // Customer
        public static string CustomerAdded = "Müşteri eklendi";
        public static string CustomerUpdated = "Müşteri güncellendi";
        public static string CustomerDeleted = "Müşteri silindi";
        public static string CompanyNameAlreadyExists = "Şirket adı zaten mevcut";
        public static string CustomerNotFound = "Müşteri bulunamadı";

        // Authorization
        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserUpdated = "Kullanıcı güncellendi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatası";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
    }
}
