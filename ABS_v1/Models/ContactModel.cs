using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABS_v1.Models
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public List<ContactModel> ContactList { get; set; }
    }
}