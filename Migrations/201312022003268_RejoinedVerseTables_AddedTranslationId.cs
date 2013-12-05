namespace WorkApplications.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RejoinedVerseTables_AddedTranslationId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Verses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TranslationId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        ChapterNumber = c.Int(nullable: false),
                        VerseNumber = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Esvs");
            DropTable("dbo.Nkjvs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Nkjvs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ChapterNumber = c.Int(nullable: false),
                        VerseNumber = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Esvs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ChapterNumber = c.Int(nullable: false),
                        VerseNumber = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Verses");
        }
    }
}
