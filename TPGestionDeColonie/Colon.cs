using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    abstract class Colon
    {

        protected int id;
        protected int positionX;
        protected int positionY;
        protected int fatigue;
        protected int sante;
        protected int faim;
        protected int soif;
        protected List<string> capacites;

        public Colon (int id, int positionX, int positionY, int fatigue, int sante, int faim, int soif) //, List<string> capacites
        {
            this.id = id;
            this.positionX = positionX;
            this.positionY= positionY;
            this.fatigue = fatigue;
            this.sante = sante;
            this.faim = faim;
            this.soif = soif;
        }

        // public int santeRestante() { }
        // public int donnerPosition() { }
        // public int afficherFaim() { }

    }
}
