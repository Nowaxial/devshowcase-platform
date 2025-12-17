using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevShowCase.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocationToModelEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Education",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Education");
        }
    }
}
