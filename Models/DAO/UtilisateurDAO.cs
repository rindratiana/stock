using MySql.Data.MySqlClient;
using stock.Models.Classe;
using stock.Models.Classe.Stock;
using stock.Models.Classe.Utilisateurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.DAO
{
    public class UtilisateurDAO
    {
        public UtilisateurDAO() { }

        public Utilisateur GetUtilisateurByLogin(LoginUsers loginUsers)
        {
            Utilisateur reponse = new Utilisateur();
            Connexion connexion = new Connexion();
            string sql = "select * from utilisateur where IDENTIFIANTS='"+loginUsers.Identifiant+"' and MDP = '" + loginUsers.Mdp + "'";
            MySqlCommand command = new MySqlCommand(sql, connexion.GetConnection());
            MySqlDataReader dataReader;
            connexion.GetConnection().Open();
            dataReader = command.ExecuteReader();
            PosteDAO posteDAO = new PosteDAO();
            try
            {
                    if (dataReader.Read().ToString() == "True")
                    {
                        Poste poste = posteDAO.GetPosteById(dataReader["ID_POSTE"].ToString());
                        reponse = new Utilisateur(dataReader["ID_UTILISATEUR"].ToString(), poste, dataReader["NOM_UTILISATEUR"].ToString(), dataReader["PRENOMS"].ToString(), dataReader["IDENTIFIANTS"].ToString(), dataReader["MDP"].ToString());
                    }
                    else {
                        throw new Exception("Identifiants / mot de passe invalide");
                    }
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
        public void ExcecuteCommande(MySqlCommand command,string sql)
        {
            try
            {
                command.CommandText = sql;
                //Execute command
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void CreateUtilisateur(Utilisateur utilisateur)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            string sql = "INSERT INTO UTILISATEUR (ID_POSTE,NOM_UTILISATEUR,PRENOMS,IDENTIFIANTS,MDP) values ('"+utilisateur.Poste.IdPoste+"','"+utilisateur.NomUtilisateur+"','"+utilisateur.Prenoms+"','"+utilisateur.Identifiants+"','"+utilisateur.Mdp+"')";
            try
            {
                this.ExcecuteCommande(command, sql);
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
        public Utilisateur GetUtilisateurByMdp(string mdp)
        {
            Utilisateur reponse = new Utilisateur();
            Connexion connexion = new Connexion();
            string sql = "select * from utilisateur where MDP = '" + mdp + "' and id_poste='2'";
            MySqlCommand command = new MySqlCommand(sql, connexion.GetConnection());
            MySqlDataReader dataReader;
            connexion.GetConnection().Open();
            dataReader = command.ExecuteReader();
            PosteDAO posteDAO = new PosteDAO();
            try
            {
                while (dataReader.Read())
                {
                    Poste poste = posteDAO.GetPosteById(dataReader["ID_POSTE"].ToString());
                    reponse = new Utilisateur(dataReader["ID_UTILISATEUR"].ToString(),poste , dataReader["NOM_UTILISATEUR"].ToString(), dataReader["PRENOMS"].ToString(), dataReader["IDENTIFIANTS"].ToString(), dataReader["MDP"].ToString());
                }
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
        public Utilisateur GetUtilisateurByNom(string nom)
        {
            Utilisateur reponse = new Utilisateur();
            Connexion connexion = new Connexion();
            string sql = "select * from utilisateur where PRENOMS = '" + nom + "' and id_poste='3'";
            MySqlCommand command = new MySqlCommand(sql, connexion.GetConnection());
            MySqlDataReader dataReader;
            connexion.GetConnection().Open();
            dataReader = command.ExecuteReader();
            PosteDAO posteDAO = new PosteDAO();
            try
            {
                while (dataReader.Read())
                {
                    Poste poste = posteDAO.GetPosteById(dataReader["ID_POSTE"].ToString());
                    reponse = new Utilisateur(dataReader["ID_UTILISATEUR"].ToString(), poste, dataReader["NOM_UTILISATEUR"].ToString(), dataReader["PRENOMS"].ToString(), dataReader["IDENTIFIANTS"].ToString(), dataReader["MDP"].ToString());
                }
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
        public Utilisateur GetMagasinier(string mdp)
        {
            Utilisateur reponse = new Utilisateur();
            Connexion connexion = new Connexion();
            string sql = "select * from utilisateur where MDP = '" + mdp + "' and id_poste='2'";
            MySqlCommand command = new MySqlCommand(sql, connexion.GetConnection());
            MySqlDataReader dataReader;
            connexion.GetConnection().Open();
            dataReader = command.ExecuteReader();
            PosteDAO posteDAO = new PosteDAO();
            try
            {
                while (dataReader.Read())
                {
                    Poste poste = posteDAO.GetPosteById(dataReader["ID_POSTE"].ToString());
                    reponse = new Utilisateur(dataReader["ID_UTILISATEUR"].ToString(), poste, dataReader["NOM_UTILISATEUR"].ToString(), dataReader["PRENOMS"].ToString(), dataReader["IDENTIFIANTS"].ToString(), dataReader["MDP"].ToString());
                }
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
        public List<string> GetNomBinome(string nom)
        {
            List<string> reponse = new List<string>();
            Connexion connexion = new Connexion();
            string sql = "select distinct prenoms from utilisateur where prenoms like '"+nom+"%' and id_poste='3'";
            MySqlCommand command = new MySqlCommand(sql, connexion.GetConnection());
            MySqlDataReader dataReader;
            connexion.GetConnection().Open();
            dataReader = command.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    reponse.Add(dataReader["prenoms"].ToString());
                }
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
    }
}