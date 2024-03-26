using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using website_shopping.Validation;

namespace website_shopping.Models
{
    public class UserModel : IdentityUser
    {
        [Column("full_name", TypeName = "nvarchar")]
        [DisplayName("Họ và tên : ")]
        [StringLength(155)]
        public string? Fullname { get; set; }
        [Column("address", TypeName = "nvarchar")]
        [DisplayName("Địa chỉ : ")]
        [StringLength(155)]
        public string? Address { get; set; }
        [Column("cart", TypeName = "nvarchar")]
        [StringLength(1000)]
        public string? carts { get; set; }
    }
}