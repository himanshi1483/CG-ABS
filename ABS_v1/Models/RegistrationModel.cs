using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace ABS_v1.Models
{

    public class FormData
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string BirthPlace { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string Gotra { get; set; }
        public string Education { get; set; }
        public string Profession { get; set; }
        public string AnnualIncome { get; set; }
        public string MaritalStatus { get; set; }
        public string BrothersDetails { get; set; }
        public string SistersDetails { get; set; }
        public string BramhinType { get; set; }
        public int AgeDifference { get; set; }
        public string Agreement { get; set; }
        public string GuardiansContact { get; set; }
        public string FullAddress { get; set; }
        public string InformantDetails { get; set; }
        public string ImageName { get; set; }
        public DateTime RegDate { get; set; }
      
        public bool PaymentStatus { get; set; }
        public string RegPlace { get; set; }
        [NotMapped]
        public PaymentModel paymentDetails { get; set; }
        [NotMapped]
        public List<FormData> DataList { get; set; }

    }
}