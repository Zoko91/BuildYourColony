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
        protected int idColon;
        public string Nom{
            get;
            set;
        }
        public int positionX;
        public int positionY;
        public int Endurance
        {
            get;
            set;
        }
        public int Sante
        {
            get;
            set;
        }
        public int Faim
        {
            get;
            set;
        }
        public int Soif
        {
            get;
            set;
        }
        // protected List<string> capacites;

        //getter ID;
        public int getId()
        {
            return id;
        }
        private Tuple<int, int> positionColon;

        public Colon (string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif) //, List<string> capacites
        {
            id = id+1;
            idColon = id;
            this.positionX = positionX;
            this.positionY= positionY;
            Endurance = endurance;
            Sante = sante;
            Faim = faim;
            Soif = soif;
            Nom = nom;
            positionColon = new Tuple<int, int>(positionX, positionY);
        }
        public Tuple<int, int> getPosition()
        {
            return positionColon;
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


        public void VerififierEtat()
        //Vérifie l'état physique du Colon
        {
            int pdvPerdus=0;
            if (0<Endurance && Endurance<= 10){pdvPerdus += 1;}
            else if(Endurance ==0){pdvPerdus +=2;}
            
            if (0<Soif && Soif<= 10){pdvPerdus += 1;}
            else if(Soif ==0){pdvPerdus +=2;}

            if (0<Faim && Faim<= 10){pdvPerdus += 1;}
            else if(Faim ==0){pdvPerdus +=2;}       

            Sante -= pdvPerdus;

            Console.WriteLine($"{Nom} a perdu {pdvPerdus} points de vie. Santé restante : {Sante} / 100.");
        }

        public override string ToString()
        {
            return $"Colon n°{idColon} : {GetType().Name} {Nom}, santé = {Sante}/100 PV, endurance = {Endurance}/100, faim = {Faim}/100, soif = {Soif}/100";
        }


    }    
}
