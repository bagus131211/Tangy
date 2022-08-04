namespace Tangy.Common
{
    public static class Constants
    {
        public const string ShoppingCart = "ShoppingCart";

        public static class Status
        {
            public const string Pending = "Pending";
            public const string Confirmed = "Confirmed";
            public const string Shipped = "Shipped";
            public const string Refunded = "Refunded";
            public const string Cancelled = "Cancelled";
        }

        public static class Role
        {
            public const string Admin = "Admin";
            public const string Customer = "Customer";
        }

        public static class Authentication
        {
            public const string Token = "JWT Token";
            public const string UserDetails = "UserDetails";
        }

        public static class Transaction
        {
            public const string OrderDetails = "OrderDetails";
        }
    }
}
