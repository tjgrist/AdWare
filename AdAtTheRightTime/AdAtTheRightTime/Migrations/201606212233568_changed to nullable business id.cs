namespace AdAtTheRightTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtonullablebusinessid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.AspNetUsers", new[] { "BusinessId" });
            AlterColumn("dbo.AspNetUsers", "BusinessId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "BusinessId");
            AddForeignKey("dbo.AspNetUsers", "BusinessId", "dbo.Businesses", "BusinessId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.AspNetUsers", new[] { "BusinessId" });
            AlterColumn("dbo.AspNetUsers", "BusinessId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "BusinessId");
            AddForeignKey("dbo.AspNetUsers", "BusinessId", "dbo.Businesses", "BusinessId", cascadeDelete: true);
        }
    }
}
