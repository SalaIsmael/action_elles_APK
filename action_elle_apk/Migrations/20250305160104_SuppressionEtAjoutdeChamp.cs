using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace action_elle_apk.Migrations
{
    /// <inheritdoc />
    public partial class SuppressionEtAjoutdeChamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimeFixe",
                table: "Garanties");

            migrationBuilder.DropColumn(
                name: "PrixPrimeFixe",
                table: "Garanties");

            migrationBuilder.DropColumn(
                name: "Taux",
                table: "Garanties");

            migrationBuilder.AddColumn<decimal>(
                name: "ValeurActuelleVehicule",
                table: "Simulations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValeurActuelleVehicule",
                table: "Simulations");

            migrationBuilder.AddColumn<bool>(
                name: "PrimeFixe",
                table: "Garanties",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PrixPrimeFixe",
                table: "Garanties",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Taux",
                table: "Garanties",
                type: "numeric",
                nullable: true);
        }
    }
}
