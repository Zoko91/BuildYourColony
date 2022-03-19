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
        public Bucheron(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif) : base(nom, positionX, positionY, endurance, sante, faim, soif)
        {

        }
    }
}
