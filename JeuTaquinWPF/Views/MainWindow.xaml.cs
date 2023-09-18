using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JeuxLib;
using JeuTaquinWPF.Views;

namespace JeuTaquinWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Auteurs: Michael Meilleur et Mahdi Ellili
    /// Description: Application WPF qui permet de jouer au Jeu de taquin.
    /// Date: 2022-04-07
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constantes
        const string URI = "pack://application:,,,/Images/Steve Jobs";
        const string URI_SONG = "pack://siteoforigin:,,,/Sons/";
        #endregion

        #region Champs
        private JeuDeTaquinLogique _JeuTaquin = new JeuDeTaquinLogique();
        private Label[,] _tableau = new Label[5, 4];
        private options _option = new options();
        #endregion

        #region Constructeurs
        public MainWindow()
        {
            InitializeComponent();

        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description : Méthode pour lire le son lorsque la grille est résolue.
        /// Date: 2022-04-13
        /// </summary>
        private void SonRéussi()
        {
            //Variables locales 
            MediaPlayer Song = new MediaPlayer();

            Song.Open(new Uri(URI_SONG + "Success.mp3"));
            Song.Play();
        }
        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Afficher la grille.
        /// Date: 2022-03-31
        /// </summary>
        private void AfficherTableau()
        {
            // Variables locales
            int iLigne = 0;
            int iColonne = 0;
            int iCpt = 1;

            if (_option.Images == true)
            {
                for (int iPosition = 0; iPosition < grdTableau.Children.Count; iPosition++)
                {
                    iLigne = Grid.GetRow(grdTableau.Children[iPosition]);
                    iColonne = Grid.GetColumn(grdTableau.Children[iPosition]);
                    _tableau[iLigne, iColonne] = (Label)grdTableau.Children[iPosition];
                    _tableau[iLigne, iColonne].Content = _JeuTaquin.Grille[iLigne, iColonne];
                    if (_JeuTaquin.Grille[iLigne, iColonne] == 20)
                    {
                        _tableau[iLigne, iColonne].Foreground = Brushes.Gray;
                        _tableau[iLigne, iColonne].Background = Brushes.Gray;
                    }
                    else
                    {
                        _tableau[iLigne, iColonne].Foreground = Brushes.Transparent;
                        _tableau[iLigne, iColonne].Background = Images(_tableau[iLigne, iColonne], _JeuTaquin.Grille[iLigne, iColonne]);
                    }
                }
            }
            else if (_option.FondCouleur == false)
            {
                for (int iPosition = 0; iPosition < grdTableau.Children.Count; iPosition++)
                {
                    iLigne = Grid.GetRow(grdTableau.Children[iPosition]);
                    iColonne = Grid.GetColumn(grdTableau.Children[iPosition]);
                    _tableau[iLigne, iColonne] = (Label)grdTableau.Children[iPosition];
                    _tableau[iLigne, iColonne].Content = _JeuTaquin.Grille[iLigne, iColonne];
                    if (_JeuTaquin.Grille[iLigne, iColonne] == 20)
                    {
                        _tableau[iLigne, iColonne].Foreground = Brushes.Gray;
                        _tableau[iLigne, iColonne].Background = Brushes.Gray;
                    }
                    else
                    {
                        _tableau[iLigne, iColonne].Foreground = Brushes.Black;
                        _tableau[iLigne, iColonne].Background = Brushes.White;
                    }
                }
            }
            else // Option avec fond de couleur.
            {
                for (int iPosition = 0; iPosition < grdTableau.Children.Count; iPosition++)
                {
                    iLigne = Grid.GetRow(grdTableau.Children[iPosition]);
                    iColonne = Grid.GetColumn(grdTableau.Children[iPosition]);
                    _tableau[iLigne, iColonne] = (Label)grdTableau.Children[iPosition];
                    _tableau[iLigne, iColonne].Content = _JeuTaquin.Grille[iLigne, iColonne];
                    if (_JeuTaquin.Grille[iLigne, iColonne] == 20)
                    {
                        _tableau[iLigne, iColonne].Foreground = Brushes.Gray;
                        _tableau[iLigne, iColonne].Background = Brushes.Gray;
                    }
                    else if (_JeuTaquin.Grille[iLigne, iColonne] == iCpt)
                    {
                        _tableau[iLigne, iColonne].Foreground = Brushes.Black;
                        _tableau[iLigne, iColonne].Background = Brushes.LightGreen;
                    }
                    else
                    {
                        _tableau[iLigne, iColonne].Foreground = Brushes.Black;
                        _tableau[iLigne, iColonne].Background = Brushes.White;
                    }
                    iCpt++;
                }
            }
        }

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Remplir la grille avec des chiffres.
        /// Date: 2022-03-31
        /// </summary>
        private void RemplirTableauJeu()
        {
            _JeuTaquin.InitialiserGrille();
            AfficherTableau();
        }

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Résoudre la grille.
        /// Date: 2022-03-31
        /// </summary>
        private void Résoudre()
        {
            _JeuTaquin.Résoudre();
            _JeuTaquin.DéterminerGrilleRésolue();
            txtRecord.Text = _JeuTaquin.Record.ToString("");
            imgFlecheBas.IsEnabled = false;
            imgFlecheHaut.IsEnabled = false;
            imgFlecheGauche.IsEnabled = false;
            imgFlecheDroite.IsEnabled = false;
            AfficherTableau();
        }

        /// <summary>
        /// Auteurs: Michael Meilleur et Mahdi Ellili
        /// Description: Déterminer si le joueur a gagné.
        /// Date: 2022-03-31
        /// </summary>
        private void VérifierVictoire()
        {
            if (_JeuTaquin.DéterminerGrilleRésolue() == true)
            {
                _JeuTaquin.DéterminerGrilleRésolue();
                if(_option.Son == true)
                SonRéussi();

                txtRecord.Text = _JeuTaquin.Record.ToString("");
                lblPartieGagnée.Content = "Trouvé!";
                imgFlecheBas.IsEnabled = false;
                imgFlecheHaut.IsEnabled = false;
                imgFlecheGauche.IsEnabled = false;
                imgFlecheDroite.IsEnabled = false;
                AfficherTableau();
            }
        }

        /// <summary>
        /// Auteurs: Michael Meilleut et Mahdi Ellili
        /// Description: Retourner une image pour remplir un label.
        /// Date: 2022-04-08
        /// </summary>
        /// <param name="label">Le label pertinent</param>
        /// <param name="iCpt">La position dans la grille</param>
        /// <returns>img</returns>
        private Brush Images(Label label,int iCpt)
        {
            // Variables locales.
            Brush img = null;

            img = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Steve Jobs/SteveJobs60_48_" + iCpt + ".jpg")));

            return img;
        }
        #endregion

        #region Événements
        /// <summary>
        /// Auteurs: Mahdi Ellili et Michael Meilleur
        /// Description: actions à faire durant le chargement de la fenêtre
        /// Date: 2022-04-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            RemplirTableauJeu();
            txtRecord.Text = "Aucun";
        }

        /// <summary>
        /// Auteurs: Mahdi Ellili et Michael Meilleur
        /// Description: Bouton pour la résolution de la grille
        /// Date:2022-04-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRésoudre_Click(object sender, RoutedEventArgs e)
        {
            //Variables locales 
            
            Résoudre();
            if(_option.Son == true)
            SonRéussi();

            lblPartieGagnée.Content = "Trouvé!";
        }

        /// <summary>
        /// Auteurs: Mahdi Ellili et Michael Meilleur
        /// Description: boutton pour recommencer le jeu
        /// Date: 2022-04-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecommencer_Click(object sender, RoutedEventArgs e)
        {
            RemplirTableauJeu();
            imgFlecheBas.IsEnabled = true;
            imgFlecheHaut.IsEnabled = true;
            imgFlecheGauche.IsEnabled = true;
            imgFlecheDroite.IsEnabled = true;
            txtEnCours.Text = "0";
            lblPartieGagnée.Content = "";
        }

        /// <summary>
        /// Auteurs: Mahdi Ellili et Michael Meilleur
        /// Description: permutation vers le haut lors du clic sur la flèche en haut
        /// Date: 2022-04-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgFlecheHaut_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_JeuTaquin._Ligne == 4)
            {
                MessageBox.Show("Mauvais déplacement!", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _JeuTaquin.DéplacerVers(JeuDeTaquinLogique.Directions.BAS);
                AfficherTableau();
                txtEnCours.Text = _JeuTaquin.Déplacements.ToString("");
                VérifierVictoire();
            }
        }

        /// <summary>
        /// Auteurs: Mahdi Ellili et Michael Meilleur
        /// Description: permutation vers le haut lors du clic sur la flèche droite
        /// Date: 2022-04-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgFlecheDroite_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_JeuTaquin._Colonne == 0)
            {
                MessageBox.Show("Mauvais déplacement!", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _JeuTaquin.DéplacerVers(JeuDeTaquinLogique.Directions.GAUCHE);
                AfficherTableau();
                txtEnCours.Text = _JeuTaquin.Déplacements.ToString("");
                VérifierVictoire();
            }
        }

        /// <summary>
        /// Auteurs: Mahdi Ellili et Michael Meilleur
        /// Description: permutation vers le haut lors du clic sur la flèche gauche
        /// Date: 2022-04-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgFlecheGauche_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_JeuTaquin._Colonne == 3)
            {
                MessageBox.Show("Mauvais déplacement!", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _JeuTaquin.DéplacerVers(JeuDeTaquinLogique.Directions.DROITE);
                AfficherTableau();
                txtEnCours.Text = _JeuTaquin.Déplacements.ToString("");
                VérifierVictoire();
            }
        }
        /// <summary>
        /// Auteurs: Mahdi Ellili et Michael Meilleur
        /// Description: permutation vers le haut lors du clic sur la flèche en bas
        /// Date: 2022-04-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgFlecheBas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_JeuTaquin._Ligne == 0)
            {
                MessageBox.Show("Mauvais déplacement!", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _JeuTaquin.DéplacerVers(JeuDeTaquinLogique.Directions.HAUT);
                AfficherTableau();
                txtEnCours.Text = _JeuTaquin.Déplacements.ToString("");
                VérifierVictoire();
            }
        }

        /// <summary>
        /// Auteurs: Mahdi Ellili et Michael Meilleur
        /// Description: Ouverture de la fenêtre des paramètres lors du clic sur le bouton paramètres
        /// Date: 2022-04-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParamètres_Click(object sender, RoutedEventArgs e)
        {
            // Variables locales
            frmParamètres frmParams = new frmParamètres();
            frmParams.Owner = this;
            frmParams.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            frmParams.grdParamètres.DataContext = _option;
            bool? bRetour = frmParams.ShowDialog();

            if (bRetour == true)
            {
               AfficherTableau();
            }
        }
        #endregion
    }
}
