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


        public Ferme(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre) : base(positionObjet, coutBois,coutPierre) { }



        //Travail du paysan
    }
}
