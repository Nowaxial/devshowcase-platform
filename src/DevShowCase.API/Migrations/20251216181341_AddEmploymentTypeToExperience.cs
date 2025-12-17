using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevShowCase.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEmploymentTypeToExperience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmploymentType",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "Experiences");
        }
    }
}
