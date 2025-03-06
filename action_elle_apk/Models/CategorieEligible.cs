namespace action_elle_apk.Models
{
    public class CategorieEligible
    {
        public int CategorieVehiculeId { get; set; }
        public CategorieVehicule CategorieVehicule { get; set; } = null!;

        public int ProduitAssuranceId { get; set; }
        public ProduitAssurance ProduitAssurance { get; set; } = null!;
    }
}
