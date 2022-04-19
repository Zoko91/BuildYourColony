using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes
{
    class Eau:ObjetFixe
    {
        // -----------------------------------------------------------------
        // Ressource de type Eau et récoltable si un Colon a soif
        // -----------------------------------------------------------------
        protected override bool EstCible {get;set;}
        public Eau(List<Tuple<int, int>> positionObjet, Monde planete) :base(positionObjet, planete) {
            EstCible = false;
        }

    }
}
