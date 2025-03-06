namespace action_elle_apk.Models
{
    public class Simulation
    {
        public int SimulationId { get; set; }
        public string QuoteReference { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateMiseEnService { get; set; }
        public int PuissanceVehicule { get; set; }
        public decimal ValeurVehicule { get; set; }
        public decimal ValeurActuelleVehicule { get; set; }
        public int ProduitAssuranceId { get; set; }
        public ProduitAssurance? ProduitAssurance { get; set; }
    }
}
