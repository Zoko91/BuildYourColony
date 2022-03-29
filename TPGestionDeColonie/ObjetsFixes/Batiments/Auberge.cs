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

        public Auberge(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre, Monde planete) : base(positionObjet, coutBois, coutPierre, planete) { }


        public override void Construire(int positionX, int positionY)
        {//Créer une méthode Construire dans Colons qui appelle cette méthode une fois avoir instancié le batiment
            if (positionX + 1 > Planete.Hauteur-1 || positionY +1 > Planete.Largeur-1) // GetLength va de 1 à 30
            {
                Console.WriteLine("Impossible de construire ici !");
            }
            else if (Planete.grille[positionX, positionY + 1] == " x " && Planete.grille[positionX +1, positionY ] == " x " && Planete.grille[positionX + 1, positionY+1] == " x ")
            {
                Planete.grille[positionX, positionY] = " A ";
                Planete.grille[positionX + 1, positionY] = " A ";
                Planete.grille[positionX, positionY + 1] = " A ";
                Planete.grille[positionX + 1, positionY + 1] = " A ";
            }
            else
            {
                Console.WriteLine("Impossible de construire ici !");
            }
        }

        //Auberge fait perdre de la fatigue
    }

    
}
