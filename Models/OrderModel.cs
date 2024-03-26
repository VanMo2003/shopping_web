using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace website_shopping.Models
{
    public class OrderModel
    {
        [Key]
        [Column("id_order")]
        public int Id { get; set; }
        [DisplayName("tên khách hàng: ")]
        public string Fullname { get; set; }
        [ForeignKey("full_name")]
        [DisplayName("tên khách hàng: ")]
        public virtual UserModel userModel { get; set; }

        [Column("payment_method")]
        public bool PaymentMethods { get; set; }
        [Required]
        [Column("address", TypeName = "nvarchar")]
        [StringLength(155)]
        public string Address { get; set; }
        [Required]
        [Column("total_money", TypeName = "money")]
        public decimal TotalMoney { get; set; }
        // [Required]
        // [Column("list_product", TypeName = "nvarchar")]
        // [StringLength(1000)]
        // public string ListProduct { get; set; }
        [Column("order_id")]
        public ICollection<ProductModel> Products { get; set; }
        [Column("time_create")]
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        [Column("time_update")]
        public DateTime TimeUpdate { get; set; } = DateTime.Now;
        [Column("payment_status")]
        public bool Payment_Status { get; set; }

    }
}