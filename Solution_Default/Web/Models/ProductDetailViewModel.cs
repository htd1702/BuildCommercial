using System;

namespace Web.Models
{
    public class ProductDetailViewModel
    {
        public int ID { set; get; }

        public int ProductID { set; get; }

        public int ColorID { set; get; }

        public int SizeID { set; get; }

        public int Quantity { set; get; }

        public string MoreImages { set; get; }

        public int Type { set; get; }

        public string Description { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public virtual ProductViewModel Products { set; get; }
    }
}