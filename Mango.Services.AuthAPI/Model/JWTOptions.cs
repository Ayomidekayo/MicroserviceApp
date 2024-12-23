namespace Mango.Services.AuthAPI.Model
{
    public class JWTOptions
    {
        public string Secrete { get; set; }=string.Empty;
        public string Issuer { get; set; }= string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
