using Microsoft.EntityFrameworkCore.Migrations;

namespace ActorService.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    ZoneType = table.Column<string>(type: "VARCHAR(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Quality = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    CurrentHealth = table.Column<int>(nullable: false),
                    BaseHealth = table.Column<int>(nullable: false),
                    BasePower = table.Column<int>(nullable: false),
                    BaseSpeed = table.Column<int>(nullable: false),
                    Health = table.Column<int>(nullable: false),
                    Power = table.Column<int>(nullable: false),
                    Speed = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Balance = table.Column<string>(nullable: true),
                    Experience = table.Column<int>(nullable: false),
                    Abilities = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ZoneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actors_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ZoneId",
                table: "Actors",
                column: "ZoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}
