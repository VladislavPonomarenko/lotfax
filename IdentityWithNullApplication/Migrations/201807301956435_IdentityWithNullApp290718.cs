namespace IdentityWithNullApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityWithNullApp290718 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "DateTimeComm", c => c.DateTime(nullable: false));
            DropColumn("dbo.Comments", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Comments", "DateTimeComm");
        }
    }
}
