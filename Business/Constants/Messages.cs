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
        public static string RentalSuccessful = "Kiralama işlemi başarıyla tamamlandı";
        public static string RentalUndeliveredCar = "Araba teslim edilmedi";
        public static string RentalNotAvailable = "Araba seçilen tarihler arasında kiralanabilir değil";
        public static string InsufficientFindexScore = "Findex puanınız, bu aracı kiralamak için yeterli değil";

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
        public static string MissingInformation = "Eksik bilgi";
        public static string UserRegistered = "Kullanıcı oluşturuldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatası";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
        public static string AccessTokenNotCreated = "Token oluşturulamadı";

        // Credit Card - Payment
        public static string CreditCardSaved = "Kredi kartı kaydedildi";
        public static string CreditCardNotValid = "Kredi kartı bilgileri doğrulanamadı";
        public static string CreditCardListed = "Kredi kartı listelendi";
        public static string CreditCardNotFound = "Kredi kartı bulunamadı";
        public static string StringMustConsistOfNumbersOnly = "Bu veri sadece sayılardan oluşmalıdır";
        public static string CustomerCreditCardNotFound = "Müşteri kredi kartı bulunamadı";
        public static string CustomerCreditCardNotDeleted = "Musteri kredi karti silinemedi";
        public static string CustomerCreditCardDeleted = "Müşteri kredi kartı başarıyla silindi";
        public static string CustomerCreditCardAlreadySaved = "Kredi kartı zaten kaydedilmiş";
        public static string CustomerCreditCardFailedToSave = "Müşteri kredi kartı kaydedilemedi";
        public static string CustomerCreditCardSaved = "Müşteri kredi kartı başarıyla kaydedildi";
        public static string CustomersCreditCardsListed = "Müşterinin kredi kartları listelendi";
        public static string InsufficientCardBalance = "Kart bakiyesi yetersiz";
        public static string PaymentSuccessful = "Ödeme başarıyla tamamlandı";
    }
}
