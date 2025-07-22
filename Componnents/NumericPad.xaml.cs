using CaissePoly.ViewModel;
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

namespace CaissePoly.Componnents
{
    /// <summary>
    /// Logique d'interaction pour NumericPad.xaml
    /// </summary>
    public partial class NumericPad : UserControl
    {
        public NumericPad()
        {
            InitializeComponent();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 🔐 Ouvre la fenêtre de login
            var loginWindow = new CaissePoly.admin.LoginAdmin();
            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                App.UtilisateurConnecte = loginWindow.UtilisateurConnecte;

                // 🔒 Vérifie si c'est un administrateur
                if (App.UtilisateurConnecte.Role?.ToLower() == "administrateur")
                {
                    // ✅ Création du ticket
                    var viewModel = this.DataContext as MainViewModel; // Remplace par ton vrai ViewModel
                    viewModel?.CreerNouveauTicket();

                    MessageBox.Show("Ticket créé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Seul un administrateur peut créer un ticket.", "Accès refusé", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Connexion annulée.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click_2()
        {

        }
    }
}
