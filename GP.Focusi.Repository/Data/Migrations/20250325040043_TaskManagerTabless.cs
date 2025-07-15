using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP.Focusi.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaskManagerTabless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskManagers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChildMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskManagerScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskManagerItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskManagerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDateAndTimeEnded = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskManagerItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskManagerItems_TaskManagers_TaskManagerId",
                        column: x => x.TaskManagerId,
                        principalTable: "TaskManagers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskManagerItems_TaskManagerId",
                table: "TaskManagerItems",
                column: "TaskManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskManagerItems");

            migrationBuilder.DropTable(
                name: "TaskManagers");
        }
    }
}
