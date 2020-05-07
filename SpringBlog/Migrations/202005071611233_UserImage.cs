namespace SpringBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserImage", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserImage");
        }
    }
}
