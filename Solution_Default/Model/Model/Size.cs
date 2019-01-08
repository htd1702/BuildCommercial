using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Model
{
    [Table("Sizes")]
    public class Size
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(100)]
        public string Name { set; get; }

        [Required]
        [Column(TypeName = "NVarChar")]
        [MaxLength(100)]
        public string NameVN { set; get; }

        [Required]
        [MaxLength(100)]
        public string Alias { set; get; }

        [MaxLength(250)]
        public string Description { set; get; }

        public int? ParentSizeID { set; get; }

        public int Type { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }
    }
}