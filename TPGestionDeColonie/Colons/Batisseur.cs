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
        // -----------------------------------------------------------------
        // Le Batisseur est un colon permettant de construire des Batiments
        // -----------------------------------------------------------------
        
        public bool EstOccupe { get; set; } // Possède une action en cours d'exécution

        public Batisseur(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
            Backpack = new int[] { 50, 50, 10 }; // bois ; pierre ; eau ;
            EstOccupe = false;
        }


        public void ViderEntrepot(int bois, int pierre)
        {
            // -----------------------------------------------------------------
            // La méthode permet de transférer les ressources de l'entrepôt vers le backpack du batisseur
            // -----------------------------------------------------------------
            
            Entrepot ent = Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault();
            if (ent.StockRessources[0] >= bois && ent.StockRessources[1] >= pierre)
            {
                Backpack[0] += bois;
                Backpack[1] += pierre;
                ent.StockRessources[0] -= bois;
                ent.StockRessources[1] -= pierre;
                AvoirFaimEtSoifSiAction();
            }
            else
            {
                Console.WriteLine("Pas assez de stock dans l'entrepôt");
                
            }

        }

        public void RemplirLeStock(int numBat) // Récupère le nombre de ressources nécessaires à l'entrepôt
        {
            // -------------------------------------------------------------------------------------------------------------
            // La méthode permet de récupérer le nombre de ressources manquantes pour construire un batiment donné en input 
            // -------------------------------------------------------------------------------------------------------------

            int besoinPierre = 0;
            int besoinBois = 0;
            Entrepot ent = Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault();
            if (numBat == 1)
            {
                if (Backpack[0] < 20 || Backpack[1] < 30)
                {
                    if (Planete.ListeBatiments.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 20)
                        {
                            besoinBois = 20 - Backpack[0];
                        }
                        if (Backpack[1] < 30)
                        {
                            besoinPierre = 30 - Backpack[1];
                        }

                        if (ent.GetPositionObjet().Contains(getPosition()))
                        {
                            
                            ViderEntrepot(besoinBois, besoinPierre);
                            PerdreCible();
                        }

                    }
                }
            }
            else if (numBat == 2)
            {
                if (Backpack[0] < 30 || Backpack[1] < 10)
                {
                    if (Planete.ListeBatiments.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 30)
                        {
                            besoinBois = 30 - Backpack[0];
                        }
                        if (Backpack[1] < 10)
                        {
                            besoinPierre = 10 - Backpack[1];
                        }

                        if (ent.GetPositionObjet().Contains(getPosition()))
                        {
                            ViderEntrepot(besoinBois, besoinPierre);
                            PerdreCible();
                        }
                    }
                }

            }
            else if (numBat == 3)
            {
                if (Backpack[0] < 30)
                {
                    if (Planete.ListeBatiments.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 30)
                        {
                            besoinBois = 30 - Backpack[0];
                        }
                        if (ent.GetPositionObjet().Contains(getPosition()))
                        {
                            ViderEntrepot(besoinBois, besoinPierre);
                            PerdreCible();
                        }
                    }
                }
            }
            else if (numBat == 4)
            {
                if (Backpack[0] < 5 || Backpack[1] < 15)
                {
                    if (Planete.ListeBatiments.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 5)
                        {
                            besoinBois = 5 - Backpack[0];
                        }
                        if (Backpack[1] < 15)
                        {
                            besoinPierre = 15 - Backpack[1];
                        }

                        if (ent.GetPositionObjet().Contains(getPosition()))
                        {
                            ViderEntrepot(besoinBois, besoinPierre);
                            PerdreCible();
                        }
                    }
                }
            }
            else if (numBat == 5)
            {
                if (Backpack[0] < 40)
                {
                    if (Planete.ListeBatiments.OfType<Entrepot>().Any())
                    {
                        if (Backpack[0] < 40)
                        {
                            besoinBois = 40 - Backpack[0];
                        }
                        if (ent.GetPositionObjet().Contains(getPosition()))
                        {
                            ViderEntrepot(besoinBois, besoinPierre);
                            PerdreCible();
                        }
                    }
                }
            }

        }

        public override void Construire(int numBat)
        {
            // ----------------------------------------- //
            // Méthode permettant de constuire tout type d'objet {utilisée ici pour des batiments}
            // ----------------------------------------- //
            PerdreCible(); // Mission accomplie, plus de cible
            ConstructionBatiment(numBat);
        }

        public void ConstructionBatiment(int numBat)
        {
            // ========================================================== //
            //                Construction d'un Entrepot                  //
            // ========================================================== //

            if (numBat == 1)
            {
                if (positionX + 1 > Planete.Hauteur - 1 || positionX - 1 < 0) //Il commence au milieu
                {
                    Console.WriteLine("Impossible de construire ici !");
                }
                else if (Planete.grille[positionX, positionY + 1] == " x " && Planete.grille[positionX, positionY - 1] == " x ")
                {
                    if (Backpack[0] >= 20 && Backpack[1] >= 30)
                    {
                        Planete.grille[positionX, positionY] = " E ";
                        Planete.grille[positionX, positionY - 1] = " E ";
                        Planete.grille[positionX, positionY - 1] = " E ";
                        
                        Tuple<int, int> pos00 = new Tuple<int, int>(positionX, positionY - 1);
                        Tuple<int, int> pos01 = new Tuple<int, int>(positionX, positionY);
                        Tuple<int, int> pos02 = new Tuple<int, int>(positionX, positionY + 1);
                        
                        List<Tuple<int, int>> listeCoordEntrepot = new List<Tuple<int, int>>();
                        listeCoordEntrepot.Add(pos00);
                        listeCoordEntrepot.Add(pos01);
                        listeCoordEntrepot.Add(pos02);
                        Entrepot ent = new Entrepot(listeCoordEntrepot, 20, 30, Planete); 

                        Backpack[0] -= 20;
                        Backpack[1] -= 30;
                        
                        AvoirFaimEtSoifSiAction();
                        AjouterBatiment(ent);
                    }
                    else
                    {
                        // Si le batisseur ne dispose pas d'assez de ressources, il se dirige vers l'entrepot (avant l'appel de ConstructionBatiment on vérifié si un Entrepot est déjà défini, sinon on le force)
                        // Le batisseur de dirige alors vers le milieu de l'entrepot, qui devient sa cible
                        Console.WriteLine("Le batisseur ne dispose pas d'assez de ressources pour cela ! ...");
                        Console.WriteLine("Le batisseur de dirige donc vers l'entrepot ...");
                        DefinirCible(Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item1,
                            Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item2);
                        AcquerirCible();
                    }
                }
                else
                {
                    // L'espace sélectionné n'est pas disponible
                    Console.WriteLine("Impossible de construire ici !");
                }
            }

            // ========================================================== //
            //              Construction d'une Auberge                    //
            // ========================================================== //

            else if (numBat == 2)
            {

                if (positionX + 1 > Planete.Hauteur - 1 || positionY + 1 > Planete.Largeur - 1) // GetLength va de 1 à 30
                {
                    Console.WriteLine("Impossible de construire ici !");
                }
                else if (Planete.grille[positionX, positionY + 1] == " x " && Planete.grille[positionX + 1, positionY] == " x " && Planete.grille[positionX + 1, positionY + 1] == " x ")
                {
                    if (Backpack[0] >= 30 && Backpack[1] >= 10)
                    {
                        Planete.grille[positionX, positionY] = " A ";
                        Planete.grille[positionX + 1, positionY] = " A ";
                        Planete.grille[positionX, positionY + 1] = " A ";
                        Planete.grille[positionX + 1, positionY + 1] = " A ";
                        
                        Tuple<int, int> pos00 = new Tuple<int, int>(positionX, positionY);
                        Tuple<int, int> pos01 = new Tuple<int, int>(positionX, positionY + 1);
                        Tuple<int, int> pos10 = new Tuple<int, int>(positionX + 1, positionY);
                        Tuple<int, int> pos11 = new Tuple<int, int>(positionX + 1, positionY + 1);
                        
                        List<Tuple<int, int>> listeCoordAuberge = new List<Tuple<int, int>>();
                        listeCoordAuberge.Add(pos00);
                        listeCoordAuberge.Add(pos01);
                        listeCoordAuberge.Add(pos10);
                        listeCoordAuberge.Add(pos11);
                        Auberge aub = new Auberge(listeCoordAuberge, 30, 10, Planete);

                        Backpack[0] -= 30;
                        Backpack[1] -= 10;
                        
                        AvoirFaimEtSoifSiAction();
                        AjouterBatiment(aub);
                    }
                    else
                    {
                        Console.WriteLine("Le batisseur ne dispose pas d'assez de ressources pour cela !");
                        Console.WriteLine("Le batisseur de dirige vers l'entrepot");
                        DefinirCible(Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item1,
                            Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item2);
                        AcquerirCible();
                    }
                }
                else
                {
                    Console.WriteLine("Impossible de construire ici !");
                }
            }

                                // ========================================================== //
                                //                 Construction d'une Maison                  //
                                // ========================================================== //

            else if (numBat == 3)
            {
                if (Backpack[0] >= 30)
                {
                    Planete.grille[positionX, positionY] = " M ";
                    Tuple<int, int> pos00 = new Tuple<int, int>(positionX, positionY);
                    List<Tuple<int, int>> listeCoordMaison = new List<Tuple<int, int>>();
                    listeCoordMaison.Add(pos00);
                    Maison maison = new Maison(listeCoordMaison, 30, 0, Planete);

                    Backpack[0] -= 30;
                    Backpack[1] -= 0;

                    AvoirFaimEtSoifSiAction();
                    AjouterBatiment(maison);
                }
                else
                {
                    Console.WriteLine("Le batisseur ne dispose pas d'assez de ressources pour cela !");
                    Console.WriteLine("Le batisseur de dirige vers l'entrepot");
                    DefinirCible(Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item1,
                        Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item2);
                    AcquerirCible();
                }

            }

                                // ========================================================== //
                                //                   Construction d'un Puits                  //
                                // ========================================================== //

            else if (numBat == 4)
            {
                if (Backpack[0] >= 5 && Backpack[1] >= 15)
                {
                    Planete.grille[positionX, positionY] = " P ";
                    Tuple<int, int> pos00 = new Tuple<int, int>(positionX, positionY);
                    List<Tuple<int, int>> listeCoordPuits = new List<Tuple<int, int>>();
                    listeCoordPuits.Add(pos00);
                    Puits puits = new Puits(listeCoordPuits, 5, 15, Planete);

                    Backpack[0] -= 5;
                    Backpack[1] -= 15;
                
                    AvoirFaimEtSoifSiAction();
                    AjouterBatiment(puits);
                }
                else{
                    Console.WriteLine("Le batisseur ne dispose pas d'assez de ressources pour cela !");
                    Console.WriteLine("Le batisseur de dirige vers l'entrepot");
                    DefinirCible(Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item1,
                        Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item2);
                    AcquerirCible();
                }

            }

                                // ========================================================== //
                                //                 Construction d'une Ferme                   //
                                // ========================================================== //

            else
            {
                if (positionX + 1 > Planete.Largeur || positionY + 1 > Planete.Largeur) //Il commence au milieu
                {
                    Console.WriteLine("Impossible de construire ici !");
                }
                else if (Planete.grille[positionX, positionY + 1] == " x " && Planete.grille[positionX + 1, positionY] == " x ")
                {
                    if (Backpack[0] >= 40)
                {
                    Planete.grille[positionX, positionY] = " F ";
                    Planete.grille[positionX + 1, positionY] = " F ";
                    Planete.grille[positionX, positionY + 1] = " F ";
                    
                    Tuple<int, int> pos00 = new Tuple<int, int>(positionX, positionY);
                    Tuple<int, int> pos10 = new Tuple<int, int>(positionX + 1, positionY);
                    Tuple<int, int> pos01 = new Tuple<int, int>(positionX, positionY + 1);
                    
                    List<Tuple<int, int>> listeCoordFerme = new List<Tuple<int, int>>();
                    listeCoordFerme.Add(pos00);
                    listeCoordFerme.Add(pos10);
                    listeCoordFerme.Add(pos01);
                    Ferme ferme = new Ferme(listeCoordFerme, 40, 0, Planete);

                    Backpack[0] -= 40;
                    Backpack[1] -= 0;
                    
                    AvoirFaimEtSoifSiAction();
                    AjouterBatiment(ferme);
                    }
                    else{
                        Console.WriteLine("Le batisseur ne dispose pas d'assez de ressources pour cela !");
                        Console.WriteLine("Le batisseur de dirige vers l'entrepot");
                        DefinirCible(Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item1,
                            Planete.ListeBatiments.OfType<Entrepot>().FirstOrDefault().GetPositionObjet()[1].Item2);
                        AcquerirCible();
                    }

                }
                else
                {
                    Console.WriteLine("Impossible de construire ici !");
                }

            }
        }

        public void AjouterBatiment(Batiment b) { Planete.ListeBatiments.Add(b); } // Ajoute un batiment à la liste des batiments contenue dans le Monde
        
        public string ToString2(){ return $"\tLe batisseur {Nom} possède {Backpack[0]} Bois et {Backpack[1]} pierres\n\t________________________";}

    }

}
