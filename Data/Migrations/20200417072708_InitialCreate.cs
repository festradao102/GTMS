using Microsoft.EntityFrameworkCore.Migrations;

namespace GTMS.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    uniqueID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    age = table.Column<int>(nullable: false),
                    height = table.Column<float>(nullable: false),
                    weight = table.Column<float>(nullable: false),
                    position = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.uniqueID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
