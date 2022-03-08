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
        public Mineur(int id, int positionX, int positionY, int fatigue, int sante, int faim, int soif) : base(id, positionX, positionY, fatigue, sante, faim, soif)
        {

        }
    }
}
