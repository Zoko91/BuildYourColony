using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Ferme:Batiment
    {
        protected bool PresencePaysan
        {
            get;
            set;
        }
        
        // Si paysan, pousse et production ++, sinon --


        public Ferme(List<int[]> positions, int coutBois, int coutPierre) : base(positions,coutBois,coutPierre) { }



        //Travail du paysan
    }
}
