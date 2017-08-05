namespace Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountClass",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountClassName = c.String(),
                        NormalBalance = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        AccountCode = c.Int(nullable: false),
                        ParentAccountCode = c.Int(nullable: false),
                        AccountName = c.String(),
                        AccountOrder = c.Int(nullable: false),
                        AccountClassID = c.Int(nullable: false),
                        AccountTypeCode = c.Int(nullable: false),
                        OpenBalanceCredit = c.Single(nullable: false),
                        OpenBalanceDebit = c.Single(nullable: false),
                        StartingBalanceDebit = c.Single(nullable: false),
                        StartingBalanceCredit = c.Single(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        AccountType_id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountClass", t => t.AccountClassID)
                .ForeignKey("dbo.AccountType", t => t.AccountType_id)
                .Index(t => t.AccountClassID)
                .Index(t => t.AccountType_id);
            
            CreateTable(
                "dbo.AccountType",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        AccountTypeCode = c.String(),
                        AccountTypeDesc = c.String(),
                        AccountLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "AccountType_id", "dbo.AccountType");
            DropForeignKey("dbo.Accounts", "AccountClassID", "dbo.AccountClass");
            DropIndex("dbo.Accounts", new[] { "AccountType_id" });
            DropIndex("dbo.Accounts", new[] { "AccountClassID" });
            DropTable("dbo.AccountType");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountClass");
        }
    }
}
