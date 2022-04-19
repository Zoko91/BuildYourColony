using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;
using TPGestionDeColonie.ObjetsFixes.Batiments;
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

            Monde planete = new Monde();

            List<Colon> listeColons = CreerColonsDepart(planete);

            planete.GenererMonde();
            planete.AfficherMonde();
            Console.WriteLine();
            Console.WriteLine("===================================");

            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();


            while (planete.GameOver() == false)
            {
                Console.WriteLine();
                JouerUnTour(listeColons, planete);
                string test = Console.ReadLine();
                if (test == "manuel")
                {
                    ProposerActions(planete);
                }

            }
        }

        public static void ProposerActions(Monde planete)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Liste des actions possibles :\n1 - Construire\n2 - Afficher l'état des colons\n3 - Afficher le stock de ressources des batiments\n4 - Planter (paysan)\n5 - Récolter (paysan)\n0 - STOP");
            Console.WriteLine("======================================");
            int numAction = int.Parse(Console.ReadLine());
            while (numAction > 6 || numAction < 0)
            {
                Console.WriteLine("Veuillez indiquez un numéro d'action correct :");
                numAction = int.Parse(Console.ReadLine());
            }
            if (numAction == 1)
            {
                foreach (Colon colon in planete.ListePJ)
                {
                    if (colon.GetType() == typeof(Batisseur))
                    {
                        Batisseur bat = (Batisseur)colon;
                        if (bat.EstOccupe)
                        {
                            Console.WriteLine($"Le batisseur {bat.Nom} est occupé");
                            ProposerActions(planete);
                        }
                        else
                        {
                            Console.WriteLine("Ou souhaitez vous déplacer le Batisseur pour construire votre batiment?");
                            Console.Write("Coordonnée ligne: ");
                            int posX = int.Parse(Console.ReadLine());
                            Console.Write("Coordonnée colonne: ");
                            int posY = int.Parse(Console.ReadLine());

                            bat.DefinirCible(posX, posY);
                            bat.AcquerirCible(); // le batisseur acquiert une cible
                        }
                        break;
                    }
                }

            }
            if(numAction == 2) // Afficher l'état des colons
            {
                Console.WriteLine();
                Console.WriteLine(" /\\ ================= État des colons ================= /\\ ");
                Console.WriteLine();
                foreach(Colon col in planete.ListePJ)
                {
                    Console.WriteLine(col.ToString());
                }
                Console.WriteLine();
                Console.WriteLine(" /\\ ___________________________________________________ /\\ ");
                Console.WriteLine();
                Console.ReadLine();
                ProposerActions(planete);
            }
            if (numAction == 3) // Afficher le stock de ressources des batiments
            {
                Console.WriteLine();
                Console.WriteLine(" /\\ ======== Stock de ressources des bâtiments ======== /\\ ");
                Console.WriteLine("\t________________________");
                foreach (Batiment bat in planete.ListeBatiments)
                {
                    if(bat.GetType() == typeof(Entrepot))
                    {
                        Console.WriteLine(bat.ToString());
                    }
                    if(bat.GetType() == typeof(Auberge))
                    {
                        Console.WriteLine(bat.ToString());
                    }
                }
                Console.WriteLine();
                Console.WriteLine(" /\\ ___________________________________________________ /\\ ");
                Console.WriteLine();
                Console.ReadLine();
                ProposerActions(planete);
            }
            if (numAction == 4) // Planter (paysan)
            {

            }
            if (numAction == 5) // Récolter (paysan)
            {

            }
        }

        public static List<Colon> CreerColonsDepart(Monde planete)
        {
            int milieuGrilleHauteur = (int)Math.Floor((double)planete.Hauteur / 2);
            int milieuGrilleLargeur = (int)Math.Floor((double)planete.Largeur / 2);
            List<Colon> listeDepart = new List<Colon>();

            Paysan p = new Paysan("p", milieuGrilleHauteur, milieuGrilleLargeur - 1, 100, 100, 100, 100, planete);
            Bucheron b1 = new Bucheron("p", milieuGrilleHauteur, milieuGrilleLargeur, 100, 100, 100, 100, planete);
            Batisseur ba = new Batisseur("p", milieuGrilleHauteur, milieuGrilleLargeur + 1, 100, 100, 100, 100, planete);
            Batisseur ba2 = new Batisseur("p", 2, 2, 100, 100, 100, 100, planete);

            Mineur m = new Mineur("Mineur", milieuGrilleHauteur + 1, milieuGrilleLargeur - 1, 100, 100, 100, 100, planete);
            Tavernier t = new Tavernier("p", milieuGrilleHauteur + 1, milieuGrilleHauteur, 100, 100, 100, 100, planete);
            Bucheron b2 = new Bucheron("p", milieuGrilleHauteur + 1, milieuGrilleLargeur + 1, 100, 100, 100, 100, planete);

            listeDepart.Add(p);
            listeDepart.Add(b1);
            listeDepart.Add(ba);
            listeDepart.Add(m);
            listeDepart.Add(t);
            listeDepart.Add(b2);

            for (int i = 0; i < listeDepart.Count; i++)
            {
                string demande = $"Indiquez le nom du colon {listeDepart[i].GetType().Name} : ";
                string space = new string(' ', demande.Length - 2);
                //Console.Write(space);
                Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3);

                Console.WriteLine("╔" + new string('═', demande.Length * 2) + "╗");
                Console.Write(new string(' ', Console.WindowWidth / 3));
                Console.WriteLine('║' + demande + new string(' ', demande.Length) + "║");
                Console.Write(new string(' ', Console.WindowWidth / 3));
                Console.WriteLine("╚" + new string('═', demande.Length * 2) + "╝");
                Console.SetCursorPosition(Console.WindowWidth / 3 + demande.Length + 1, Console.WindowHeight / 3 + 1);

                string nom = Console.ReadLine();
                Console.Clear();
                listeDepart[i].Nom = nom;
                planete.AjouterColon(listeDepart[i]);
            }
            planete.AjouterColon(ba2);
            return listeDepart;
        }

        public static void JouerUnTour(List<Colon> listeColons, Monde planete)
        {
            //Console.Clear();
            foreach (Colon col in listeColons)
            {
                Console.WriteLine(col.Endurance);
                if (col.EtreRempli())
                {
                    Console.WriteLine(string.Join("/",col.Backpack));
                    col.BougerSiRempli();
                    // test enlever ciblage de l'objet en cours de destruction
                    foreach(ObjetFixe obj in planete.ListeBlocs)
                    {
                        obj.NePlusEtreCible();
                    }
                }

                else if (col.ATIlCible() == false) // Si pas de cible définie
                {
                    if (col.GetType() == typeof(Bucheron) || col.GetType() == typeof(Mineur))
                    { // Bucherons et mineurs doivent trouver l'objet le plus proche
                        Tuple<int, int> coords = col.RechercherPlusProcheItem();

                        int targetX = coords.Item1;
                        int targetY = coords.Item2;

                        col.DefinirCible(targetX, targetY);
                        col.AcquerirCible(); // le colon a une cible

                        foreach (ObjetFixe obj in planete.ListeBlocs)
                        { // définir l'objet le plus proche comme ciblé    
                            if (obj.GetPositionObjet().Contains(coords))
                            {
                                obj.DevenirCible();
                            }
                        }
                        col.SeDeplacerVersItem(targetX, targetY);
                    }
                }
                else if (col.GetType() == typeof(Batisseur))
                {
                    if (!col.getPosition().Equals(col.RecupererCoordonneesCible()))
                    {
                        col.SeDeplacer(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                    }
                    else
                    {
                        Batisseur bat = (Batisseur)col;
                        int numBat = ChoixBatiment();
                        if (planete.ListeBatiments.OfType<Entrepot>().Any()) // s'il existe un entrepôt
                        {
                            if (planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet().Contains(bat.getPosition()))
                            {
                                bat.RemplirLeStock(numBat);
                            }
                            else
                            {
                                bat.Construire(numBat);
                            }
                        }
                        else
                        {
                            while (numBat != 1) // Première construction : Il ne peut construire qu'un entrepot car ce batiment est essentiel
                            {
                                Console.WriteLine("/!\\ Il faut construire un entrepôt pour bien débuter");
                                numBat = ChoixBatiment();
                            }
                            bat.Construire(numBat);
                        }
                    }
                }
                else if (!col.getPosition().Equals(col.PlusProcheDistanceVersItem(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2))) // si le colon a déjà une cible et n'est pas sur la case adjacente
                {
                    col.SeDeplacerVersItem(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                }
                else if (col.getPosition().Equals(col.PlusProcheDistanceVersItem(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2)))
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
            planete.MettreAJourMonde();
        }

        public static int ChoixBatiment()
        {
            Console.WriteLine("Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
                    + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
                     + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)");
            int numBat = int.Parse(Console.ReadLine());
            while (numBat > 6 || numBat < 0)
            {
                Console.WriteLine("Veuillez indiquer un numéro valide\n// ==================================== //");
                Console.WriteLine("Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
                + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
                + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)");
                numBat = int.Parse(Console.ReadLine());
            }
            return numBat;
        }

        // public void NouvelArrivant() { } // quand nouveau colon arrive de façon random






    }
}
