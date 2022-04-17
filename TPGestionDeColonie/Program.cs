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
        /*
         * Exemple de comment faire fonctionner le menu => renvoie un entier lors de l'appui d'Entrée, -1 si Echap
        int selectedClass = ConsoleHelper.MultipleChoice(true, "Warrior", "Bard", "Mage", "Archer",
"Thief", "Assassin", "Cleric", "Paladin", "etc.");
        Console.WriteLine();
            Console.WriteLine(selectedClass);
        */

        
        
        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;
            /*
            Console.WriteLine("==========================");
            Console.WriteLine();
            Console.WriteLine("==========================");
            Console.SetCursorPosition(0, 1);
            */
            
            Monde planete = new Monde();

            List<Colon> listeColons = CreerColonsDepart(planete);
            
            planete.GenererMonde();
            planete.AfficherMonde();
            Console.WriteLine();
            //planete.AfficherFenetre(7,7);

            Console.WriteLine();
            Console.WriteLine();

/*
            Tuple<int,int> testTuple = new Tuple<int, int>(1,2);
            List<Tuple<int,int>> testList = new List<Tuple<int, int>>();
            testList.Add(testTuple);
            Arbre arb = new Arbre(testList, planete);
            Console.WriteLine(arb.GetPositionObjet().FirstOrDefault().Item1);
            Console.WriteLine(arb.GetPositionObjet().FirstOrDefault().Item2);

            Console.WriteLine(listeColons[1].CalculerDistancePlusProche(arb));
*/
            // ======= ZONE TEST FONCTIONS ======== //
            /*
            foreach (Colon col in listeColons)
            {
                Console.WriteLine(col.ToString());
                Console.WriteLine(col.getPosition());
                Console.WriteLine();
            }
            */


            
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());


            listeColons[0].SeDeplacerVersItem(x,y);
            // listeColons[1].Deplacer(0,25);
            // listeColons[2].Deplacer(3, 27);

            planete.MettreAJourMonde();

            // ==================================== //


            int targetX = listeColons[1].RechercherPlusProcheItem().Item1;
            int targetY = listeColons[1].RechercherPlusProcheItem().Item2;
            listeColons[1].SeDeplacerVersItem(targetX,targetY);

            planete.MettreAJourMonde();
            


            Console.ReadLine();
        }



        public static List<Colon> CreerColonsDepart(Monde planete)
        {
            int milieuGrilleHauteur = (int)Math.Floor((double)planete.Hauteur/2);
            int milieuGrilleLargeur = (int)Math.Floor((double)planete.Largeur/2);
            List<Colon> listeDepart = new List<Colon>();

            Paysan p = new Paysan("p", milieuGrilleHauteur, milieuGrilleLargeur-1, 100, 100, 100, 100, planete);
            Bucheron b1 = new Bucheron("p", milieuGrilleHauteur, milieuGrilleLargeur, 100, 100, 100, 100, planete);
            Batisseur ba = new Batisseur("p", milieuGrilleHauteur, milieuGrilleLargeur+1, 100, 100, 100, 100, planete);
            Mineur m = new Mineur("Mineur", milieuGrilleHauteur+1, milieuGrilleLargeur-1, 100, 100, 100, 100, planete);
            Tavernier t = new Tavernier("p", milieuGrilleHauteur+1, milieuGrilleHauteur, 100, 100, 100, 100, planete);
            Bucheron b2 = new Bucheron("p", milieuGrilleHauteur+1, milieuGrilleLargeur+1, 100, 100, 100, 100, planete);

            listeDepart.Add(p); 
            listeDepart.Add(b1); 
            listeDepart.Add(ba); 
            listeDepart.Add(m); 
            listeDepart.Add(t); 
            listeDepart.Add(b2); 
            
            for (int i = 0; i < listeDepart.Count; i++)
            {
                string demande = $"Indiquez le nom du colon {listeDepart[i].GetType().Name} : ";
                string space = new string(' ', demande.Length-2);
                //Console.Write(space);
                Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3);
                
                Console.WriteLine("╔" + new string('═',demande.Length*2) + "╗");
                Console.Write(new string(' ', Console.WindowWidth / 3));
                Console.WriteLine('║' + demande + new string(' ',demande.Length) + "║");
                Console.Write(new string(' ', Console.WindowWidth / 3));
                Console.WriteLine("╚" + new string('═', demande.Length * 2) + "╝");
                Console.SetCursorPosition(Console.WindowWidth / 3 + demande.Length +1, Console.WindowHeight / 3 + 1);


                string nom = Console.ReadLine();
                Console.Clear();
                listeDepart[i].Nom = nom;
                planete.AjouterColon(listeDepart[i]);
            }
            return listeDepart;
        }



        // public void NouvelArrivant() { } // quand nouveau colon arrive de façon random

        
    


        
    } 
}
