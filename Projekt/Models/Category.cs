using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        //dodao ForeignKey annotation nakon migracije. Problem?????
        [ForeignKey("CategoryId")]
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
