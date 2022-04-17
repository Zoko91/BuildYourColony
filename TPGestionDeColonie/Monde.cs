using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;

namespace TPGestionDeColonie
{
    class Monde
    {
        // Variables -------------------
        Random rng = new Random();

        public string[,] grille = new string[35, 35];

        public int Hauteur { get; private set; }
        public int Largeur { get; private set; }

        public List<Colon> ListePJ { get; }
        public List<ObjetFixe> ListeBlocs { get ;  }
        // -----------------------------

        public Monde() {
            ListePJ = new List<Colon>();
            ListeBlocs = new List<ObjetFixe>();
            Hauteur = grille.GetLength(0);
            Largeur = grille.GetLength(1);
        }

        public bool VerifCoordonnees(Tuple<int, int> coordonnees) // Check Si y'a déjà un block OU un co
        {
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

        
        public bool VerifListeCoordonnees(List<Tuple<int, int>> coordonnees) // Pour la construction des batiments ou des plans d'eau
        {
            for (int i = 0; i < coordonnees.Count; i++)
            {
                if (VerifCoordonnees(coordonnees[i])==false)
                {
                    return false;
                }
            }
            return true;
        }


        public Tuple<int, int> GenererCoupleCoordonnees()
        {
            Tuple<int,int> coupleXY = new Tuple<int, int>(rng.Next(grille.GetLength(0)),rng.Next(grille.GetLength(1)));

            return coupleXY;
        }

        public void GenererBloc(){
            //Génère un bloc arbre ou rocher
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

            for (int i = 0; i< Math.Floor(Largeur*1.57); i++){
                GenererBloc(); //génère tous les blocs
            }
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
                                    grille[i, j] = " 🌴";
                                    
                                }
                                else if(obj.GetType() == typeof(Rocher))
                                {
                                    grille[i, j] = " ⛰ ";
                                }
                                else if (obj.GetType() == typeof(Ble))
                                {
                                    grille[i, j] = " 🟨";
                                }
                                else if (obj.GetType()== typeof(Eau)){
                                    grille[i,j] = " 🌊";
                                }
                            }
                        }
                        foreach(Colon c in ListePJ)
                        {
                            if (listeCoordonneesColons.Contains(coords))
                            {
                                grille[i, j] = "🤵 ";
                            }
                        }
                    }
                }   
            }
        }


        public void MettreAJourMonde()
        {
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
                    if (VerifCoordonnees(coords)) // si la case est dispo
                    {
                        grille[i, j] = " x ";
                    }
                    else  //si la case n'est pas dispo
                    {
                        foreach (ObjetFixe obj in ListeBlocs)
                        {
                            if (obj.GetPositionObjet().Contains(coords))
                            {
                                if (obj.GetType() == typeof(Arbre))
                                {
                                    grille[i, j] = " 🌴";

                                }
                                else if (obj.GetType() == typeof(Rocher))
                                {
                                    grille[i, j] = " ⛰ ";
                                }
                                else if (obj.GetType() == typeof(Ble))
                                {
                                    grille[i, j] = " 🟨";
                                }
                                else if (obj.GetType() == typeof(Eau))
                                {
                                    grille[i, j] = " 🌊";
                                }
                            }
                        }
                        foreach (Colon c in ListePJ)
                        {
                            if (listeCoordonneesColons.Contains(coords))
                            {
                                grille[i, j] = "🤵 ";
                            }
                        }
                    }
                }
            }
            AfficherMonde();
        }

        public void AfficherMonde()
        {
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (grille[i, j] == " 🌴")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == " ⛰ ")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == "🤵 ")
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == " 🟨")
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == " 🌊")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(grille[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }




        public void AfficherFenetre(int x,int y)
        {
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
            /*
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
            }*/
        }


        /*public List<ObjetFixe> GenererListe()
         {

         }*/

        public void AjouterColon(Colon c)
        {
            ListePJ.Add(c);
        }

        public void GameOver(){
            /// Fonction Game Over
            if (ListePJ.Count == 0){
                Console.Clear();
                Console.WriteLine("Game Over");
            }
        }

        public void SupprimerColon(){
            //supprime le colon de la liste
            for (int i = 0; i < ListePJ.Count; i++)
            {
                if (ListePJ[i].Sante ==0)
                {
                    Console.WriteLine($"Le colon: {ListePJ[i].Nom} est mort");
                    ListePJ.RemoveAt(i);
                }
            }
        }

        public void PresenceItem(){
            
        }

        
    }
}
