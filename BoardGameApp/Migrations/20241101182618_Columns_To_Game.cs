using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameApp.Migrations
{
    /// <inheritdoc />
    public partial class Columns_To_Game : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameType",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMine",
                table: "Game",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameType",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "IsMine",
                table: "Game");
        }
    }
}
