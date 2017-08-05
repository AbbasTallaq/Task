using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Task.Models
{
    public class Taskcontext : DbContext
    {
        //create the dateset and attached to context so migaration can run
        public Taskcontext() : base("T askcontext")
        {
        }

        public DbSet<AccountClass> AccountClass { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<AccountType> AccountType { get; set; }


        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //initailize the db while creating
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<Taskcontext> (null);
            base.OnModelCreating(modelBuilder);

        }

        public System.Data.Entity.DbSet<Task.Models.TaskDTO> TaskDTOes { get; set; }
    }
   
}