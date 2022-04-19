using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    class Chasseur:Colon
    {        
        // -----------------------------------------------------------------
        // Le Chasseur peut chasser les mobs et extraire de la viande [Non implémenté dans le jeu]
        // -----------------------------------------------------------------
        public Chasseur(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
            
        }
    }
}
