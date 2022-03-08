using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    abstract class ObjetFixe
    {
        protected List<int[]> positions;  //positions des obstacles naturels 
        

        public ObjetFixe (List<int[]> positions)
        {
            this.positions = positions;
        }
    }
}
