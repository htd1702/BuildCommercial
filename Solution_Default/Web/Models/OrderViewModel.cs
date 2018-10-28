using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class OrderViewModel
    {
        public int ID { set; get; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string Address { set; get; }

        [Required]
        public string Email { set; get; }

        [Required]
        public string Phone { set; get; }

        public decimal Total { get; set; }

        public DateTime OrderDate { set; get; }

        public string CustomerMessage { set; get; }

        public string PaymentMethod { set; get; }

        public string PaymentStatus { set; get; }

        public bool Status { set; get; }

        public virtual IEnumerable<OrderDetailViewModel> OrderDetails { set; get; }
    }
}