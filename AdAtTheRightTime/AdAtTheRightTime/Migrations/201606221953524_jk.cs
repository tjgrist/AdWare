namespace AdAtTheRightTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Businesses", "Description", c => c.String());
            AddColumn("dbo.Businesses", "Promotion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Businesses", "Promotion");
            DropColumn("dbo.Businesses", "Description");
        }
    }
}
