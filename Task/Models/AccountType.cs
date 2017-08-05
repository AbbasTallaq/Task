using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task.Models
{
    //create AccountType class

    public class AccountType
    {
        [Key]

        public int id { get; set; }
        public string AccountTypeCode { get; set; }
        public string AccountTypeDesc { get; set; }

        public int AccountLevel { get; set; }

    }
}