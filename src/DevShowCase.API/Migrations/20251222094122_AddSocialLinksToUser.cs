using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevShowCase.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSocialLinksToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "UserPreferences");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "UserPreferences",
                newName: "GoogleAnalyticsId");

            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "ContactMessages",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SenderEmail",
                table: "ContactMessages",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ContactMessages",
                newName: "SentDate");

            migrationBuilder.RenameIndex(
                name: "IX_ContactMessages_UserId_CreatedAt",
                table: "ContactMessages",
                newName: "IX_ContactMessages_UserId_SentDate");

            migrationBuilder.AddColumn<bool>(
                name: "ShowEmailPublicly",
                table: "UserPreferences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowResumeDownload",
                table: "UserPreferences",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomInstructions",
                table: "UserAISettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableAIRewriting",
                table: "UserAISettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PreferredTone",
                table: "UserAISettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GithubUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowEmailPublicly",
                table: "UserPreferences");

            migrationBuilder.DropColumn(
                name: "ShowResumeDownload",
                table: "UserPreferences");

            migrationBuilder.DropColumn(
                name: "CustomInstructions",
                table: "UserAISettings");

            migrationBuilder.DropColumn(
                name: "EnableAIRewriting",
                table: "UserAISettings");

            migrationBuilder.DropColumn(
                name: "PreferredTone",
                table: "UserAISettings");

            migrationBuilder.DropColumn(
                name: "GithubUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedInUrl",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "GoogleAnalyticsId",
                table: "UserPreferences",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "SentDate",
                table: "ContactMessages",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ContactMessages",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "ContactMessages",
                newName: "SenderEmail");

            migrationBuilder.RenameIndex(
                name: "IX_ContactMessages_UserId_SentDate",
                table: "ContactMessages",
                newName: "IX_ContactMessages_UserId_CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "UserPreferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
