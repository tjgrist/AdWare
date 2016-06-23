namespace AdAtTheRightTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Queries", new[] { "BusinessId" });
            AlterColumn("dbo.Queries", "BusinessId", c => c.Int());
            CreateIndex("dbo.Queries", "BusinessId");
            AddForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses", "BusinessId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Queries", new[] { "BusinessId" });
            AlterColumn("dbo.Queries", "BusinessId", c => c.Int(nullable: false));
            CreateIndex("dbo.Queries", "BusinessId");
            AddForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses", "BusinessId", cascadeDelete: true);
        }
    }
}
