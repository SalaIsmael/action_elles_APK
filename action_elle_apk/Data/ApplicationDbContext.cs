using action_elle_apk.Models;
using Microsoft.EntityFrameworkCore;


namespace action_elle_apk.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<Souscription> Souscriptions { get; set; }
        public DbSet<ProduitAssurance> ProduitsAssurances { get; set; }
        public DbSet<Garantie> Garanties { get; set; }
        public DbSet<CategorieVehicule> CategoriesVehicules { get; set; }
        public DbSet<GarantieIncluse> GarantiesIncluses { get; set; }
        public DbSet<CategorieEligible> CategoriesEligibles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<GarantieIncluse>()
                .HasKey(gi => new { gi.ProduitAssuranceId, gi.GarantieId });

            modelBuilder.Entity<CategorieEligible>()
                .HasKey(ce => new { ce.ProduitAssuranceId, ce.CategorieVehiculeId });
        }

        public async Task SeedDataAsync()
        {
            var produitsAssuranceData = new List<(string Nom, string[] Garanties)>
            {
                ("Papillon", new[] { "RC", "Dommages", "Vol" }),
                ("Douby", new[] { "RC", "Dommages", "Tierce Collision" }),
                ("Douyou", new[] { "RC", "Dommages", "Tierce Collision", "Incendie" }),
                ("Toutourisquou", new[] { "RC", "Dommages", "Vol", "Incendie", "Tierce Collision", "Tierce Plafonnée" })
            };

            foreach (var (nomProduit, garantiesRequises) in produitsAssuranceData)
            {
                if (!await ProduitsAssurances.AnyAsync(p => p.NomProduitAssurance == nomProduit))
                {
                    var produitAssurance = new ProduitAssurance
                    {
                        NomProduitAssurance = nomProduit,
                        Garanties = new List<GarantieIncluse>()
                    };

                    // 🔹 Vérifier et ajouter les garanties si elles n'existent pas déjà
                    var garanties = await Garanties.Where(g => garantiesRequises.Contains(g.Nom)).ToListAsync();

                    foreach (var garantieNom in garantiesRequises)
                    {
                        if (!garanties.Any(g => g.Nom == garantieNom))
                        {
                            garanties.Add(garantieNom switch
                            {
                                "RC" => new Garantie { Nom = "RC"},
                                "Dommages" => new Garantie { Nom = "Dommages" },
                                "Vol" => new Garantie { Nom = "Vol" },
                                "Incendie" => new Garantie { Nom = "Incendie" },
                                "Tierce Collision" => new Garantie { Nom = "Tierce Collision" },
                                "Tierce Plafonnée" => new Garantie { Nom = "Tierce Plafonnée" },
                                _ => null
                            });
                        }
                    }

                    // 🔹 Ajouter les nouvelles garanties en base
                    await Garanties.AddRangeAsync(garanties.Where(g => g.GarantieId == 0)); // Seules les garanties nouvelles sont ajoutées
                    await SaveChangesAsync();

                    // 🔹 Associer les garanties au produit
                    produitAssurance.Garanties = garanties.Select(g => new GarantieIncluse { Garantie = g }).ToList();

                    // 🔹 Ajouter le produit en base
                    await ProduitsAssurances.AddAsync(produitAssurance);
                    await SaveChangesAsync();
                }
            }

        }


        public async Task SeedCategoriesVehiculesAsync()
        {
            var categoriesVehiculesData = new List<(int Code, string Libelle, string Description)>
            {
                (201, "Promenade et Affaire", "Usage personnel"),
                (202, "Véhicules Motorisés à 2 ou 3 roues", "Motocycle, tricycles"),
                (203, "Transport public de voyage", "Véhicule transport de personnes"),
                (204, "Véhicule de transport avec taximètres", " Taxis")
            };

            foreach (var (code, libelle, description) in categoriesVehiculesData)
            {
                if (!await CategoriesVehicules.AnyAsync(c => c.Code == code))
                {
                    await CategoriesVehicules.AddAsync(new CategorieVehicule
                    {
                        Code = code,
                        Libelle = libelle,
                        Description = description
                    });
                }
            }
            await SaveChangesAsync();
        }


        public async Task SeedCategoriesEligiblesAsync()
        {
            var categoriesEligiblesData = new List<(string NomProduitAssurance, int[] CodesCategories)>
            {
                ("Papillon", new[] { 201 }),
                ("Douby", new[] { 202 }),
                ("Douyou", new[] { 201, 202 }),
                ("Toutourisquou", new[] { 201 })
            };

            foreach (var (nomProduit, codesCategories) in categoriesEligiblesData)
            {
                var produit = await ProduitsAssurances.FirstOrDefaultAsync(p => p.NomProduitAssurance == nomProduit);
                if (produit != null)
                {
                    foreach (var codeCategorie in codesCategories)
                    {
                        var categorie = await CategoriesVehicules.FirstOrDefaultAsync(c => c.Code == codeCategorie);
                        if (categorie != null)
                        {
                            if (!await CategoriesEligibles.AnyAsync(ce => ce.ProduitAssuranceId == produit.ProduitAssuranceId && ce.CategorieVehiculeId == categorie.CategorieVehiculeId))
                            {
                                await CategoriesEligibles.AddAsync(new CategorieEligible
                                {
                                    ProduitAssuranceId = produit.ProduitAssuranceId,
                                    CategorieVehiculeId = categorie.CategorieVehiculeId
                                });
                            }
                        }
                    }
                }
            }
            await SaveChangesAsync();
        }



    }
}
