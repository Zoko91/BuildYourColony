﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie.ObjetsFixes.Batiments
{
    class Ferme:Batiment
    {
        // -----------------------------------------------------------------
        // A completer
        // -----------------------------------------------------------------
        protected bool PresencePaysan { get; set; }
        // Si paysan, pousse et production ++, sinon --
        
        public Ferme(List<Tuple<int, int>> positionObjet, int coutBois, int coutPierre,Monde planete) : base(positionObjet, coutBois,coutPierre,planete) { }
        
        public void ProductionBle()
        // -----------------------------------------------------------------
        //  Verifie la présence du Paysan dans la ferme. Si présent, lui 
        //  donne du blé.
        // -----------------------------------------------------------------
        {
            foreach (Colon col in Planete.ListePJ)
            {
                if (positionObjet.Contains(col.getPosition()))
                {
                    if (col.GetType() == typeof(Paysan))
                    {
                        col.Backpack[0] += 3;
                    }
                }
            }
        }

    }
}
