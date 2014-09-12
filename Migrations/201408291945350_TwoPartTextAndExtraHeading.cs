namespace WorkApplications.Migrations.Bible
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwoPartTextAndExtraHeading : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Verses", "MidVerseHeading", c => c.String());
            AddColumn("dbo.Verses", "SecondText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Verses", "SecondText");
            DropColumn("dbo.Verses", "MidVerseHeading");
        }
    }
}
