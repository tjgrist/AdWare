namespace AdAtTheRightTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postmergewithjoeFri : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Relationships", new[] { "BusinessId" });
            DropIndex("dbo.Relationships", new[] { "AppUser_Id" });
            DropColumn("dbo.Relationships", "UserId");
            RenameColumn(table: "dbo.Relationships", name: "AppUser_Id", newName: "UserId");
            AlterColumn("dbo.Relationships", "BusinessId", c => c.Int());
            AlterColumn("dbo.Relationships", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Relationships", "BusinessId");
            CreateIndex("dbo.Relationships", "UserId");
            AddForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses", "BusinessId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses");
            DropIndex("dbo.Relationships", new[] { "UserId" });
            DropIndex("dbo.Relationships", new[] { "BusinessId" });
            AlterColumn("dbo.Relationships", "UserId", c => c.String());
            AlterColumn("dbo.Relationships", "BusinessId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Relationships", name: "UserId", newName: "AppUser_Id");
            AddColumn("dbo.Relationships", "UserId", c => c.String());
            CreateIndex("dbo.Relationships", "AppUser_Id");
            CreateIndex("dbo.Relationships", "BusinessId");
            AddForeignKey("dbo.Relationships", "BusinessId", "dbo.Businesses", "BusinessId", cascadeDelete: true);
        }
    }
}
