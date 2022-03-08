using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes
{
    abstract class Batiment:ObjetFixe
    {

        public Batiment(List<int[]> positions) : base(positions) { }

    }
}
