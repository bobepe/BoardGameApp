using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameApp.Migrations
{
    /// <inheritdoc />
    public partial class PlayPlayerColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayPlayer",
                table: "PlayPlayer");

            migrationBuilder.DropColumn(
                name: "IsWinner",
                table: "PlayPlayer");

            migrationBuilder.AlterColumn<double>(
                name: "Score",
                table: "PlayPlayer",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "PlayPlayer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "PlayPlayer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayPlayer",
                table: "PlayPlayer",
                columns: new[] { "PlayId", "PlayerId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayPlayer",
                table: "PlayPlayer");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "PlayPlayer");

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "PlayPlayer",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "PlayPlayer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWinner",
                table: "PlayPlayer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayPlayer",
                table: "PlayPlayer",
                columns: new[] { "PlayId", "PlayerId", "RoleId" });
        }
    }
}
