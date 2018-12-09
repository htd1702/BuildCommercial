using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Model
{
    [Table("ProductDetails")]
    public class ProductDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        public int ProductID { set; get; }

        public int ColorID { set; get; }

        public int SizeID { set; get; }

        public int Quantity { set; get; }

        public int Inventory { set; get; }

        public int Type { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }

        [MaxLength(250)]
        public string Description { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Products { set; get; }

        [ForeignKey("ColorID")]
        public virtual Color Colors { set; get; }

        [ForeignKey("SizeID")]
        public virtual Size Sizes { set; get; }
    }
}