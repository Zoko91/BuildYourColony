using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    class Monde
    {
        string[,] grille = new string[30, 30];
        
        
        
        List<Colon> listePJ;
        // List<ObjetsFixes> Blocs

        
        public void GenererMonde()
        {
            for (int i=0; i<grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    grille[i, j] = " x ";
                }
                
            }
        }


        public void AfficherMonde()
        {
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    Console.Write(grille[i, j]);
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

        
        public void GameOver(){
            /// Fonction Game Over
            if (listePJ.Count == 0){
                Console.Clear();
                Console.WriteLine("Game Over");
            }
        }

        
    }
}
