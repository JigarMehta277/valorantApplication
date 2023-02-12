namespace valorantApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valorantUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.valorantUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        ValorantID = c.String(),
                        MostPlayedAgent = c.String(),
                        PlayHours = c.String(),
                        Scale = c.Int(nullable: false),
                        Reason = c.String(),
                        Skill = c.String(),
                        Position = c.String(),
                        Strategies = c.String(),
                        Ranks = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.valorantUsers");
        }
    }
}
