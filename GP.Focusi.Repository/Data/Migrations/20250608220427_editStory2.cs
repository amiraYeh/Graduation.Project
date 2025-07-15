using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP.Focusi.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class editStory2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Stories",
                newName: "StoryName");

            migrationBuilder.RenameColumn(
                name: "StoryName",
                table: "Advices",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoryName",
                table: "Stories",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Advices",
                newName: "StoryName");
        }
    }
}
