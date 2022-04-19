using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    class Bucheron:Colon
    {
        // -----------------------------------------------------------------
        // Le Bucheron peut récolter les arbres présents sur la map
        // -----------------------------------------------------------------
        public Bucheron(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete) { }
       
        public override void Couper(int x, int y)
        {
            // Coupe un arbre, gagne 10 bois par coup
            Tuple<int, int> positionArbre = new Tuple<int, int>(x, y);
            if (Planete.grille[x, y] == " A ")
            {
                Backpack[0] += 10;
                Planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(positionArbre)).DestructionEnCours(x, y, this);
                //Planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(positionArbre)) a réutiliser pour enlever le ciblage
            }
        }
    }
}
