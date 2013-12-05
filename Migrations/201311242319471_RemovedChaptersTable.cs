namespace WorkApplications.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedChaptersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Translations", "FullName", c => c.String());
            AddColumn("dbo.Verses", "ChapterNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Verses", "VerseNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Verses", "EsvText", c => c.String());
            AddColumn("dbo.Verses", "NkjvText", c => c.String());
            DropColumn("dbo.Verses", "VersionId");
            DropColumn("dbo.Verses", "ChapterId");
            DropColumn("dbo.Verses", "Text");
            DropTable("dbo.Chapters");
        }
        
        public override void Down()
        {
            CreateTable(
            "dbo.Chapters",
            c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.Int(nullable: false),
                })
            .PrimaryKey(t => t.Id);

            AddColumn("dbo.Verses", "Text", c => c.String());
            AddColumn("dbo.Verses", "ChapterId", c => c.Int(nullable: false));
            AddColumn("dbo.Verses", "VersionId", c => c.Int(nullable: false));
            DropColumn("dbo.Verses", "NkjvText");
            DropColumn("dbo.Verses", "EsvText");
            DropColumn("dbo.Verses", "VerseNumber");
            DropColumn("dbo.Verses", "ChapterNumber");
            DropColumn("dbo.Translations", "FullName");
        }
    }
}
