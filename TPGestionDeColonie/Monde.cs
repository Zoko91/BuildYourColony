using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;
using TPGestionDeColonie.ObjetsFixes.Batiments;

namespace TPGestionDeColonie
{
    class Monde
    {
        // ---------------------------------------------------------
        // Classe définissant l'environnement du jeu :
            // La carte dans un premier mais permet à tous les élements du jeu d'interagir entre eux
            // en implémentant des listes de tous les élements présents dans Monde que l'on met à jour a chaque ajout/Suppression
        // ---------------------------------------------------------

        // -- \\ Variables
        // -------------------------------------------
        Random rng = new Random();

        public string[,] grille = new string[35, 35];

        public int Hauteur { get; private set; }
        public int Largeur { get; private set; }

        public List<Colon> ListePJ { get; }
        public List<ObjetFixe> ListeBlocs { get ;  }
        public List<Batiment> ListeBatiments { get ;  }
        // -------------------------------------------

        public Monde() {
            ListePJ = new List<Colon>();
            ListeBlocs = new List<ObjetFixe>();
            ListeBatiments = new List<Batiment>();
            Hauteur = grille.GetLength(0);
            Largeur = grille.GetLength(1);
        }

        // -- \\ Fonctions de vérification des coordonnées
        // -----------------------------------------------

        public bool VerifCoordonnees(Tuple<int, int> coordonnees) 
        {
            // Fonction indiquant si un autre élément (objet ou colon) est présent sur les coordonnées prises en input

            List<Tuple<int, int>> listeCoordonneesColons = new List<Tuple<int, int>>();

            foreach(Colon col in ListePJ)
            {
               listeCoordonneesColons.Add(col.getPosition());
               if (listeCoordonneesColons.Contains(coordonnees))
                {
                    return false; //case non dispo, occupée par Colon
                }
            }
            foreach (ObjetFixe obj in ListeBlocs)
            {
                List<Tuple<int, int>> listeCoordonnees = obj.GetPositionObjet();
                if (listeCoordonnees.Contains(coordonnees))
                {
                    return false; //case non disponible, occupée par objet fixe
                }
            }
            return true; //case disponible
        }
        
        public bool NePasMarcherSurUnItem(Tuple<int, int> coordonnees) 
        {
            // Fonction indiquant si un bloc item (Pierre ou bois) est présent sur la case ciblée
            
            foreach (ObjetFixe obj in ListeBlocs)
            {
                if (obj.GetType() != typeof(Eau))
                {
                    List<Tuple<int, int>> listeCoordonnees = obj.GetPositionObjet();
                    if (listeCoordonnees.Contains(coordonnees))
                    {
                        return false; //case non disponible, occupée par objet fixe
                    }
                }
            }
            return true; //case disponible
        }

        public bool VerifCoordonneesBatiment(Tuple<int, int> coordonnees) 
        {
            // Fonction qui vérifie si la case est occupée par un batiment

            foreach (Batiment bat in ListeBatiments)
            {
                List<Tuple<int, int>> listeCoordonnees = bat.GetPositionObjet();
                if (listeCoordonnees.Contains(coordonnees))
                {
                    return false; //case non disponible, occupée par un batiment
                }
            }
            return true; //case disponible
        }

        public bool VerifListeCoordonnees(List<Tuple<int, int>> coordonnees) 
        {
            // Fonction permetant de vérifier une liste de coordonnées, utilisée pour la construction de batiments ou des plans d'eau

            for (int i = 0; i < coordonnees.Count; i++)
            {
                if (VerifCoordonnees(coordonnees[i])==false)
                {
                    return false;
                }
            }
            return true;
        }

        // -- \\ Fonctions de Génération
        // -----------------------------

        public Tuple<int, int> GenererCoupleCoordonnees()
        {
            // Donne un couple de coordonnées aléatoire sous forme d'un couple de 2 entiers
            Tuple<int,int> coupleXY = new Tuple<int, int>(rng.Next(grille.GetLength(0)),rng.Next(grille.GetLength(1)));

            return coupleXY;
        }

