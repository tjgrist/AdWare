namespace AdAtTheRightTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postmergeandpull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Relationships", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Relationships", new[] { "BusinessId" });
            DropIndex("dbo.Relationships", new[] { "UserId" });
            AddColumn("dbo.Relationships", "AppUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Relationships", "BusinessId", c => c.Int(nullable: false));
            AlterColumn("dbo.Relationships", "UserId", c => c.String());
            CreateIndex("dbo.Relationships", "BusinessId");
            CreateIndex("dbo.Relationships", "AppUser_Id");
            AddForeignKey("dbo.Relationships", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses", "BusinessId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.Relationships", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Relationships", new[] { "AppUser_Id" });
            DropIndex("dbo.Relationships", new[] { "BusinessId" });
            AlterColumn("dbo.Relationships", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Relationships", "BusinessId", c => c.Int());
            DropColumn("dbo.Relationships", "AppUser_Id");
            CreateIndex("dbo.Relationships", "UserId");
            CreateIndex("dbo.Relationships", "BusinessId");
            AddForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses", "BusinessId");
            AddForeignKey("dbo.Relationships", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
