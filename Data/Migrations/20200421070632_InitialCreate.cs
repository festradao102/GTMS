using Microsoft.EntityFrameworkCore.Migrations;

namespace GTMS.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    uniqueTeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    trainer = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.uniqueTeamID);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    uniquePlayerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    identification = table.Column<int>(nullable: false),
                    team = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    age = table.Column<int>(nullable: false),
                    height = table.Column<float>(nullable: false),
                    weight = table.Column<float>(nullable: false),
                    position = table.Column<string>(nullable: false),
                    TeamuniqueTeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.uniquePlayerID);
                    table.ForeignKey(
                        name: "FK_Player_Team_TeamuniqueTeamID",
                        column: x => x.TeamuniqueTeamID,
                        principalTable: "Team",
                        principalColumn: "uniqueTeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamuniqueTeamID",
                table: "Player",
                column: "TeamuniqueTeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
