namespace SKAutoNew.Common
{
    public static class GlobalConstants
    {
        // Brand
        public const string BrandUpdateErrorMessage = "В базата вече съществува такъв бранд!!!!!";
        public const string BrandDeleteErrorMessage = "Бранда не може да бъде изтрит - има модел/и свързани с него!!!!!";
        public const string BrandCreateErrorMessage = "В базата вече съществува такъва бранд с това име!!!!!";

        // Model
        public const string ModelCreateErrorMessage = "В базата вече има модел с това име!!!!!";
        public const string ModelDeleteErrorMessage = "Модела не може да бъде изтрит - има част/и свързани с него!!!!!";
        public const string ModelUpdateErrorMessage = "В базата вече съществува такъв модел!!!!!";

        // Category
        public const string CategotyUpdateErrorMessage = "В базата вече съществува такава категория!!!!!";
        public const string CategotyCreateErrorMessage = "В базата вече съществува категория с това име!!!!!";
        public const string CategoryDeleteErrorMessage = "Категорията не може да бъде изтрита - има част/и свързани с нея!!!!";

        // Role
        public const string AdministratorRoleName = "Administrator";
        public const string UserRoleName = "User";

        // User
        public const int UserMinLenght = 3;

        // Photo
        public const string ImageAddress = "/Images/No picture.jpg";

        // Part
        public const string PartPriceFormat = "0.00";

        // Manufactory
        public const string ManufactoryName = "Липсва информация";

        // OrderStatus
        public const string PendingBGStatus = "Заявена поръчка";
        public const string DeliverBGStatus = "Доставена поръчка";
        public const string ShippedBGStatus = "Изпратена поръчка";
        public const string AcquiredBGStatus = "Получена поръчка";

        // Recipient
        public const string RecipientFindErrorMessage = "В базата няма получател с такъв телефон!!!!!";
        public const string RecipientCreateErrorMessage = "В базата има получател с такъв телефон!!!!!";

        // Order
        public const string OrderModelValidationMessege = "Невалиден модел!!!!!";
        public const string OrderSameMessage = "Поръчката в базата е със същия статус на доставка!!!!!";
    }
}
