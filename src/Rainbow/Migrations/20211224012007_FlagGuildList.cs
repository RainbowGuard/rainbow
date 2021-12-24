using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rainbow.Migrations
{
    public partial class FlagGuildList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlagCount",
                table: "FlaggedUser");

            migrationBuilder.DropColumn(
                name: "UnflagReason",
                table: "FlaggedUser");

            migrationBuilder.CreateTable(
                name: "Guild",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FlaggedUserId = table.Column<ulong>(type: "bigint unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guild_FlaggedUser_FlaggedUserId",
                        column: x => x.FlaggedUserId,
                        principalTable: "FlaggedUser",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Guild_FlaggedUserId",
                table: "Guild",
                column: "FlaggedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guild");

            migrationBuilder.AddColumn<ulong>(
                name: "FlagCount",
                table: "FlaggedUser",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<string>(
                name: "UnflagReason",
                table: "FlaggedUser",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
