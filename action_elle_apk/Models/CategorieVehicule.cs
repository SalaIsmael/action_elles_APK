namespace action_elle_apk.Models
{
    public class CategorieVehicule
    {
        public int CategorieVehiculeId { get; set; }
        public int Code { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<CategorieEligible> ProduitsAssurance { get; set; } = new();
    }
}
