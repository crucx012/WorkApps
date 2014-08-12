namespace WorkApplications.Migrations.Bible
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BibleInitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Chapters = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Translations",
                c => new
                    {
                        TranslationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TranslationId);
            
            CreateTable(
                "dbo.Verses",
                c => new
                    {
                        VerseId = c.Int(nullable: false, identity: true),
                        TranslationId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        ChapterNumber = c.Int(nullable: false),
                        VerseNumber = c.Int(nullable: false),
                        SectionHeading = c.String(),
                        Pretext = c.String(),
                        Text = c.String(nullable: false),
                        IsParagraphEnd = c.Boolean(),
                    })
                .PrimaryKey(t => t.VerseId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Translations", t => t.TranslationId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.TranslationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Verses", "TranslationId", "dbo.Translations");
            DropForeignKey("dbo.Verses", "BookId", "dbo.Books");
            DropIndex("dbo.Verses", new[] { "TranslationId" });
            DropIndex("dbo.Verses", new[] { "BookId" });
            DropTable("dbo.Verses");
            DropTable("dbo.Translations");
            DropTable("dbo.Books");
        }
    }
}
