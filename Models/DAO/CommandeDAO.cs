using MySql.Data.MySqlClient;
using stock.Models.Classe;
using stock.Models.Classe.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.DAO
{
    public class CommandeDAO
    {
        public CommandeDAO() { }
        public StatCommande GetStatCommandesMouvement(string dateDebut, string dateFin)
        {
            Connexion connexion = new Connexion();
            string query = "";
            if (dateDebut == "" && dateFin == "")
            {
                query = "SELECT count(*) as total FROM commande where ETAT='111' and client='1'";
            }
            else
            {
                query = "SELECT count(*) as total FROM commande where ETAT='111' and date_commande<='" + dateFin + "' and date_commande>='" + dateDebut + "' and client='1'";
            }
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            AccesSageDAO accesSageDAO = new AccesSageDAO();
            MySqlDataReader dataReader;
            //Creation d'une liste
            StatCommande reponse = new StatCommande();
            try
            {
                //Ouverture connexion
                if (connexion.OpenConnection() == true)
                {
                    //Excecution de la commande
                    dataReader = command.ExecuteReader();

                    //Lecture des donnees et stockage dans la liste
                    while (dataReader.Read())
                    {
                        StatCommande stat = new StatCommande();
                        stat.Nombre = Int32.Parse(dataReader["total"].ToString());
                        reponse = stat;
                    }
                    dataReader.Close();
                }
                //return
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                connexion.CloseAll(command, null, connexion.GetConnection());
            }
        }
        public StatCommande GetStatCommandesAnnule(string dateDebut, string dateFin)
        {
            Connexion connexion = new Connexion();
            string query = "";
            if(dateDebut=="" && dateFin == "") { 
                query = "SELECT count(*) as total FROM commande where ETAT='000' and client='1'";
            }
            else
            {
                query = "SELECT count(*) as total FROM commande where ETAT='000' and date_commande<='"+dateFin+"' and date_commande>='"+dateDebut+"' and client='1'";
            }
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            AccesSageDAO accesSageDAO = new AccesSageDAO();
            MySqlDataReader dataReader;
            //Creation d'une liste
            StatCommande reponse = new StatCommande();
            try
            {
                //Ouverture connexion
                if (connexion.OpenConnection() == true)
                {
                    //Excecution de la commande
                    dataReader = command.ExecuteReader();

                    //Lecture des donnees et stockage dans la liste
                    while (dataReader.Read())
                    {
                        StatCommande stat = new StatCommande();
                        stat.Nombre = Int32.Parse(dataReader["total"].ToString());
                        reponse = stat;
                    }
                    dataReader.Close();
                }
                //return
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                connexion.CloseAll(command, null, connexion.GetConnection());
            }
        }
        public List<DetailCommande> GetArticlesCommandes(string numerocomplete){
            Connexion connexion = new Connexion();
            string query = "select commande.numero, detail_commande.reference_article from detail_commande join commande on commande.id_commande = detail_commande.id_commande where commande.numero='"+numerocomplete+"'";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            MySqlDataReader dataReader;
            //Creation d'une liste
            List<DetailCommande> reponse = new List<DetailCommande>();
            try
            {
                //Ouverture connexion
                if (connexion.OpenConnection() == true)
                {
                    //Excecution de la commande
                    dataReader = command.ExecuteReader();

                    //Lecture des donnees et stockage dans la liste
                    while (dataReader.Read())
                    {
                        DetailCommande detailCommande = new DetailCommande();
                        detailCommande.Numero = dataReader["numero"].ToString();
                        Article article = new Article();
                        article.References = dataReader["reference_article"].ToString();
                        detailCommande.Article = article;
                        reponse.Add(detailCommande);
                    }
                    dataReader.Close();
                }
                //return
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connexion.GetConnection() != null)
                {
                    connexion.GetConnection().Close();
                }
            }
        }
        public void AnnulerCommande(MySqlCommand command, string id_commande)
        {
            try
            {
                string query = "UPDATE COMMANDE SET ETAT = '000' where ID_COMMANDE = '" + id_commande + "'";
                //create command and assign the query and connection from the constructor
                command.CommandText = query;
                //Execute command
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Annuler(string id_commande)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            try
            {
                this.AnnulerCommande(command, id_commande);
                if (transaction != null)
                {
                    transaction.Commit();
                }

            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
            finally
            {
                connexion.CloseAll(command, transaction, connection);
            }
        }
        public void ValiderCommande(MySqlCommand command,string id_commande)
        {
            try
            {
                string query = "UPDATE COMMANDE SET ETAT = '111' where ID_COMMANDE = '"+id_commande+"'";
                //create command and assign the query and connection from the constructor
                command.CommandText = query;
                //Execute command
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void SortieStock(MySqlCommand command,string id_magasinier,string id_binome,string numero_ticket)
        {
            string idcommande = this.GetIdCommande(numero_ticket);
            try
            {
                string query = "INSERT INTO SORTIE (ID_COMMANDE,ID_BINOME,ID_MAGASINIER) values('"+idcommande+"','"+id_binome+"','"+id_magasinier+"')";
                //create command and assign the query and connection from the constructor
                command.CommandText = query;
                //Execute command
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Sortie(string numero_ticket,string id_magasinier,string id_binome)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            
            Duree duree = new Duree();
            string idcommande = this.GetIdCommande(numero_ticket);
            Commande commande = new Commande();
            commande.IdCommande = Int32.Parse(idcommande);
            duree.Commande = commande;
            duree.HeureSortie = DateTime.Now;
            DureeDAO dureeDAO = new DureeDAO();
            try
            {
                this.SortieCommande(command, numero_ticket);
                this.SortieStock(command, id_magasinier, id_binome,numero_ticket);
                dureeDAO.UpdateDuree(command, duree, "SORTIE");
                if (transaction != null)
                {
                    transaction.Commit();
                }

            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
            finally
            {
                connexion.CloseAll(command, transaction, connection);
            }
        }
        public void SortieCommande(MySqlCommand command, string numero_ticket)
        {
            try
            {
                string query = "UPDATE COMMANDE SET ETAT = '110' where numero = '" + numero_ticket + "'";
                //create command and assign the query and connection from the constructor
                command.CommandText = query;
                //Execute command
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Valider(string id_commande)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;

            Duree duree = new Duree();
            Commande commande = new Commande();
            commande.IdCommande = Int32.Parse(id_commande);
            duree.Commande = commande;
            duree.HeureLivraison = DateTime.Now;
            DureeDAO dureeDAO = new DureeDAO();

            try
            {
                this.ValiderCommande(command, id_commande);
                dureeDAO.UpdateDuree(command, duree, "LIVRAISON");
                if (transaction != null)
                {
                    transaction.Commit();
                }

            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
            finally
            {
                connexion.CloseAll(command, transaction, connection);
            }
        }
        public Boolean testExistence(string numero_ticket)
        {
            Connexion connexion = new Connexion();
            string query = "SELECT id_commande FROM commande where numero='" + numero_ticket + "'";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            //Creation d'une liste
            List<string> liste= new List<string>();
            try
            {
                //Ouverture connexion
                if (connexion.OpenConnection() == true)
                {
                    //Excecution de la commande
                    MySqlDataReader dataReader = command.ExecuteReader();

                    //Lecture des donnees et stockage dans la liste
                    while (dataReader.Read())
                    {
                        liste.Add(dataReader["id_commande"].ToString());
                    }

                    //fermeture du Data Reader
                    dataReader.Close();
                }
                //return
                if (liste.Count == 0)
                {
                    return false;
                }
                else
                {
                    throw new Exception("Ce ticket a été déjà utilisé");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connexion.GetConnection() != null)
                {
                    connexion.GetConnection().Close();
                }
            }
        }

        public string getDernierNumeroTicket()
        {
            Connexion connexion = new Connexion();
            string query = "SELECT NUMERO FROM COMMANDE WHERE ID_COMMANDE = (SELECT MAX(ID_COMMANDE) FROM COMMANDE)";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            //Creation d'une liste
            string reponse = "";
            try
            {
                MySqlConnection connection = connexion.GetConnection();
                //Ouverture connexion
                if (connexion.OpenConnection() == true)
                {
                    //Excecution de la commande
                    MySqlDataReader dataReader = command.ExecuteReader();

                    //Lecture des donnees et stockage dans la liste
                    while (dataReader.Read())
                    {
                        reponse = dataReader["NUMERO"].ToString();
                    }
                    //fermeture du Data Reader
                    dataReader.Close();
                }
                //return
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connexion.GetConnection() != null)
                {
                    connexion.GetConnection().Close();
                }
            }
        }
        public Commande GetCommandeById(string id_commande)
        {
            Connexion connexion = new Connexion();
            string query = "SELECT * FROM commande where ID_COMMANDE='" + id_commande + "'";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            AccesSageDAO accesSageDAO = new AccesSageDAO();
            MySqlDataReader dataReader;
            //Creation d'une liste
            Commande reponse = new Commande();
            try
            {
                //Ouverture connexion
                if (connexion.OpenConnection() == true)
                {
                    //Excecution de la commande
                    dataReader = command.ExecuteReader();

                    //Lecture des donnees et stockage dans la liste
                    while (dataReader.Read())
                    {
                        Comptoir comptoir = accesSageDAO.GetComptoirByNumTicket(dataReader["NUMERO"].ToString());
                        Commande commande = new Commande(Int32.Parse(dataReader["id_commande"].ToString()), dataReader["DATE_COMMANDE"].ToString(), dataReader["NUMERO"].ToString(), comptoir, Int32.Parse(dataReader["CLIENT"].ToString()), dataReader["ETAT"].ToString());
                        reponse = commande;
                    }
                    dataReader.Close();
                }
                //return
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                connexion.CloseAll(command, null, connexion.GetConnection());
            }
        }
        public List<Commande> GetListeToutCommandeEnCours(string etat)
        {
            Connexion connexion = new Connexion();
            string query = "SELECT * FROM commande where ETAT='" + etat + "'";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            AccesSageDAO accesSageDAO = new AccesSageDAO();
            MySqlDataReader dataReader;
            //Creation d'une liste
            List<Commande> reponse = new List<Commande>();
            try
            {
                //Ouverture connexion
                if (connexion.OpenConnection() == true)
                {
                    //Excecution de la commande
                    dataReader = command.ExecuteReader();

                    //Lecture des donnees et stockage dans la liste
                    while (dataReader.Read())
                    {
                        Comptoir comptoir = accesSageDAO.GetComptoirByNumTicket(dataReader["NUMERO"].ToString());
                        Commande commande = new Commande(Int32.Parse(dataReader["id_commande"].ToString()), dataReader["DATE_COMMANDE"].ToString(), dataReader["NUMERO"].ToString(), comptoir, Int32.Parse(dataReader["CLIENT"].ToString()), dataReader["ETAT"].ToString());
                        reponse.Add(commande);
                    }
                    dataReader.Close();
                }
                //return
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                connexion.CloseAll(command, null, connexion.GetConnection());
            }
        }
        public List<Commande> GetListeCommandeEnCours(Comptoir comptoir,string etat)
        {
            Connexion connexion = new Connexion();
            string query = "SELECT * FROM commande where ID_COMPTOIR='"+comptoir.IdComptoir+"' and ETAT='"+etat+"'";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            MySqlDataReader dataReader;
            //Creation d'une liste
            List<Commande> reponse = new List<Commande>();
            try
            {
                //Ouverture connexion
                if (connexion.OpenConnection() == true)
                {
                    //Excecution de la commande
                    dataReader = command.ExecuteReader();

                    //Lecture des donnees et stockage dans la liste
                    while (dataReader.Read())
                    {
                        Commande commande = new Commande(Int32.Parse(dataReader["id_commande"].ToString()), dataReader["DATE_COMMANDE"].ToString(), dataReader["NUMERO"].ToString(),comptoir, Int32.Parse(dataReader["CLIENT"].ToString()), dataReader["ETAT"].ToString());
                        reponse.Add(commande);
                    }
                    dataReader.Close();
                }
                //return
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connexion.GetConnection() != null)
                {
                    connexion.GetConnection().Close();
                }
            }
        }
        public void AjoutDetailsCommande(MySqlCommand cmd, DetailCommande commande)
        {
            Connexion connexion = new Connexion();
            try
            {
                string query = "INSERT INTO detail_commande (ID_COMMANDE,REFERENCE_ARTICLE,QUANTITE,EMPLACEMENT) " +
                    "VALUES('" + commande.IdCommande + "','" + commande.Article.References + "','" + commande.Quantite + "','" + commande.Emplacement + "')";
                //create command and assign the query and connection from the constructor
                cmd.CommandText = query;
                //Execute command
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void createDetailsCommande(List<DetailCommande> detailCommandes,string id_commande)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            Commande commande = new Commande();
            commande.IdCommande = Int32.Parse(id_commande);
            Duree duree = new Duree();
            duree.Commande = commande;
            duree.HeureCommande = DateTime.Now;
            DureeDAO dureeDAO = new DureeDAO();
            try
            {
                dureeDAO.ValiderDuree(command, duree);
                for (int i = 0; i < detailCommandes.Count; i++) { 
                    this.AjoutDetailsCommande(command, detailCommandes[i]);
                }
                if (transaction != null)
                {
                    transaction.Commit();
                }

            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (transaction != null)
                {
                    transaction.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public string GetIdCommande(string numero_ticket)
        {
            Connexion connexion = new Connexion();
            string query = "SELECT id_commande FROM commande where numero='"+numero_ticket+"'";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            //Creation d'une liste
            string reponse = "";
            try { 
            //Ouverture connexion
                if (connexion.OpenConnection() == true)
                {
                    //Excecution de la commande
                    MySqlDataReader dataReader = command.ExecuteReader();

                    //Lecture des donnees et stockage dans la liste
                    while (dataReader.Read())
                    {
                        reponse = dataReader["id_commande"].ToString();
                    }

                    //fermeture du Data Reader
                    dataReader.Close();
                }
                //return
                return reponse;
            }
            catch(Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connexion.GetConnection() != null)
                {
                    connexion.GetConnection().Close();
                }
            }
        }
        public void createCommande(Commande commande)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            try
            {
                this.AjoutCommande(command,commande);
                if (transaction != null)
                {
                    transaction.Commit();
                }

            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
            finally
            {
                connexion.CloseAll(command, transaction, connection);
            }
        }
        public void AjoutCommande(MySqlCommand cmd, Commande commande)
        {
            try
            {
                string query = "INSERT INTO commande (DATE_COMMANDE,NUMERO,ID_COMPTOIR,CLIENT,ETAT) " +
                    "VALUES('"+commande.DateCommande+"','"+commande.Numero+"','"+commande.Comptoir.IdComptoir+"','"+commande.Client+"','"+commande.Etat+"')";
                //create command and assign the query and connection from the constructor
                cmd.CommandText = query;
                //Execute command
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}