using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class Emplacement
    {
        private string numeroEmplacement;
        private string intitule;

        public Emplacement() { }

        public Emplacement(string numeroEmplacement, string intitule)
        {
            this.NumeroEmplacement = numeroEmplacement;
            this.Intitule = intitule;
        }

        public string NumeroEmplacement { get => numeroEmplacement; set => numeroEmplacement = value; }
        public string Intitule { get => intitule; set => intitule = value; }
    }
}