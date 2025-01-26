using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTB.Utulek.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToAdoptionApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasOtherAnimals",
                table: "AdoptionApplications",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasYardSpace",
                table: "AdoptionApplications",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserComment",
                table: "AdoptionApplications",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasOtherAnimals",
                table: "AdoptionApplications");

            migrationBuilder.DropColumn(
                name: "HasYardSpace",
                table: "AdoptionApplications");

            migrationBuilder.DropColumn(
                name: "UserComment",
                table: "AdoptionApplications");
        }
    }
}
