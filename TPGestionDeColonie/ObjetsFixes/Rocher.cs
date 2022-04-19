using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes
{
    class Rocher:ObjetFixe
    {
        // -----------------------------------------------------------------
        // Le rocher est une source finie de pierre
        // -----------------------------------------------------------------
        protected override bool EstCible {get;set;}
       
        public Rocher(List<Tuple<int, int>> positionObjet,Monde planete) : base(positionObjet,planete) {
            EstCible = false;
        }



    }
}
