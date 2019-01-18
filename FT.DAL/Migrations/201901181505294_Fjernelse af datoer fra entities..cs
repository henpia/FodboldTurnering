namespace FT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fjernelseafdatoerfraentities : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HoldListe", "DatoForOprettelse");
            DropColumn("dbo.Kampe", "DatoForKamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kampe", "DatoForKamp", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.HoldListe", "DatoForOprettelse", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
