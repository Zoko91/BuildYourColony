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

            /*
            Console.WriteLine("==========================");
            Console.WriteLine();
            Console.WriteLine("==========================");
            Console.SetCursorPosition(0, 1);
            */
            Console.WindowHeight = 50;
            Console.WindowWidth = 200;

            Monde planete = new Monde();

            List<Colon> listeColons = CreerColonsDepart(planete);
            
            planete.GenererMonde();
            planete.AfficherMonde();
            Console.WriteLine();
            Console.WriteLine("===================================");    
            /*foreach(ObjetFixe obj in planete.ListeBlocs){
                    Tuple<int, int> position = obj.GetPositionObjet().FirstOrDefault();
                    Console.WriteLine(planete.ListeBlocs.Find(z => z.GetPositionObjet().First() == position));
                }
            Console.WriteLine("==================================="); */   
            //planete.AfficherFenetre(7,7);

            Console.WriteLine();
            Console.WriteLine();
            /*
            Console.WriteLine(planete.ListePJ[6].ToString());
            planete.ListePJ[6].Construire();
            Console.WriteLine("======== Affichage listeBatiments ========");
            foreach(Batiment bat in planete.ListeBatiments){
                Console.WriteLine(bat.GetType().Name);
            }
            Console.ReadLine();
            */
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
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());


            listeColons[0].SeDeplacerVersItem(x,y);
            */
            // listeColons[1].Deplacer(0,25);
            // listeColons[2].Deplacer(3, 27);
            /*
            foreach (Colon col in listeColons)
            {
                Console.WriteLine(col.ToString());
                Console.WriteLine(col.getPosition());
                Console.WriteLine();
            }
            */

            // ==================================== //

            /*
            int targetX = listeColons[1].RechercherPlusProcheItem().Item1;
            int targetY = listeColons[1].RechercherPlusProcheItem().Item2;
            listeColons[1].SeDeplacerVersItem(targetX,targetY);
            targetX = listeColons[3].RechercherPlusProcheItem().Item1;
            targetY = listeColons[3].RechercherPlusProcheItem().Item2;
            listeColons[3].SeDeplacerVersItem(targetX, targetY);

            planete.MettreAJourMonde();*/

            while(planete.GameOver()==false){
                Console.WriteLine();
                JouerUnTour(listeColons, planete);
                string test = Console.ReadLine();
                if (test == "manuel"){
                    Console.WriteLine("======================================");
                    Console.WriteLine("Liste des actions possibles :\n1 - Construire\n");
                    int numAction = int.Parse(Console.ReadLine());
                    while(numAction > 6 || numAction <1){
                        Console.WriteLine("Veuillez affichez un numéro d'action correct :");
                        numAction = int.Parse(Console.ReadLine());
                    }
                    if (numAction == 1){

                    }
                }
                
            }
        }



        public static List<Colon> CreerColonsDepart(Monde planete)
        {
            int milieuGrilleHauteur = (int)Math.Floor((double)planete.Hauteur/2);
            int milieuGrilleLargeur = (int)Math.Floor((double)planete.Largeur/2);
            List<Colon> listeDepart = new List<Colon>();

            Paysan p = new Paysan("p", milieuGrilleHauteur, milieuGrilleLargeur-1, 100, 100, 100, 100, planete);
            Bucheron b1 = new Bucheron("p", milieuGrilleHauteur, milieuGrilleLargeur, 100, 100, 100, 100, planete);
            Batisseur ba = new Batisseur("p", milieuGrilleHauteur, milieuGrilleLargeur+1, 100, 100, 100, 100, planete);
            Batisseur ba2 = new Batisseur("p", 2, 2, 100, 100, 100, 100, planete);

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
                // A supprimer après

            }
            planete.AjouterColon(ba2);
            return listeDepart;
        }

        public static void JouerUnTour(List<Colon> listeColons, Monde planete)
        {
            foreach(Colon col in listeColons)
            {
                if (col.GetType() == typeof(Bucheron) || col.GetType() == typeof(Mineur) ) // si le colon n'a pas déjà un batiment ciblé
                {
                    if (col.ATIlCible() == false)
                    {
                        Tuple<int, int> coords = col.RechercherPlusProcheItem();
                        
                        int targetX = coords.Item1;
                        int targetY = coords.Item2;

                        col.DefinirCible(targetX, targetY);
                        col.AcquerirCible(); // le colon a une cible
                        
                        foreach(ObjetFixe obj in planete.ListeBlocs)
                        { // définir l'objet le plus proche comme ciblé    
                            if (obj.GetPositionObjet().Contains(coords)){
                                obj.DevenirCible();
                            }
                        }
                        col.SeDeplacerVersItem(targetX, targetY);
                  
                    }

                    else if (!col.getPosition().Equals(col.PlusProcheDistanceVersItem(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2))) // si le colon a déjà une cible et n'est pas sur la case adjacente
                    {
                        Console.WriteLine("position trouvée");
                        col.SeDeplacerVersItem(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                    }
                    else if(col.getPosition().Equals(col.PlusProcheDistanceVersItem(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2)))
                    {
                        if (col.GetType() == typeof(Bucheron))
                        {
                            col.Couper(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                        }
                        else if (col.GetType() == typeof(Mineur))
                        {
                            col.Miner(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                        }
                    }
                }
                //Console.WriteLine(col.ToString());    
                //Console.WriteLine(planete.ListeBlocs.ToString());
            }
           

            planete.MettreAJourMonde();
        }

        // public void NouvelArrivant() { } // quand nouveau colon arrive de façon random

        
    


        
    } 
}
