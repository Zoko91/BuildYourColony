using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes
{
    abstract class Batiment:ObjetFixe
    {

        protected int coutBois;
        protected int coutPierre;


        public Batiment(List<int[]> positions, int coutBois, int coutPierre) : base(positions)
        {
            this.coutBois = coutBois;
            this.coutPierre= coutPierre;
        }

    }
}
