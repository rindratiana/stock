using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.DAO
{
    public class Connexion
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Connexion()
        {
            Initialisation();
        }

        private void Initialisation()
        {
            server = "localhost";
            database = "stock";
            uid = "root";
            password = "root";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }
        public MySqlConnection GetConnection()
        {
            return this.connection;
        }
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public void CloseAll(MySqlCommand command,MySqlTransaction transaction, MySqlConnection connection)
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
}