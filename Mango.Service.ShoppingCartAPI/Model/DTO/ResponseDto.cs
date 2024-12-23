namespace Mango.Service.ShoppingCartAPI.Model.DTO
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = " ";
    }
}
