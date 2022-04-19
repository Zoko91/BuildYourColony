using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.Mobs
{
    class Loup:Mob
    {
        // -----------------------------------------------------------------
        // Monstre ayant un comportement automatique et aggressif a l'approche d'un colon
        // [possède de la vie et donne de la viande lorsqu'il meurt] -- Non implémenté dans le jeu
        // -----------------------------------------------------------------

        public Loup(int id, int positionX, int positionY, int sante) : base(id,positionX,positionY,sante)
        {

        }
    }
}
