namespace action_elle_apk.Models
{
    public class Garantie
    {
        public int GarantieId { get; set; }
        public string Nom { get; set; } = string.Empty;
        public List<GarantieIncluse> ProduitsAssurance { get; set; } = new();
    }
}
