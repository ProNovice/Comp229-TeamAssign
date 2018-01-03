using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comp229_TeamAssign
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Company { get; set; }
        public string PublishedDate { get; set; }
        public int Duration { get; set; }
        public string OfficialLink { get; set; }
        public string Description { get; set; }
        public float ReviewScore { get; set; }
        public string Status { get; set; }
        public string PostedDate { get; set; }
        public string PictureUrl { get; set; }
    }
}