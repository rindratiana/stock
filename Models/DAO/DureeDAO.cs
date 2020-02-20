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

		public List<StatDuree> GetStatDurees(string dateDebut,string dateFin)
		{
            Connexion connexion = new Connexion();
            string query = "";
            if (dateFin == "" && dateDebut == "")
            {
                query = "select commande.id_commande,TIMESTAMPDIFF(MINUTE,heure_commande,heure_livraison) as duree from duree join commande on commande.id_commande=duree.id_commande where ETAT='111' and client='1'";
            }
            else
            {
                query = "select commande.id_commande as id_commande,TIMESTAMPDIFF(MINUTE,heure_commande,heure_livraison) as duree from duree join commande on commande.id_commande=duree.id_commande where date_commande<='"+dateFin+"' and date_commande>='"+dateDebut+ "' and ETAT='111' and client='1'";
            }
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            MySqlDataReader dataReader;
            //Creation d'une liste
            List<StatDuree> reponse = new List<StatDuree>();
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
                        StatDuree stat = new StatDuree();
                        Commande commande = new Commande();
                        commande.IdCommande = Int32.Parse(dataReader["id_commande"].ToString());
                        stat.Commande = commande;
                        stat.Minutes = Int32.Parse(dataReader["duree"].ToString());
                        reponse.Add(stat);
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
                string query = "INSERT INTO DUREE VALUES('" + duree.Commande.IdCommande + "','" + heureCommande + "','" + duree.HeureSortie + "','" + duree.HeureLivraison + "')";
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

        public void UpdateDuree(MySqlCommand command, Duree duree, string condition)
        {
            try
            {
                string query = "";
                if (condition == "SORTIE")
                {
                    string heureSortie = duree.HeureSortie.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    query = "UPDATE DUREE SET HEURE_SORTIE = '" + heureSortie + "' where ID_COMMANDE = '" + duree.Commande.IdCommande + "'";
                }
                else if (condition == "LIVRAISON")
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