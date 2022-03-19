using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    class Mineur:Colon
    {

        // Créer la liste des capacités de base
        // List<string> capacites
        public Mineur(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif) : base(nom, positionX, positionY, endurance, sante, faim, soif)
        {

        }
    }
}
