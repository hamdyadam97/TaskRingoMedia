using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RingoMedia.Context.Migrations
{
    /// <inheritdoc />
    public partial class mail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mail",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mail",
                table: "Reminders");
        }
    }
}
