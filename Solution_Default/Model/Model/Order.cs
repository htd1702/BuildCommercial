using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Model
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Address { set; get; }

        [Required]
        [MaxLength(50)]
        public string Email { set; get; }

        [Required]
        [MaxLength(15)]
        public string Phone { set; get; }

        public decimal Total { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public string PaymentStatus { set; get; }

        public bool Status { set; get; }

        public virtual IEnumerable<OrderDetail> OrderDetails { set; get; }
    }
}