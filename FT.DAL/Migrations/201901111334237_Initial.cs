namespace FT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HoldListe",
                c => new
                    {
                        HoldId = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                        DatoForTilmelding = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TurneringId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HoldId)
                .ForeignKey("dbo.Turnering", t => t.TurneringId, cascadeDelete: true)
                .Index(t => t.TurneringId);
            
            CreateTable(
                "dbo.Kampe",
                c => new
                    {
                        KampId = c.Int(nullable: false, identity: true),
                        DatoForKamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Resultat = c.String(),
                        RundeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KampId)
                .ForeignKey("dbo.Runder", t => t.RundeId, cascadeDelete: true)
                .Index(t => t.RundeId);
            
            CreateTable(
                "dbo.Runder",
                c => new
                    {
                        RundeId = c.Int(nullable: false, identity: true),
                        Betegnelse = c.String(),
                        TurneringId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RundeId)
                .ForeignKey("dbo.Turnering", t => t.TurneringId, cascadeDelete: true)
                .Index(t => t.TurneringId);
            
            CreateTable(
                "dbo.Turnering",
                c => new
                    {
                        TurneringId = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                    })
                .PrimaryKey(t => t.TurneringId);
            
            CreateTable(
                "dbo.KampHolds",
                c => new
                    {
                        Kamp_KampId = c.Int(nullable: false),
                        Hold_HoldId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Kamp_KampId, t.Hold_HoldId })
                .ForeignKey("dbo.Kampe", t => t.Kamp_KampId, cascadeDelete: false)
                .ForeignKey("dbo.HoldListe", t => t.Hold_HoldId, cascadeDelete: false)
                .Index(t => t.Kamp_KampId)
                .Index(t => t.Hold_HoldId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Runder", "TurneringId", "dbo.Turnering");
            DropForeignKey("dbo.HoldListe", "TurneringId", "dbo.Turnering");
            DropForeignKey("dbo.Kampe", "RundeId", "dbo.Runder");
            DropForeignKey("dbo.KampHolds", "Hold_HoldId", "dbo.HoldListe");
            DropForeignKey("dbo.KampHolds", "Kamp_KampId", "dbo.Kampe");
            DropIndex("dbo.KampHolds", new[] { "Hold_HoldId" });
            DropIndex("dbo.KampHolds", new[] { "Kamp_KampId" });
            DropIndex("dbo.Runder", new[] { "TurneringId" });
            DropIndex("dbo.Kampe", new[] { "RundeId" });
            DropIndex("dbo.HoldListe", new[] { "TurneringId" });
            DropTable("dbo.KampHolds");
            DropTable("dbo.Turnering");
            DropTable("dbo.Runder");
            DropTable("dbo.Kampe");
            DropTable("dbo.HoldListe");
        }
    }
}
