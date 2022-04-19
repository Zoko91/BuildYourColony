using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes
{
    abstract class Batiment:ObjetFixe
    {
        
        // -----------------------------------------------------------------
        // Classe abstraite définissant chaque batiment par leur position et leur cout en ressources
        // -----------------------------------------------------------------

        protected int coutBois;
        protected int coutPierre;


        public Batiment(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre, Monde planete) : base(positionObjet,planete)
        {
            this.coutBois = coutBois;
            this.coutPierre= coutPierre;
        }


    }
}
