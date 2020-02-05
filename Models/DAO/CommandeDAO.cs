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
        public Boolean testExistance(string numero_ticket)
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
                    throw new Exception("Ce ticket a été déjà utiliser");
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
        public void createDetailsCommande(List<DetailCommande> detailCommandes)
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
                for(int i = 0; i < detailCommandes.Count; i++) { 
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
        public void AjoutCommande(MySqlCommand cmd, Commande commande)
        {
            Connexion connexion = new Connexion();
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