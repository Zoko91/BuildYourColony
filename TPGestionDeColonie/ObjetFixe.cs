using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    abstract class ObjetFixe
    {
        /*Liste de coordonnées d'un objet
         * Tuple Item1 = les x
         * Tuple Item2 = les y
         */
        protected List<Tuple<int, int>> positionObjet;
        
        public ObjetFixe (List<Tuple<int, int>> position)
        {
            this.positionObjet = position;
        }


        public List<Tuple<int, int>> GetPositionObjet()
        {
            return positionObjet;
        }

    }
}
