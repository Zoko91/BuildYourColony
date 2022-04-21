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

            Console.Title = "The Game You Wish You Had Known Before || BEASSE Joseph - GONCALVES Tristan";
            Console.SetWindowPosition(0, 0);

            // -------------------------------------
            // Définition de la taille de la console
            // -------------------------------------

            Console.WindowHeight = Console.LargestWindowHeight - 5;
            Console.WindowWidth = Console.LargestWindowWidth - 25;



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

            Console.ReadLine();



            while (planete.GameOver() == false)
            {
                Console.WriteLine();
                JouerUnTour(listeColons, planete);
                Console.Write("\t");
                string test = Console.ReadLine();
                if (test == "manuel")
                {
                    ProposerActions(planete);
                }

            }
            AfficherGameOver();

        }

        public static void ProposerActions(Monde planete)
        {

            // ---------------------------------------------------------------------------------------------------
            // Proposer une liste d'actions possibles que le joueur peut demander chaque tour en écrivant "manuel"
            // ---------------------------------------------------------------------------------------------------

            Console.WriteLine("\t======================================");
            Console.WriteLine(
                "\tListe des actions possibles :\n\t1 - Construire\n\t2 - Afficher l'état des colons\n\t3 - Afficher les Backpacks de colons\n\t4 - Afficher le stock de ressources des bâtiments\n\t5 - Planter (paysan)\n\t6 - Récolter / Déplacement (paysan)\n\t0 - STOP");
            Console.WriteLine("\t======================================");

            // ----------------------------------------------------------------------------------
            // Sélection de l'action et définition du comportement du jeu en fonction de l'action
            // ----------------------------------------------------------------------------------

            string ans = Console.ReadLine();
            string[] dicoReponses = {"1", "2", "3", "4", "5", "0"};
            while (!dicoReponses.Contains(ans))
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
                    + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
                    + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)\n- 0 : Quitter");
                ans = Console.ReadLine();
            }

            int numAction = int.Parse(ans);


            while (numAction > 7 || numAction < 0)
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
                        Batisseur bat = (Batisseur) colon;
                        if (bat.EstOccupe)
                        {
                            Console.WriteLine($"Le batisseur {bat.Nom} est occupé");
                            ProposerActions(planete);
                        }
                        else
                        {
                            Console.WriteLine(
                                "Ou souhaitez vous déplacer le Batisseur pour construire votre batiment?");
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
                Console.WriteLine("Appuyez sur Entrée pour continuer ...");
                Console.ReadLine();
                ProposerActions(planete);

            }

            if (numAction == 3) // 
            {

                // ---------------------------------------------------------------------------------------------------------
                // Affiche les backpacks des colons
                // ---------------------------------------------------------------------------------------------------------

                foreach (Colon colon in planete.ListePJ)
                {
                    Console.WriteLine($"Backpack du colon {colon.GetType().Name} {colon.Nom} : {BackpackColon(colon)}");
                }

                Console.WriteLine("Appuyez sur Entrée pour continuer ...");
                Console.ReadLine();
                ProposerActions(planete);
            }

            if (numAction == 4)
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
                        Batisseur bati = (Batisseur) col;
                        bati.ToString2();
                    }
                }

                Console.WriteLine();
                Console.WriteLine(" /\\ ___________________________________________________ /\\ ");
                Console.WriteLine("Appuyez sur Entrée pour continuer ...");
                Console.ReadLine();
                ProposerActions(planete);
            }

            if (numAction == 5)
            {

                // -----------------------------------------------------------------------
                // Déplace le paysan sur la case ciblée, une fois sur place, plante un blé
                // -----------------------------------------------------------------------

                foreach (Colon colon in planete.ListePJ)
                {
                    if (colon.GetType() == typeof(Paysan))
                    {
                        Paysan pay = (Paysan) colon;
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

            if (numAction == 6) // 
            {

                // ---------------------------------------------------------------------------------------------------------
                // Déplace le paysan sur la case ciblée, une fois sur place, récolte le blé en 2 tours si la case est un blé
                // ---------------------------------------------------------------------------------------------------------

                Console.WriteLine("\tDéplacer le colon vers du blé le fera récolter");
                Console.WriteLine("\tDéplacer le colon vers la ferme le fera produire du blé");

                foreach (Colon colon in planete.ListePJ)
                {
                    if (colon.GetType() == typeof(Paysan))
                    {
                        Paysan pay = (Paysan) colon;
                        Console.WriteLine("Ou souhaitez vous déplacer le Paysan ?");
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

            int milieuGrilleHauteur = (int) Math.Floor((double) planete.Hauteur / 2);
            int milieuGrilleLargeur = (int) Math.Floor((double) planete.Largeur / 2);
            List<Colon> listeDepart = new List<Colon>();

            Paysan p = new Paysan("p", milieuGrilleHauteur, milieuGrilleLargeur - 1, 100, 100, 100, 100, planete);
            Bucheron b1 = new Bucheron("p", milieuGrilleHauteur, milieuGrilleLargeur, 100, 100, 100, 100, planete);
            Batisseur ba = new Batisseur("p", milieuGrilleHauteur, milieuGrilleLargeur + 1, 100, 100, 100, 100,
                planete);
            Mineur m = new Mineur("Mineur", milieuGrilleHauteur + 1, milieuGrilleLargeur - 1, 100, 100, 100, 100,
                planete);
            Tavernier t = new Tavernier("p", milieuGrilleHauteur + 1, milieuGrilleHauteur, 100, 100, 100, 100, planete);
            Bucheron b2 = new Bucheron("p", milieuGrilleHauteur + 1, milieuGrilleLargeur + 1, 100, 100, 100, 100,
                planete);

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


            Random rng = new Random();
            Console.Clear();
            
            // -------------------------------------------------------------------------------------------
            // Parcours les états des colons pour savoir quels sont les problèmes de santé ou d'inventaire
            // -------------------------------------------------------------------------------------------
            
            foreach (Colon col in listeColons)
            {

                if (col.EtreRempli()) 
                {
                    foreach (ObjetFixe obj in planete.ListeBlocs)
                    {
                        if (obj.GetPositionObjet().Contains(col.RecupererCoordonneesCible()))
                        {
                            obj.NePlusEtreCible();
                        }
                    }

                    col.BougerSiRempli();
                }

                else if (col.EtreFatigue()) // Si il est fatigué....
                {
                    if (col.ATIlCible())
                    {
                        foreach (ObjetFixe obj in planete.ListeBlocs)
                        {
                            if (obj.GetPositionObjet().Contains(col.RecupererCoordonneesCible()))
                            {
                                obj.NePlusEtreCible();
                            }
                        }
                    }

                    if (planete.ListeBatiments.OfType<Maison>().Any())
                    {
                        Maison maisonCible = planete.ListeBatiments.OfType<Maison>().First();

                        col.DefinirCible(maisonCible.GetPositionObjet().First().Item1,
                            maisonCible.GetPositionObjet().First().Item2);
                        col.AcquerirCible();
                        col.SeDeplacer(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                        maisonCible.SeReposer();
                    }
                }
                else if (col.AvoirFaim() || col.AvoirSoif()) // Si il a soif ou si il a faim ....
                {
                    if (col.ATIlCible())
                    {
                        foreach (ObjetFixe obj in planete.ListeBlocs)
                        {
                            if (obj.GetPositionObjet().Contains(col.RecupererCoordonneesCible()))
                            {
                                obj.NePlusEtreCible();
                            }
                        }
                    }

                    if (planete.ListeBatiments.OfType<Auberge>().Any())
                    {
                        Auberge aubergeCible = planete.ListeBatiments.OfType<Auberge>().First();

                        col.DefinirCible(aubergeCible.GetPositionObjet().First().Item1,
                            aubergeCible.GetPositionObjet().First().Item2);
                        col.AcquerirCible();
                        if (!col.getPosition()
                                .Equals(col
                                    .RecupererCoordonneesCible())) // On peut changer ça et mettre si les coordonnées colons sont comprises dans l'auberge
                        {
                            col.SeDeplacer(col.RecupererCoordonneesCible().Item1,
                                col.RecupererCoordonneesCible().Item2);
                        }
                        else
                        {
                            if (col.AvoirFaim())
                            {
                                if (aubergeCible.StockRessources[1] > 15)
                                {
                                    col.Faim += 15;
                                    aubergeCible.StockRessources[1] -= 15;
                                }
                                else if (aubergeCible.StockRessources[1] >= 0)
                                {
                                    col.Faim += aubergeCible.StockRessources[1];
                                    aubergeCible.StockRessources[1] = 0;
                                }
                                else
                                {
                                    Console.WriteLine("Il n'y a pas assez de blé, pensez à appeler le paysan !");
                                }
                            }
                            else if (col.AvoirSoif())
                            {
                                if (aubergeCible.StockRessources[0] > 15)
                                {
                                    col.Faim += 15;
                                    aubergeCible.StockRessources[0] -= 15;
                                }
                                else if (aubergeCible.StockRessources[0] >= 0)
                                {
                                    col.Faim += aubergeCible.StockRessources[1];
                                    aubergeCible.StockRessources[0] = 0;
                                }
                                else
                                {
                                    Console.WriteLine("Il n'y a pas assez d'eau, pensez à construire un puits !");
                                }
                            }

                            if (!col.AvoirFaim() || !col.AvoirSoif())
                            {
                                col.PerdreCible();
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            "Pensez à construire une auberge, c'est URGENT, vos colons ont faim et soif !");
                    }

                }
                else if (col.ATIlCible() == false) // Si pas de cible définie
                {
                    if (col.GetType() == typeof(Bucheron) || col.GetType() == typeof(Mineur))
                    {
                        // Bûcherons et mineurs doivent trouver l'objet le plus proche

                        Tuple<int, int> coords = col.RechercherPlusProcheItem();

                        int targetX = coords.Item1;
                        int targetY = coords.Item2;

                        col.DefinirCible(targetX, targetY);
                        col.AcquerirCible(); // le colon a une cible
                        foreach (ObjetFixe obj in planete.ListeBlocs)
                        {
                            // définir l'objet le plus proche comme ciblé    
                            if (obj.GetPositionObjet().Contains(coords))
                            {
                                obj.DevenirCible();
                            }
                        }

                        col.SeDeplacerVersItem(targetX, targetY);
                    }
                    else if (col.GetType() == typeof(Tavernier))
                    {
                        if (planete.ListeBatiments.OfType<Auberge>().Any())
                        {
                            Tavernier tav = (Tavernier) col;
                            Auberge aub = planete.ListeBatiments.OfType<Auberge>().FirstOrDefault();
                            if (!aub.GetPositionObjet().Contains(tav.getPosition()))
                            {
                                tav.AcquerirCible();
                                tav.DefinirCible(aub.GetPositionObjet().FirstOrDefault().Item1,
                                    aub.GetPositionObjet().FirstOrDefault().Item2);
                            }
                            else
                            {
                                tav.AllerRemplirRessource();
                            }
                        }
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
                        Batisseur bat = (Batisseur) col;

                        int numBat = ChoixBatiment(planete);

                        if (numBat == 0)
                        {
                            bat.PerdreCible();
                        }
                        else if (planete.ListeBatiments.OfType<Entrepot>().Any()) // s'il existe un entrepôt
                        {
                            if (planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()
                                .Contains(bat.getPosition()))
                            {
                                string[] dicoBatiments =
                                    {"l'Entrepôt", "la Taverne", "la Maison", "le Puits", "la Ferme"};
                                Console.WriteLine(
                                    $"Le Batisseur {bat.Nom} a rempli son stock de ressources pour construire {dicoBatiments[numBat]}");
                                bat.RemplirLeStock(numBat);
                            }
                            else
                            {
                                bat.Construire(numBat);
                            }
                        }
                        else
                        {
                            while
                                (numBat !=
                                 1) // Première construction : Il ne peut construire qu'un entrepot car ce batiment est essentiel
                            {
                                Console.WriteLine("/!\\ Il faut construire un entrepôt pour bien débuter");
                                numBat = ChoixBatiment(planete);
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
                        Paysan pay = (Paysan) col;
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

                        if (surBle == false)
                        {
                            pay.Planter();
                        }
                    }
                }
                else if (col.GetType() == typeof(Tavernier))
                {

                    Tavernier tavernier = (Tavernier) col;
                    if (planete.ListeBatiments.OfType<Auberge>().Any())
                    {
                        planete.ListeBatiments.OfType<Auberge>().FirstOrDefault().PresenceTavernier = false;
                    }

                    if (!col.getPosition()
                            .Equals(col
                                .RecupererCoordonneesCible())) //|| !col.getPosition().Equals(col.PlusProcheDistanceVersItem(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2))
                    {


                        Tuple<int, int> coordCible = col.RecupererCoordonneesCible();
                        Auberge aub = planete.ListeBatiments.OfType<Auberge>().FirstOrDefault();

                        col.SeDeplacer(col.RecupererCoordonneesCible().Item1,
                            col.RecupererCoordonneesCible().Item2); //car on veut rentrer dans l'auberge ...

                    }
                    else
                    {
                        foreach (Batiment bat in planete.ListeBatiments)
                        {
                            if (bat.GetType() == typeof(Puits))
                            {
                                if (bat.GetPositionObjet().Contains(col.RecupererCoordonneesCible()))
                                {
                                    if (tavernier.Backpack[3] < 100)
                                    {
                                        Console.WriteLine($"Remplissage.... {col.Backpack[3]}%");
                                        tavernier.RemplirLeSeau();
                                    }
                                    else
                                    {
                                        tavernier.PerdreCible();
                                    }
                                }

                            }
                            else if (bat.GetType() == typeof(Auberge) &&
                                     bat.GetPositionObjet().Contains(col.getPosition()))
                            {
                                tavernier.PerdreCible();
                                Auberge aub = (Auberge) bat;
                                aub.PresenceTavernier = true;
                                if (tavernier.Backpack[3] > 0)
                                {
                                    aub.StockRessources[0] += tavernier.Backpack[3];
                                    tavernier.Backpack[3] = 0;
                                }
                            }
                        }
                    }
                }

                else if (!col.getPosition().Equals(col.PlusProcheDistanceVersItem(col.RecupererCoordonneesCible().Item1,
                             col.RecupererCoordonneesCible()
                                 .Item2))) // si le colon a déjà une cible et n'est pas sur la case adjacente
                {
                    col.SeDeplacerVersItem(col.RecupererCoordonneesCible().Item1,
                        col.RecupererCoordonneesCible().Item2);
                }
                else if (col.getPosition().Equals(col.PlusProcheDistanceVersItem(col.RecupererCoordonneesCible().Item1,
                             col.RecupererCoordonneesCible().Item2)))
                {
                    // colon arrivé à la case adjacente, va pouvoir se mettre à couper/miner
                    if (col.GetType() == typeof(Bucheron))
                    {
                        col.Couper(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                    }
                    else if (col.GetType() == typeof(Mineur))
                    {
                        col.Miner(col.RecupererCoordonneesCible().Item1, col.RecupererCoordonneesCible().Item2);
                    }
                }

                if (rng.Next(0, 5) == 0)
                {
                    col.Soif -= 2; // Avoir soif un tour sur 4
                }

                col.VerififierEtat(); // Vérifie l'état vital du colon
            }
            // Chercher ferme pour production

            if (planete.ListeBatiments.OfType<Ferme>().Any())
            {
                int nbFermes = planete.ListeBatiments.OfType<Ferme>().Count();
                Ferme[] arrayFerme = planete.ListeBatiments.OfType<Ferme>().ToArray();
                // va faire appel à la méthode de production dans toutes les fermes de la map
                for (int i = 0; i < nbFermes; i++)
                {
                    arrayFerme[i].ProductionBle();
                }
            }

            planete.MettreAJourMonde();
        }

        public static int ChoixBatiment(Monde planete)
        {
            // ----------------------------------------------------------------
            // Récupère le numéro du batiment que le joueur souhaite construire
            // ----------------------------------------------------------------

            planete.AfficherMonde();
            Console.WriteLine();
            Console.WriteLine("Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
                              + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
                              + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)\n- 0 : Quitter");
            Console.Write("\t |");
            string ans = Console.ReadLine();
            string[] dicoReponses = {"1", "2", "3", "4", "5", "0"};
            while (!dicoReponses.Contains(ans))
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
                    + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
                    + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)\n- 0 : Quitter");

                Console.Write("\t |");


                ans = Console.ReadLine();
            }

            int numBat = int.Parse(ans);
            while (numBat > 6 || numBat < 0)
            {
                Console.WriteLine("Veuillez indiquer un numéro valide\n// ==================================== //");
                Console.WriteLine(
                    "Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
                    + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
                    + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)\n- 0 : Quitter");
                Console.Write("\t |");
                numBat = int.Parse(Console.ReadLine());
            }

            Console.Clear();
            return numBat;
        }


        public static string BackpackColon(Colon col)
        {
            if (col.GetType() == typeof(Tavernier))
            {
                return "Bois: " + col.Backpack[0] + ", Pierre: " + col.Backpack[1] + ", Eau: " + col.Backpack[2] +
                       ", Seau d'eau: " + col.Backpack[2]; //Bois / Pierre / Eau / Seau d'eau
            }
            else if (col.GetType() == typeof(Paysan))
            {
                return $"Blé : {col.Backpack[0]}, Eau : {col.Backpack[1]}"; // ble / eau
            }
            else
            {
                return
                    $"Bois : {col.Backpack[0]}, Pierre : {col.Backpack[1]}, Eau : {col.Backpack[2]} "; // bois ; pierre ; eau ;
            }

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
            Console.WriteLine(
                "\tBienvenue sur 'The Game You Wish You Had Known Before'\n\tNotre nouveau jeu de colonie en avant première !!!");
            Console.WriteLine("\t__________________________________________________________________\n");
            Console.WriteLine("\tComment prendre part au développement interractif du jeu en tour par tour?");
            Console.Write(
                "\tChaque tour, vous avez la possibilité de réaliser une liste d'actions définies en écrivant ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("'manuel'");
            Console.ResetColor();
            Console.WriteLine(
                "\tOu bien vous pouvez tout simplement laissez les colons opérer, mais attention, vous devrez prendre soin de la colonie !");
            Console.WriteLine(
                "\tIls ne pourront vivre sans vous, il vous faudra nécessairement interragir avec eux ! Pour débuter, je vous conseille de construire un entrepot");
            Console.WriteLine(
                "\tIl faudra très souvent s'enquérir des conditions de colons pour ne pas les perdre, car une fois mort, le colon ne reviendra pas !");
            Console.WriteLine("\t__________________________________________________________________\n");
            Console.WriteLine("\tInformations supplémentaires:\n");
            Console.WriteLine(
                "\tIl faut 4 tours à un bucheron ou un mineur pour casser un arbre ou respectivement un rocher");
            Console.WriteLine("\tTandis qu'il ne faut que 2 tours au paysan pour récolter du blé");
            Console.WriteLine(
                "\tSeul le tavernier peut interragir avec le puits. Ce dernier peut servir des bevrages uniquement si il se situe dans l'auberge !");
            Console.WriteLine("\tLa ferme produira du blé uniquement si le paysan est présent dedans");
            Console.WriteLine(
                "\t__________________________________________________________________\n\tAppuyez sur ENTR pour continuer...");
            Console.WriteLine();
            Console.ReadLine();
            Console.Clear();
        }

        public static void AfficherGameOver()
        {
            // ----------------------------
            // Affichage de fin de partie
            // ----------------------------
            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Clear(); 

            string centrer = new string(' ', Console.WindowHeight / 2); //variable pour centrer le texte;
            // Affiche le message "Game Over"
            Console.Write(centrer);
            Console.WriteLine(@"                                  )                    ");
            Console.Write(centrer);
            Console.WriteLine(@"  (                            ( /(                    ");
            Console.Write(centrer);
            Console.WriteLine(@"   )\ )       )     )      (    )\())   )      (   (    ");
            Console.Write(centrer);
            Console.WriteLine(@"  (()/(    ( /(    (      ))\  ((_)\   /((    ))\  )(   ");
            Console.Write(centrer);
            Console.WriteLine(@" /(_))_  )(_))   )\  ' /((_)   ((_) (_))\  /((_)(()\  ");
            Console.Write(centrer);
            Console.WriteLine(@"(_)) __|((_)_  _((_)) (_))    / _ \ _)((_)(_))   ((_) ");
            Console.Write(centrer);
            Console.WriteLine(@"  | (_ |/ _` || '  \()/ -_)  | (_) |\ V / / -_) | '_| ");
            Console.Write(centrer);
            Console.WriteLine(@"   \___|\__,_||_|_|_| \___|   \___/  \_/  \___| |_|   ");
            Console.WriteLine();
            Console.Write(centrer);
            Console.ReadLine();
            Environment.Exit(0);

        }
    }
}