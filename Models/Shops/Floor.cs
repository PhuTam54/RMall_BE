﻿namespace RMall_BE.Models.Shops
{
    public class Floor
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public ICollection<Shop>? Shops { get; set; }
    }
}
