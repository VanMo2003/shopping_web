using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace website_shopping.Models
{
    public class UserModel
    {
        [Key]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Tài khoản dài từ {2} đến {1} ký tự")]
        [DisplayName("Tài khoản : ")]
        [Column("username", TypeName = "nvarchar")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Tối thiểu {2} ký tự")]
        [DisplayName("Mật khẩu : ")]
        [Column("password", TypeName = "nvarchar")]

        public string Password { get; set; }
        [Column("full_name", TypeName = "nvarchar")]
        [DisplayName("Họ và tên : ")]
        public string Fullname { get; set; }
        [Column("email", TypeName = "nvarchar")]
        [DisplayName("Địa chỉ : ")]
        [EmailAddress]
        public string Email { get; set; }
        // [DisplayName("Giới tính : ")]
        // public bool Sex { get; set; }
    }
}