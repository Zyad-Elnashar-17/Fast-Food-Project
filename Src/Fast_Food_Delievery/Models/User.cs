using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Fast_Food_Delievery.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<OrderHeader> Orders { get; set; } = new List<OrderHeader>();

    }
}
