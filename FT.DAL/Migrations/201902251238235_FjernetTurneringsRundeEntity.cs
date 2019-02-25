namespace FT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FjernetTurneringsRundeEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kampe", "TurneringsRundeId", "dbo.TurneringsRunder");
            DropForeignKey("dbo.TurneringsRunder", "TurneringId", "dbo.Turnering");
            DropIndex("dbo.Kampe", new[] { "TurneringsRundeId" });
            DropIndex("dbo.TurneringsRunder", new[] { "TurneringId" });
            AddColumn("dbo.Kampe", "TurneringsRunde", c => c.Int(nullable: false));
            AddColumn("dbo.Kampe", "Turnering_TurneringId", c => c.Int());
            CreateIndex("dbo.Kampe", "Turnering_TurneringId");
            AddForeignKey("dbo.Kampe", "Turnering_TurneringId", "dbo.Turnering", "TurneringId");
            DropColumn("dbo.Kampe", "TurneringsRundeId");
            DropTable("dbo.TurneringsRunder");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TurneringsRunder",
                c => new
                    {
                        TurneringsRundeId = c.Int(nullable: false, identity: true),
                        TurneringId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TurneringsRundeId);
            
            AddColumn("dbo.Kampe", "TurneringsRundeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Kampe", "Turnering_TurneringId", "dbo.Turnering");
            DropIndex("dbo.Kampe", new[] { "Turnering_TurneringId" });
            DropColumn("dbo.Kampe", "Turnering_TurneringId");
            DropColumn("dbo.Kampe", "TurneringsRunde");
            CreateIndex("dbo.TurneringsRunder", "TurneringId");
            CreateIndex("dbo.Kampe", "TurneringsRundeId");
            AddForeignKey("dbo.TurneringsRunder", "TurneringId", "dbo.Turnering", "TurneringId", cascadeDelete: true);
            AddForeignKey("dbo.Kampe", "TurneringsRundeId", "dbo.TurneringsRunder", "TurneringsRundeId", cascadeDelete: true);
        }
    }
}
