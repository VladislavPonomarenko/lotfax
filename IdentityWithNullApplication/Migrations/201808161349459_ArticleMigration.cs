namespace IdentityWithNullApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "ImageString");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "ImageString", c => c.String());
        }
    }
}
