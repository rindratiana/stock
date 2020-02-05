using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Stock
{
    public class Comptoir
    {
        private string idComptoir;
        private string nomComptoir;

        public Comptoir() { }
        public Comptoir(string idComptoir, string nomComptoir)
        {
            this.IdComptoir = idComptoir;
            this.NomComptoir = nomComptoir;
        }

        public string IdComptoir { get => idComptoir; set => idComptoir = value; }
        public string NomComptoir { get => nomComptoir; set => nomComptoir = value; }
    }
}