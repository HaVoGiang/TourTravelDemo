namespace TourTravelDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Blogs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blogs", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Blogs", new[] { "Category_Id" });
            DropColumn("dbo.Blogs", "Category_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogs", "Category_Id", c => c.Int());
            CreateIndex("dbo.Blogs", "Category_Id");
            AddForeignKey("dbo.Blogs", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
