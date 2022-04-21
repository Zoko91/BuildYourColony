using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;
using TPGestionDeColonie.ObjetsFixes.Batiments;

namespace TPGestionDeColonie.Colons
{
    class Tavernier : Colon
    {
        // -----------------------------------------------------------------
        // Le Tavernier permet d'abreuver les colons lorsqu'il se situe dans la taverne
        //      mais également d'aller récolter de l'eau au puits
        // -----------------------------------------------------------------
        public Tavernier(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
            Backpack = new int[] { 0, 0, 10, 0 };  //Bois / Pierre / Eau / Seau d'eau
        }

        public bool VerifierStock(int ressource) // Ressource: 0 => eau, 1 => Blé
        {
            // La fonction indique si le stock d'eau ou de blé de l'auberge est vide ou pas

            // Vérification de la présence du Tavernier dans l'auberge
            //bool verifPresence = false; 

            // true : Stock est plein ; false : Stock est vide
            bool stockPlein = true;
            foreach (Batiment b in Planete.ListeBatiments)
            {
                if (b.GetType() == typeof(Auberge))
                {
                    Auberge aub = (Auberge)b;
                    if (aub.PresenceTavernier == true) // Le Tavernier est présent dans la taverne
                    {
                        if (aub.StockRessources[ressource] <= 0) // Le stock est vide
                        {
                            stockPlein = false;
                        }
                        break;
                    };

                }
            }
            return stockPlein;
        }



        public void AllerRemplirRessource()
        {
            // La méthode déplace le colon vers le puits et rempli ses seaux d'eau

            if (VerifierStock(0) == false)
            {
                if (Planete.ListeBatiments.OfType<Puits>().Any())
                {
                    int x = RechercherPlusProcheItem().Item1;
                    int y = RechercherPlusProcheItem().Item2;
                    AcquerirCible();
                    DefinirCible(x, y);
                }
                else
                {
                    Console.WriteLine("Pensez à construire un puits, la taverne ne dispose plus d'eau et le tavernier ne peut aller en chercher... !");
                }
            }
            else if (VerifierStock(1) == false)
            {
                Console.WriteLine("Pensez a utiliser le paysan pour produire de la nourriture !");
            }
        }


        public void RemplirLeSeau()
        {
            AvoirFaimEtSoifSiAction();
            Backpack[3] += 20;
        }
    }
}
