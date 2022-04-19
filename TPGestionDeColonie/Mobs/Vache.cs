using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.Mobs
{
    class Vache:Mob
    {
        // -----------------------------------------------------------------
        // Mob ayant un comportement automatique et passif (arrete de bouger) a l'approche d'un chasseur
        // [possède de la vie et donne de la viande lorsqu'il meurt] -- Non implémenté dans le jeu
        // -----------------------------------------------------------------

        public Vache(int id, int positionX, int positionY, int sante):base(id,positionX,positionY,sante) {}
    }
}
