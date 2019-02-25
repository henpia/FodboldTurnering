namespace FT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TilføjetTurneringIdTilKampEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kampe", "Turnering_TurneringId", "dbo.Turnering");
            DropIndex("dbo.Kampe", new[] { "Turnering_TurneringId" });
            RenameColumn(table: "dbo.Kampe", name: "Turnering_TurneringId", newName: "TurneringId");
            AlterColumn("dbo.Kampe", "TurneringId", c => c.Int(nullable: false));
            CreateIndex("dbo.Kampe", "TurneringId");
            AddForeignKey("dbo.Kampe", "TurneringId", "dbo.Turnering", "TurneringId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kampe", "TurneringId", "dbo.Turnering");
            DropIndex("dbo.Kampe", new[] { "TurneringId" });
            AlterColumn("dbo.Kampe", "TurneringId", c => c.Int());
            RenameColumn(table: "dbo.Kampe", name: "TurneringId", newName: "Turnering_TurneringId");
            CreateIndex("dbo.Kampe", "Turnering_TurneringId");
            AddForeignKey("dbo.Kampe", "Turnering_TurneringId", "dbo.Turnering", "TurneringId");
        }
    }
}
