using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    abstract class ObjetFixe
    {
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

        public bool EtreCible()
        {
            return EstCible;
        }
        public void DevenirCible()
        {
            EstCible = true;
        }
        public void NePlusEtreCible()
        {
            EstCible = false;
        }

        public List<Tuple<int, int>> GetPositionObjet() //position d'un seul objet
        {
            return positionObjet;
        }


        public void DestructionEnCours(int x, int y, Colon col) {
            Tuple<int, int> position = new Tuple<int, int>(x, y);
            Sante -= 100;
            if (Sante <= 0)
            {
                //Console.WriteLine(x + " "+ y);
                //Console.WriteLine("L'élément "+ GetType().Name + " est cassé");
                Planete.grille[x,  y] = " x ";
                foreach(ObjetFixe obj in Planete.ListeBlocs){
                    if(obj.GetPositionObjet().Contains(position)){
                        Planete.ListeBlocs.Remove(obj);
                        col.PerdreCible();
                        break;
                    }
                }
                
                //return true; Lors de l'action de destruction on peut lui dire tant que = true;
            }
            //return false;
        }

        
        public override string ToString()
        {
            return $"{positionObjet.FirstOrDefault().Item1} et {positionObjet.FirstOrDefault().Item2} + nature: {this.GetType().Name} "; //+ Ciblé ? {EtreCible()}
        }

    }
}
