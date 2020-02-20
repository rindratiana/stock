using stock.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class Poste
    {
        private string idPoste;
        private string nomPoste;

        public List<Poste> GetPostes()
        {
            PosteDAO posteDAO = new PosteDAO();
            try
            {
                return posteDAO.GetPostes();
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public Poste() { }

        public Poste(string idPoste, string nomPoste)
        {
            this.IdPoste = idPoste;
            this.NomPoste = nomPoste;
        }

        public string IdPoste { get => idPoste; set => idPoste = value; }
        public string NomPoste { get => nomPoste; set => nomPoste = value; }
    }
}