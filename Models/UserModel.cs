using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.IdentityModel.Tokens;
using website_shopping.Validation;

namespace website_shopping.Models
{
    public class UserModel
    {
        [Key]
        [StringLength(155)]
        [Required(ErrorMessage = "Mời bạn nhập tài khoản")]
        [DisplayName("Tài khoản : ")]
        [Column("username", TypeName = "nvarchar")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập mật khẩu")]
        [StringLength(155, MinimumLength = 6, ErrorMessage = "Tối thiểu {2} ký tự")]
        [DisplayName("Mật khẩu : ")]
        [Column("password", TypeName = "nvarchar")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập họ tên")]
        [Column("full_name", TypeName = "nvarchar")]
        [DisplayName("Họ và tên : ")]
        [StringLength(155)]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập địa chỉ")]
        [Column("address", TypeName = "nvarchar")]
        [DisplayName("Địa chỉ : ")]
        [StringLength(155)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập số điện thoại")]
        [Column("phone_number", TypeName = "nvarchar")]
        [DisplayName("Số điện thoại : ")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại sai định dạng, vd : 0365737582")]
        [MyValidation]
        public string PhoneNumber { get; set; }
        // [DisplayName("Giới tính : ")]
        // public bool Sex { get; set; }

        public bool CheckValidLogin() => !Username.IsNullOrEmpty() && !Password.IsNullOrEmpty();
    }
}