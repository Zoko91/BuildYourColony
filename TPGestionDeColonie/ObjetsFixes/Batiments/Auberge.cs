using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Auberge:Batiment
    {
        // -----------------------------------------------------------------
        // Batiment ou les Colons se désaltèrent et peuvent également manger
        // -----------------------------------------------------------------
        public bool PresenceTavernier { get; set; }
        public int[] StockRessources{ get ; set; }

        public Auberge(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre, Monde planete) : base(
            positionObjet, coutBois, coutPierre, planete)
        {
            StockRessources = new int[] {40, 40}; // Eau, Blé
        }

        public override string ToString()
        {
            return $"\t{GetType().Name} :\n\tStock d'eau: {StockRessources[0]}\n\tStock de Blé: {StockRessources[1]}\n\t________________________";
        }
    }

    
}
