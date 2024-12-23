namespace Mango.Web.Utility
{
    public class SD
    {
        public static string CouponApiBase { get; set; }
        public static string ProductApiBase { get; set; }
        public static string ShoopingCartAPIBase { get; set; }
        public static string AuthApiBase { get; set; }
        public const string ROLeAdmin = "ADMIN";
        public const string ROLeCustomer = "cUSTOMER";
        public const string TokenCookie = "JWTTOKEN";
     
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE, 
        }
    }
}
