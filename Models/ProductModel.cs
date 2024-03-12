using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace website_shopping.Models
{
    public class ProductModel
    {
        [Key]
        [Column("id_product")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập tên sản phẩm")]
        [Column("name_product", TypeName = "nvarchar")]
        [DisplayName("Tên sản phẩm : ")]
        [StringLength(155)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập mô tả")]
        [Column("description_product", TypeName = "ntext")]
        [DisplayName("Mô tả sản phẩm : ")]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập đơn giá")]
        [Column("unit_price", TypeName = "money")]
        [DisplayName("Giá sản phẩm : ")]
        [Range(0, 100000000.0, ErrorMessage = "Giá phải trong khoảng [{1}, {2}]")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập số lượng")]
        [Column("quantity")]
        [DisplayName("Số lượng : ")]
        [Range(0, 100, ErrorMessage = "số lượng phải trong khoảng [{1}, {2}]")]
        public int Quantity { get; set; }
        [StringLength(200)]
        [DisplayName("Chọn ảnh :")]
        [Column("image", TypeName = "nvarchar")]
        public string? ImageString { get; set; }
        [DisplayName("danh mục :")]
        public int? id_category { get; set; }
        [ForeignKey("id_category")]
        [DisplayName("danh mục :")]
        public virtual CategoryModel? CategoryModel { get; set; }
        [Column("time_create")]
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        [Column("time_update")]
        public DateTime TimeUpdate { get; set; } = DateTime.Now;

        public void PrintInfo() => Console.WriteLine($"{Id} - {Name} - {Description} - {UnitPrice} - {Quantity} - {id_category} - {ImageString} - {TimeCreate} - {TimeUpdate}");
        public bool checkNull() => !Name.IsNullOrEmpty() && !Description.IsNullOrEmpty() && UnitPrice > 0 && Quantity > 0;
    }
}