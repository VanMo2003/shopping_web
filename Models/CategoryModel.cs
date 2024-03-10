using System;
using System.Collections.Generic;
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
        public int id { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập tài khoản")]
        [Column("name_category", TypeName = "nvarchar")]
        public string Name { get; set; }
        [Column("time_create")]
        public DateTime TimeCreate { get; set; } = DateTime.Now;
        [Column("time_update")]
        public DateTime TimeUpdate { get; set; } = DateTime.Now;
    }
}