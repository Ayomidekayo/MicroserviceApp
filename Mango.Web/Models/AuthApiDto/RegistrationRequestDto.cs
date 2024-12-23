namespace Mango.Web.Models.AuthApiDto
{
    public class RegistrationRequestDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string password { get; set; }
        public string? Role { get; set; }


    }
}
