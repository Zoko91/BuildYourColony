using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Entrepot:Batiment
    {
        // -----------------------------------------------------------------
        // Batiment servant de Stockage de ressources 
        // -----------------------------------------------------------------

        public int[] StockRessources{get;set;}
        public Entrepot(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre, Monde planete) : base(positionObjet,coutBois,coutPierre,planete) {
            StockRessources = new int[]{0,0,0}; //bois / pierre / ble
        }



        public override string ToString()
        {
            return $"\t{GetType().Name} :\n\tStock de bois: {StockRessources[0]}\n\tStock de pierre: {StockRessources[1]}\n\tStock de blé: {StockRessources[2]}\n\t________________________";
        }

        //Stockage des ressources
    }
}
