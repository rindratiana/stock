using MySql.Data.MySqlClient;
using stock.Models.Classe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.DAO
{
    public class UtilisateurDAO
    {
        public UtilisateurDAO() { }
        public Utilisateur GetUtilisateurByMdp(string mdp)
        {
            Utilisateur reponse = new Utilisateur();
            Connexion connexion = new Connexion();
            string sql = "select * from utilisateur where MDP = '" + mdp + "' and id_poste='2'";
            MySqlCommand command = new MySqlCommand(sql, connexion.GetConnection());
            MySqlDataReader dataReader;
            connexion.GetConnection().Open();
            dataReader = command.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    reponse = new Utilisateur(dataReader["ID_UTILISATEUR"].ToString(), dataReader["ID_POSTE"].ToString(), dataReader["NOM_UTILISATEUR"].ToString(), dataReader["PRENOMS"].ToString(), dataReader["IDENTIFIANTS"].ToString(), dataReader["MDP"].ToString());
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
            try
            {
                while (dataReader.Read())
                {
                    reponse = new Utilisateur(dataReader["ID_UTILISATEUR"].ToString(), dataReader["ID_POSTE"].ToString(), dataReader["NOM_UTILISATEUR"].ToString(), dataReader["PRENOMS"].ToString(), dataReader["IDENTIFIANTS"].ToString(), dataReader["MDP"].ToString());
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
            try
            {
                while (dataReader.Read())
                {
                    reponse = new Utilisateur(dataReader["ID_UTILISATEUR"].ToString(), dataReader["ID_POSTE"].ToString(), dataReader["NOM_UTILISATEUR"].ToString(), dataReader["PRENOMS"].ToString(), dataReader["IDENTIFIANTS"].ToString(), dataReader["MDP"].ToString());
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