using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;
using TPGestionDeColonie.ObjetsFixes.Batiments;


namespace TPGestionDeColonie
{
    abstract class Colon
    {
        // Variables
        private Tuple<int, int> positionColon;
        protected static int id = 0;
        protected int idColon;
        public string Nom
        {
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

        protected int TargetX { get; set; }
        protected int TargetY { get; set; }
        protected bool AvoirCible { get; set; }
        // protected List<string> capacites;

        //getter ID;
        public int getId()
        {
            return idColon;
        }
        public int[] Backpack { get; set; }

        public Monde Planete { get; }

        // -------------------------------------------

        public Colon(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) //, List<string> capacites
        {
            id = id + 1;
            idColon = id;
            this.positionX = positionX;
            this.positionY = positionY;
            Endurance = endurance;
            Sante = sante;
            Faim = faim;
            Soif = soif;
            Nom = nom;
            positionColon = new Tuple<int, int>(positionX, positionY);
            Planete = planete;
            Backpack = new int[] { 0, 0, 10 }; //Bois//Pierre//Eau
            AvoirCible = false;
        }

        public void AcquerirCible()
        {
            AvoirCible = true;
        }
        public void PerdreCible()
        {
            AvoirCible = false;
        }
        public bool ATIlCible() { return AvoirCible; }
        public void DefinirCible(int positionX, int positionY)
        {
            TargetX = positionX;
            TargetY = positionY;
        }
        public Tuple<int, int> RecupererCoordonneesCible()
        {
            Tuple<int, int> coords = new Tuple<int, int>(TargetX, TargetY);
            return coords;
        }




        public Tuple<int, int> getPosition()
        {
            return positionColon;
        }

        public void EtreFatigue()
        {
            if (Endurance < 20)
            {
                Console.WriteLine($"{Nom} est raplapla.");
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

        public void PrendreDegats(int degats)
        {
            Sante -= degats;
        }

        public void VerififierEtat()
        //Vérifie l'état physique du Colon
        {
            int pdvPerdus = 0;
            if (0 < Endurance && Endurance <= 10) { pdvPerdus += 1; }
            else if (Endurance == 0) { pdvPerdus += 2; }

            if (0 < Soif && Soif <= 10) { pdvPerdus += 1; }
            else if (Soif == 0) { pdvPerdus += 2; }

            if (0 < Faim && Faim <= 10) { pdvPerdus += 1; }
            else if (Faim == 0) { pdvPerdus += 2; }

            Sante -= pdvPerdus;

            Console.WriteLine($"{Nom} a perdu {pdvPerdus} points de vie. Santé restante : {Sante} / 100.");
        }

        public override string ToString()
        {
            return $"Colon n°{idColon} : {GetType().Name} {Nom}, santé = {Sante}/100 PV, endurance = {Endurance}/100, faim = {Faim}/100, soif = {Soif}/100"; // \nA-t-il une cible ? ${ATIlCible()}
        }


        public virtual void Miner(int x, int y) { Console.Write("test"); } //pour Mineur

        public virtual void Couper(int x, int y) { } //pour Bûcheron

        public virtual void Planter() { } //pour Paysan

        public virtual void Recolter(int x, int y) { } //pour Paysan

        public virtual void Construire(int numBat) {} // pour Batisseur


        public void Deplacer(int x, int y)
        {
            Endurance -= Math.Abs(positionX - x) + Math.Abs(positionY - y);
            while (positionX != x && positionY != y)
            {
                if (positionX > x && positionY > y)
                {
                    positionX -= 1;
                    positionY -= 1;
                }
                else if (positionX < x && positionY > y)
                {
                    positionX += 1;
                    positionY -= 1;
                }
                else if (positionX > x && positionY < y)
                {
                    positionX -= 1;
                    positionY += 1;
                }
                else if (positionX < x && positionY < y)
                {
                    positionX += 1;
                    positionY += 1;
                }
            }
            if (positionX == x)
            {
                if (positionY > y)
                {
                    while (positionY != y)
                    {
                        positionY -= 1;
                    }
                }
                else if (positionY < y)
                {
                    while (positionY != y)
                    {
                        positionY += 1;
                    }
                }
            }
            if (positionY == y)
            {
                if (positionX > x)
                {
                    while (positionX != x)
                    {
                        positionX -= 1;
                    }
                }
                else if (positionX < y)
                {
                    while (positionX != x)
                    {
                        positionX += 1;
                    }
                }
            }

            positionColon = new Tuple<int, int>(positionX, positionY);

        }
        public void SeDeplacer1Iteration(int x, int y)
        {
            Endurance -= 1;
            if (positionX != x && positionY != y)
            {
                if (positionX > x && positionY > y) // vers haut gauche
                {
                    if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX - 1, positionY - 1))) // case en diagonale disponible, peut se déplacer
                    {
                        positionX -= 1;
                        positionY -= 1;
                    }
                    else if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX - 1, positionY))) //case du dessus disponible
                    {
                        positionX -= 1;
                    }
                    else if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX, positionY - 1))) //case de gauche disponible
                    {
                        positionY -= 1;
                    }
                }
                else if (positionX < x && positionY > y) // vers bas gauche
                {
                    if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX + 1, positionY - 1))) // case en diagonale disponible, peut se déplacer
                    {
                        positionX += 1;
                        positionY -= 1;
                    }
                    else if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX + 1, positionY))) //case du bas disponible
                    {
                        positionX += 1;
                    }
                    else if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX, positionY - 1))) //case de gauche disponible
                    {
                        positionY -= 1;
                    }
                }
                else if (positionX > x && positionY < y) // vers haut droite
                {
                    if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX - 1, positionY + 1))) // case en diagonale disponible, peut se déplacer
                    {
                        positionX -= 1;
                        positionY += 1;
                    }
                    else if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX - 1, positionY))) //case du haut disponible
                    {
                        positionX -= 1;
                    }
                    else if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX, positionY + 1))) //case de droite disponible
                    {
                        positionY += 1;
                    }
                }
                else if (positionX < x && positionY < y) // vers bas droite
                {
                    if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX + 1, positionY + 1))) // case en diagonale disponible, peut se déplacer
                    {
                        positionX += 1;
                        positionY += 1;
                    }
                    else if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX + 1, positionY))) //case du bas disponible
                    {
                        positionX += 1;
                    }
                    else if (Planete.VerifCoordonnees(new Tuple<int, int>(positionX, positionY + 1))) //case de gauche disponible
                    {
                        positionY += 1;
                    }
                }
            }

            if (positionX == x) // on est sur la bonne ligne
            {
                if (positionY > y) // il faut se déplacer sur la gauche
                {
                    if (positionY != y)
                    {
                        positionY -= 1;
                    }
                }
                else if (positionY < y) // il faut se déplacer sur la droite
                {
                    if (positionY != y)
                    {
                        positionY += 1;
                    }
                }
            }

            if (positionY == y) // on est sur la bonne colonne
            {
                if (positionX > x)
                {
                    if (positionX != x)
                    {
                        positionX -= 1;
                    }
                }
                else if (positionX < x)
                {
                    if (positionX != x)
                    {
                        positionX += 1;
                    }
                }
            }
            positionColon = new Tuple<int, int>(positionX, positionY);
        }

        public Tuple<int, int> PlusProcheDistanceVersItem(int x, int y)
        {
            //cases autour de la cible : { x , y , distance au colon }
            // calcul de quelle case adjacente (haut bas gauche droite) à l'objet visé est la plus proche du colon
            int[] haut = { x - 1, y, Math.Abs(positionX - (x - 1)) + Math.Abs(positionY - y) };
            int[] bas = { x + 1, y, Math.Abs(positionX - (x + 1)) + Math.Abs(positionY - y) };
            int[] gauche = { x, y - 1, Math.Abs(positionX - x) + Math.Abs(positionY - (y - 1)) };
            int[] droite = { x, y + 1, Math.Abs(positionX - x) + Math.Abs(positionY - (y + 1)) };

            int distanceMin = Math.Min(Math.Min(Math.Min(haut[2], bas[2]), gauche[2]), droite[2]);

            if (haut[2] == distanceMin)
            {
                return new Tuple<int, int>(haut[0], haut[1]);
            }
            else if (bas[2] == distanceMin)
            {
                return new Tuple<int, int>(bas[0], bas[1]);
            }
            else if (gauche[2] == distanceMin)
            {
                return new Tuple<int, int>(gauche[0], gauche[1]);
            }
            else if (droite[2] == distanceMin)
            {
                return new Tuple<int, int>(droite[0], droite[1]);
            }
            return null;
        }

        public void SeDeplacerVersItem(int x, int y)
        {
            Tuple<int,int> coupleCoord = PlusProcheDistanceVersItem(x,y);
            SeDeplacer1Iteration(coupleCoord.Item1,coupleCoord.Item2);
        }

        public void SeDeplacer(int x, int y)
        {
            Tuple<int,int> coupleCoord = new Tuple<int,int>(x,y);
            SeDeplacer1Iteration(coupleCoord.Item1,coupleCoord.Item2);
        }


        public int CalculerDistancePlusProche(ObjetFixe obj)
        {
            int objX = obj.GetPositionObjet().FirstOrDefault().Item1;
            int objY = obj.GetPositionObjet().FirstOrDefault().Item2;

            return Math.Abs(objY - positionY) + Math.Abs(objX - positionX);
        }

        public Tuple<int, int> RechercherPlusProcheItem()
        {
            string typeDuColon = GetType().ToString();
            int indiceDeDistance = Planete.Hauteur * Planete.Largeur; //indice très grand
            Tuple<int, int> coordonnees = new Tuple<int, int>(positionX, positionY); // Attention au chasseur ça le renvoie en 0,0

            switch (typeDuColon)
            {
                case "TPGestionDeColonie.Bucheron":
                    foreach (ObjetFixe arb in Planete.ListeBlocs)
                    {
                        //Console.WriteLine(arb.ToString());
                        if (arb.GetType().Name == "Arbre" && arb.EtreCible() == false)
                        {
                            if (Math.Min(indiceDeDistance, CalculerDistancePlusProche(arb)) == CalculerDistancePlusProche(arb))
                            {
                                indiceDeDistance = CalculerDistancePlusProche(arb);
                                coordonnees = new Tuple<int, int>(arb.GetPositionObjet().FirstOrDefault().Item1, arb.GetPositionObjet().FirstOrDefault().Item2);
                            }
                        }
                    }
                    break;

                case "TPGestionDeColonie.Mineur":
                    foreach (ObjetFixe roc in Planete.ListeBlocs)
                    {
                        if (roc.GetType().Name == "Rocher" && roc.EtreCible() == false)
                        {

                            if (Math.Min(indiceDeDistance, CalculerDistancePlusProche(roc)) == CalculerDistancePlusProche(roc))
                            {
                                indiceDeDistance = CalculerDistancePlusProche(roc);
                                coordonnees = new Tuple<int, int>(roc.GetPositionObjet().FirstOrDefault().Item1, roc.GetPositionObjet().FirstOrDefault().Item2);
                            }
                        }
                    }
                    break;

                case "TPGestionDeColonie.Paysan":
                    foreach (ObjetFixe ble in Planete.ListeBlocs)
                    {
                        if (ble.GetType().Name == "Ble" && ble.EtreCible() == false)
                        {

                            if (Math.Min(indiceDeDistance, CalculerDistancePlusProche(ble)) == CalculerDistancePlusProche(ble))
                            {
                                indiceDeDistance = CalculerDistancePlusProche(ble);
                                coordonnees = new Tuple<int, int>(ble.GetPositionObjet().FirstOrDefault().Item1, ble.GetPositionObjet().FirstOrDefault().Item2);
                            }
                        }
                    }
                    break;

                case "TPGestionDeColonie.Batisseur":
                    foreach (ObjetFixe ent in Planete.ListeBlocs)
                    {
                        if (ent.GetType().Name == "Entrepot")
                        {
                            if (Math.Min(indiceDeDistance, CalculerDistancePlusProche(ent)) == CalculerDistancePlusProche(ent))
                            {
                                indiceDeDistance = CalculerDistancePlusProche(ent);
                                coordonnees = new Tuple<int, int>(ent.GetPositionObjet().FirstOrDefault().Item1, ent.GetPositionObjet().FirstOrDefault().Item2);
                            }
                        }
                    }
                    break;
                case "TPGestionDeColonie.Tavernier":
                    foreach (ObjetFixe puits in Planete.ListeBlocs)
                    {
                        if (puits.GetType().Name == "Puits")
                        {
                            if (Math.Min(indiceDeDistance, CalculerDistancePlusProche(puits)) == CalculerDistancePlusProche(puits))
                            {
                                indiceDeDistance = CalculerDistancePlusProche(puits);
                                coordonnees = new Tuple<int, int>(puits.GetPositionObjet().FirstOrDefault().Item1, puits.GetPositionObjet().FirstOrDefault().Item2);
                            }
                        }
                    }
                    break;
            }
            return coordonnees;

        }

        public void AllerVersBatiment(string nomBatiment)
        {
            int indiceDeDistance = Planete.Hauteur * Planete.Largeur; //indice très grand
            Tuple<int, int> coordonnees = new Tuple<int, int>(positionX, positionY);
            if (nomBatiment == "Auberge")
            {
                foreach (ObjetFixe aub in Planete.ListeBlocs)
                {
                    if (aub.GetType().Name == "Auberge")
                    {

                        if (Math.Min(indiceDeDistance, CalculerDistancePlusProche(aub)) == CalculerDistancePlusProche(aub))
                        {
                            indiceDeDistance = CalculerDistancePlusProche(aub);
                            coordonnees = new Tuple<int, int>(aub.GetPositionObjet().FirstOrDefault().Item1, aub.GetPositionObjet().FirstOrDefault().Item2);
                        }
                    }
                }
            }

            if (nomBatiment == "Entrepot")
            {
                foreach (ObjetFixe ent in Planete.ListeBlocs)
                {
                    if (ent.GetType().Name == "Entrepot")
                    {

                        if (Math.Min(indiceDeDistance, CalculerDistancePlusProche(ent)) == CalculerDistancePlusProche(ent))
                        {
                            indiceDeDistance = CalculerDistancePlusProche(ent);
                            coordonnees = new Tuple<int, int>(ent.GetPositionObjet().FirstOrDefault().Item1, ent.GetPositionObjet().FirstOrDefault().Item2);
                        }
                    }
                }
            }
            Deplacer(coordonnees.Item1, coordonnees.Item2);
        }

        public virtual void SeVider(Entrepot ent)
        {
            if (GetType() == typeof(Bucheron))
            {
                Backpack[0] -= 100;
                ent.StockRessources[0]+=100;

            }
            else if (GetType() == typeof(Mineur))
            {
                Backpack[1] -= 100;
                ent.StockRessources[1]+=100;
            }
            else if (GetType() == typeof(Paysan))
            {
                if (Backpack[0] >= 100)
                {
                    // vide son ble, et remplit l'entrepot
                    Backpack[0] -= 100;
                    ent.StockRessources[2] += 100;
                } 
            }
            else{
                if (Backpack[0] >= 100){
                    Backpack[0]-=100;
                    ent.StockRessources[0]+=100;
                }
                if(Backpack[1] >= 100){
                    Backpack[1] -= 100;
                    ent.StockRessources[1] += 100;

                }
            }
            PerdreCible();
        }

        public void BougerSiRempli(){
            if (Planete.ListeBatiments.OfType<Entrepot>().Any()) // s'il existe un entrepôt
            {
                Entrepot ent = Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault();
                //Tuple<int, int> coordEntrepot = Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1]; // milieu de l'entrepôt
                DefinirCible(ent.GetPositionObjet()[1].Item1, ent.GetPositionObjet()[1].Item2);
                SeDeplacer(TargetX, TargetY);
                AcquerirCible();

                if (ent.GetPositionObjet().Contains(getPosition()))
                {
                    SeVider(ent);
                }
            }
            else
            {
                Console.WriteLine("Au moins un colon a son sac plein, il serait temps de construire un entrepôt.");
            }
        }

        /*
                if (Planete.ListeBlocs.OfType<Entrepot>().Any())
                    {
                        // Se déplace vers l'entrepot et y stocke les ressources puis recommence
                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1,
                            ent.GetPositionObjet().FirstOrDefault().Item2);
                        return true;
                    }
                    else
                    {
                        //Reste sur place, ne fait rien
                        return true;
                    }
         */
          
         
        public bool EtreRempli() // Renvoie true si Backpack du personnage est plein
        {
            if (GetType() == typeof(Bucheron))
            {
                if (Backpack[0] >= 100)
                {
                    return true;
                }

                return false;
            }
            else if (GetType() == typeof(Mineur))
            {
                if (Backpack[1] >= 100)
                {
                    return true;
                }
                return false;
            }
            else if (GetType() == typeof(Paysan))
            {
                if (Backpack[0] >= 100)
                {
                        //Reste sur place, ne fait rien
                        return true;
                } 
                return false;
            }
            else{
                if (Backpack[0] >= 100 || Backpack[1] >= 100 || Backpack[2] >= 100){
                    return true;
                }
                return false;
            }
        }

        
    }
}
