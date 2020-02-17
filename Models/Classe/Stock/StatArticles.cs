using stock.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class StatArticles
    {
        private Article article;
        private int quantite;

        public List<StatArticles> GetStatArticles(string dateDebut, string dateFin,string numeroEmplacement)
        {
            try
            {
                EmplacementDAO emplacementDAO = new EmplacementDAO();
                List<StatArticles> reponse = emplacementDAO.GetStatArticles(dateDebut, dateFin,numeroEmplacement);
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public StatArticles() { }
        public StatArticles(Article article, int quantite)
        {
            this.Article = article;
            this.Quantite = quantite;
        }

        public Article Article { get => article; set => article = value; }
        public int Quantite { get => quantite; set => quantite = value; }
    }
}