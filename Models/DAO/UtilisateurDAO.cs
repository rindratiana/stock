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
        public List<string> GetListeIdentifiant(string identifiant)
        {
            List<String> reponse = new List<String>();
            Connexion connexion = new Connexion();
            string sql = "select identifiants from utilisateur where identifiants like '" + identifiant + "%'";
            MySqlCommand command = new MySqlCommand(sql, connexion.GetConnection());
            MySqlDataReader dataReader;
            connexion.GetConnection().Open();
            dataReader = command.ExecuteReader();
            PosteDAO posteDAO = new PosteDAO();
            try
            {
                while (dataReader.Read())
                {
                    reponse.Add(dataReader["IDENTIFIANTS"].ToString());
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
        public void UpdateMdp(Utilisateur utilisateur)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            string sql = "UPDATE UTILISATEUR SET MDP ='" + utilisateur.Mdp + "', etatmdp='1' where ID_UTILISATEUR='" + utilisateur.IdUtilisateur + "'";
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
        public Utilisateur GetUtilisateurByIdentifiant(string identifiants)
        {
            Utilisateur reponse = new Utilisateur();
            Connexion connexion = new Connexion();
            string sql = "select * from utilisateur where IDENTIFIANTS='" + identifiants + "'";
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
        public Utilisateur GetUtilisateurById(string id)
        {
            Utilisateur reponse = new Utilisateur();
            Connexion connexion = new Connexion();
            string sql = "select * from utilisateur where ID_UTILISATEUR='"+id+"'";
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
                    reponse.EtatMdp = dataReader["etatmdp"].ToString();
                }
                else
                {
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
        public void ResetPwd(Utilisateur utilisateur)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            string sql = "UPDATE UTILISATEUR SET MDP ='"+utilisateur.Mdp+ "', etatmdp='0' where ID_UTILISATEUR='" + utilisateur.IdUtilisateur + "'";
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
        public void HideUser(string id)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            string sql = "UPDATE UTILISATEUR SET ETAT ='3' where ID_UTILISATEUR='" + id + "'";
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
        public void DeleteRequest(string id)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            string sql = "DELETE FROM UTILISATEUR where ID_UTILISATEUR='" + id + "'";
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
        public void ConfirmUser(string id)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            string sql = "UPDATE UTILISATEUR SET ETAT ='1' where ID_UTILISATEUR='" + id + "'";
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
        public List<Utilisateur> GetUtilisateurValidation(string etat)
        {
            List<Utilisateur> reponse = new List<Utilisateur>();
            Connexion connexion = new Connexion();
            string sql = "select * from utilisateur where etat='"+etat+"'";
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
                    reponse.Add(new Utilisateur(dataReader["ID_UTILISATEUR"].ToString(), poste, dataReader["NOM_UTILISATEUR"].ToString(), dataReader["PRENOMS"].ToString(), dataReader["IDENTIFIANTS"].ToString(), dataReader["MDP"].ToString()));
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
        public void UpdateProfil(Utilisateur utilisateur)
        {
            Connexion connexion = new Connexion();
            MySqlConnection connection = connexion.GetConnection();
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            string sql = "UPDATE UTILISATEUR SET NOM_UTILISATEUR ='"+utilisateur.NomUtilisateur+"', PRENOMS='"+utilisateur.Prenoms+"',MDP='"+utilisateur.Mdp+"' where ID_UTILISATEUR='"+utilisateur.IdUtilisateur+"'";
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
        public Utilisateur GetUtilisateurByLogin(LoginUsers loginUsers)
        {
            Utilisateur reponse = new Utilisateur();
            Connexion connexion = new Connexion();
            string sql = "select * from utilisateur where IDENTIFIANTS='"+loginUsers.Identifiant+"' and MDP = '" + loginUsers.Mdp + "' and ETAT='1'";
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
                        reponse.EtatMdp = dataReader["etatmdp"].ToString();
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
            string sql = "INSERT INTO UTILISATEUR (ID_POSTE,NOM_UTILISATEUR,PRENOMS,IDENTIFIANTS,MDP,ETAT,etatmdp) values ('" + utilisateur.Poste.IdPoste+"','"+utilisateur.NomUtilisateur+"','"+utilisateur.Prenoms+"','"+utilisateur.Identifiants+"','"+utilisateur.Mdp+ "','" + utilisateur.Etat + "','1')";
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