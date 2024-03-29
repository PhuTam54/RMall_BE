using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using RMall_BE.Models.User;

namespace RMall_BE.Models.Shops
{
    public class Contract
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rental_fee { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public string Terms_and_conditions { get; set; }
        public string Additional_notes { get; set; }
        public int Tenant_Id { get; set; }
        public Tenant Tenant { get; set; }
        public int Shop_Id { get; set; }
        public Shop Shop { get; set; }
    }
}
