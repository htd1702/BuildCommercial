using Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Model
{
    [Table("List")]
    public class List : Auditable
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

        public int Type { set; get; }

        [MaxLength(250)]
        public string Description { set; get; }
    }
}