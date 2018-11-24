using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ProductViewModel
    {
        public int ID { set; get; }

        [Required]
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
        public string Alias { set; get; }

        public int CategoryID { set; get; }

        public string Image { set; get; }

        public string MoreImages { set; get; }

        [Required]
        public double Price { set; get; }

        public int PromotionPrice { set; get; }

        [Required]
        public int Quantity { set; get; }

        public int? Warranty { set; get; }

        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }

        public string Tags { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        [Required]
        public bool Status { set; get; }

        public virtual ProductCategoryViewModel ProductCategories { set; get; }
    }
}