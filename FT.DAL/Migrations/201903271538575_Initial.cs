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
                    })
                .PrimaryKey(t => t.HoldId);
            
            CreateTable(
                "dbo.Kampe",
                c => new
                    {
                        KampId = c.Int(nullable: false, identity: true),
                        TurneringId = c.Int(nullable: false),
                        TurneringsRunde = c.Int(nullable: false),
                        ScoreHjemmeHold = c.String(),
                        ScoreUdeHold = c.String(),
                    })
                .PrimaryKey(t => t.KampId)
                .ForeignKey("dbo.Turnering", t => t.TurneringId, cascadeDelete: true)
                .Index(t => t.TurneringId);
            
            CreateTable(
                "dbo.Turnering",
                c => new
                    {
                        TurneringId = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                        MaxAntalHold = c.Int(nullable: false),
                        AabenForTilmelding = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.Kampe", t => t.Kamp_KampId, cascadeDelete: true)
                .ForeignKey("dbo.HoldListe", t => t.Hold_HoldId, cascadeDelete: true)
                .Index(t => t.Kamp_KampId)
                .Index(t => t.Hold_HoldId);
            
            CreateTable(
                "dbo.TurneringHolds",
                c => new
                    {
                        Turnering_TurneringId = c.Int(nullable: false),
                        Hold_HoldId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Turnering_TurneringId, t.Hold_HoldId })
                .ForeignKey("dbo.Turnering", t => t.Turnering_TurneringId, cascadeDelete: true)
                .ForeignKey("dbo.HoldListe", t => t.Hold_HoldId, cascadeDelete: true)
                .Index(t => t.Turnering_TurneringId)
                .Index(t => t.Hold_HoldId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kampe", "TurneringId", "dbo.Turnering");
            DropForeignKey("dbo.TurneringHolds", "Hold_HoldId", "dbo.HoldListe");
            DropForeignKey("dbo.TurneringHolds", "Turnering_TurneringId", "dbo.Turnering");
            DropForeignKey("dbo.KampHolds", "Hold_HoldId", "dbo.HoldListe");
            DropForeignKey("dbo.KampHolds", "Kamp_KampId", "dbo.Kampe");
            DropIndex("dbo.TurneringHolds", new[] { "Hold_HoldId" });
            DropIndex("dbo.TurneringHolds", new[] { "Turnering_TurneringId" });
            DropIndex("dbo.KampHolds", new[] { "Hold_HoldId" });
            DropIndex("dbo.KampHolds", new[] { "Kamp_KampId" });
            DropIndex("dbo.Kampe", new[] { "TurneringId" });
            DropTable("dbo.TurneringHolds");
            DropTable("dbo.KampHolds");
            DropTable("dbo.Turnering");
            DropTable("dbo.Kampe");
            DropTable("dbo.HoldListe");
        }
    }
}
