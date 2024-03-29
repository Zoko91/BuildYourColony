﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TPGestionDeColonie.ObjetsFixes;

namespace TPGestionDeColonie
{
    class Paysan : Colon
    {
        // -----------------------------------------------------------------
        // Le Paysan peut récolter ou semer du blé, ces actions sont définies par le Joueur
        // -----------------------------------------------------------------

        public Paysan(string nom, int positionX, int positionY, int endurance, int sante, int faim, int soif, Monde planete) : base(nom, positionX, positionY, endurance, sante, faim, soif, planete)
        {
            // Redéfinition du Backpack pour le paysan
            Backpack = new int[] { 10, 100 }; // ble / eau
        }


        public override void Recolter()
        {
            Tuple<int, int> positionBle = new Tuple<int, int>(positionX, positionY);
            foreach(ObjetFixe obj in Planete.ListeBlocs)
            {
                if (obj.GetType()==typeof(Ble))
                {
                    if (obj.GetPositionObjet().Contains(positionBle))
                    {
                        Console.WriteLine($"Récolte en cours.... {Backpack[0]}%");
                        Backpack[0] += 10;
                        Planete.ListeBlocs.Find(z => z.GetPositionObjet().Contains(positionBle)).DestructionEnCours(positionX, positionY, this);
                        break;
                    }
                }
            }
        }

        public override void Planter()
        {
            if (Backpack[1] >= 5)
            {
                Tuple<int, int> positionPaysan = getPosition();
                List<Tuple<int, int>> listePositionPaysan = new List<Tuple<int, int>>();
                listePositionPaysan.Add(positionPaysan);
                Ble nouveauBle = new Ble(listePositionPaysan, Planete);
                AvoirFaimEtSoifSiAction();
                Planete.ListeBlocs.Add(nouveauBle);
                Backpack[1] -= 5;

            }
            else
            {
                Console.WriteLine("Vous n'avez pas assez d'eau pour planter du blé.");
                Console.WriteLine($"Il vous manque: {5 - Backpack[1]} Eau, dirigez vous vers l'Auberge");
            }
        }

    }
}
