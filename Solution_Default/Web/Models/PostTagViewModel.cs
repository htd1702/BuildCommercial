namespace Web.Models
{
    public class PostTagViewModel
    {
        public int PostID { set; get; }

        public string TagID { set; get; }

        public virtual PostViewModel Posts { set; get; }

        public virtual TagViewModel Tags { set; get; }
    }
}