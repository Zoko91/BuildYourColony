using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Puits : Batiment
    {
        public Puits(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre,Monde planete) : base(positionObjet, coutBois, coutPierre,planete) { }



        public override void Construire(int positionX, int positionY)
        {//Créer une méthode Construire dans Colons qui appelle cette méthode une fois avoir instancié le batiment
            Planete.grille[positionX, positionY] = " P ";
        }

        //Puit

    }
}
