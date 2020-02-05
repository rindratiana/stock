using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe
{
    public class Utilisateur
    {
        private string idUtilisateur;
        private string idPoste;
        private string nomUtilisateur;
        private string prenoms;
        private string identifiants;
        private string mdp;

        public Utilisateur() { }
        public Utilisateur(string idUtilisateur, string idPoste, string nomUtilisateur, string prenoms, string identifiants, string mdp)
        {
            this.IdUtilisateur = idUtilisateur;
            this.IdPoste = idPoste;
            this.NomUtilisateur = nomUtilisateur;
            this.Prenoms = prenoms;
            this.Identifiants = identifiants;
            this.Mdp = mdp;
        }

        public string IdUtilisateur { get => idUtilisateur; set => idUtilisateur = value; }
        public string IdPoste { get => idPoste; set => idPoste = value; }
        public string NomUtilisateur { get => nomUtilisateur; set => nomUtilisateur = value; }
        public string Prenoms { get => prenoms; set => prenoms = value; }
        public string Identifiants { get => identifiants; set => identifiants = value; }
        public string Mdp { get => mdp; set => mdp = value; }
    }
}