namespace Products.Api.Application.Dtos
{
    public class ProductDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public UserDto UserDto { get; set; }
    }
}
