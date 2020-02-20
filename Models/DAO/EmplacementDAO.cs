using MySql.Data.MySqlClient;
using stock.Models.Classe.Stock;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace stock.Models.DAO
{
    public class EmplacementDAO
    {
        public EmplacementDAO() { }

        public List<Emplacement> GetEmplacements()
        {
            List<Emplacement> reponse = new List<Emplacement>();
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            connexion.Open();
            string sql = "select CL_No,CL_Intitule from F_CATALOGUE";
            command = new SqlCommand(sql, connexion.Connection);
            dataReader = command.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    Emplacement emplacement = new Emplacement();
                    emplacement.NumeroEmplacement = dataReader[0].ToString();
                    emplacement.Intitule = dataReader[1].ToString();
                    reponse.Add(emplacement);
                }

                return reponse;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connexion.CloseRest(dataReader, command, connexion.Connection);
            }
        }
        public Emplacement GetEmplacementsByNumero(string numero)
        {
            Emplacement reponse = new Emplacement();
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            connexion.Open();
            string sql = "select CL_No,CL_Intitule from F_CATALOGUE where CL_No = '"+numero+"'";
            command = new SqlCommand(sql, connexion.Connection);
            dataReader = command.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    Emplacement emplacement = new Emplacement();
                    emplacement.NumeroEmplacement = dataReader[0].ToString();
                    emplacement.Intitule = dataReader[1].ToString();
                    reponse = emplacement;
                }

                return reponse;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connexion.CloseRest(dataReader, command, connexion.Connection);
            }
        }
        public List<StatEmplacement> GetStatEmplacements()
        {
            Connexion connexion = new Connexion();
            string query = "select emplacement, count(*) as total from detail_commande join commande on commande.id_commande=detail_commande.id_commande where commande.client = '1' and commande.etat='111' group by emplacement";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            MySqlDataReader dataReader;
            //Creation d'une liste
            List<StatEmplacement> reponse = new List<StatEmplacement>();
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
                        StatEmplacement stat = new StatEmplacement();
                        stat.Emplacement = GetEmplacementsByNumero(dataReader["emplacement"].ToString());
                        stat.Quantite = Int32.Parse(dataReader["total"].ToString());
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
        public List<StatEmplacement> GetStatEmplacementsEntreDeuxDates(string dateDebut, string dateFin)
        {
            Connexion connexion = new Connexion();
            string query = "select emplacement, count(*) as total from detail_commande join commande on commande.id_commande=detail_commande.id_commande where commande.client = '1' and commande.etat='111' and date_commande<='" + dateFin+"' and date_commande>='"+dateDebut+"' group by emplacement";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            MySqlDataReader dataReader;
            //Creation d'une liste
            List<StatEmplacement> reponse = new List<StatEmplacement>();
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
                        StatEmplacement stat = new StatEmplacement();
                        stat.Emplacement = GetEmplacementsByNumero(dataReader["emplacement"].ToString());
                        stat.Quantite = Int32.Parse(dataReader["total"].ToString());
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
        public List<StatArticles> GetStatArticles(string dateDebut, string dateFin, string numeroEmplacement)
        {
            Connexion connexion = new Connexion();
            string query = "";
            if (dateFin=="" && dateDebut == "")
            {
                query = "select reference_article, count(*) as total from detail_commande join commande on commande.id_commande=detail_commande.id_commande where emplacement ='"+numeroEmplacement+ "' group by reference_article";
            }
            else
            {
                query = "select reference_article, count(*) as total from detail_commande join commande on commande.id_commande=detail_commande.id_commande where emplacement ='"+numeroEmplacement+"' and date_commande<='" + dateFin + "' and date_commande>='" + dateDebut + "' group by reference_article";
            }
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            MySqlDataReader dataReader;
            //Creation d'une liste
            List<StatArticles> reponse = new List<StatArticles>();
            AccesSageDAO accesSageDAO = new AccesSageDAO();
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
                        StatArticles stat = new StatArticles();
                        Article article = accesSageDAO.GetArticleByReferences(dataReader["reference_article"].ToString());
                        stat.Article = article;
                        stat.Quantite = Int32.Parse(dataReader["total"].ToString());
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
    }
}