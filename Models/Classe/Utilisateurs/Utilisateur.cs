using stock.Models.Classe.Stock;
using stock.Models.Classe.Utilisateurs;
using stock.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace stock.Models.Classe
{
    public class Utilisateur
    {
        private string idUtilisateur;
        private Poste poste;
        private string nomUtilisateur;
        private string prenoms;
        private string identifiants;
        private string mdp;

        public static Utilisateur Connecter(LoginUsers loginUsers)
        {
            Utilisateur utilisateur = new Utilisateur();
            try
            {
                UtilisateurDAO utilisateurDAO = new UtilisateurDAO();
                MD5 md5Hash = MD5.Create();
                string mdp = GetMd5Hash(md5Hash,loginUsers.Mdp);
                loginUsers.Mdp = mdp;
                utilisateur = utilisateurDAO.GetUtilisateurByLogin(loginUsers);
                return utilisateur;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public void comparaisonMdp(string mdp1,string mdp2)
        {
            try { 
                if (mdp1 != mdp2)
                {
                    throw new Exception("Veuillez vérifiez les deux mots de passe");
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        public void CreateUtilisateur()
        {
            UtilisateurDAO utilisateurDAO = new UtilisateurDAO();
            try
            {
                MD5 md5Hash = MD5.Create();
                string mdp = GetMd5Hash(md5Hash, this.Mdp);
                this.Mdp = mdp;
                utilisateurDAO.CreateUtilisateur(this);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Utilisateur() { }
        public Utilisateur(string idUtilisateur, Poste poste, string nomUtilisateur, string prenoms, string identifiants, string mdp)
        {
            this.IdUtilisateur = idUtilisateur;
            this.Poste = poste;
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
                MD5 md5Hash = MD5.Create();
                string mdpHash = GetMd5Hash(md5Hash, mdp);
                Utilisateur utilisateur = utilisateurDAO.GetUtilisateurByMdp(mdpHash);
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
        public Poste Poste { get => poste; set => poste = value; }
        public string NomUtilisateur { 
            get => nomUtilisateur; 
            set
            {
                if (value == "")
                {
                    throw new Exception("Veuillez entrez un nom d'utilisateur valide");
                }
                else
                {
                    nomUtilisateur = value;
                }
            }
        }
        public string Prenoms { get => prenoms; set
            {
                if (value == "")
                {
                    throw new Exception("Veuillez entrez un prénom valide");
                }
                else
                {
                    prenoms = value;
                }
            }
        }
        public string Identifiants { get => identifiants; set
            {
                try { 
                    if (value == "")
                    {
                        throw new Exception("Veuillez entrez un identifiant valide");
                    }
                    else
                    {
                        AccesSageDAO accesSageDAO = new AccesSageDAO();
                        if (this.Poste.IdPoste == "1") { 
                            Comptoir comptoir = accesSageDAO.GetComptoirByNomCaisse(value);
                            if (comptoir.IdComptoir == "") {
                                throw new Exception("Votre identifiant doît être identique à votre nom de caisse dans SAGE");
                            }
                            else
                            {
                                identifiants = value;
                            }
                        }
                        else
                        {
                            identifiants = value;
                        }
                    }
                }
                catch(Exception exception)
                {
                    throw exception;
                }
            }
        }
        public string Mdp { get => mdp; set
            {
                if (value == "")
                {
                    throw new Exception("Veuillez entrez un mot de passe valide");
                }
                else
                {
                    mdp = value;
                }
            }
        }
    }
}