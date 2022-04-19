    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using TPGestionDeColonie.ObjetsFixes;
    using TPGestionDeColonie.ObjetsFixes.Batiments;

    namespace TPGestionDeColonie.Colons
{
    class Tavernier:Colon
    {
        // -----------------------------------------------------------------
        // Le Tavernier permet d'abreuver les colons lorsqu'il se situe dans la taverne
        //      mais également d'aller récolter de l'eau au puits
        // -----------------------------------------------------------------
        public Tavernier(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
              Backpack = new int[]{ 0, 0, 10, 0};  //Bois/Pierre/Eau/Seau d'eau
        }
        
        public bool VerifierStock(int ressource) // Ressource: 0 => eau, 1 => viande
        {
            // La fonction indique si le stock d'eau ou de viande de l'auberge est vide ou pas
            
            // Vérification de la présence du Tavernier dans l'auberge
            bool verifPresence = false;
            bool verifStock = true;
            foreach (Auberge b in Planete.ListeBlocs)
            {
                if (b.PresenceTavernier == true)
                {
                    if (b.StockRessources[ressource] <= 0)
                    {
                        verifStock = false;
                    }
                    break; // Le Tavernier est présent dans la taverne
                } ;
            }
            return verifStock;
        }

        public void AllerRemplirLeSeauDeau()
        {
            // La méthode déplace le colon vers le puits et rempli ses seaux d'eau

            if (Planete.ListeBlocs.OfType<Puits>().Any())
            {
                int x = RechercherPlusProcheItem().Item1;
                int y = RechercherPlusProcheItem().Item2;
                SeDeplacerVersItem(x,y); // A CHANGER AVEC ITERATION
                while (Backpack[3]< 100)
                {
                    RemplirLeSeau();
                }
                // IMPLEMENTER LE DEPLACEMENT DU TAVERNIER VERS LA TAVERNE APRES
                
            }
        }
        public void RemplirLeSeau()
        {
            Backpack[3] += 20;
        }
    }
}
