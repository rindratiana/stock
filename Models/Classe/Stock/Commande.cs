using stock.Models.DAO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class Commande
    {
        private int idCommande;
        private string dateCommande;
        private string numero;
        private Comptoir comptoir;
        private int client;
        private string etat;

        public Commande() { }
        public Commande(int idCommande, string dateCommande, string numero, Comptoir comptoir, int client, string etat)
        {
            this.IdCommande = idCommande;
            this.DateCommande = dateCommande;
            this.Numero = numero;
            this.Comptoir = comptoir;
            this.Client = client;
            this.Etat = etat;
        }

        public int IdCommande { get => idCommande; set => idCommande = value; }
        public string DateCommande { get => dateCommande; set => dateCommande = value; }
        public string Numero { get => numero; set => numero = value; }
        public Comptoir Comptoir { get => comptoir; set => comptoir = value; }
        public int Client { get => client; set => client = value; }
        public string Etat { get => etat; set => etat = value; }

        public List<Commande> GetListeCommandeEnCours(Comptoir comptoir)
        {
            CommandeDAO commandeDAO = new CommandeDAO();
            List<Commande> reponse = new List<Commande>();
            try
            {
                reponse  = commandeDAO.GetListeCommandeEnCours(comptoir,"100");
                //Si validation est au niveau stock
                if (reponse.Count == 0)
                {
                    reponse = commandeDAO.GetListeCommandeEnCours(comptoir, "110");
                }
                return reponse;
            }
            catch(Exception execption)
            {
                throw execption;
            }
        }
        public void CreateCommande(List<DetailCommande> detailCommandes, string numero_ticket)
        {
            AccesSageDAO accesSage = new AccesSageDAO();
            CommandeDAO commandeDao = new CommandeDAO();
            try
            {
                //Comptoir par numéro_ticket
                Comptoir comptoir = accesSage.GetComptoirByNumTicket(numero_ticket);

                DateTime dateNow = DateTime.Now;
                string date = dateNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (existeCommande(numero_ticket)!=true)
                {
                    Commande commande = new Commande();
                    commande.DateCommande = date;
                    commande.Numero = numero_ticket;
                    commande.Comptoir = comptoir;
                    commande.Client = this.client;
                    commande.Etat = "100";
                    commandeDao.createCommande(commande);

                    string idCommande = commandeDao.GetIdCommande(numero_ticket);
                    for(int i = 0; i<detailCommandes.Count; i++)
                    {
                        detailCommandes[i].IdCommande = Int32.Parse(idCommande);
                    }
                    commandeDao.createDetailsCommande(detailCommandes);
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public int ParseInt(string client,string message)
        {
            try
            {
                return Int32.Parse(client);
            }
            catch(Exception exeption)
            {
                throw new Exception(message);            
            }
        }

        private Boolean existeCommande(string numero_ticket)
        {
            CommandeDAO commandeDao = new CommandeDAO();
            try
            {
                return commandeDao.testExistance(numero_ticket);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}