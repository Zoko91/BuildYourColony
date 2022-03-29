using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Entrepot:Batiment
    {
        protected int[] StockRessources{get;set;}
        public Entrepot(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre, Monde planete) : base(positionObjet,coutBois,coutPierre,planete) {
            StockRessources = new int[]{0,0,0};
        }

        public override void Construire(int positionX, int positionY)
        {//Créer une méthode Construire dans Colons qui appelle cette méthode une fois avoir instancié le batiment
            if (positionX + 1 > Planete.Hauteur-1 || positionX -1 <0) //Il commence au milieu
            {
                Console.WriteLine("Impossible de construire ici !");
            }
            else if (Planete.grille[positionX, positionY+1] == " x " && Planete.grille[positionX, positionY -1] == " x ")
            {
                Planete.grille[positionX, positionY] = " E ";
                Planete.grille[positionX, positionY - 1] = " E ";
                Planete.grille[positionX, positionY - 1] = " E ";
            }
            else
            {
                Console.WriteLine("Impossible de construire ici !");
            }
        }
        //Stockage des ressources
    }
}
