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
            Monde planete = new Monde();
            planete.GenererMonde();
            planete.AfficherMonde();
            Console.WriteLine();
            planete.AfficherFenetre(7,7);

            Paysan papi = new Paysan(1, 7, 7,  15,  15,  15,  15);
            papi.SanteFaible();
            papi.EtreFatigue();
            papi.AvoirSoif();

            
            Console.WriteLine("Ceci est un test");

            Console.ReadLine();
        }

        // public void NouvelArrivant() { }
    }
}
