using stock.Models.Classe;
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

        public Comptoir GetComptoirByNomCaisse(string nom)
        {
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            Comptoir comptoirTemp = new Comptoir();
            connexion.Open();
            string sql = "SELECT * FROM F_CAISSE where CA_Intitule='"+nom+"'";
            command = new SqlCommand(sql, connexion.Connection);
            dataReader = command.ExecuteReader();
            try
            {
                if (dataReader.Read().ToString()=="True") {
                    comptoirTemp.IdComptoir = dataReader["CO_No"].ToString();
                    comptoirTemp.NomComptoir = dataReader["CA_Intitule"].ToString();
                }
                else
                {
                    comptoirTemp.IdComptoir = "";
                    comptoirTemp.NomComptoir = "";
                }

                return comptoirTemp;
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
        public Comptoir GetComptoirByNumTicket(string numero_ticket)
        {
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            Comptoir comptoirTemp = new Comptoir();
            connexion.Open();
            string sql = "SELECT DISTINCT F_DOCLIGNE.CO_No, F_CAISSE.CA_Intitule FROM F_DOCLIGNE join F_CAISSE on F_CAISSE.CO_No = F_DOCLIGNE.CO_No WHERE F_DOCLIGNE.DO_Piece='" + numero_ticket + "'";
            command = new SqlCommand(sql, connexion.Connection);
            dataReader = command.ExecuteReader();
            try
            {
                if (dataReader.Read().ToString()=="True") {
                    comptoirTemp.IdComptoir = dataReader["CO_No"].ToString();
                    comptoirTemp.NomComptoir = dataReader["CA_Intitule"].ToString();
                }
                else
                {
                    comptoirTemp.IdComptoir = "0";
                    comptoirTemp.NomComptoir = "Autres";
                }

                return comptoirTemp;
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
        public List<string> GetNumeroPiece(string piece,Utilisateur utilisateur)
        {
            Comptoir comptoir = GetComptoirByNomCaisse(utilisateur.Identifiants);
            List<string> reponse = new List<string>();
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            connexion.Open();
            string sql = "SELECT DISTINCT F_DOCLIGNE.DO_Piece FROM F_DOCLIGNE WHERE F_DOCLIGNE.DO_Piece like '"+piece+ "%' and F_DOCLIGNE.DO_Type=30 and F_DOCLIGNE.CO_No='" + comptoir.IdComptoir+"'";
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
                "F_ARTICLE.CL_No2, F_DOCLIGNE.DL_Qte,F_DOCLIGNE.DO_Piece FROM F_DOCLIGNE " +
                "join F_ARTICLE on F_ARTICLE.AR_Ref=F_DOCLIGNE.AR_Ref  " +
                "join F_CATALOGUE on F_ARTICLE.CL_No2=F_CATALOGUE.CL_No " +
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

        public Article GetArticleByReferences(string references)
        {
            Article reponse = new Article();
            ConnexionSage connexion = new ConnexionSage();
            SqlCommand command;
            SqlDataReader dataReader;
            connexion.Open();
            string sql = "SELECT AR_Ref,AR_Design,FA_CodeFamille,CL_No2 FROM F_ARTICLE where AR_Ref='"+references+"'";
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
                    reponse = article;
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
    }
}