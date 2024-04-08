using RMall_BE.Models.Shops;

namespace RMall_BE.Dto.ShopsDto
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Phone_Number { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public FloorDto Floor { get; set; }
        public ICollection<ProductDto>? Products { get; set; }
        public ICollection<ContractDto>? Contracts { get; set; }
    }
}
