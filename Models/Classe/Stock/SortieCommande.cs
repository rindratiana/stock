using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class SortieCommande
    {
        private string idSortieCommande;
        private Commande commande;
        private Utilisateur binome;
        private Utilisateur magasinier;
public SortieCommande(string idSortieCommande, Commande commande, Utilisateur binome, Utilisateur magasinier)
        {
            this.IdSortieCommande = idSortieCommande;
            this.Commande = commande;
            this.Binome = binome;
            this.Magasinier = magasinier;
        }

        public string IdSortieCommande { get => idSortieCommande; set => idSortieCommande = value; }
        public Commande Commande { get => commande; set => commande = value; }
        public Utilisateur Binome { get => binome; set => binome = value; }
        public Utilisateur Magasinier { get => magasinier; set => magasinier = value; }
    }
}