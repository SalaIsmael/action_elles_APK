namespace action_elle_apk.Models
{
    public class Souscription
    {
        public int SouscriptionId { get; set; }
        public string Status { get; set; } = "en attente";

        public DateTime DateMiseEnService { get; set; }
        public string NumeroImmat { get; set; }
        public string Couleur { get; set; }
        public int NombreSiege { get; set; }
        public int NombrePorte { get; set; }
        public int CategorieVehiculeId { get; set; }
        public CategorieVehicule CategorieVehicule { get; set; }


        public string NomAssure { get; set; }
        public string PrenomAssure { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string NumeroIdentite { get; set; }
    }
}
