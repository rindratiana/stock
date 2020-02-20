using MySql.Data.MySqlClient;
using stock.Models.Classe.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.DAO
{
    public class PosteDAO
    {
        public PosteDAO() { }

        public List<Poste> GetPostes()
        {
            Connexion connexion = new Connexion();
            string query = "select * from poste where id_poste!='4'";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            MySqlDataReader dataReader;
            //Creation d'une liste
            List<Poste> reponse = new List<Poste>();
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
                        Poste poste = new Poste(dataReader["ID_POSTE"].ToString(), dataReader["NOM_POSTE"].ToString());
                        reponse.Add(poste);
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

        public Poste GetPosteById(string idPoste)
        {
            Connexion connexion = new Connexion();
            string query = "select * from poste where id_poste='"+idPoste+"'";
            MySqlCommand command = new MySqlCommand(query, connexion.GetConnection());
            MySqlDataReader dataReader;
            //Creation d'une liste
            Poste reponse = new Poste();
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
                        reponse = new Poste(dataReader["ID_POSTE"].ToString(), dataReader["NOM_POSTE"].ToString());
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
    }
}