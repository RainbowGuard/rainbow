using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rainbow.Migrations
{
    public partial class FlaggedUserReasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "FlaggedUser",
                newName: "UnflagReason");

            migrationBuilder.AddColumn<string>(
                name: "FlagReason",
                table: "FlaggedUser",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlagReason",
                table: "FlaggedUser");

            migrationBuilder.RenameColumn(
                name: "UnflagReason",
                table: "FlaggedUser",
                newName: "Reason");
        }
    }
}
