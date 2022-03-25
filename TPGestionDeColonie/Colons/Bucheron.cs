using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    class Bucheron:Colon
    {
        // Créer la liste des capacités de base
        // List<string> capacites
        public Bucheron(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
            
        }
        public override void Couper(int x, int y)
        {
            Tuple<int, int> positionArbre = new Tuple<int, int>(x, y);
            if (Planete.grille[x, y] == " A ")
            {
                StockRessources[2] += 10;
                Planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(positionArbre)).DestructionEnCours(x, y);
            }
        }
    }
}
