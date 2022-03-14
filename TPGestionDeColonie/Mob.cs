using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    class 
        Mob
    {
        protected int id;
        protected int positionX;
        protected int positionY;
        protected int sante;

        public Mob(int id, int positionX, int positionY, int sante)
        {
            this.id = id;
            this.positionX = positionX;
            this.positionY = positionY;
            this.sante = sante;
        }



    }
}
