using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes.Batiments;
using TPGestionDeColonie.ObjetsFixes;

namespace TPGestionDeColonie
{
    class Batisseur:Colon
    {
        // Créer la liste des capacités de base
        // List<string> capacites
        public Batisseur(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
            
        }
        
        
        public void ViderEntrepot(int bois, int pierre){
            Entrepot ent = Planete.ListeBlocs.OfType<Entrepot>().FirstOrDefault();
            Backpack[0]+= bois;
            Backpack[1]+= pierre;
            ent.StockRessources[0] -= bois;
            ent.StockRessources[1] -= pierre;
        }

        public void RemplirLeStock() {
            Console.WriteLine("Choisissez le numéro du batiment à construire:\n- 1 : Entrepôt (Bois : 20, Roche : 30)\n"
            + "- 2 : Taverne (Bois : 30, Roche : 10\n- 3 : Maison (Bois : 30, Roche : 0)\n"
            +"- 4 : Puits (Bois : 5, Roche : 15)\n- 5 : Ferme (Bois : 40, Roche : 0)");
            int numBat = int.Parse(Console.ReadLine());
            int besoinPierre = 0;
            int besoinBois = 0;
            Entrepot ent = Planete.ListeBlocs.OfType<Entrepot>().FirstOrDefault();
            if(numBat == 1){
                if(Backpack[0]<20 || Backpack[1]<30) {
                    if(Planete.ListeBlocs.OfType<Entrepot>().Any()){
                        if(Backpack[0]<20){
                            besoinBois = 20 - Backpack[0];
                        }
                        if(Backpack[1]<30){
                            besoinPierre = 30 - Backpack[1];
                        }

                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1,ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois,besoinPierre);
                    }
                }
            }
            else if(numBat == 2){
                if(Backpack[0]<30 || Backpack[1]<10) {
                    if(Planete.ListeBlocs.OfType<Entrepot>().Any()){
                        if(Backpack[0]<30){
                            besoinBois = 30 - Backpack[0];
                        }
                        if(Backpack[1]<10){
                            besoinPierre = 10 - Backpack[1];
                        }

                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1,ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois,besoinPierre);
                    }
                }
                
            }
            else if(numBat == 3){
                if(Backpack[0]<30) {
                    if(Planete.ListeBlocs.OfType<Entrepot>().Any()){
                        if(Backpack[0]<30){
                            besoinBois = 30 - Backpack[0];
                        }
                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1,ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois,besoinPierre);
                    }
                }
            }
            else if(numBat == 4){
                if(Backpack[0]<5 || Backpack[1]<15) {
                    if(Planete.ListeBlocs.OfType<Entrepot>().Any()){
                        if(Backpack[0]<5){
                            besoinBois = 5 - Backpack[0];
                        }
                        if(Backpack[1]<15){
                            besoinPierre = 15 - Backpack[1];
                        }
                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1,ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois,besoinPierre);
                    }
                }
            }
            else if(numBat == 5){
                if(Backpack[0]<40) {
                    if(Planete.ListeBlocs.OfType<Entrepot>().Any()){
                        if(Backpack[0]<40){
                            besoinBois = 40 - Backpack[0];
                        }
                        Deplacer(ent.GetPositionObjet().FirstOrDefault().Item1,ent.GetPositionObjet().FirstOrDefault().Item2);
                        ViderEntrepot(besoinBois,besoinPierre);
                    }
                }
            }

        }

        public void Construire(int numBat){
            // Detecte quel batiment la personne veut construire (utilisé dnas remplir les stocks, refaire une fonction)
        }

    }

}
