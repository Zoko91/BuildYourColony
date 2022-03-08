using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HELLO WORLD");

            Monde planete = new Monde();
            planete.GenererMonde();
        //planete.AfficherMonde();
       // https://github.com/Zoko91/Gestion-De-colonie/invitations

       // https://github.com/ensc-progav/projet-2022-beasse_goncalves

            Console.ReadLine();
        }
    }
}
