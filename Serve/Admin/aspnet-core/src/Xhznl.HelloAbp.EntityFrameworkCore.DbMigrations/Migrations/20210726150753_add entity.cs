using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xhznl.HelloAbp.Migrations
{
    public partial class addentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicSongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Purl = table.Column<string>(nullable: true),
                    SongId = table.Column<int>(maxLength: 128, nullable: false),
                    SongmId = table.Column<string>(nullable: true),
                    AlbummId = table.Column<string>(nullable: true),
                    AlbumId = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    SongName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicSongs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicSongs");
        }
    }
}
