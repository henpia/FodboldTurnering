namespace FT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxAntalHoldtilføjettilTurnering : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Turnering", "MaxAntalHold", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Turnering", "MaxAntalHold");
        }
    }
}
