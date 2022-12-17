using System;

namespace AnnonceAPI.Models
{
    public class Annonce
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Localisation { get; set; }
        public string TypeDeBien { get; set; }
        public string Statut { get; set; }
        public DateTime DateDeCreation { get; set; }
        public DateTime DateDePublication { get; set; }
        public string Weather { get; set; }
    }
}
