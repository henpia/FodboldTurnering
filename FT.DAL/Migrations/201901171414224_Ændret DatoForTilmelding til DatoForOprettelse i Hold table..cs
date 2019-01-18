namespace FT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ÆndretDatoForTilmeldingtilDatoForOprettelseiHoldtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HoldListe", "DatoForOprettelse", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.HoldListe", "DatoForTilmelding");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HoldListe", "DatoForTilmelding", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.HoldListe", "DatoForOprettelse");
        }
    }
}
