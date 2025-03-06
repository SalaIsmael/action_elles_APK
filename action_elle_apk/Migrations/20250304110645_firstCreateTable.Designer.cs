﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using action_elle_apk.Data;

#nullable disable

namespace action_elle_apk.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250304110645_firstCreateTable")]
    partial class firstCreateTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("action_elle_apk.Models.CategorieEligible", b =>
                {
                    b.Property<int>("ProduitAssuranceId")
                        .HasColumnType("integer");

                    b.Property<int>("CategorieVehiculeId")
                        .HasColumnType("integer");

                    b.HasKey("ProduitAssuranceId", "CategorieVehiculeId");

                    b.HasIndex("CategorieVehiculeId");

                    b.ToTable("CategoriesEligibles");
                });

            modelBuilder.Entity("action_elle_apk.Models.CategorieVehicule", b =>
                {
                    b.Property<int>("CategorieVehiculeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategorieVehiculeId"));

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategorieVehiculeId");

                    b.ToTable("CategoriesVehicules");
                });

            modelBuilder.Entity("action_elle_apk.Models.Garantie", b =>
                {
                    b.Property<int>("GarantieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GarantieId"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("PrimeFixe")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("PrixPrimeFixe")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Taux")
                        .HasColumnType("numeric");

                    b.HasKey("GarantieId");

                    b.ToTable("Garanties");
                });

            modelBuilder.Entity("action_elle_apk.Models.GarantieIncluse", b =>
                {
                    b.Property<int>("ProduitAssuranceId")
                        .HasColumnType("integer");

                    b.Property<int>("GarantieId")
                        .HasColumnType("integer");

                    b.HasKey("ProduitAssuranceId", "GarantieId");

                    b.HasIndex("GarantieId");

                    b.ToTable("GarantiesIncluses");
                });

            modelBuilder.Entity("action_elle_apk.Models.ProduitAssurance", b =>
                {
                    b.Property<int>("ProduitAssuranceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProduitAssuranceId"));

                    b.Property<string>("NomProduitAssurance")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProduitAssuranceId");

                    b.ToTable("ProduitsAssurances");
                });

            modelBuilder.Entity("action_elle_apk.Models.Simulation", b =>
                {
                    b.Property<int>("SimulationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SimulationId"));

                    b.Property<DateTime>("DateMiseEnService")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("ProduitAssuranceId")
                        .HasColumnType("integer");

                    b.Property<int>("PuissanceVehicule")
                        .HasColumnType("integer");

                    b.Property<string>("QuoteReference")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("ValeurVehicule")
                        .HasColumnType("numeric");

                    b.HasKey("SimulationId");

                    b.HasIndex("ProduitAssuranceId");

                    b.ToTable("Simulations");
                });

            modelBuilder.Entity("action_elle_apk.Models.CategorieEligible", b =>
                {
                    b.HasOne("action_elle_apk.Models.CategorieVehicule", "CategorieVehicule")
                        .WithMany("ProduitsAssurance")
                        .HasForeignKey("CategorieVehiculeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("action_elle_apk.Models.ProduitAssurance", "ProduitAssurance")
                        .WithMany("CategorieVehicules")
                        .HasForeignKey("ProduitAssuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategorieVehicule");

                    b.Navigation("ProduitAssurance");
                });

            modelBuilder.Entity("action_elle_apk.Models.GarantieIncluse", b =>
                {
                    b.HasOne("action_elle_apk.Models.Garantie", "Garantie")
                        .WithMany("ProduitsAssurance")
                        .HasForeignKey("GarantieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("action_elle_apk.Models.ProduitAssurance", "ProduitAssurance")
                        .WithMany("Garanties")
                        .HasForeignKey("ProduitAssuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garantie");

                    b.Navigation("ProduitAssurance");
                });

            modelBuilder.Entity("action_elle_apk.Models.Simulation", b =>
                {
                    b.HasOne("action_elle_apk.Models.ProduitAssurance", "ProduitAssurance")
                        .WithMany()
                        .HasForeignKey("ProduitAssuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProduitAssurance");
                });

            modelBuilder.Entity("action_elle_apk.Models.CategorieVehicule", b =>
                {
                    b.Navigation("ProduitsAssurance");
                });

            modelBuilder.Entity("action_elle_apk.Models.Garantie", b =>
                {
                    b.Navigation("ProduitsAssurance");
                });

            modelBuilder.Entity("action_elle_apk.Models.ProduitAssurance", b =>
                {
                    b.Navigation("CategorieVehicules");

                    b.Navigation("Garanties");
                });
#pragma warning restore 612, 618
        }
    }
}
