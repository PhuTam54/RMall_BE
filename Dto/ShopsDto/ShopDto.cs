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
        public int Category_Id { get; set; }
        public int Floor_Id { get; set; }
    }
}
