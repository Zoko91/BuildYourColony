using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    abstract class ObjetFixe
    {
        // -----------------------------------------------------------------------
        // Classe abstraite définissant un objet non mobile, comme un item ou un batiment
        // -----------------------------------------------------------------------

        

        // -- \\ Variables
        // -------------------------------------------

        /*Liste de coordonnées d'un objet
         * Tuple Item1 = les x
         * Tuple Item2 = les y
         */

        protected List<Tuple<int, int>> positionObjet;
        protected int Sante { get; set; }
        protected Monde Planete { get; }
        protected virtual bool EstCible { get; set; }

        public ObjetFixe (List<Tuple<int, int>> position, Monde planete)
        {
            this.positionObjet = position;
            Sante = 100;
            Planete = planete;
            EstCible=false;
        }

        // -- \\ Gestion des cibles
        // -------------------------------------------
        public bool EtreCible() {return EstCible;}
        public void DevenirCible(){EstCible = true;}
        public void NePlusEtreCible(){EstCible = false;}


        public List<Tuple<int, int>> GetPositionObjet() 
        {
            return positionObjet;
        }

        public void DestructionEnCours(int x, int y, Colon col) 
        {
            // Fonction de destruction d'un Item (4 coups sont nécessaire pour casser un bloc)

            Random rng = new Random();
            Tuple<int, int> position = new Tuple<int, int>(x, y);
            Sante -= 25;
            if (rng.Next(0, 2) == 0)
            {
                col.AvoirFaimEtSoifSiAction();
            }
            if (Sante <= 0)
            {
                Planete.grille[x,  y] = " x ";
                /*
                foreach(ObjetFixe obj in Planete.ListeBlocs){
                    if(obj.GetPositionObjet().Contains(position)){
                        Planete.ListeBlocs.Remove(obj);
                        col.PerdreCible();
                        break;
                    }
                }*/
                foreach(Colon colon in Planete.ListePJ)
                {
                    if (Planete.ListePJ.Find(z => z.RecupererCoordonneesCible().Equals(new Tuple<int, int>(x, y))) == colon)
                    {
                        col.PerdreCible();
                    }
                }
                Planete.ListeBlocs.Remove(Planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(new Tuple<int, int>(x, y))));
                col.PerdreCible();
            }
        }

        
        public override string ToString()
        {
            return $"{positionObjet.FirstOrDefault().Item1} et {positionObjet.FirstOrDefault().Item2} + nature: {this.GetType().Name} \n+ Ciblé ? {EtreCible()}"; //+ Ciblé ? {EtreCible()}
        }

    }
}
