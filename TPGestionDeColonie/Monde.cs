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
        Random rng = new Random();

        private string[,] grille = new string[30, 30];

        public int Hauteur { get; private set; }
        public int Largeur { get; private set; }

        List<Colon> listePJ;
        List<ObjetFixe> listeBlocs;
        public Monde() {
            listePJ = new List<Colon>();
            listeBlocs = new List<ObjetFixe>();
            Hauteur = grille.GetLength(0);
            Largeur = grille.GetLength(1);
        }

        public bool VerifCoordonnees(Tuple<int, int> coordonnees) // Check Si y'a déjà un block OU un co
        {
            List<Tuple<int, int>> listeCoordonneesColons = new List<Tuple<int, int>>();

            foreach(Colon col in listePJ)
            {
                listeCoordonneesColons.Add(col.getPosition());
=               if (listeCoordonneesColons.Contains(coordonnees))
                {
                    return false; //case non dispo, occupée par Colon
                }
            }
            foreach (ObjetFixe obj in listeBlocs)
            {
                List<Tuple<int, int>> listeCoordonnees = obj.GetPositionObjet();
                if (listeCoordonnees.Contains(coordonnees))
                {
                    return false; //case non disponible, occupée par objet fixe
                }
            }
            return true; //case disponible
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
                Arbre arbre = new Arbre(listeCoordonnees) ;
                listeBlocs.Add(arbre);
            }
            else {
                Rocher rocher = new Rocher(listeCoordonnees);
                listeBlocs.Add(rocher);
            }
        }

        
        public void GenererMonde()
        {
            for(int i = 0; i<20; i++){
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
                        foreach (ObjetFixe obj in listeBlocs)
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
                                else
                                {
                                    grille[i, j] = " C ";
                                }
                            }
                        }
                    }
                }   
            }
        }


        public void AfficherMonde()
        {
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (grille[i, j] == " A ")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(grille[i, j]);
                        Console.ResetColor();
                    }
                    else if (grille[i, j] == " R ")
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
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
            listePJ.Add(c);
        }

        public void GameOver(){
            /// Fonction Game Over
            if (listePJ.Count == 0){
                Console.Clear();
                Console.WriteLine("Game Over");
            }
        }

        public void SupprimerColon(){
            //supprime le colon de la liste
            for (int i = 0; i < listePJ.Count; i++)
            {
                if (listePJ[i].Sante ==0)
                {
                    Console.WriteLine($"Le colon: {listePJ[i].Nom} est mort");
                    listePJ.RemoveAt(i);
                }
            }
        }

        public void PresenceItem(){
            
        }

        
    }
}
