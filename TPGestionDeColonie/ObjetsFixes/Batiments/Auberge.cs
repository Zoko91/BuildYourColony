using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Auberge:Batiment
    {
        protected bool PresenceTavernier
        {
            get;
            set;
        }

        public Auberge(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre) : base(positionObjet, coutBois, coutPierre) { }





        //Auberge fait perdre de la fatigue
    }
}
