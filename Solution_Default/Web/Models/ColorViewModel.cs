using System;

namespace Web.Models
{
    public class ColorViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string NameVN { set; get; }

        public string Alias { set; get; }

        public int Type { set; get; }

        public string Description { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }
    }
}