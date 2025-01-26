using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTB.Utulek.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompletionFieldsToVolunteerSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletionReport",
                table: "VolunteerSchedules",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "VolunteerSchedules",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionReport",
                table: "VolunteerSchedules");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "VolunteerSchedules");
        }
    }
}
