using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPGestionDeColonie
{
    class ControleurMenu
    {
        public static int Menu(string nomMenu)
        
            
        {
        // -----------------------------------------------------------------------------
        // // Création du menu navigable avec les boutons: "fleche haut" et "fleche bas"
        // -----------------------------------------------------------------------------

            if (nomMenu == "Principal")
            {
                //options du menu
                string[] optionsMenu = { " Jouer ", " Commande à connaître ", " Quitter " };

                ConsoleKey key;

                Console.CursorVisible = false;
                int choixActuel = 0;

                do
                {
                    for (int i = 0; i < optionsMenu.Length; i++)
                    {
                        if (choixActuel == i)
                        {// change la couleur de la touche sélectionnée
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(Console.WindowWidth / 4, i + Console.WindowHeight / 2);
                            Console.WriteLine(optionsMenu[i]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 4, i + Console.WindowHeight / 2);
                            Console.WriteLine(optionsMenu[i]);
                        }

                    }
                    key = Console.ReadKey().Key; // récupérer la touche

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            //remonte dans les choix
                            if (choixActuel != 0)
                            {
                                choixActuel--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            //descend dans les choix
                            if (choixActuel != optionsMenu.Length)
                            {
                                choixActuel++;
                            }
                            break;
                    }

                } while (key != ConsoleKey.Enter);


                Console.CursorVisible = false;
                return choixActuel;
            }

            return 0;
        }
    }
}
