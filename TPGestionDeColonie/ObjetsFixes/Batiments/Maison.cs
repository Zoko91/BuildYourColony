using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Maison:Batiment
    {

        int capacite;

        public Maison(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre,int capacite, Monde planete) : base(positionObjet, coutBois, coutPierre,planete) { this.capacite = capacite; }

        

        //Maison

    }
}
