using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;
using TPGestionDeColonie.Colons;

namespace TPGestionDeColonie
{
    class Program
    {
        static void Main(string[] args)
        {
            Monde planete = new Monde();

            List<Colon> listeColons = CreerColonsDepart(planete);

            planete.GenererMonde();
            planete.AfficherMonde();
            Console.WriteLine();
            planete.AfficherFenetre(7,7);

            Console.WriteLine();
            Console.WriteLine();




            // ======= ZONE TEST FONCTIONS ======== //

            foreach (Colon col in listeColons)
            {
                Console.WriteLine(col.ToString());
                Console.WriteLine(col.getPosition());
                Console.WriteLine();
            }


            // ==================================== //


            Console.ReadLine();
        }

        public static List<Colon> CreerColonsDepart(Monde planete)
        {
            int milieuGrilleHauteur = (int)Math.Floor((double)planete.Hauteur/2);
            int milieuGrilleLargeur = (int)Math.Floor((double)planete.Largeur/2);
            List<Colon> listeDepart = new List<Colon>();

            Paysan p = new Paysan("p", milieuGrilleHauteur, milieuGrilleLargeur-1, 100, 100, 100, 100);
            Bucheron b = new Bucheron("p", milieuGrilleHauteur, milieuGrilleLargeur, 100, 100, 100, 100);
            Batisseur ba = new Batisseur("p", milieuGrilleHauteur, milieuGrilleLargeur+1, 100, 100, 100, 100);
            Mineur m = new Mineur("p", milieuGrilleHauteur+1, milieuGrilleLargeur-1, 100, 100, 100, 100);
            Tavernier t = new Tavernier("p", milieuGrilleHauteur+1, milieuGrilleHauteur, 100, 100, 100, 100);
            Chasseur c = new Chasseur("p", milieuGrilleHauteur+1, milieuGrilleLargeur+1, 100, 100, 100, 100);

            listeDepart.Add(p); 
            listeDepart.Add(b); 
            listeDepart.Add(ba); 
            listeDepart.Add(m); 
            listeDepart.Add(t); 
            listeDepart.Add(c); 
            
            for (int i = 0; i < listeDepart.Count; i++)
            {
                Console.Write($"Indiquez le nom du colon {listeDepart[i].GetType().Name} : ");
                string nom = Console.ReadLine();
                listeDepart[i].Nom = nom;
                planete.AjouterColon(listeDepart[i]);
            }
            return listeDepart;
        }



        // public void NouvelArrivant() { }

        
    


        
    } 
}
