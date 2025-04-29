using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP.Focusi.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class addItemsToTaskManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TaskManagers",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TaskManagerItems",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "ItemsId",
                table: "TaskManagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDateAndTimeEnded",
                table: "TaskManagerItems",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsId",
                table: "TaskManagers");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TaskManagers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TaskManagerItems",
                newName: "Id");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDateAndTimeEnded",
                table: "TaskManagerItems",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
