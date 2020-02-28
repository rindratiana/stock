using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class ConnexionSage
    {
        private SqlConnection connection;

        public ConnexionSage()
        {
            try { 
                Connection = new SqlConnection("Server = 10.0.0.4; Database = METROPOLE_2019; User ID = IT_Manager; Password = @2019Dmin***:p; Trusted_Connection = False;");
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public SqlConnection Connection { get => connection; set => connection = value; }

        public void Close()
        {
            Connection.Close();
        }
        public void Open()
        {
            Connection.Open();
        }

        public void CloseRest(SqlDataReader dataReader, SqlCommand command, SqlConnection connection)
        {
            if (dataReader != null)
            {
                dataReader.Close();
            }
            if (command != null)
            {
                command.Dispose();
            }
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}