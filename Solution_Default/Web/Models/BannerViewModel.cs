using System;

namespace Web.Models
{
    public class BannerViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Title { set; get; }

        public int type { set; get; }

        public string TitleType { set; get; }

        public string Image { set; get; }

        public string MoreImages { set; get; }

        public string Description { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }
    }
}