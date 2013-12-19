namespace WorkApplications.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSectionHeading : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Verses", "SectionHeading", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Verses", "SectionHeading");
        }
    }
}
