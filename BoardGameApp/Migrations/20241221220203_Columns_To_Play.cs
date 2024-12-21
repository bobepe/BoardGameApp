using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameApp.Migrations
{
    /// <inheritdoc />
    public partial class Columns_To_Play : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Play",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSolo",
                table: "Play",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Play",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Play");

            migrationBuilder.DropColumn(
                name: "IsSolo",
                table: "Play");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Play");
        }
    }
}
