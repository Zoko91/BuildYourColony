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

        public Maison(List<int[]> positions, int coutBois, int coutPierre,int capacite) : base(positions, coutBois, coutPierre) { this.capacite = capacite; }

        

        //Maison

    }
}
