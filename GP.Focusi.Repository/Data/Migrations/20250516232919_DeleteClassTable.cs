using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP.Focusi.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteClassTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advices_Classes_ChildClassID",
                table: "Advices");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Classes_ChildClassID",
                table: "Stories");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Stories_ChildClassID",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Advices_ChildClassID",
                table: "Advices");

            migrationBuilder.DropColumn(
                name: "ChildClassID",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "ChildClassID",
                table: "Advices");

            //migrationBuilder.AddColumn<string>(
            //    name: "ChildClass",
            //    table: "AppUserChild",
            //    type: "nvarchar(max)",
            //    nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "ChildClass",
            //    table: "AppUserChild");

            migrationBuilder.AddColumn<int>(
                name: "ChildClassID",
                table: "Stories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChildClassID",
                table: "Advices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassScore = table.Column<int>(type: "int", nullable: false),
                    Game_Duration = table.Column<double>(type: "float", nullable: false),
                    Game_Score = table.Column<int>(type: "int", nullable: false),
                    Video_CorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    Video_Questions = table.Column<int>(type: "int", nullable: false),
                    Video_Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_ChildClassID",
                table: "Stories",
                column: "ChildClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Advices_ChildClassID",
                table: "Advices",
                column: "ChildClassID");

            migrationBuilder.AddForeignKey(
                name: "FK_Advices_Classes_ChildClassID",
                table: "Advices",
                column: "ChildClassID",
                principalTable: "Classes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Classes_ChildClassID",
                table: "Stories",
                column: "ChildClassID",
                principalTable: "Classes",
                principalColumn: "ID");
        }
    }
}
