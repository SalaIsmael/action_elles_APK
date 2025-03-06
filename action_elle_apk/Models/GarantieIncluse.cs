namespace action_elle_apk.Models
{
    public class GarantieIncluse
    {
        public int GarantieId { get; set; }
        public Garantie Garantie { get; set; } = null!;

        public int ProduitAssuranceId { get; set; }
        public ProduitAssurance ProduitAssurance { get; set; } = null!;
    }
}
