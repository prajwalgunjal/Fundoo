using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Labels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    labelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noteID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.labelId);
                    table.ForeignKey(
                        name: "FK_Labels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Labels_Notes_noteID",
                        column: x => x.noteID,
                        principalTable: "Notes",
                        principalColumn: "noteID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Labels_UserId",
                table: "Labels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_noteID",
                table: "Labels",
                column: "noteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Labels");
        }
    }
}
