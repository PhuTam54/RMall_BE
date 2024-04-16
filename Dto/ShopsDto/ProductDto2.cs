namespace RMall_BE.Dto.ShopsDto
{
    public class ProductDto2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Shop_Id { get; set; }
    }
}