        public void GenererBloc()
        {
            //Génère un bloc arbre ou rocher aléatoirement

            Tuple<int, int> coordonnees = GenererCoupleCoordonnees();

            while (VerifCoordonnees(coordonnees)==false){
                coordonnees=GenererCoupleCoordonnees();
            }
            int proba = rng.Next(1,3);
            List<Tuple<int, int>> listeCoordonnees = new List<Tuple<int, int>>();
            listeCoordonnees.Add(coordonnees);
            if (proba==1)
            {
                Arbre arbre = new Arbre(listeCoordonnees,this) ;
                ListeBlocs.Add(arbre);
            }
            else {
                Rocher rocher = new Rocher(listeCoordonnees,this);
                ListeBlocs.Add(rocher);
            }
        }

        public List<Tuple<int,int>> GenererPremierPlanDEau()
        {
            // Fonction de génération du premier plan d'eau (forme indiquée ci dessous)

            List<Tuple<int, int>> listeCoordEau = new List<Tuple<int, int>>();
            int positionXEau = rng.Next(0, Hauteur - 1);
            int positionYEau = rng.Next(0, Largeur - 1);
            /* forme 
             * E E E    00 01 02
             * E E      10 11
             * E        20
             */
            Tuple<int, int> coordEau00 = new Tuple<int, int>(positionXEau, positionYEau);
            Tuple<int, int> coordEau01 = new Tuple<int, int>(positionXEau, positionYEau + 1);
            Tuple<int, int> coordEau02 = new Tuple<int, int>(positionXEau, positionYEau + 2);
            Tuple<int, int> coordEau10 = new Tuple<int, int>(positionXEau + 1, positionYEau);
            Tuple<int, int> coordEau11 = new Tuple<int, int>(positionXEau + 1, positionYEau + 1);
            Tuple<int, int> coordEau20 = new Tuple<int, int>(positionXEau + 2, positionYEau);
            listeCoordEau.Add(coordEau00);
            listeCoordEau.Add(coordEau01);
            listeCoordEau.Add(coordEau02);
            listeCoordEau.Add(coordEau10);
            listeCoordEau.Add(coordEau11);
            listeCoordEau.Add(coordEau20);
            return listeCoordEau;
        }

        public List<Tuple<int, int>> GenererDeuxiemePlanDEau()
        {
            // Fonction de génération du deuxième plan d'eau (forme indiquée ci dessous)

            List<Tuple<int, int>> listeCoordEau = new List<Tuple<int, int>>();
            int positionXEau = rng.Next(0, Hauteur - 1);
            int positionYEau = rng.Next(1, Largeur - 1);
            /* forme 
             *   E           01 
             * E E E      10 11 12
             *   E           21
             */
            Tuple<int, int> coordEau01 = new Tuple<int, int>(positionXEau, positionYEau);
            Tuple<int, int> coordEau10 = new Tuple<int, int>(positionXEau+1, positionYEau - 1);
            Tuple<int, int> coordEau11 = new Tuple<int, int>(positionXEau+1, positionYEau);
            Tuple<int, int> coordEau12 = new Tuple<int, int>(positionXEau + 1, positionYEau + 1);
            Tuple<int, int> coordEau21 = new Tuple<int, int>(positionXEau + 2, positionYEau);

            listeCoordEau.Add(coordEau01);
            listeCoordEau.Add(coordEau10);
            listeCoordEau.Add(coordEau11);
            listeCoordEau.Add(coordEau12);
            listeCoordEau.Add(coordEau21);
            return listeCoordEau;
        }

