    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.Colons
{
    class Tavernier:Colon
    {
        public Tavernier(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
              Backpack = new int[]{ 0, 0, 10, 0};  //Bois/Pierre/Eau/Seau d'eau
        }


        public void VerifierStockEau()
        {
            //On a le monde, vérifier si il est sur la taverne
        }
        public void VerifierStockBle()
        {

        }
        public void VerifierStockViande()
        {

        }


    }
}
