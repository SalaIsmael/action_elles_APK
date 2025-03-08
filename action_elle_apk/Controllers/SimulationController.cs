using action_elle_apk.Data;
using action_elle_apk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace action_elle_apk.Controllers
{
    [Route("api/v1/simulations")]
    [ApiController]
    public class SimulationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SimulationsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // On Récupère toutes les simulations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Simulation>>> GetSimulations()
        {
            return await _context.Simulations.ToListAsync();
        }

        // On Récupère une simulation par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Simulation>> GetSimulation(int id)
        {
            var simulation = await _context.Simulations.FindAsync(id);
            if (simulation == null) return NotFound();
            return simulation;
        }

        // On Crée une simulation avec calcul de la prime
        [HttpPost]
        public async Task<ActionResult<Simulation>> CreateSimulation(Simulation simulation)
        {
            // On Vérifie que le produit d’assurance existe et inclut les garanties associées
            var produit = await _context.ProduitsAssurances
                .Include(p => p.Garanties)
                .ThenInclude(g => g.Garantie)
                .FirstOrDefaultAsync(p => p.ProduitAssuranceId == simulation.ProduitAssuranceId);

            if (produit == null)
                return NotFound("Produit d'assurance introuvable.");

            // On Associe le produit récupéré à la simulation
            simulation.ProduitAssurance = produit;


            //On réccupère les données pour la simulation 
            simulation.QuoteReference = "QT" + Guid.NewGuid().ToString().Substring(0, 12).ToUpper();
            simulation.EndDate = DateTime.SpecifyKind(DateTime.UtcNow.AddDays(14), DateTimeKind.Utc);
            //simulation.DateMiseEnService = DateTime.SpecifyKind(simulation.DateMiseEnService, DateTimeKind.Utc);
            
            
            simulation.Price = CalculerPrime(simulation);

            _context.Simulations.Add(simulation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSimulation), new { id = simulation.SimulationId }, simulation);
        }

        //fonction de calcul de la prime en  fonction des contraintes
        private decimal CalculerPrime(Simulation simulation)
        {
            decimal primeDommages = 0;
            decimal primeTierceCollision = 0;
            decimal primeTiercePlafonnee = 0;
            decimal primeVol = 0;
            decimal primeIncendie = 0;

            // Calcul de la prime Garantie Responsabilité Civile
            decimal primeRC = simulation.PuissanceVehicule switch
            {
                2 => 37601,
                >= 3 and <= 6 => 45181,
                >= 7 and <= 10 => 51078,
                >= 11 and <= 14 => 65677,
                >= 15 and <= 23 => 86456,
                _ => 104143
            };
            if (simulation?.ProduitAssurance?.Garanties != null && simulation.ProduitAssurance.Garanties.Any())
            {
                //Vérification des garanties sélectionnées
                foreach (var garantieIncluse in simulation.ProduitAssurance.Garanties)
                {
                    var garantie = garantieIncluse.Garantie;
                    simulation.DateMiseEnService = DateTime.SpecifyKind(simulation.DateMiseEnService, DateTimeKind.Utc);
                    var nom = garantie.Nom;
                    
                    if (garantie.Nom == "Dommages" && AgeVehicule(simulation.DateMiseEnService) <= 5)
                    {
                        primeDommages = simulation.ValeurVehicule * 0.026m;
                    }

                    
                    if (garantie.Nom == "Tierce Collision" && AgeVehicule(simulation.DateMiseEnService) <= 8)
                    {
                        primeTierceCollision = simulation.ValeurVehicule * 0.0165m;
                    }

                    
                    if (garantie.Nom == "Tierce Plafonnée" && AgeVehicule(simulation.DateMiseEnService) <= 10)
                    {
                        decimal valeurAssuree = simulation.ValeurActuelleVehicule * 0.5m; 
                        primeTiercePlafonnee = Math.Max(valeurAssuree * 0.042m, 100000); 
                    }

                   
                    if (garantie.Nom == "Vol")
                    {
                        primeVol = simulation.ValeurActuelleVehicule * 0.0014m;
                    }

                    if (garantie.Nom == "Incendie")
                    {
                        primeIncendie = simulation.ValeurActuelleVehicule * 0.0015m;
                    }
                }

            }

            else
            {
                
                Console.WriteLine("Aucune garantie");
            }

           
            decimal primeTotale = primeRC + primeDommages + primeTierceCollision + primeTiercePlafonnee + primeVol + primeIncendie;
            return primeTotale;
        }

        // Méthode pour calculer l'âge du véhicule
        private int AgeVehicule(DateTime dateMiseEnService)
        {
            return DateTime.UtcNow.Year - dateMiseEnService.Year;
        }

    }

}
