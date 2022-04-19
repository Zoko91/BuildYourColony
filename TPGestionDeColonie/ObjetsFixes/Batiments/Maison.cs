using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Maison:Batiment
    {
        // --------------------------------------------------------------------
        // La Maison fait diminuer la fatigue du Colon si il est présent dedans
        // --------------------------------------------------------------------

        public Maison(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre, Monde planete) : base(positionObjet, coutBois, coutPierre,planete) {}

        

        public void SeReposer(){
            foreach(Colon col in Planete.ListePJ){
                if(positionObjet.Contains(col.getPosition())){
                    if (col.Endurance >=100)
                    {
                        
                    }
                    else
                    {
                        col.Endurance += 10;
                    }
                }
            }
        }
    }
}
