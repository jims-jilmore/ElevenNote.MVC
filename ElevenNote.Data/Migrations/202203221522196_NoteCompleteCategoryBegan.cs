namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteCompleteCategoryBegan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        NoteID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 25),
                        CreatedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Note", t => t.NoteID, cascadeDelete: true)
                .Index(t => t.NoteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "NoteID", "dbo.Note");
            DropIndex("dbo.Category", new[] { "NoteID" });
            DropTable("dbo.Category");
        }
    }
}
