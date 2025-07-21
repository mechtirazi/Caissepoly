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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaissePoly.admin
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipale.xaml
    /// </summary>
    public partial class MenuPrincipale : UserControl
    {
        public MenuPrincipale()
        {
            InitializeComponent();
        }
        private void CaisseMainButton_Loaded(object sender, RoutedEventArgs e)
        {
            // Démarrer l'animation de pulsation pour la caisse principale
            if (sender is Button btn)
            {
                // Définir un DropShadowEffect si le bouton n'en a pas
                if (btn.Effect == null)
                {
                    btn.Effect = new DropShadowEffect
                    {
                        Color = Colors.Black,
                        BlurRadius = 15,
                        ShadowDepth = 4,
                        Opacity = 0.3
                    };
                }

                // Récupérer le storyboard et l’appliquer au bouton
                var pulseAnimation = (Storyboard)FindResource("PulseAnimation");

                // 🔥 Important : définir la cible de l’animation
                Storyboard.SetTarget(pulseAnimation, btn);
                pulseAnimation.Begin();
            }
        }

        // Gestionnaires d'événements pour les boutons
        private void OnArticlesClick(object sender, RoutedEventArgs e)
        {
            // Navigation vers la page des articles
            NavigateToPage("Articles");
        }

        private void OnBonEntreeClick(object sender, RoutedEventArgs e)
        {
            // Navigation vers la page bon d'entrée et sortie
            NavigateToPage("BonEntreeSortie");
        }

        private void OnInventaireClick(object sender, RoutedEventArgs e)
        {
            // Navigation vers la page inventaire
            var inventaireWindow = new inventaire.inventaire(); // ← tu dois avoir une classe InventaireWindow
            inventaireWindow.Show();

            // Optionnel : Fermer la fenêtre actuelle si c'est une Window (non un UserControl)
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }

        private void OnInformationClick(object sender, RoutedEventArgs e)
        {
            // Navigation vers la page information
            NavigateToPage("Information");
        }

        private void OnFermerCaisseClick(object sender, RoutedEventArgs e)
        {
            // Afficher une boîte de dialogue pour confirmer la fermeture
            var result = MessageBox.Show(
                "Voulez-vous vraiment fermer la caisse ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Logique de fermeture de la caisse
                FermerCaisse();
            }
        }

        private void OnFacturationClick(object sender, RoutedEventArgs e)
        {
            // Navigation vers la page facturation
            NavigateToPage("Facturation");
        }

        private void OnUtilisateursClick(object sender, RoutedEventArgs e)
        {
            // Navigation vers la page utilisateurs
            NavigateToPage("Utilisateurs");
        }

        private void OnContactClick(object sender, RoutedEventArgs e)
        {
            // Navigation vers la page contact
            NavigateToPage("Contact");
        }

        private void OnParametreClick(object sender, RoutedEventArgs e)
        {
            // Navigation vers la page paramètres
            NavigateToPage("Parametres");
        }

        private void OnCaisseMainClick(object sender, RoutedEventArgs e)
        {
            var existingWindow = Application.Current.Windows
                               .OfType<MainWindow>()
                               .FirstOrDefault();

            if (existingWindow != null)
            {
                // Si trouvée, on la met au premier plan
                if (existingWindow.WindowState == WindowState.Minimized)
                    existingWindow.WindowState = WindowState.Normal;
                existingWindow.Activate();
            }
            else
            {
                // Sinon, on crée et on affiche une nouvelle fenêtre
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }

        // Méthodes utilitaires
        private void NavigateToPage(string pageName)
        {
            // Implémentation de la navigation selon votre architecture
            // Par exemple, si vous utilisez un Frame :
            // this.Frame.Navigate(new Uri($"Pages/{pageName}.xaml", UriKind.Relative));

            // Ou si vous utilisez un système de navigation personnalisé :
            // NavigationService.Navigate(pageName);

            // Pour le moment, on affiche juste le nom de la page
            MessageBox.Show($"Navigation vers : {pageName}");
        }

        private void FermerCaisse()
        {
            // Logique pour fermer la caisse
            MessageBox.Show("Caisse fermée", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            // Vous pouvez ajouter ici :
            // - Sauvegarde des données
            // - Fermeture des connexions
            // - Nettoyage des ressources
            // - Fermeture de l'application
        }
    }
}
