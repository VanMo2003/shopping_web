using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace website_shopping.Models
{
    public class CategoryModel
    {
        [Key]
        [Column("id_category")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập tên danh mục")]
        [StringLength(155)]
        [Column("name_category", TypeName = "nvarchar")]
        [DisplayName("Tên danh mục :")]
        public string Name { get; set; }
        [Column("time_create")]
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        [Column("time_update")]
        public DateTime TimeUpdate { get; set; } = DateTime.Now;

        public void PrintInfo() => Console.WriteLine($"{Id} - {Name} - {TimeCreate} - {TimeUpdate}");
    }
}