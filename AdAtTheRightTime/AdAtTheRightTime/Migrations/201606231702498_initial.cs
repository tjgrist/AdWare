namespace AdAtTheRightTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Queries", new[] { "BusinessId" });
            AddColumn("dbo.Businesses", "Description", c => c.String());
            AddColumn("dbo.Businesses", "Promotion", c => c.String());
            AlterColumn("dbo.Queries", "BusinessId", c => c.Int());
            CreateIndex("dbo.Queries", "BusinessId");
            AddForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses", "BusinessId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Queries", new[] { "BusinessId" });
            AlterColumn("dbo.Queries", "BusinessId", c => c.Int(nullable: false));
            DropColumn("dbo.Businesses", "Promotion");
            DropColumn("dbo.Businesses", "Description");
            CreateIndex("dbo.Queries", "BusinessId");
            AddForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses", "BusinessId", cascadeDelete: true);
        }
    }
}
