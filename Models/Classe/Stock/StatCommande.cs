using stock.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class StatCommande
    {
        private Commande commande;
        private int nombre;
        public StatCommande() { }

        public StatCommande(Commande commande, int nombre)
        {
            this.Commande = commande;
            this.Nombre = nombre;
        }
        public StatCommande GetStatCommandesMouvement(string dateDebut, string dateFin)
        {
            try
            {
                CommandeDAO commandeDAO = new CommandeDAO();
                StatCommande reponse = commandeDAO.GetStatCommandesMouvement(dateDebut, dateFin);
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public StatCommande GetStatCommandesAnnule(string dateDebut,string dateFin)
        {
            try
            {
                CommandeDAO commandeDAO = new CommandeDAO();
                StatCommande reponse = commandeDAO.GetStatCommandesAnnule(dateDebut,dateFin);
                return reponse;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public Commande Commande { get => commande; set => commande = value; }
        public int Nombre { get => nombre; set => nombre = value; }
    }
}