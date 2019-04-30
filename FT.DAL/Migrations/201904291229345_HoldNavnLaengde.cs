namespace FT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HoldNavnLaengde : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HoldListe", "Navn", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HoldListe", "Navn", c => c.String());
        }
    }
}
