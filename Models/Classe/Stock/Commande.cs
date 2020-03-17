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
        private List<DetailCommande> listeDetailCommande;
        private Duree duree;
        private SortieCommande sortieCommande;

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

        public List<Commande> GetListeCommande(string dateDebut, string dateFin)
        {
            CommandeDAO commandeDAO = new CommandeDAO();
            try
            {
                return commandeDAO.GetListeCommande(dateDebut,dateFin);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public List<Commande> GetListeCommandeAnnule(string dateDebut, string dateFin)
        {
            CommandeDAO commandeDAO = new CommandeDAO();
            try
            {
                return commandeDAO.GetListeCommandeAnnule(dateDebut, dateFin);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public List<DetailCommande> ListeDetailCommande { get => listeDetailCommande; set => listeDetailCommande = value; }
        public Duree Duree { get => duree; set => duree = value; }
        public SortieCommande SortieCommande { get => sortieCommande; set => sortieCommande = value; }

        public List<DetailCommande> GetArticlesCommandesStock(string numerocomplete)
        {
            AccesSageDAO accesSageDAO = new AccesSageDAO();
            CommandeDAO commandeDAO = new CommandeDAO();
            List<DetailCommande> listeArticleSage = accesSageDAO.GetArticlesCommandes(numerocomplete);
            List<DetailCommande> listeArticleStock = commandeDAO.GetArticlesCommandes(numerocomplete);
            try
            {
                List<DetailCommande> reponse= new List<DetailCommande>();
                for(int i = 0; i < listeArticleSage.Count; i++)
                {
                    DetailCommande temp = listeArticleStock.Find(x => x.Article.References.Contains(listeArticleSage[i].Article.References));
                    if (temp != null)
                    {
                        listeArticleSage[i].Comptoir = accesSageDAO.GetComptoirByNumTicket(listeArticleSage[i].Numero);
                        listeArticleSage[i].Quantite = temp.Quantite;
                        reponse.Add(listeArticleSage[i]);
                    }
                }
                return reponse;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public Boolean TestExistence(string num_ticket)
        {
            try
            {
                CommandeDAO commandeDAO = new CommandeDAO();
                return commandeDAO.testExistence(num_ticket);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Annuler(string id_commande)
        {
            try
            {
                CommandeDAO commandeDAO = new CommandeDAO();
                commandeDAO.Annuler(id_commande);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Sortie(string id_commande,string id_magasinier,string id_binome)
        {
            try
            {
                CommandeDAO commandeDAO = new CommandeDAO();
                commandeDAO.Sortie(id_commande, id_magasinier, id_binome);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public Commande Valider(string id_commande)
        {
            try
            {
                CommandeDAO commandeDAO = new CommandeDAO();
                Commande commande = commandeDAO.GetCommandeById(id_commande);
                if(commande.Etat != "110")
                {
                    return commande;
                }
                else { 
                    commandeDAO.Valider(id_commande);
                    return commande;
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public List<Commande> GetListeToutCommandeEnCours()
        {
            CommandeDAO commandeDAO = new CommandeDAO();
            List<Commande> reponse = new List<Commande>();
            try
            {
                reponse = commandeDAO.GetListeToutCommandeEnCours("100");
                for(int i = 0; i < reponse.Count; i++)
                {
                    List<DetailCommande> detailCommande = GetArticlesCommandesStock(reponse[i].Numero);
                    reponse[i].ListeDetailCommande = detailCommande;
                }
                return reponse;
            }
            catch (Exception execption)
            {
                throw execption;
            }
        }
        public List<Commande> GetListeCommandeEnCours(Comptoir comptoir)
        {
            CommandeDAO commandeDAO = new CommandeDAO();
            List<Commande> reponse = new List<Commande>();
            try
            {
                reponse  = commandeDAO.GetListeCommandeEnCours(comptoir,"100");
                List<Commande> commandeValiderStock = commandeDAO.GetListeCommandeEnCours(comptoir, "110");
                for(int i = 0; i < commandeValiderStock.Count; i++)
                {
                    reponse.Add(commandeValiderStock[i]);
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
                if (detailCommandes.Count == 0)
                {
                    throw new Exception("Veuillez choisir un article svp");
                }
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
                    commandeDao.createDetailsCommande(detailCommandes,idCommande);
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
                return commandeDao.testExistence(numero_ticket);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}