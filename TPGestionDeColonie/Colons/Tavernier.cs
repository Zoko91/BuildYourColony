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
        public Tavernier(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
              Backpack = new int[]{ 0, 0, 10, 0};  //Bois/Pierre/Eau/Seau d'eau
        }
        
        public bool VerifierStock(int ressource) // Ressource = 0 => eau, = 1 => blé , = 2 => viande
        {
            //On a le monde, vérifier si il est sur la taverne
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
            if (Planete.ListeBlocs.OfType<Puits>().Any())
            {
                int x = RechercherPlusProcheItem()[0];
                int y = RechercherPlusProcheItem()[1];
                SeDeplacerVersItem(x,y);
                while (Backpack[3]< 100)
                {
                    RemplirLeSeau();
                }
                AllerVersBatiment("Auberge");
                
            }
        }
        public void RemplirLeSeau()
        {
            Backpack[3] += 20;
        }
        

    }
}
