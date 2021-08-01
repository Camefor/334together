using Microsoft.EntityFrameworkCore.Migrations;

namespace Xhznl.HelloAbp.Migrations
{
    public partial class addfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LyricUrl",
                table: "MusicSongs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LyricUrl",
                table: "MusicSongs");
        }
    }
}
