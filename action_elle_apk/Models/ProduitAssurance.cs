namespace action_elle_apk.Models
{
    public class ProduitAssurance
    {
        public int ProduitAssuranceId { get; set; }
        public string NomProduitAssurance { get; set; } = string.Empty;
        // Garanties incluses
        public List<GarantieIncluse> Garanties { get; set; } = new();

        // Catégories de véhicules éligibles
        public List<CategorieEligible> CategorieVehicules { get; set; } = new();
    }
}
