using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace website_shopping.Models
{
    public class ProductModel
    {
        [Key]
        [Column("id_product")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập tên sản phẩm")]
        [Column("name_product", TypeName = "nvarchar")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập mô tả")]
        [Column("description_product", TypeName = "ntext")]
        public int Description { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập đơn giá")]
        [Column("unit_price", TypeName = "money")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập số lượng")]
        [Column("quantity")]
        public int quantity { get; set; }
        [Column("image")]
        public string? image { get; set; }
        public int id_category { get; set; }
        [ForeignKey("id_category")]
        public virtual CategoryModel CategoryModel { get; set; }
        [Column("time_create")]
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        [Column("time_update")]
        public DateTime TimeUpdate { get; set; } = DateTime.Now;
    }
}