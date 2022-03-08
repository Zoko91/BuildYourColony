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
            planete.AfficherMonde();



            Console.ReadLine();
        }
    }
}
