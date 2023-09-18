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
using System.Windows.Shapes;
using JeuxLib;

namespace JeuTaquinWPF.Views
{
    /// <summary>
    /// Interaction logic for frmParamètres.xaml
    /// </summary>
    public partial class frmParamètres : Window
    {
        #region Champs
        options _option = new options();
        #endregion

        public frmParamètres()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Auteur: Mahdi Ellili et Michael Meilleur
        /// Description: appliquer les modification des paramètres
        /// Date: 2022-04-13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK__Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
