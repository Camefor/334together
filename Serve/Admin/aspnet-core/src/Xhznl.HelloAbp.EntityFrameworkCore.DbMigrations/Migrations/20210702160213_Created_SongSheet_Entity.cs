using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xhznl.HelloAbp.Migrations
{
    public partial class Created_SongSheet_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicSongSheet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Dissid = table.Column<string>(maxLength: 128, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CommitTime = table.Column<DateTime>(nullable: false),
                    DissName = table.Column<string>(nullable: true),
                    ImgUrl = table.Column<string>(nullable: true),
                    Introduction = table.Column<string>(nullable: true),
                    ListenNum = table.Column<string>(nullable: true),
                    Score = table.Column<double>(nullable: false),
                    Version = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicSongSheet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicSongSheet");
        }
    }
}
