using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxLib
{
    /// <summary>
    /// Auteur: Michael Meilleur et Mahdi Ellili
    /// Description: Logique pour le jeu de taquin
    /// Date: 2022-03-25
    /// </summary>
    public class JeuDeTaquinLogique
    {
        #region Énumérations
        public enum Directions
        {
            BAS,
            HAUT,
            DROITE,
            GAUCHE
        }
        #endregion

        #region Champs
        private byte[,] _abyGrille = new byte[5, 4];
        public byte[] _abyCoordonnéesCaseVide = new byte[1];
        public bool _bQuitter = false;
        public byte _Ligne = 0;
        public byte _Colonne = 0;
        static private Random _rnd = new Random();

        #endregion

        #region Propriétés
        public byte[,] Grille
        {
            get
            {
                return _abyGrille;
            }
        }
        public int Déplacements { get; private set; }
        public int Record { get; private set; }

        options _options = new options();
        #endregion

        #region Méthodes

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Déterminer si le joueur a gagné.
        /// Date: 2022-03-03
        /// </summary>
        /// <returns></returns>
        public bool DéterminerGrilleRésolue()
        {
            // Variables locales
            bool bGagné = false;
            string sRéponseJoueur = "";
            string sRéponse = "1234567891011121314151617181920";

            for (int _Ligne = 0; _Ligne < _abyGrille.GetLength(0); _Ligne++)
            {
                for (int _Colonne = 0; _Colonne < _abyGrille.GetLength(1); _Colonne++)
                {
                    sRéponseJoueur += _abyGrille[_Ligne, _Colonne];
                }
            }

            if (sRéponse == sRéponseJoueur)
                bGagné = true;

            if (bGagné == true && Record == 0)
                Record = Déplacements;
            else if (bGagné == true && Déplacements > Record)
                Record = Record;
            else if (bGagné == true && Déplacements < Record)
                Record = Déplacements;

            return bGagné;
        }

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Permet de résoudre la grille.
        /// Date: 2022-03-01
        /// </summary>
        public void Résoudre()
        {

            // Variables locales
            byte byChiffre = 1;

            // Remplir le tableau avec la suite de 1 à 20.
            for (int iLigne = 0; iLigne < _abyGrille.GetLength(0); iLigne++)
            {
                for (int iColonne = 0; iColonne < _abyGrille.GetLength(1); iColonne++)
                {
                    _abyGrille[iLigne, iColonne] = byChiffre;

                    byChiffre += 1;
                }
            }
        }

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Permuter des index
        /// Date: 2022-03-01
        /// </summary>
        /// <param name="iAx">Ligne 1 case rouge</param>
        /// <param name="iAy">Colonne 1 case rouge</param>
        /// <param name="iBx">Ligne 2</param>
        /// <param name="iBy">Colonne 2</param>
        private void Permuter(int iAx, int iAy, int iBx, int iBy)
        {
            //Variable locales
            byte byTemporaire = 0;

            byTemporaire = _abyGrille[iAx, iAy];
            _abyGrille[iAx, iAy] = _abyGrille[iBx, iBy];
            _abyGrille[iBx, iBy] = byTemporaire;
        }

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Initier la grille
        /// Date: 2022-03-01
        /// </summary>
        public void InitialiserGrille()
        {
            Résoudre();

            Déplacements = 0;
            _Ligne = 4;
            _Colonne = 3;
            int iRandom = 0;

            // Bouger les chiffres aléatoirement 400 fois.
            for (int iCpt = 0; iCpt <= 400; iCpt++)
            {
                iRandom = _rnd.Next(1, 4 + 1);
                switch (iRandom)
                {
                    case 1:
                        if (_Ligne < 4)
                        {
                            Permuter(_Ligne, _Colonne, _Ligne + 1, _Colonne);
                            _Ligne += 1;
                        }
                        break;
                    case 2:
                        if (_Ligne > 0)
                        {
                            Permuter(_Ligne, _Colonne, _Ligne - 1, _Colonne);
                            _Ligne -= 1;
                        }
                        break;
                    case 3:
                        if (_Colonne < 3)
                        {
                            Permuter(_Ligne, _Colonne, _Ligne, _Colonne + 1);
                            _Colonne += 1;
                        }
                        break;
                    case 4:
                        if (_Colonne > 0)
                        {
                            Permuter(_Ligne, _Colonne, _Ligne, _Colonne - 1);
                            _Colonne -= 1;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Déplacer la case rouge
        /// Date: 2022-03-01
        /// </summary>
        /// <param name="direction">Direction souhaitée</param>
        public void DéplacerVers(Directions direction)
        {
            // Lire le choix et permuter.
            switch (direction)
            {
                case Directions.BAS:
                    Permuter(_Ligne, _Colonne, _Ligne + 1, _Colonne);
                    _Ligne += 1;
                    Déplacements += 1;
                    break;
                case Directions.HAUT:
                    Permuter(_Ligne, _Colonne, _Ligne - 1, _Colonne);
                    _Ligne -= 1;
                    Déplacements += 1;
                    break;
                case Directions.DROITE:
                    Permuter(_Ligne, _Colonne, _Ligne, _Colonne + 1);
                    _Colonne += 1;
                    Déplacements += 1;
                    break;
                case Directions.GAUCHE:
                    Permuter(_Ligne, _Colonne, _Ligne, _Colonne - 1);
                    _Colonne -= 1;
                    Déplacements += 1;
                    break;
                default:
                    break;
            }
            #endregion
        }
    }
}
