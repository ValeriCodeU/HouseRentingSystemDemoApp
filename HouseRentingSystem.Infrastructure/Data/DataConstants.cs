namespace HouseRentingSystem.Infrastructure.Data
{
    public static class DataConstants
    {
        public static class Category
        {
            public const int MaxCategoryName = 50;
        }

        public static class Agent
        {
            public const int MaxPhoneNumber = 15;
            public const int MinPhoneNumber = 7;
        }

        public static class House
        {
            public const int MaxHouseTitle = 50;
            public const int MinHouseTitle = 10;

            public const int MaxHouseAddress = 150;
            public const int MinHouseAddress = 30;

            public const int MaxHouseDescription = 500;
            public const int MinHouseDescription = 50;

            public const int MaxHouseImageUrl = 200;

            public const int PrecisionDecimal = 18;
            public const int ScaleDecimal = 2;

            public const string MaxPricePerMonth = "2000";
            public const string MinPricePerMonth = "0.0";
        }
    }
}
