using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    abstract class Colon
    {

        protected static int id=0;
        protected string Nom{
            get;
            set;
        }
        protected int positionX;
        protected int positionY;
        protected int Endurance
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



        public Colon (string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif) //, List<string> capacites
        {
            this.id = id+1;               
            this.positionX = positionX;
            this.positionY= positionY;
            Endurance = endurance;
            Sante = sante;
            Faim = faim;
            Soif = soif;
            Nom = nom;
        }
        
        public void EtreFatigue()
        {
            if (Endurance<20)
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

        public void VerififierEtat()
        //Vérifie l'état physique du Colon
        {
            int pdvPerdus;
            if (0<Endurance && Endurance<= 10){pdvPerdus += 1;}
            else if(Endurance ==0){pdvPerdus +=2;}
            
            if (0<Soif && Soif<= 10){pdvPerdus += 1;}
            else if(Soif ==0){pdvPerdus +=2;}

            if (0<Faim && Faim<= 10){pdvPerdus += 1;}
            else if(Faim ==0){pdvPerdus +=2;}       

            Sante -= pdvPerdus;

            Console.WriteLine($"{Nom} a perdu {pdvPerdus} points de vie. Santé restante : {Sante} / 100.");
        }




    }
}
