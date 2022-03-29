using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Ferme:Batiment
    {
        protected bool PresencePaysan
        {
            get;
            set;
        }
        
        // Si paysan, pousse et production ++, sinon --


        public Ferme(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre,Monde planete) : base(positionObjet, coutBois,coutPierre,planete) { }

        public override void Construire(int positionX, int positionY)
        {//Créer une méthode Construire dans Colons qui appelle cette méthode une fois avoir instancié le batiment
            if (positionX + 1 > Planete.Largeur || positionY +1  > Planete.Largeur) //Il commence au milieu
            {
                Console.WriteLine("Impossible de construire ici !");
            }
            else if (Planete.grille[positionX, positionY + 1] == " x " && Planete.grille[positionX + 1, positionY] == " x ")
            {
                Planete.grille[positionX, positionY] = " F ";
                Planete.grille[positionX + 1, positionY] = " F ";
                Planete.grille[positionX, positionY + 1] = " F ";
            }
            else
            {
                Console.WriteLine("Impossible de construire ici !");
            }
        }

        //Travail du paysan
    }
}
