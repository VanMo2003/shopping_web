using System;
using System.Collections.Generic;
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
        public string username { get; set; }
        [ForeignKey("username")]
        public virtual UserModel UserModel { get; set; }
        [Column("payment_method")]
        public bool PaymentMethods { get; set; }
        [Column("address", TypeName = "nvarchar")]
        public string Address { get; set; }
        [Column("total_money", TypeName = "money")]
        public decimal TotalMoney { get; set; }
        [Column("list_product", TypeName = "nvarchar")]
        public string ListProduct { get; set; }
        [Column("time_create")]
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        [Column("time_update")]
        public DateTime TimeUpdate { get; set; } = DateTime.Now;
        [Column("payment_status")]
        public bool Payment_Status { get; set; }

    }
}