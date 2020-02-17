using stock.Models.DAO;
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
        public Utilisateur GetUtilisateurByMdp(string mdp)
        {
            try
            {
                UtilisateurDAO utilisateurDAO = new UtilisateurDAO();
                Utilisateur utilisateur = utilisateurDAO.GetUtilisateurByMdp(mdp);
                if (utilisateur.IdUtilisateur == "")
                {
                    throw new Exception("Magasinier introuvable");
                }
                else
                {
                    return utilisateur;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public Utilisateur GetUtilisateurByName(string nom)
        {
            try
            {
                UtilisateurDAO utilisateurDAO = new UtilisateurDAO();
                Utilisateur utilisateur = utilisateurDAO.GetUtilisateurByNom(nom);
                if (utilisateur.IdUtilisateur == "")
                {
                    throw new Exception("Binôme introuvable");
                }
                else {
                    return utilisateur;
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public string IdUtilisateur { get => idUtilisateur; set => idUtilisateur = value; }
        public string IdPoste { get => idPoste; set => idPoste = value; }
        public string NomUtilisateur { get => nomUtilisateur; set => nomUtilisateur = value; }
        public string Prenoms { get => prenoms; set => prenoms = value; }
        public string Identifiants { get => identifiants; set => identifiants = value; }
        public string Mdp { get => mdp; set => mdp = value; }
    }
}