        public void GenererMonde()
        {
            // Fonction de création des élements de la grille fixes ou mobiles

            List<Tuple<int, int>> listeCoordonneesColons = new List<Tuple<int, int>>();

            foreach (Colon col in ListePJ)
            {
                listeCoordonneesColons.Add(col.getPosition());
            }

            // Générer les plans d'eau
            // ============================================================= //

            int nombrePlansEau = Hauteur/10;
            int nbPremierPlan = rng.Next(1,3);
            int nbDeuxiemePlan = nombrePlansEau - nbPremierPlan;
            for(int j = 0; j< nbPremierPlan; j++){
                List<Tuple<int, int>> listeCoordEau = GenererPremierPlanDEau(); 
                while(VerifListeCoordonnees(listeCoordEau)==false){ // tant qu'il y a des blocs / colons bloquant la génération
                    listeCoordEau=GenererPremierPlanDEau();
                }
                Eau eau = new Eau(listeCoordEau,this);
                ListeBlocs.Add(eau); // ajout de l'eau à la liste des blocs
                
            }
            for(int l = 0; l< nbDeuxiemePlan; l++){
                List<Tuple<int, int>> listeCoordEau = GenererDeuxiemePlanDEau(); 
                while(VerifListeCoordonnees(listeCoordEau)==false){ // tant qu'il y a des blocs / colons bloquant la génération
                    listeCoordEau=GenererDeuxiemePlanDEau();        // on regénère un plan d'eau
                }
                Eau eau = new Eau(listeCoordEau,this);
                ListeBlocs.Add(eau);
            }

            // ============================================================= //


            // Génère les rochers et les arbres [Le nombre d'entité est déterminé en fonction de la taille de la grille]
            // ============================================================= //

            for (int i = 0; i< Math.Floor(Largeur*1.57); i++){
                GenererBloc(); //génère tous les blocs
            }
            // ============================================================= //


            // Affichage des élements du jeu sur la grille à la génération
            // ============================================================= //

            for (int i=0; i<grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    Tuple<int, int> coords = new Tuple<int, int>(i, j);
                    if (VerifCoordonnees(coords)) // si la case est dispo
                    {
                        grille[i, j] = " x ";
                    }
                    else  //si la case n'est pas dispo
                    {
                        foreach (ObjetFixe obj in ListeBlocs)
                        {
                           if(obj.GetPositionObjet().Contains(coords))
                            {
                                if (obj.GetType() == typeof(Arbre))
                                {
                                    grille[i, j] = " A ";
                                    
                                }
                                else if(obj.GetType() == typeof(Rocher))
                                {
                                    grille[i, j] = " R ";
                                }
                                else if (obj.GetType() == typeof(Ble))
                                {
                                    grille[i, j] = " B ";
                                }
                                else if (obj.GetType()== typeof(Eau)){
                                    grille[i,j] = " E ";
                                }
                            }
                        }
                        foreach(Colon c in ListePJ)
                        {
                            if (listeCoordonneesColons.Contains(coords))
                            {
                                grille[i, j] = " C ";
                            }
                        }
                    }
                }   
            }
            // ============================================================= //
        }


