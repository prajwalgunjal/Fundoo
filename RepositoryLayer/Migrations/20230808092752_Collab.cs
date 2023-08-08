using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Collab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collabs",
                columns: table => new
                {
                    ColabId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    noteID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collabs", x => x.ColabId);
                    table.ForeignKey(
                        name: "FK_Collabs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Collabs_Notes_noteID",
                        column: x => x.noteID,
                        principalTable: "Notes",
                        principalColumn: "noteID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collabs_UserId",
                table: "Collabs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Collabs_noteID",
                table: "Collabs",
                column: "noteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collabs");
        }
    }
}
