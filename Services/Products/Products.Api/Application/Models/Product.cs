namespace Products.Api.Application.Models
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long UserId { get; set; }

        public Product(long id, long userId, string name)
        {
            Id = id;
            UserId = userId;
            Name = name;
        }
    }
}
