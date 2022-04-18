using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Maison:Batiment
    {


        public Maison(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre, Monde planete) : base(positionObjet, coutBois, coutPierre,planete) {}


        //Maison

    }
}
