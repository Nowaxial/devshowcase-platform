using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevShowCase.API.Migrations
{
    /// <inheritdoc />
    public partial class AddThemeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewImageUrl",
                table: "Themes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SupportsDarkMode",
                table: "Themes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TargetAudience",
                table: "Themes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewImageUrl",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "SupportsDarkMode",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "TargetAudience",
                table: "Themes");
        }
    }
}
