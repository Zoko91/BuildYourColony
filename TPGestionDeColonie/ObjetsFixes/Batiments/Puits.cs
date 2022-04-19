using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Puits : Batiment
    {
        // -----------------------------------------------------------------
        // Le puits est une source infini d'eau
        // -----------------------------------------------------------------
        public Puits(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre,Monde planete) : base(positionObjet, coutBois, coutPierre,planete) { }


        //Puit

    }
}
