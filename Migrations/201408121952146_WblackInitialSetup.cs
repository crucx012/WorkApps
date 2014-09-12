namespace WorkApplications.Migrations.Wblack
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WblackInitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        AccountTypeID = c.Int(nullable: false),
                        EntityID = c.Int(nullable: false),
                        Number = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeID)
                .ForeignKey("dbo.Entities", t => t.EntityID)
                .Index(t => t.AccountTypeID)
                .Index(t => t.EntityID);
            
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        AccountTypeID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountTypeID);
            
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        EntityID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EntityID);
            
            CreateTable(
                "dbo.BankStatementAccounts",
                c => new
                    {
                        BankStatementAccountID = c.Int(nullable: false, identity: true),
                        BankStatementID = c.Int(nullable: false),
                        AccountID = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BankStatementAccountID)
                .ForeignKey("dbo.Accounts", t => t.AccountID)
                .ForeignKey("dbo.BankStatements", t => t.BankStatementID)
                .Index(t => t.AccountID)
                .Index(t => t.BankStatementID);
            
            CreateTable(
                "dbo.BankStatements",
                c => new
                    {
                        BankStatementID = c.Int(nullable: false, identity: true),
                        EntityID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BankStatementID)
                .ForeignKey("dbo.Entities", t => t.EntityID)
                .Index(t => t.EntityID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.GasReceipts",
                c => new
                    {
                        GasReceiptID = c.Int(nullable: false, identity: true),
                        EntityID = c.Int(nullable: false),
                        ReceiptDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Gallons = c.Decimal(nullable: false, precision: 7, scale: 3),
                        Miles = c.Decimal(nullable: false, precision: 5, scale: 1),
                        TankStart = c.Decimal(nullable: false, precision: 3, scale: 2),
                        TankEnd = c.Decimal(nullable: false, precision: 3, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GasReceiptID)
                .ForeignKey("dbo.Entities", t => t.EntityID)
                .Index(t => t.EntityID);
            
            CreateTable(
                "dbo.SleepTimes",
                c => new
                    {
                        SleepID = c.Int(nullable: false, identity: true),
                        SleepStartTime = c.DateTime(nullable: false),
                        SleepEndTime = c.DateTime(nullable: false),
                        Note = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SleepID);
            
            CreateTable(
                "dbo.Tenders",
                c => new
                    {
                        TenderID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TenderID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        EntityID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        TenderID = c.Int(nullable: false),
                        TransactionTypeID = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false, storeType: "date"),
                        TransactionAmt = c.Decimal(nullable: false, precision: 10, scale: 2),
                        CashBack = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Description = c.String(nullable: false),
                        TransferAccount = c.Int(),
                        BankStatementID = c.Int(nullable: false),
                        BankRecorded = c.Boolean(nullable: false),
                        BankRecordedDate = c.DateTime(storeType: "date"),
                        BankRecordedAmt = c.Decimal(precision: 10, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Accounts", t => t.AccountID)
                .ForeignKey("dbo.BankStatements", t => t.BankStatementID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Entities", t => t.EntityID)
                .ForeignKey("dbo.Tenders", t => t.TenderID)
                .ForeignKey("dbo.TransactionTypes", t => t.TransactionTypeID)
                .Index(t => t.AccountID)
                .Index(t => t.BankStatementID)
                .Index(t => t.CategoryID)
                .Index(t => t.EntityID)
                .Index(t => t.TenderID)
                .Index(t => t.TransactionTypeID);
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        TransactionTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "TransactionTypeID", "dbo.TransactionTypes");
            DropForeignKey("dbo.Transactions", "TenderID", "dbo.Tenders");
            DropForeignKey("dbo.Transactions", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.Transactions", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Transactions", "BankStatementID", "dbo.BankStatements");
            DropForeignKey("dbo.Transactions", "AccountID", "dbo.Accounts");
            DropForeignKey("dbo.GasReceipts", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.BankStatementAccounts", "BankStatementID", "dbo.BankStatements");
            DropForeignKey("dbo.BankStatements", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.BankStatementAccounts", "AccountID", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.Accounts", "AccountTypeID", "dbo.AccountTypes");
            DropIndex("dbo.Transactions", new[] { "TransactionTypeID" });
            DropIndex("dbo.Transactions", new[] { "TenderID" });
            DropIndex("dbo.Transactions", new[] { "EntityID" });
            DropIndex("dbo.Transactions", new[] { "CategoryID" });
            DropIndex("dbo.Transactions", new[] { "BankStatementID" });
            DropIndex("dbo.Transactions", new[] { "AccountID" });
            DropIndex("dbo.GasReceipts", new[] { "EntityID" });
            DropIndex("dbo.BankStatementAccounts", new[] { "BankStatementID" });
            DropIndex("dbo.BankStatements", new[] { "EntityID" });
            DropIndex("dbo.BankStatementAccounts", new[] { "AccountID" });
            DropIndex("dbo.Accounts", new[] { "EntityID" });
            DropIndex("dbo.Accounts", new[] { "AccountTypeID" });
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.Transactions");
            DropTable("dbo.Tenders");
            DropTable("dbo.SleepTimes");
            DropTable("dbo.GasReceipts");
            DropTable("dbo.Categories");
            DropTable("dbo.BankStatements");
            DropTable("dbo.BankStatementAccounts");
            DropTable("dbo.Entities");
            DropTable("dbo.AccountTypes");
            DropTable("dbo.Accounts");
        }
    }
}
