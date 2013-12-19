namespace WorkApplications.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParagraphEndField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Verses", "IsParagraphEnd", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Verses", "IsParagraphEnd");
        }
    }
}
