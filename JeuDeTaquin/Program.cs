using JeuxLib;
using System;

namespace JeuDeTaquin
{
    /// <summary>
    /// Auteurs: Michael Meilleur et Mahdi Ellili.
    /// Description: Jeu de taquin.
    /// Date: 2022-02-17
    /// </summary>
    class Program
    {
        #region Champs
        static JeuDeTaquinLogique _Jeu = new JeuDeTaquinLogique();
        #endregion

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili. 
        /// Description: Afficher la grille de jeu.
        /// Date: 2022-02-17
        /// </summary>
        #region Méthodes
        private static void AfficherGrille()
        {
            //Variables locales.
            byte byNbLigne = 0;
            bool bCapteur = false;

            Console.WriteLine("╔══════╦══════╦══════╦══════╗ Déplacements: " + _Jeu.Déplacements);
            while (byNbLigne < _Jeu.Grille.GetLength(0))
            {

                for (int iColonne = 0; iColonne < _Jeu.Grille.GetLength(1); iColonne++)
                {
                    if (_Jeu.Grille[byNbLigne, iColonne] == 20)
                    {
                        Console.Write("║  ");
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(_Jeu.Grille[byNbLigne, iColonne].ToString().PadLeft(2, ' '));
                        Console.ResetColor();
                        Console.Write("  ");
                        if (iColonne==3)
                            Console.Write("║");
                        if (byNbLigne == 0 && iColonne == 3)
                          Console.Write(" Record: " + _Jeu.Record);
            
                    }
                    else if (byNbLigne == 0 && bCapteur == false && iColonne == 3)
                    {
                        Console.Write("║  " + _Jeu.Grille[byNbLigne, iColonne].ToString().PadLeft(2, ' ') +   "  ║ Record: " + _Jeu.Record);
                        bCapteur = true;
                    }
                    else if (iColonne == _Jeu.Grille.GetLength(1) - 1)
                        Console.Write("║  " + _Jeu.Grille[byNbLigne, iColonne].ToString().PadLeft(2, ' ') + "  ║");
                    else
                        Console.Write("║  " + _Jeu.Grille[byNbLigne, iColonne].ToString().PadLeft(2, ' ') + "  ");
                }
                Console.WriteLine();
                if (byNbLigne == 4)
                    Console.WriteLine("╚══════╩══════╩══════╩══════╝");
                else
                    Console.WriteLine("╠══════╬══════╬══════╬══════╣");

                byNbLigne++;
            }
        }

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Afficher un message d'erreur (Hors limite)
        /// Date: 2022-03-03
        /// </summary>
        static void MessageErreur()
        {
            Console.WriteLine("Hors limites!");
            Console.Write("Appuyer sur une touche...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Afficher un message 
        /// Date: 2022-03-03
        /// </summary>
        /// <param name="sMessage">Message</param>
        /// <param name="bPause">Booléen</param>
        static void AfficherMessage(string sMessage, bool bPause)
        {
            if(bPause == true)
            Console.WriteLine(sMessage);
        }
        #endregion

        static void Main(string[] args)
        {
            // Jouer au jeu
            _Jeu.InitialiserGrille();
            AfficherGrille();

            // Afficher les choix.
            Console.WriteLine("Flèches pour déplacer.");
            Console.WriteLine("Q=Quitter");
            Console.WriteLine("R=Recommencer.");
            Console.WriteLine("S=Résoudre");
            Console.Write("Direction: ");

            do
            {
                Console.Clear();
                AfficherGrille();

                // Afficher les choix.
                Console.WriteLine("Flèches pour déplacer.");
                Console.WriteLine("Q=Quitter");
                Console.WriteLine("R=Recommencer.");
                Console.WriteLine("S=Résoudre");
                Console.Write("Direction: ");
                if (_Jeu.DéterminerGrilleRésolue() == true)
                {
                    AfficherMessage("Bravo! Appuyez sur R pour recommencer.", true);
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (_Jeu._Ligne == 0)
                            MessageErreur();
                        else
                        {
                            _Jeu.DéplacerVers(JeuDeTaquinLogique.Directions.HAUT);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (_Jeu._Ligne == 4)
                            MessageErreur();
                        else
                        {
                            _Jeu.DéplacerVers(JeuDeTaquinLogique.Directions.BAS);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (_Jeu._Colonne == 0)
                            MessageErreur();
                        else
                        {
                            _Jeu.DéplacerVers(JeuDeTaquinLogique.Directions.GAUCHE);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (_Jeu._Colonne == 3)
                            MessageErreur();
                        else
                        {
                            _Jeu.DéplacerVers(JeuDeTaquinLogique.Directions.DROITE);
                        }
                        break;
                    case ConsoleKey.R:
                        Console.Clear();
                        _Jeu.InitialiserGrille();
                        AfficherGrille();
                        break;
                    case ConsoleKey.S:
                        Console.Clear();
                        _Jeu.Résoudre();
                        break;
                    case ConsoleKey.Q:
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Au revoir!");
                        _Jeu._bQuitter = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("ERREUR: Choix invalide");
                        Console.Write("Appuyer sur une touche...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (!_Jeu._bQuitter);

        }
    }
}
