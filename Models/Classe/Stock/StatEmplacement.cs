using stock.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class StatEmplacement
    {
        private Emplacement emplacement;
        private int quantite;

        public List<StatEmplacement> GetStatEmplacementsEntreDeuxDates(string dateDebut, string dateFin)
        {
            try
            {
                EmplacementDAO emplacementDAO = new EmplacementDAO();
                List<StatEmplacement> reponse = emplacementDAO.GetStatEmplacementsEntreDeuxDates(dateDebut,dateFin);
                return reponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public List<StatEmplacement> GetStatEmplacements()
        {
            try
            {
                EmplacementDAO emplacementDAO = new EmplacementDAO();
                List<StatEmplacement> reponse = emplacementDAO.GetStatEmplacements();
                return reponse;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public StatEmplacement() { }
        public StatEmplacement(Emplacement emplacement, int quantite)
        {
            this.emplacement = emplacement;
            this.quantite = quantite;
        }

        public int Quantite { get => quantite; set => quantite = value; }
        public Emplacement Emplacement { get => emplacement; set => emplacement = value; }
    }
}