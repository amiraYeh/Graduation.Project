using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP.Focusi.Repository.Identity.Migrations
{
    /// <inheritdoc />
    public partial class addGameScoreColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameTestScore",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "4", null, "ClassAccess", "CLASSACCESS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "4");

            migrationBuilder.DropColumn(
                name: "GameTestScore",
                table: "AspNetUsers");
        }
    }
}
