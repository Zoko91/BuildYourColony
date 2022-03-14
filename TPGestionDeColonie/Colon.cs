using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    abstract class Colon
    {

        protected int id;
        protected int positionX;
        protected int positionY;
        protected int Fatigue
        {
            get;
            set;
        }
        protected int Sante
        {
            get;
            set;
        }
        protected int Faim
        {
            get;
            set;
        }
        protected int Soif
        {
            get;
            set;
        }
        // protected List<string> capacites;


        public Colon (int id, int positionX, int positionY, int fatigue, int sante, int faim, int soif) //, List<string> capacites
        {
            this.id = id;               
            this.positionX = positionX;
            this.positionY= positionY;
            Fatigue = fatigue;
            Sante = sante;
            Faim = faim;
            Soif = soif;
        }
        
        public void EtreFatigue()
        {
            if (Fatigue<20)
            {
                Console.WriteLine("Tristan est raplapla");
            }
        }
       
        public void AvoirSoif()
        {
            if (Soif < 20)
            {
                Console.WriteLine("Glouglou");
            }
        }
        
        public void SanteFaible()
        {
            if (Sante < 20)
            {
                Console.WriteLine("Aïe aïe aïe, je ne suis pas en bon état.");
            }
            
        }

        public int[] getPosition()
        {
            int[] position = {  positionX,  positionY  };
            return position;
        }






    }
}
