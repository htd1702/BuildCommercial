using Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Model
{
    [Table("Products")]
    public class Product : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(15)]
        public string Code { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string NameVN { set; get; }

        [Required]
        [MaxLength(256)]
        public string NameFr { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        [Required]
        public int CategoryID { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }
        
        public double Price { set; get; }

        public double PriceVN { set; get; }

        public double PriceFr { set; get; }

        public double Scale { set; get; }

        public int PromotionPrice { set; get; }

        public int Quantity { set; get; }

        public int? Warranty { set; get; }

        public string Composition { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }

        public string Tags { set; get; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategories { set; get; }

        public virtual IEnumerable<ProductTag> ProductTags { set; get; }
    }
}