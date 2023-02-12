namespace valorantApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TournamentDetailsvalorantUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TournamentDetails",
                c => new
                    {
                        TournamentId = c.Int(nullable: false, identity: true),
                        LatestTournament = c.String(),
                        LatestAgent = c.String(),
                        TotalKills = c.Int(nullable: false),
                        result = c.String(),
                    })
                .PrimaryKey(t => t.TournamentId);
            
            CreateTable(
                "dbo.valorantUserTournamentDetails",
                c => new
                    {
                        valorantUser_UserId = c.Int(nullable: false),
                        TournamentDetails_TournamentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.valorantUser_UserId, t.TournamentDetails_TournamentId })
                .ForeignKey("dbo.valorantUsers", t => t.valorantUser_UserId, cascadeDelete: true)
                .ForeignKey("dbo.TournamentDetails", t => t.TournamentDetails_TournamentId, cascadeDelete: true)
                .Index(t => t.valorantUser_UserId)
                .Index(t => t.TournamentDetails_TournamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.valorantUserTournamentDetails", "TournamentDetails_TournamentId", "dbo.TournamentDetails");
            DropForeignKey("dbo.valorantUserTournamentDetails", "valorantUser_UserId", "dbo.valorantUsers");
            DropIndex("dbo.valorantUserTournamentDetails", new[] { "TournamentDetails_TournamentId" });
            DropIndex("dbo.valorantUserTournamentDetails", new[] { "valorantUser_UserId" });
            DropTable("dbo.valorantUserTournamentDetails");
            DropTable("dbo.TournamentDetails");
        }
    }
}
