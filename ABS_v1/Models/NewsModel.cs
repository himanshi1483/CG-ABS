using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABS_v1.Models
{
    public class NewsModel
    {
        [Key]
        public int NewsId { get; set; }
        [Display(Name = "समाचार  तिथि")]
        public DateTime? NewsDate { get; set; }
        [Display(Name = "समाचार का नाम")]
        public string NewsTitle { get; set; }
        [Display(Name = "विस्तृत जानकारी")]
        public string NewsDescription { get; set; }
        [Display(Name = "समाचार स्थल")]
        public string Location { get; set; }
        [Display(Name = "फ़ोटो")]
        public string Image { get; set; }
        [NotMapped]
        public List<NewsModel> NewsList { get; set; }
    }
}