using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABS_v1.Models
{
    public class EventModel
    {
        [Key]
        public int EventId { get; set; }

        [Display(Name = "आयोजन तिथि")]
        public DateTime? EventDate { get; set; }
        [Display(Name = "आयोजन का नाम")]
        public string EventName { get; set; }
        [Display(Name = "विस्तृत जानकारी")]
        public string EventDescription { get; set; }
        [Display(Name = "आयोजन स्थल")]
        public string EventLocation { get; set; }
        [Display(Name = "फ़ोटो ")]    
        public string Image { get; set; }
        [NotMapped]
        public List<EventModel> EventList { get; set; }
    }
}