namespace action_elle_apk.Models
{
    public class Vehicule
    {
        public int VehiculeId { get; set; }
        public string NumeroImmatri { get; set; }
        public DateTime DateMiseEnService { get; set; }
        public int NombreSiege { get; set; }
        public int NombrePorte { get; set; }
        public string Couleur { get; set; }
        public int CategorieVehiculeId { get; set; }
        public CategorieVehicule Categorie { get; set; }
    }
}
