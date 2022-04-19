using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes
{
    class Arbre:ObjetFixe
    {
        // -----------------------------------------------------------------
        // L'arbre est une source finie de bois
        // -----------------------------------------------------------------
        protected override bool EstCible {get;set;}
        
        public Arbre(List<Tuple<int, int>> positionObjet, Monde planete) : base(positionObjet, planete) {
            EstCible = false;
        }

    }
}
