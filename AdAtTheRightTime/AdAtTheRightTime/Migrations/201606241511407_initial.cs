namespace AdAtTheRightTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Queries", new[] { "BusinessId" });
            DropIndex("dbo.Relationships", new[] { "BusinessId" });
            DropIndex("dbo.Relationships", new[] { "AppUser_Id" });
            DropColumn("dbo.Relationships", "UserId");
            RenameColumn(table: "dbo.Relationships", name: "AppUser_Id", newName: "UserId");
            AddColumn("dbo.Businesses", "Description", c => c.String());
            AddColumn("dbo.Businesses", "Promotion", c => c.String());
            AlterColumn("dbo.Queries", "BusinessId", c => c.Int());
            AlterColumn("dbo.Relationships", "BusinessId", c => c.Int());
            AlterColumn("dbo.Relationships", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Queries", "BusinessId");
            CreateIndex("dbo.Relationships", "BusinessId");
            CreateIndex("dbo.Relationships", "UserId");
            AddForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses", "BusinessId");
            AddForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses", "BusinessId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Relationships", new[] { "UserId" });
            DropIndex("dbo.Relationships", new[] { "BusinessId" });
            DropIndex("dbo.Queries", new[] { "BusinessId" });
            AlterColumn("dbo.Relationships", "UserId", c => c.String());
            AlterColumn("dbo.Relationships", "BusinessId", c => c.Int(nullable: false));
            AlterColumn("dbo.Queries", "BusinessId", c => c.Int(nullable: false));
            DropColumn("dbo.Businesses", "Promotion");
            DropColumn("dbo.Businesses", "Description");
            RenameColumn(table: "dbo.Relationships", name: "UserId", newName: "AppUser_Id");
            AddColumn("dbo.Relationships", "UserId", c => c.String());
            CreateIndex("dbo.Relationships", "AppUser_Id");
            CreateIndex("dbo.Relationships", "BusinessId");
            CreateIndex("dbo.Queries", "BusinessId");
            AddForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses", "BusinessId", cascadeDelete: true);
            AddForeignKey("dbo.Queries", "BusinessId", "dbo.Businesses", "BusinessId", cascadeDelete: true);
        }
    }
}
