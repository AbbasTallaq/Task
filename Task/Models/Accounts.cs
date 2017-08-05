using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task.Models
{    
    //create Account class

    public class Accounts
    {
        [Key]
        public int ID { get; set; }
        public int CompanyId { get; set; }
        public int AccountCode        { get; set; }
        public int ParentAccountCode        { get; set; }
        public string AccountName { get; set; }
        public int AccountOrder { get; set; }
        public int AccountClassID { get; set; }
        public int AccountTypeCode        { get; set; }
        public float OpenBalanceCredit        { get; set; }
        public float OpenBalanceDebit      { get; set; }
        public float StartingBalanceDebit        { get; set; }
        public float StartingBalanceCredit { get; set; }
        public string CreatedBy  { get; set; }
        [DataType(DataType.DateTime)]

        public DateTime CreatedOn { get; set; }
        public string ModifiedBy        { get; set; }
        [DataType(DataType.DateTime)]

        public DateTime ModifiedOn  { get; set; }

        public AccountClass AccountClass { get; set; }
        public AccountType AccountType { get; set; }

    }
}