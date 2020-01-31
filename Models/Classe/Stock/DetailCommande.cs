using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class DetailCommande : Commande
    {
        private Article article;
        private int quantite;
        private int emplacement;
        public DetailCommande() { }

        public DetailCommande(Article article, int quantite, int emplacement)
        {
            this.Article = article;
            this.Quantite = quantite;
            this.Emplacement = emplacement;
        }

        public Article Article { get => article; set => article = value; }
        public int Quantite { get => quantite; set => quantite = value; }
        public int Emplacement { get => emplacement; set => emplacement = value; }
    }
}