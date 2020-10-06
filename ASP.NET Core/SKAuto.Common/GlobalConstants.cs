namespace SKAuto.Common
{
    public static class GlobalConstants
    {
        // Brand
        public const string BrandUpdateErrorMessage = "В базата вече съществува такъв бранд!!!!!";
        public const string BrandDeleteErrorMessage = "Бранда не може да бъде изтрит - има модел/и свързани с него!!!!!";
        public const string BrandCreateErrorMessage = "В базата вече съществува такъва бранд с това име!!!!!";

        // Model
        public const string ModelCreateErrorMessage = "В базата вече има модел с това име!!!!!";
        public const string ModelDeleteErrorMessage = "Модела не може да бъде изтрит - има категория/и или част/и свързани с него!!!!!";
        public const string ModelUpdateErrorMessage = "В базата вече съществува такъв модел!!!!!";

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
    }
}
