using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Entrepot:Batiment
    {
        public int[] StockRessources{get;set;}
        public Entrepot(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre, Monde planete) : base(positionObjet,coutBois,coutPierre,planete) {
            StockRessources = new int[]{0,0,0}; //bois / pierre / ble
        }

        //Stockage des ressources
    }
}
