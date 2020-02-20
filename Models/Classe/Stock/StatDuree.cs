using stock.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class StatDuree
    {
        private Commande commande;
        private int minutes;

        public double GetStatDuree(string dateDebut,string dateFin)
        {
            try
            {
                double reponse = 0;
                DureeDAO dureeDAO = new DureeDAO();
                List<StatDuree> liste = dureeDAO.GetStatDurees(dateDebut, dateFin);
                if (liste.Count == 0)
                {
                    return reponse;
                }
                else
                {
                    double temp = 0;
                    for (int i = 0; i < liste.Count; i++)
                    {
                        temp = temp + liste[i].Minutes;
                    }
                    reponse = temp / liste.Count;
                    return reponse;
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public StatDuree()
        {
        }

        public StatDuree(Commande commande, int minutes)
        {
            this.Commande = commande;
            this.Minutes = minutes;
        }

        public Commande Commande { get => commande; set => commande = value; }
        public int Minutes { get => minutes; set => minutes = value; }
    }
}