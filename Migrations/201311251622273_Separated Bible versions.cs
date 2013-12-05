namespace WorkApplications.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeparatedBibleversions : DbMigration
    {
        public override void Up()
        {
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

            DropTable("dbo.Verses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Verses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ChapterNumber = c.Int(nullable: false),
                        VerseNumber = c.Int(nullable: false),
                        EsvText = c.String(),
                        NkjvText = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            DropTable("dbo.Nkjvs");
            DropTable("dbo.Esvs");
        }
    }
}
