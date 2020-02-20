using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stock.Models.Classe.Utilisateurs
{
    public class LoginUsers
    {
        private string identifiant;
        private string mdp;
        public LoginUsers() { }

        public LoginUsers(string identifiant, string mdp)
        {
            this.Identifiant = identifiant;
            this.Mdp = mdp;
        }

        public string Identifiant { get => identifiant; set
            {
                if (value == "")
                {
                    throw new Exception("Veuillez entrez un identifiant valide");
                }
                else
                {
                    identifiant = value;
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