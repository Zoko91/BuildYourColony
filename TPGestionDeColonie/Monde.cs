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
        
        
        
        List<Colon> listePNJ;
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

        /* public List<ObjetFixe> GenererListe()
         {

         }*/



        
    }
}
