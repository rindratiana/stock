using MySql.Data.MySqlClient;
using stock.Models.Classe.Stock;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace stock.Models.DAO
{
    public class DureeDAO
    {
        public DureeDAO() { }

        public void CreateDuree(Duree duree)
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
                this.ValiderDuree(command, duree);
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

        public void ValiderDuree(MySqlCommand command, Duree duree)
        {
            try
            {
                string heureCommande = duree.HeureCommande.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                string query = "INSERT INTO DUREE VALUES('"+duree.Commande.IdCommande+"','"+heureCommande+"','"+duree.HeureSortie+"','"+duree.HeureLivraison+"')";
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

        public void UpdateDuree(MySqlCommand command,Duree duree,string condition)
        {
            try
            {
                string query = "";
                if (condition == "SORTIE") {
                    string heureSortie = duree.HeureSortie.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    query = "UPDATE DUREE SET HEURE_SORTIE = '"+heureSortie+"' where ID_COMMANDE = '"+duree.Commande.IdCommande+"'";
                }
                else if(condition == "LIVRAISON")
                {
                    string heureLivraison = duree.HeureLivraison.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    query = "UPDATE DUREE SET HEURE_LIVRAISON = '" + heureLivraison + "' where ID_COMMANDE = '" + duree.Commande.IdCommande + "'";
                }
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
    }
}