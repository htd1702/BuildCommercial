namespace Web.Models
{
    public class ProductTagViewModel
    {
        public int ProductID { set; get; }

        public string TagID { set; get; }

        public virtual ProductViewModel Products { set; get; }

        public virtual TagViewModel Tags { set; get; }
    }
}