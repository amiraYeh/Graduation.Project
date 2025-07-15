using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GP.Focusi.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditTaskManager00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<int>(
			   name: "ItemsCount",
			   table: "TaskManagers",
			   type: "int",
			   nullable: false,
			   defaultValue: 0);
			migrationBuilder.AddColumn<DateTimeOffset>(
				name: "CreationDate",
				table: "TaskManagers",
				type: "datetimeoffset",
				nullable: false,
				defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
				name: "ItemsCount",
				table: "TaskManagers");

			migrationBuilder.DropColumn(
			   name: "CreatiDate",
			   table: "TaskManagers");
		}
    }
}
