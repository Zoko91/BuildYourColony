using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes.Batiments;
using TPGestionDeColonie.ObjetsFixes;

namespace TPGestionDeColonie
{
    class Batisseur : Colon
    {
        // Créer la liste des capacités de base
        // List<string> capacites
        public Batisseur(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
            Backpack = new int[] { 300, 300, 100 };

        }


        public void ViderEntrepot(int bois, int pierre)
        {
            Entrepot ent = Planete.ListeBlocs.OfType<Entrepot>().FirstOrDefault();
            Backpack[0] += bois;
            Backpack[1] += pierre;
            ent.StockRessources[0] -= bois;
            ent.StockRessources[1] -= pierre;
        }

        public void RemplirLeStock(int numBat)
        {
            int besoinPierre = 0;
            int besoinBois = 0;
            Entrepot ent = Planete.ListeBlocs.OfType<Entrepot>().FirstOrDefault();
            if (numBat == 1)
            {
                if (Backpack[0] < 20 || Backpack[1] < 30)
                {
                    if (Planete.ListeBlocs.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 20)
                        {
                            besoinBois = 20 - Backpack[0];
                        }
                        if (Backpack[1] < 30)
                        {
                            besoinPierre = 30 - Backpack[1];
                        }

                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1, ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois, besoinPierre);
                    }
                }
            }
            else if (numBat == 2)
            {
                if (Backpack[0] < 30 || Backpack[1] < 10)
                {
                    if (Planete.ListeBlocs.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 30)
                        {
                            besoinBois = 30 - Backpack[0];
                        }
                        if (Backpack[1] < 10)
                        {
                            besoinPierre = 10 - Backpack[1];
                        }

                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1, ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois, besoinPierre);
                    }
                }

            }
            else if (numBat == 3)
            {
                if (Backpack[0] < 30)
                {
                    if (Planete.ListeBlocs.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 30)
                        {
                            besoinBois = 30 - Backpack[0];
                        }
                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1, ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois, besoinPierre);
                    }
                }
            }
            else if (numBat == 4)
            {
                if (Backpack[0] < 5 || Backpack[1] < 15)
                {
                    if (Planete.ListeBlocs.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 5)
                        {
                            besoinBois = 5 - Backpack[0];
                        }
                        if (Backpack[1] < 15)
                        {
                            besoinPierre = 15 - Backpack[1];
                        }
                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1, ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois, besoinPierre);
                    }
                }
            }
            else if (numBat == 5)
            {
                if (Backpack[0] < 40)
                {
                    if (Planete.ListeBlocs.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 40)
                        {
                            besoinBois = 40 - Backpack[0];
                        }
                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1, ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois, besoinPierre);
                    }
                }
            }

        }

        public override void Construire()
        {
            // Detecte quel batiment la personne veut construire (utilisé dnas remplir les stocks, refaire une fonction)
            // 1 Entrepot 2 Auberge 3 Maison 4 Puits 5 Ferme
            Console.WriteLine("Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
            + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
            + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)");
            int numBat = int.Parse(Console.ReadLine());
            while(numBat>6 || numBat<0){
                Console.WriteLine("Veuillez indiquer un numéro valide\n// ==================================== //");
                Console.WriteLine("Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
            + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
            + "- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)");
            numBat = int.Parse(Console.ReadLine());
            }
            RemplirLeStock(numBat);
            // ----------------------------------------- //
            // Etape de construction
            // ----------------------------------------- // 
            ConstructionBatiment(numBat);

        }

        public void ConstructionBatiment(int numBat)
        {
            // ========================================================== //
            // Construction d'un Entrepot
            // ========================================================== //

            if (numBat == 1)
            { 
                if (positionX + 1 > Planete.Hauteur - 1 || positionX - 1 < 0) //Il commence au milieu
                {
                    Console.WriteLine("Impossible de construire ici !");
                }
                else if (Planete.grille[positionX, positionY + 1] == " x " && Planete.grille[positionX, positionY - 1] == " x ")
                {
                    Planete.grille[positionX, positionY] = " E ";
                    Planete.grille[positionX, positionY - 1] = " E ";
                    Planete.grille[positionX, positionY - 1] = " E ";
                    Tuple<int,int> pos00 = new Tuple<int,int>(positionX,positionY-1);
                    Tuple<int,int> pos01 = new Tuple<int,int>(positionX,positionY);
                    Tuple<int,int> pos02 = new Tuple<int,int>(positionX,positionY+1);
                    List<Tuple<int,int>> listeCoordEntrepot = new List<Tuple<int,int>>();
                    listeCoordEntrepot.Add(pos00);
                    listeCoordEntrepot.Add(pos01);
                    listeCoordEntrepot.Add(pos02);
                    Entrepot ent = new Entrepot(listeCoordEntrepot,20,30,Planete);
                    AjouterBatiment(ent);
                }
                else
                {
                    Console.WriteLine("Impossible de construire ici !");
                }
            }

            // ========================================================== //
            // Construction d'une Auberge
            // ========================================================== //

            else if (numBat == 2)
            { 
                if (positionX + 1 > Planete.Hauteur-1 || positionY +1 > Planete.Largeur-1) // GetLength va de 1 à 30
            {
                Console.WriteLine("Impossible de construire ici !");
            }
            else if (Planete.grille[positionX, positionY + 1] == " x " && Planete.grille[positionX +1, positionY ] == " x " && Planete.grille[positionX + 1, positionY+1] == " x ")
            {
                Planete.grille[positionX, positionY] = " A ";
                Planete.grille[positionX + 1, positionY] = " A ";
                Planete.grille[positionX, positionY + 1] = " A ";
                Planete.grille[positionX + 1, positionY + 1] = " A ";
                Tuple<int,int> pos00 = new Tuple<int,int>(positionX,positionY);
                Tuple<int,int> pos01 = new Tuple<int,int>(positionX,positionY+1);
                Tuple<int,int> pos10 = new Tuple<int,int>(positionX+1,positionY);
                Tuple<int,int> pos11 = new Tuple<int,int>(positionX+1,positionY+1);
                List<Tuple<int,int>> listeCoordAuberge = new List<Tuple<int,int>>();
                listeCoordAuberge.Add(pos00);
                listeCoordAuberge.Add(pos01);
                listeCoordAuberge.Add(pos10);
                listeCoordAuberge.Add(pos11);
                Auberge aub = new Auberge(listeCoordAuberge,30,10,Planete);
                AjouterBatiment(aub);
            }
            else
            {
                Console.WriteLine("Impossible de construire ici !");
            }

            // ========================================================== //
            // Construction d'une Maison
            // ========================================================== //

            }
            else if (numBat == 3)
            { 
                Planete.grille[positionX, positionY] = " M ";
                Tuple<int,int> pos00 = new Tuple<int,int>(positionX,positionY);
                List<Tuple<int,int>> listeCoordMaison = new List<Tuple<int,int>>();
                listeCoordMaison.Add(pos00);
                Maison maison = new Maison(listeCoordMaison,30,0,Planete);
                AjouterBatiment(maison);

            }

            // ========================================================== //
            // Construction d'un Puits
            // ========================================================== //

            else if (numBat == 4)
            { 
                Planete.grille[positionX, positionY] = " P ";
                Tuple<int,int> pos00 = new Tuple<int,int>(positionX,positionY);
                List<Tuple<int,int>> listeCoordPuits = new List<Tuple<int,int>>();
                listeCoordPuits.Add(pos00);
                Puits puits = new Puits(listeCoordPuits,5,15,Planete);
                AjouterBatiment(puits);

            }

            // ========================================================== //
            // Construction d'une Ferme
            // ========================================================== //

            else
            { 
                if (positionX + 1 > Planete.Largeur || positionY +1  > Planete.Largeur) //Il commence au milieu
            {
                Console.WriteLine("Impossible de construire ici !");
            }
            else if (Planete.grille[positionX, positionY + 1] == " x " && Planete.grille[positionX + 1, positionY] == " x ")
            {
                Planete.grille[positionX, positionY] = " F ";
                Planete.grille[positionX + 1, positionY] = " F ";
                Planete.grille[positionX, positionY + 1] = " F ";
                Tuple<int,int> pos00 = new Tuple<int,int>(positionX,positionY);
                Tuple<int,int> pos10 = new Tuple<int,int>(positionX+1,positionY);
                Tuple<int,int> pos01 = new Tuple<int,int>(positionX,positionY+1);
                List<Tuple<int,int>> listeCoordFerme = new List<Tuple<int,int>>();
                listeCoordFerme.Add(pos00);
                listeCoordFerme.Add(pos10);
                listeCoordFerme.Add(pos01);
                Ferme ferme = new Ferme(listeCoordFerme,40,0,Planete);
                AjouterBatiment(ferme);
                
            }
            else
            {
                Console.WriteLine("Impossible de construire ici !");
            }

            }
        }

        public void AjouterBatiment(Batiment b) { Planete.ListeBatiments.Add(b); }

    }

}
