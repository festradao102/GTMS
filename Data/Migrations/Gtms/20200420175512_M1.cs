using Microsoft.EntityFrameworkCore.Migrations;

namespace GTMS.Data.Migrations.Gtms
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    uniqueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    entrenador = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.uniqueID);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    uniqueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<int>(nullable: false),
                    team = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    age = table.Column<int>(nullable: false),
                    height = table.Column<float>(nullable: false),
                    weight = table.Column<float>(nullable: false),
                    position = table.Column<string>(nullable: false),
                    TeamuniqueID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.uniqueID);
                    table.ForeignKey(
                        name: "FK_Player_Team_TeamuniqueID",
                        column: x => x.TeamuniqueID,
                        principalTable: "Team",
                        principalColumn: "uniqueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamuniqueID",
                table: "Player",
                column: "TeamuniqueID");
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
