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
        public static class Mouse {
            
        }

        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;
  
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

            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());

            /*
            foreach (Colon c in listeColons)
            {             
                if (c.getId() == 1)
                {
                    c.Planter();
                }                 
            }*/

            listeColons[1].SeDeplacerVersItem(x,y);
            // listeColons[1].Deplacer(0,25);
            // listeColons[2].Deplacer(3, 27);

            planete.MettreAJourMonde();
            foreach (Colon col in listeColons)
            {
                Console.WriteLine(col.ToString());
                Console.WriteLine(col.getPosition());
                Console.WriteLine();
            }

            planete.AfficherMonde();

            /*
            Mineur bla = listeColons.FirstOrDefault(predicate: bla  =>  bla.Sante  ==  100);
            Console.WriteLine(bla.ToString());

            /*
            if (Mouse.LeftButton == MouseDownEvent)
            {
                Console.WriteLine("CLICK");
            }

            if (Mouse.LeftButton == MouseDownEvent)
            {
                Console.WriteLine("CLICK 2");
            }*/

            // ==================================== //



            Console.ReadLine();
        }



        public static List<Colon> CreerColonsDepart(Monde planete)
        {
            int milieuGrilleHauteur = (int)Math.Floor((double)planete.Hauteur/2);
            int milieuGrilleLargeur = (int)Math.Floor((double)planete.Largeur/2);
            List<Colon> listeDepart = new List<Colon>();

            Paysan p = new Paysan("p", milieuGrilleHauteur, milieuGrilleLargeur-1, 100, 100, 100, 100, planete);
            Bucheron b = new Bucheron("p", milieuGrilleHauteur, milieuGrilleLargeur, 100, 100, 100, 100, planete);
            Batisseur ba = new Batisseur("p", milieuGrilleHauteur, milieuGrilleLargeur+1, 100, 100, 100, 100, planete);
            Mineur m = new Mineur("Mineur", milieuGrilleHauteur+1, milieuGrilleLargeur-1, 100, 100, 100, 100, planete);
            Tavernier t = new Tavernier("p", milieuGrilleHauteur+1, milieuGrilleHauteur, 100, 100, 100, 100, planete);
            Chasseur c = new Chasseur("p", milieuGrilleHauteur+1, milieuGrilleLargeur+1, 100, 100, 100, 100, planete);

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



        // public void NouvelArrivant() { } // quand nouveau colon arrive de façon random

        
    


        
    } 
}
