using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace action_elle_apk.Migrations
{
    /// <inheritdoc />
    public partial class creationTableSouscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Souscriptions",
                columns: table => new
                {
                    SouscriptionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DateMiseEnService = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumeroImmatriculation = table.Column<string>(type: "text", nullable: false),
                    Couleur = table.Column<string>(type: "text", nullable: false),
                    NombreSiege = table.Column<int>(type: "integer", nullable: false),
                    NombrePorte = table.Column<int>(type: "integer", nullable: false),
                    CategorieVehiculeId = table.Column<int>(type: "integer", nullable: false),
                    NomAssure = table.Column<string>(type: "text", nullable: false),
                    PrenomAssure = table.Column<string>(type: "text", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: false),
                    Adresse = table.Column<string>(type: "text", nullable: false),
                    Ville = table.Column<string>(type: "text", nullable: false),
                    NumeroCarteIdentite = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Souscriptions", x => x.SouscriptionId);
                    table.ForeignKey(
                        name: "FK_Souscriptions_CategoriesVehicules_CategorieVehiculeId",
                        column: x => x.CategorieVehiculeId,
                        principalTable: "CategoriesVehicules",
                        principalColumn: "CategorieVehiculeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Souscriptions_CategorieVehiculeId",
                table: "Souscriptions",
                column: "CategorieVehiculeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Souscriptions");
        }
    }
}
