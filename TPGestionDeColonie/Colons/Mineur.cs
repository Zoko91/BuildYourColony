using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;
using TPGestionDeColonie;


namespace TPGestionDeColonie
{
     class Mineur:Colon
    {
        // Créer la liste des capacités de base
        // List<string> capacites
        public Mineur(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
        }

         public override void Miner(int x, int y)
         {
             Tuple<int, int> positionRocher = new Tuple<int, int>(x, y);
             if(Planete.grille[x,y] == " R ")
             {
                //Planete.ListeBlocs
                Backpack[1] += 10;
                //Console.WriteLine(Planete.ListeBlocs[0].GetPositionObjet().First());
                Planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(positionRocher)).DestructionEnCours(x,y, this);

             }
         }


    }
}
