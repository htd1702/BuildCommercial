using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PostCategoryViewModel
    {
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string NameVN { set; get; }

        [Required]
        [MaxLength(256)]
        public string NameFr { set; get; }

        public string Alias { set; get; }

        public string Description { set; get; }

        public string DescriptionFr { set; get; }

        public string DescriptionVN { set; get; }

        public int? ParentID { set; get; }

        public int? DisplayOrder { set; get; }

        public string Image { set; get; }

        public bool? HomeFlag { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        public bool? Promotion { set; get; }

        public bool Status { set; get; }

        public virtual IEnumerable<PostViewModel> Posts { set; get; }
    }
}