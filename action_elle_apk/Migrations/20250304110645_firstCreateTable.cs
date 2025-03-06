using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace action_elle_apk.Migrations
{
    /// <inheritdoc />
    public partial class firstCreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriesVehicules",
                columns: table => new
                {
                    CategorieVehiculeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Libelle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesVehicules", x => x.CategorieVehiculeId);
                });

            migrationBuilder.CreateTable(
                name: "Garanties",
                columns: table => new
                {
                    GarantieId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    PrimeFixe = table.Column<bool>(type: "boolean", nullable: false),
                    PrixPrimeFixe = table.Column<decimal>(type: "numeric", nullable: true),
                    Taux = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garanties", x => x.GarantieId);
                });

            migrationBuilder.CreateTable(
                name: "ProduitsAssurances",
                columns: table => new
                {
                    ProduitAssuranceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomProduitAssurance = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitsAssurances", x => x.ProduitAssuranceId);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesEligibles",
                columns: table => new
                {
                    CategorieVehiculeId = table.Column<int>(type: "integer", nullable: false),
                    ProduitAssuranceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesEligibles", x => new { x.ProduitAssuranceId, x.CategorieVehiculeId });
                    table.ForeignKey(
                        name: "FK_CategoriesEligibles_CategoriesVehicules_CategorieVehiculeId",
                        column: x => x.CategorieVehiculeId,
                        principalTable: "CategoriesVehicules",
                        principalColumn: "CategorieVehiculeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriesEligibles_ProduitsAssurances_ProduitAssuranceId",
                        column: x => x.ProduitAssuranceId,
                        principalTable: "ProduitsAssurances",
                        principalColumn: "ProduitAssuranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarantiesIncluses",
                columns: table => new
                {
                    GarantieId = table.Column<int>(type: "integer", nullable: false),
                    ProduitAssuranceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarantiesIncluses", x => new { x.ProduitAssuranceId, x.GarantieId });
                    table.ForeignKey(
                        name: "FK_GarantiesIncluses_Garanties_GarantieId",
                        column: x => x.GarantieId,
                        principalTable: "Garanties",
                        principalColumn: "GarantieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarantiesIncluses_ProduitsAssurances_ProduitAssuranceId",
                        column: x => x.ProduitAssuranceId,
                        principalTable: "ProduitsAssurances",
                        principalColumn: "ProduitAssuranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Simulations",
                columns: table => new
                {
                    SimulationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuoteReference = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateMiseEnService = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PuissanceVehicule = table.Column<int>(type: "integer", nullable: false),
                    ValeurVehicule = table.Column<decimal>(type: "numeric", nullable: false),
                    ProduitAssuranceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.SimulationId);
                    table.ForeignKey(
                        name: "FK_Simulations_ProduitsAssurances_ProduitAssuranceId",
                        column: x => x.ProduitAssuranceId,
                        principalTable: "ProduitsAssurances",
                        principalColumn: "ProduitAssuranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesEligibles_CategorieVehiculeId",
                table: "CategoriesEligibles",
                column: "CategorieVehiculeId");

            migrationBuilder.CreateIndex(
                name: "IX_GarantiesIncluses_GarantieId",
                table: "GarantiesIncluses",
                column: "GarantieId");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_ProduitAssuranceId",
                table: "Simulations",
                column: "ProduitAssuranceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriesEligibles");

            migrationBuilder.DropTable(
                name: "GarantiesIncluses");

            migrationBuilder.DropTable(
                name: "Simulations");

            migrationBuilder.DropTable(
                name: "CategoriesVehicules");

            migrationBuilder.DropTable(
                name: "Garanties");

            migrationBuilder.DropTable(
                name: "ProduitsAssurances");
        }
    }
}
