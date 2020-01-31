using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class Commande
    {
        private int idCommande;
        private DateTime dateCommande;
        private string numero;
        private Utilisateur utilisateur;
        private Comptoir comptoir;
        private int client;
        private string etat;

        public Commande() { }
        public Commande(int idCommande, DateTime dateCommande, string numero, Utilisateur utilisateur, Comptoir comptoir, int client, string etat)
        {
            this.IdCommande = idCommande;
            this.DateCommande = dateCommande;
            this.Numero = numero;
            this.Utilisateur = utilisateur;
            this.Comptoir = comptoir;
            this.Client = client;
            this.Etat = etat;
        }

        public int IdCommande { get => idCommande; set => idCommande = value; }
        public DateTime DateCommande { get => dateCommande; set => dateCommande = value; }
        public string Numero { get => numero; set => numero = value; }
        public Utilisateur Utilisateur { get => utilisateur; set => utilisateur = value; }
        public Comptoir Comptoir { get => comptoir; set => comptoir = value; }
        public int Client { get => client; set => client = value; }
        public string Etat { get => etat; set => etat = value; }
    }
}