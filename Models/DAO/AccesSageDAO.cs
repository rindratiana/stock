using stock.Models.Classe.Stock;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace stock.Models.DAO
{
    public class AccesSageDAO
    {
        public AccesSageDAO() { }

        public Comptoir GetComptoirByNumTicket(string numero_ticket)
        {
            Comptoir reponse = new Comptoir();
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            connexion.Open();
            string sql = "SELECT DISTINCT F_DOCLIGNE.CA_No, F_CAISSE.CA_Intitule FROM F_DOCLIGNE join F_CAISSE on F_CAISSE.CA_No = F_DOCLIGNE.CA_No WHERE F_DOCLIGNE.DO_Piece='" + numero_ticket + "'";
            throw new Exception(sql);
            command = new SqlCommand(sql, connexion.Connection);
            dataReader = command.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    reponse = new Comptoir(dataReader["CA_No"].ToString(), dataReader["CA_Intitule"].ToString());
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
        public List<Article> GetArticles()
        {
            List<Article> reponse = new List<Article>();
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            connexion.Open();
            string sql = "SELECT AR_Ref,AR_Design,FA_CodeFamille,CL_No2 FROM F_ARTICLE";
            command = new SqlCommand(sql, connexion.Connection);
            dataReader = command.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    Article article = new Article();
                    article.References = dataReader.GetValue(0).ToString();
                    article.Designation = dataReader.GetValue(1).ToString();
                    article.Code = dataReader.GetValue(2).ToString();
                    article.Emplacement = dataReader.GetValue(3).ToString();
                    reponse.Add(article);
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
        public List<string> GetNumeroPiece(string piece)
        {
            List<string> reponse = new List<string>();
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            connexion.Open();
            string sql = "SELECT DISTINCT F_DOCLIGNE.DO_Piece FROM F_DOCLIGNE WHERE F_DOCLIGNE.DO_Piece like '"+piece+"%' and F_DOCLIGNE.DO_Type=30";
            command = new SqlCommand(sql, connexion.Connection);
            dataReader = command.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    reponse.Add(dataReader["DO_Piece"].ToString());
                }
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                connexion.CloseRest(dataReader, command, connexion.Connection);
            }
        }
        public List<DetailCommande> GetArticlesCommandes(string numero)
        {
            List<DetailCommande> reponse = new List<DetailCommande>();
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            connexion.Open();
            string sql = "SELECT F_ARTICLE.AR_Ref,F_ARTICLE.AR_Design,F_ARTICLE.FA_CodeFamille," +
                "F_ARTICLE.CL_No1, F_DOCLIGNE.DL_Qte,F_DOCLIGNE.DO_Piece FROM F_DOCLIGNE " +
                "join F_ARTICLE on F_ARTICLE.AR_Ref=F_DOCLIGNE.AR_Ref  " +
                "join F_CATALOGUE on F_ARTICLE.CL_No1=F_CATALOGUE.CL_No " +
                "WHERE F_DOCLIGNE.DO_Piece='"+ numero +"' and F_DOCLIGNE.DO_Type='30'";
            command = new SqlCommand(sql, connexion.Connection);
            dataReader = command.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    Article article = new Article();
                    article.References = dataReader.GetValue(0).ToString();
                    article.Designation = dataReader.GetValue(1).ToString();
                    article.Code = dataReader.GetValue(2).ToString();
                    article.Emplacement = dataReader.GetValue(3).ToString();
                    int emplacement = (int)float.Parse(dataReader.GetValue(3).ToString());
                    int quantite = (int)float.Parse(dataReader.GetValue(4).ToString());
                    DetailCommande detailcommande = new DetailCommande(article,quantite,emplacement);
                    reponse.Add(detailcommande);
                }

                return reponse;
            }
            catch (Exception exception)
            {
                //throw new Exception(sql);
                throw exception;
            }
            finally
            {
                connexion.CloseRest(dataReader, command, connexion.Connection);
            }
        }
    }
}