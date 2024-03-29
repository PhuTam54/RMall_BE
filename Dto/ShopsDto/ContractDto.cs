using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMall_BE.Models.Shops;
using RMall_BE.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Dto.ShopsDto
{
    public class ContractDto
    {
        public int Id { get; set; }
        public decimal Rental_fee { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public string Terms_and_conditions { get; set; }
        public string Additional_notes { get; set; }
    }
}