        public void MettreAJourMonde()
        {
            // Met à jour les cases de la grille // Affichage (méthode appellée à chaque tour de jeu)

            List<Tuple<int, int>> listeCoordonneesColons = new List<Tuple<int, int>>();

            foreach (Colon col in ListePJ)
            {
                listeCoordonneesColons.Add(col.getPosition());
            }
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    Tuple<int, int> coords = new Tuple<int, int>(i, j);
                    if (VerifCoordonnees(coords) && VerifCoordonneesBatiment(coords)) // si la case est vide
                    {
                        grille[i, j] = " x ";
                    }
                    else  //si la case n'est pas vide
                    {
                        foreach (ObjetFixe obj in ListeBlocs)
                        {
                            if (obj.GetPositionObjet().Contains(coords))
                            {
                                if (obj.GetType() == typeof(Arbre))
                                {
                                    grille[i, j] = " A ";

                                }
                                else if (obj.GetType() == typeof(Rocher))
                                {
                                    grille[i, j] = " R ";
                                }
                                else if (obj.GetType() == typeof(Ble))
                                {
                                    grille[i, j] = " B ";
                                }
                                else if (obj.GetType() == typeof(Eau))
                                {
                                    grille[i, j] = " E ";
                                }
                            }
                        }
                        foreach(Batiment obj in ListeBatiments){
                            if (obj.GetPositionObjet().Contains(coords))
                            {
                                
                                if (obj.GetType() == typeof(Auberge)){
                                    grille[i, j] = "AUB";
                                }
                                else if (obj.GetType() == typeof(Entrepot)){
                                    grille[i, j] = "ENT";
                                }
                                else if (obj.GetType() == typeof(Ferme)){
                                    grille[i, j] = "FRM";
                                }
                                else if (obj.GetType() == typeof(Maison)){
                                    grille[i, j] = "MSN";
                                }
                                else if (obj.GetType() == typeof(Puits)){
                                    grille[i, j] = "PUI";
                                }
                            }
                        }
                        foreach (Colon c in ListePJ)
                        {
                            if (listeCoordonneesColons.Contains(coords))
                            {
                                grille[i, j] = " C ";
                            }
                        }
                    }
                }
            }
            AfficherMonde();
        }

        public void AfficherMonde()
        {
            // Affichage supplémentaire (couleurs, n° colonne etc) une fois que les cases ont été mises à jour dans la grille

            Console.Write("   ");
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                if (i<10)
                {
                    Console.Write($" {i} ");
                }
                else if(i>=10)
                {
                    Console.Write($" {i}");
                }
                
            }
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("XXX ");
            Console.ResetColor();
            Console.Write("   ");
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                Console.Write("___");
            }
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("XXX ");
            Console.ResetColor();
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                Console.Write(" | ");
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    
                    if (grille[i, j] == " A ")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == " R ")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == " C ")
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == " B ")
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == " E ")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == "AUB" || grille[i, j] == "ENT" || grille[i, j] == "FRM" || grille[i, j] == "MSN" || grille[i, j] == "PUI")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(grille[i, j]);
                    }
                }
                Console.Write($" | {i}");
                Console.WriteLine();
            }
            Console.Write("   ");

            for (int i = 0; i < grille.GetLength(0); i++)
            {
                Console.Write("___");
            }
            Console.WriteLine();

        }

        public void AfficherFenetre(int x,int y)
        {
            // Méthode non utilisée permettant l'affichage d'une portion de la carte (style minimap)

            // position X
            int posxhaut = x-5;
            int posxbas = x + 5;
            if (x < 6)
            {
                posxhaut = 0;
            }
            if (x > grille.GetLength(0)-6)
            {
                posxbas = grille.GetLength(0)-1;
            }

            // Position Y
            int posygauche = y -5;
            int posydroit = y +5;
            if (y < 6)
            {
                posygauche = 0;
            }
            if (y > grille.GetLength(1) - 6)
            {
                posydroit = grille.GetLength(1)-1;
            }

            // Affichage de la fenêtre
            for (int i = posxhaut; i <= posxbas; i++)
            {
                for (int j = posygauche; j <= posydroit; j++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(grille[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            // Affichage de la fenetre sur la map générale
            
            int posDepartLigne = posxhaut;
            int posFinLigne = posygauche;
            int var = 0;
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (i == posDepartLigne && j == posFinLigne)
                    {
                      if (posDepartLigne < posxbas) { posDepartLigne += 1; }
                        
                        
                      for (int h = posygauche; h <= posydroit; h++)
                      {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(grille[i, j]);
                            Console.ResetColor();
                            var = h;
                            j = posydroit + 1;
                        }
                    }
                    Console.Write(grille[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void AjouterColon(Colon c)
        {
            // Ajoute un colon dans la liste des colons du Monde
            ListePJ.Add(c);
        }

        public bool GameOver(){
            /// Fonction Game Over
            if (ListePJ.Count == 0){
                Console.Clear();
                Console.WriteLine("Game Over");
                return true;
            }
            return false;
        }

        public void SupprimerColon(){
            //Supprime le colon de la liste
            for (int i = 0; i < ListePJ.Count; i++)
            {
                if (ListePJ[i].Sante ==0)
                {
                    Console.WriteLine($"Le colon: {ListePJ[i].Nom} est mort");
                    ListePJ.RemoveAt(i);
                }
            }
        }
        
    }
}
