using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rainbow.Migrations
{
    public partial class GuildPrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "GuildConfigurations",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "GuildConfigurations");
        }
    }
}
