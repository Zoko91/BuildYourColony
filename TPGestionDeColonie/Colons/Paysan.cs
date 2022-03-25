using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;

namespace TPGestionDeColonie
{
    class Paysan:Colon
    {
        // Créer la liste des capacités de base
        // List<string> capacites
        public Paysan(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {}


        public override void Recolter(int x, int y)
        {
            Tuple<int, int> positionBle = new Tuple<int, int>(x, y);
            if (Planete.grille[x, y] == " A ")
            {

                StockRessources[2] += 10;
                Planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(positionBle)).DestructionEnCours(x, y);

            }
        }
        
        public override void Planter() {

            if (StockRessources[2] > 3)
            {
                Tuple<int, int> positionPaysan = getPosition();
                List<Tuple<int, int>> listePositionPaysan = new List<Tuple<int, int>>();


                listePositionPaysan.Add(positionPaysan);
                Ble nouveauBle = new Ble(listePositionPaysan, Planete);
                Planete.ListeBlocs.Add(nouveauBle);
                StockRessources[2] -= 3; 

            }
            else
            {
                   Console.WriteLine("Vous n'avez pas assez d'eau pour planter du blé.");   
            }
            

        } 
    }
}
