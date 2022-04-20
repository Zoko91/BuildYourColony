using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;
using TPGestionDeColonie.ObjetsFixes.Batiments;
using TPGestionDeColonie.Colons;


namespace TPGestionDeColonie
{

    class Program
    {

        static void Main(string[] args)
        {
            
        // ---------------------------
        // Menu d'affichage du départ
        // ---------------------------
            
            int optionChoisie = 10;
            do
            {
                optionChoisie = MenuDepart();
                if (optionChoisie == 1)
                {
                    AfficherTutoriel();
                }
                else if (optionChoisie == 2)
                {
                    Environment.Exit(0);
                }
            } while (optionChoisie != 0);

        // -------------------------------------
        // Définition de la taille de la console
        // -------------------------------------

            //Console.WindowHeight = Console.LargestWindowHeight - 5;
            //Console.WindowWidth = Console.LargestWindowWidth - 25;

        // -------------------
        // Génération du monde 
        // -------------------

            Monde planete = new Monde();

            List<Colon> listeColons = CreerColonsDepart(planete);

            planete.GenererMonde();
            planete.AfficherMonde();

        // ---------------------------------------------------------------------
        // Gestion des tours de jeu et des propositions d'actions pour le joueur
        // ---------------------------------------------------------------------
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();



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

        // ---------------------------------------------------------------------------------------------------
        // Proposer une liste d'actions possibles que le joueur peut demander chaque tour en écrivant "manuel"
        // ---------------------------------------------------------------------------------------------------

            Console.WriteLine("======================================");
            Console.WriteLine("Liste des actions possibles :\n1 - Construire\n2 - Afficher l'état des colons\n3 - Afficher le stock de ressources des batiments\n4 - Planter (paysan)\n5 - Récolter (paysan)\n0 - STOP");
            Console.WriteLine("======================================");

        // ----------------------------------------------------------------------------------
        // Sélection de l'action et définition du comportement du jeu en fonction de l'action
        // ----------------------------------------------------------------------------------

            int numAction = int.Parse(Console.ReadLine());

            while (numAction > 6 || numAction < 0)
            {
                Console.WriteLine("Veuillez indiquez un numéro d'action correct :");
                numAction = int.Parse(Console.ReadLine());
            }
            if (numAction == 1)
            {
                // La batisseur se déplace vers la case ciblée par le joueur, une fois arrivé à la case on lui propose un listing des batiments a construire

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
            if (numAction == 2) 
            {

            // -------------------------
            // Affiche l'état des colons
            // -------------------------

                Console.WriteLine();
                Console.WriteLine(" /\\ ================= État des colons ================= /\\ ");
                Console.WriteLine();
                foreach (Colon col in planete.ListePJ)
                {
                    Console.WriteLine(col.ToString());
                }
                Console.WriteLine();
                Console.WriteLine(" /\\ ___________________________________________________ /\\ ");
                Console.WriteLine();
                Console.ReadLine();
                ProposerActions(planete);
            }
            if (numAction == 3) 
            {

            // ---------------------------------------------
            // Afficher le stock de ressources des batiments
            // ---------------------------------------------

                Console.WriteLine();
                Console.WriteLine(" /\\ ======== Stock de ressources des bâtiments ======== /\\ ");
                Console.WriteLine("\t________________________");
                foreach (Batiment bat in planete.ListeBatiments)
                {
                    if (bat.GetType() == typeof(Entrepot))
                    {
                        Console.WriteLine(bat.ToString());
                    }
                    if (bat.GetType() == typeof(Auberge))
                    {
                        Console.WriteLine(bat.ToString());
                    }
                }
                foreach (Colon col in planete.ListePJ)
                {
                    if (col.GetType() == typeof(Batisseur))
                    {
                        Batisseur bati = (Batisseur)col;
                        bati.ToString2();
                    }
                }
                Console.WriteLine();
                Console.WriteLine(" /\\ ___________________________________________________ /\\ ");
                Console.WriteLine();
                Console.ReadLine();
                ProposerActions(planete);
            }
            if (numAction == 4) 
            {

            // -----------------------------------------------------------------------
            // Déplace le paysan sur la case ciblée, une fois sur place, plante un blé
            // -----------------------------------------------------------------------

                foreach (Colon colon in planete.ListePJ)
                {
                    if (colon.GetType() == typeof(Paysan))
                    {
                        Paysan pay = (Paysan)colon;
                        Console.WriteLine("Ou souhaitez vous déplacer le Paysan pour qu'il plante du blé ?");
                        Console.Write("Coordonnée ligne: ");
                        int posX = int.Parse(Console.ReadLine());
                        Console.Write("Coordonnée colonne: ");
                        int posY = int.Parse(Console.ReadLine());
                        pay.DefinirCible(posX, posY);
                        pay.AcquerirCible(); // le paysan acquiert une cible
                    }
                    break;
                }


            }
            if (numAction == 5) // 
            {

            // ---------------------------------------------------------------------------------------------------------
            // Déplace le paysan sur la case ciblée, une fois sur place, récolte le blé en 2 tours si la case est un blé
            // ---------------------------------------------------------------------------------------------------------

                foreach (Colon colon in planete.ListePJ)
                {
                    if (colon.GetType() == typeof(Paysan))
                    {
                        Paysan pay = (Paysan)colon;
                        Console.WriteLine("Ou souhaitez vous déplacer le Paysan pour qu'il récolte le blé ?");
                        Console.Write("Coordonnée ligne: ");
                        int posX = int.Parse(Console.ReadLine());
                        Console.Write("Coordonnée colonne: ");
                        int posY = int.Parse(Console.ReadLine());
                        pay.DefinirCible(posX, posY);
                        pay.AcquerirCible(); // le paysan acquiert une cible
                    }
                    break;
                }
            }
        }

        public static List<Colon> CreerColonsDepart(Monde planete)
        {

        // --------------------------------------------------------------------------------------------------------------
        // Fonction créant la liste des colons au départ du jeu, les plaçant au milieu de la carte et renvoyant une liste
        // --------------------------------------------------------------------------------------------------------------

            int milieuGrilleHauteur = (int)Math.Floor((double)planete.Hauteur / 2);
            int milieuGrilleLargeur = (int)Math.Floor((double)planete.Largeur / 2);
            List<Colon> listeDepart = new List<Colon>();

            Paysan p = new Paysan("p", milieuGrilleHauteur, milieuGrilleLargeur - 1, 100, 100, 100, 100, planete);
            Bucheron b1 = new Bucheron("p", milieuGrilleHauteur, milieuGrilleLargeur, 100, 100, 100, 100, planete);
            Batisseur ba = new Batisseur("p", milieuGrilleHauteur, milieuGrilleLargeur + 1, 100, 100, 100, 100, planete);
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

        // -----------------------------------------------
        // On demande à l'utilisateur de nommer les colons
        // -----------------------------------------------
        
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
            return listeDepart;
        }

        public static void JouerUnTour(List<Colon> listeColons, Monde planete)
        {
            
        // ----------------------------------------------------------------------------------------------------------
        // Fonction principale du jeu définissant les actions automatiques et manuelles des colons chaque tour de jeu
        // ----------------------------------------------------------------------------------------------------------
            

            // JE ME SUIS ARRETE LA DANS LES COMMENTAIRES PROGRAM.CS


            //Console.Clear();
            foreach (Colon col in listeColons)
            {

                if (col.EtreRempli())
                {
                    col.BougerSiRempli();
                    // test enlever ciblage de l'objet en cours de destruction

                    // A MODIFIER : PRECISER LE CIBLAGE DE L'OBJET DEVANT PERDRE CIBLAGE
                    foreach (ObjetFixe obj in planete.ListeBlocs)
                    {
                        obj.NePlusEtreCible();
                    }
                }
                else if (col.EtreFatigue())
                {
                    if (planete.ListeBatiments.OfType<Maison>().Any())
                    {
                        Maison maisonCible = planete.ListeBatiments.OfType<Maison>().First();
                        col.DefinirCible(maisonCible.GetPositionObjet().First().Item1, maisonCible.GetPositionObjet().First().Item2);
                        col.AcquerirCible();
                        col.SeDeplacer(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                        maisonCible.SeReposer();
                    }
                }
                else if (col.ATIlCible() == false) // Si pas de cible définie
                {
                    if (col.GetType() == typeof(Bucheron) || col.GetType() == typeof(Mineur))
                    { // Bûcherons et mineurs doivent trouver l'objet le plus proche
                        Tuple<int, int> coords = col.RechercherPlusProcheItem();

                        int targetX = coords.Item1;
                        int targetY = coords.Item2;

                        // Déclare l'objet désigné comme étant ciblé
                        if (planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(new Tuple<int, int>(targetX, targetY))).EtreCible()==false)
                        {
                            planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(new Tuple<int, int>(targetX, targetY))).DevenirCible();
                            col.DefinirCible(targetX, targetY);
                            col.AcquerirCible(); // le colon a une cible
                        }
                        else
                        {
                            while (planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(new Tuple<int, int>(targetX, targetY))).EtreCible())
                            {
                                coords = col.RechercherPlusProcheItem();
                                targetX = coords.Item1;
                                targetY = coords.Item2;
                            }
                            planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(new Tuple<int, int>(targetX, targetY))).DevenirCible();
                            col.DefinirCible(targetX, targetY);
                            col.AcquerirCible(); // le colon a une cible
                        }
                        Console.WriteLine($" objet ciblé : {planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(new Tuple<int, int>(targetX, targetY)))}");
                        /*
                        foreach (ObjetFixe obj in planete.ListeBlocs)
                        { // définir l'objet le plus proche comme ciblé    
                            if (obj.GetPositionObjet().Contains(coords))
                            {
                                obj.DevenirCible();
                            }
                        }*/
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
                        if (numBat == 0)
                        {

                        }
                        else if (planete.ListeBatiments.OfType<Entrepot>().Any()) // s'il existe un entrepôt
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
                else if (col.GetType() == typeof(Paysan))
                {
                    if (!col.getPosition().Equals(col.RecupererCoordonneesCible()))
                    {
                        col.SeDeplacer(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                    }
                    else
                    {
                        Paysan pay = (Paysan)col;
                        bool surBle = false;
                        foreach (ObjetFixe obj in planete.ListeBlocs)
                        {
                            if (obj.GetPositionObjet().Contains(pay.getPosition()))
                            {
                                if (obj.GetType() == typeof(Ble))
                                {
                                    for (int i = 0; i < 2; i++)
                                    {
                                        pay.Recolter();
                                    }

                                    
                                    surBle = true;
                                    break;
                                }
                            }
                        }
                        if(surBle == false){
                            pay.Planter();
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

                col.VerififierEtat();
            }
            // Chercher ferme pour production

            if (planete.ListeBatiments.OfType<Ferme>().Any())
            {

                    int nbFermes = planete.ListeBatiments.OfType<Ferme>().Count();
                    Ferme[] arrayFerme = planete.ListeBatiments.OfType<Ferme>().ToArray();
                    for (int i = 0; i < nbFermes; i++){arrayFerme[i].ProductionBle();}                
            }

            planete.MettreAJourMonde();
        }

        public static int ChoixBatiment()
        {
            // ----------------------------------------------------------------
            // Récupère le numéro du batiment que le joueur souhaite construire
            // ----------------------------------------------------------------
            
            Console.WriteLine("Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
                    + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
                     + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)\n- 0 : Quitter");
            int numBat = int.Parse(Console.ReadLine());
            while (numBat > 6 || numBat < 0)
            {
                Console.WriteLine("Veuillez indiquer un numéro valide\n// ==================================== //");
                Console.WriteLine("Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
                + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
                + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)\n- 0 : Quitter");
                numBat = int.Parse(Console.ReadLine());
            }
            return numBat;
        }


        public static int MenuDepart()
        {
            // ----------------------------
            // Définition du menu de départ
            // ----------------------------
            
            
            /*
             
                     ██████╗ ██████╗ ██╗      ██████╗ ███╗   ██╗██╗███████╗
                    ██╔════╝██╔═══██╗██║     ██╔═══██╗████╗  ██║██║██╔════╝
                    ██║     ██║   ██║██║     ██║   ██║██╔██╗ ██║██║█████╗  
                    ██║     ██║   ██║██║     ██║   ██║██║╚██╗██║██║██╔══╝  
                    ╚██████╗╚██████╔╝███████╗╚██████╔╝██║ ╚████║██║███████╗
                     ╚═════╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝╚══════╝

             */
            Console.SetCursorPosition(Console.WindowWidth / 4, 2);
            Console.WriteLine(" ██████╗ ██████╗ ██╗      ██████╗ ███╗   ██╗██╗███████╗");
            Console.SetCursorPosition(Console.WindowWidth / 4, 3);
            Console.WriteLine("██╔════╝██╔═══██╗██║     ██╔═══██╗████╗  ██║██║██╔════╝");
            Console.SetCursorPosition(Console.WindowWidth / 4, 4);
            Console.WriteLine("██║     ██║   ██║██║     ██║   ██║██╔██╗ ██║██║█████╗  ");
            Console.SetCursorPosition(Console.WindowWidth / 4, 5);
            Console.WriteLine("██║     ██║   ██║██║     ██║   ██║██║╚██╗██║██║██╔══╝  ");
            Console.SetCursorPosition(Console.WindowWidth / 4, 6);
            Console.WriteLine("╚██████╗╚██████╔╝███████╗╚██████╔╝██║ ╚████║██║███████╗");
            Console.SetCursorPosition(Console.WindowWidth / 4, 7);
            Console.WriteLine(" ╚═════╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝╚══════╝");

            int selectionOption = ControleurMenu.Menu("Principal");
            Console.Clear();
            return selectionOption;
        }

        public static void AfficherTutoriel()
        {
            // ----------------------------
            // Définition du tutoriel
            // ----------------------------
            
                    Console.WriteLine();
                    Console.WriteLine("\t████████ ██    ██ ████████  ██████  ██████  ██ ███████ ██");
                    Console.WriteLine("\t   ██    ██    ██    ██    ██    ██ ██   ██ ██ ██      ██");
                    Console.WriteLine("\t   ██    ██    ██    ██    ██    ██ ██████  ██ █████   ██");
                    Console.WriteLine("\t   ██    ██    ██    ██    ██    ██ ██   ██ ██ ██      ██");
                    Console.WriteLine("\t   ██     ██████     ██     ██████  ██   ██ ██ ███████ ███████");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("\tBienvenue sur 'The Game You Wish You Had Known Before'\n\tNotre nouveau jeu de colonie en avant première !!!");
                    Console.WriteLine("\t__________________________________________________________________\n");
                    Console.WriteLine("\tComment prendre part au développement interractif du jeu en tour par tour?");
                    Console.WriteLine("\tChaque tour, vous avez la possibilité de réaliser une liste d'actions définies en écrivant 'manuel'");
                    Console.WriteLine("\tOu bien vous pouvez tout simplement laissez les colons opérer, mais attention, vous devrez prendre soin de la colonie !");
                    Console.WriteLine("\tIls ne pourront vivre sans vous, il vous faudra nécessairement interragir avec eux ! Pour débuter, je vous conseille de construire un entrepot");
                    Console.WriteLine("\tIl faudra très souvent s'enquérir des conditions de colons pour ne pas les perdre, car une fois mort, le colon ne reviendra pas !");
                    Console.WriteLine("\t__________________________________________________________________\n");
                    Console.WriteLine("\tInformations supplémentaires:\n");
                    Console.WriteLine("\tIl faut 4 tours à un bucheron ou un mineur pour casser un arbre ou respectivement un rocher");
                    Console.WriteLine("\tTandis qu'il ne faut que 2 tours au paysan pour récolter du blé");
                    Console.WriteLine("\tSeul le tavernier peut interragir avec le puits. Ce dernier peut servir des bevrages uniquement si il se situe dans l'auberge !");
                    Console.WriteLine("\tLa ferme produira du blé uniquement si le paysan est présent dedans");
                    Console.WriteLine("\t__________________________________________________________________\n\tAppuyez sur ENTR pour continuer...");
                    Console.WriteLine();
                    Console.ReadLine();
                    Console.Clear();
        }

    }
}
