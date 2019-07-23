using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABS_v1.Models
{
    public class PaymentModel
    {
        [Key]
        public int Id { get; set; }
        public string PayeeName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double Amount { get; set; }
        public string PaymentReqId { get; set; }
        public string PaymentStatus { get; set; }
        public int RegId { get; set; }
    }
}