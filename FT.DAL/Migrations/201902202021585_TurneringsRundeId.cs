namespace FT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurneringsRundeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kampe", "Runde_TurneringsRundeId", "dbo.TurneringsRunder");
            DropIndex("dbo.Kampe", new[] { "Runde_TurneringsRundeId" });
            RenameColumn(table: "dbo.Kampe", name: "Runde_TurneringsRundeId", newName: "TurneringsRundeId");
            AlterColumn("dbo.Kampe", "TurneringsRundeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Kampe", "TurneringsRundeId");
            AddForeignKey("dbo.Kampe", "TurneringsRundeId", "dbo.TurneringsRunder", "TurneringsRundeId", cascadeDelete: true);
            DropColumn("dbo.Kampe", "RundeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kampe", "RundeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Kampe", "TurneringsRundeId", "dbo.TurneringsRunder");
            DropIndex("dbo.Kampe", new[] { "TurneringsRundeId" });
            AlterColumn("dbo.Kampe", "TurneringsRundeId", c => c.Int());
            RenameColumn(table: "dbo.Kampe", name: "TurneringsRundeId", newName: "Runde_TurneringsRundeId");
            CreateIndex("dbo.Kampe", "Runde_TurneringsRundeId");
            AddForeignKey("dbo.Kampe", "Runde_TurneringsRundeId", "dbo.TurneringsRunder", "TurneringsRundeId");
        }
    }
}
