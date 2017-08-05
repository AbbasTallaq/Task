using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task.Models
{

    //create Account Class class
    public class AccountClass
    {
        [Key]
        public int Id { get; set; }
        public string AccountClassName   { get; set; }
        public string NormalBalance { get; set; }

    }
}