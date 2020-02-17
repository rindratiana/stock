using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class Duree
    {
        private Commande commande;
        private DateTime heureCommande;
        private DateTime heureSortie;
        private DateTime heureLivraison;

        public Duree(){}

        public Duree(Commande commande, DateTime heureCommande, DateTime heureSortie, DateTime heureLivraison)
        {
            this.Commande = commande;
            this.HeureCommande = heureCommande;
            this.HeureSortie = heureSortie;
            this.HeureLivraison = heureLivraison;
        }

        public Commande Commande { get => commande; set => commande = value; }
        public DateTime HeureCommande { get => heureCommande; set => heureCommande = value; }
        public DateTime HeureSortie { get => heureSortie; set => heureSortie = value; }
        public DateTime HeureLivraison { get => heureLivraison; set => heureLivraison = value; }
    }
}