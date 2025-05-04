using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP.Focusi.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "ItemsId",
            //    table: "TaskManagers");

            migrationBuilder.CreateTable(
                name: "FeedBacks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Q1ProgramHelpParent = table.Column<int>(type: "int", nullable: false),
                    Q2SuitableActivity = table.Column<int>(type: "int", nullable: false),
                    Q3ContentUnderstand = table.Column<int>(type: "int", nullable: false),
                    Q4behaviurImprovement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Q5ContinueInProgram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Q6RecomendProgram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Q7MostHelpfulPart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suggestions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBacks", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedBacks");

            //migrationBuilder.AddColumn<int>(
            //    name: "ItemsId",
            //    table: "TaskManagers",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }
    }
}
