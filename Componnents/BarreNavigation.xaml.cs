using CaissePoly.admin;
using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace CaissePoly.Componnents
{
    public partial class BarreNavigation : UserControl, INotifyPropertyChanged
    {
        private System.Timers.Timer _timer;

        public BarreNavigation()
        {
            InitializeComponent();
            DataContext = this;

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += UpdateDateTime;
            _timer.Start();
        }


        private string _currentDate = DateTime.Now.ToString("dd/MM/yyyy");
        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        private string _currentTime = DateTime.Now.ToString("HH:mm:ss");
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        private void UpdateDateTime(object sender, ElapsedEventArgs e)
        {
            CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Récupérer la fenêtre parente
            Window parentWindow = Window.GetWindow(this);

            // Fermer la fenêtre
            parentWindow.Close();
        }

        private void AccueilButton_Click(object sender, RoutedEventArgs e)
        {
            
                // Ouvrir la fenêtre de connexion
                var loginWindow = new LoginAdmin();
                var result = loginWindow.ShowDialog();

                // Vérifier si la connexion est réussie et qu’un utilisateur est connecté
                if (result == true && loginWindow.UtilisateurConnecte != null)
                {
                    var utilisateur = loginWindow.UtilisateurConnecte;

                    // Vérifier que l'utilisateur a bien le rôle administrateur
                    if (utilisateur.Role == "Administrateur")
                    {
                        // Vérifier si la fenêtre MenuPrincipale est déjà ouverte
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.Content is CaissePoly.admin.MenuPrincipale)
                            {
                                window.Activate(); // Mettre au premier plan
                                Window.GetWindow(this)?.Close(); // Fermer la fenêtre actuelle (si besoin)
                                return;
                            }
                        }

                        // Sinon, créer et afficher une nouvelle fenêtre MenuPrincipale
                        var menuPrincipal = new Window
                        {
                            Title = "Menu Principal",
                            Content = new CaissePoly.admin.MenuPrincipale(),
                            Width = 800,
                            Height = 600
                        };
                        menuPrincipal.Show();

                        // Fermer la fenêtre actuelle
                        Window.GetWindow(this)?.Close();
                    }
                    else
                    {
                        MessageBox.Show("Accès refusé. Vous n'avez pas les droits administrateur.",
                                        "Accès refusé", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                
            }
        }

    }
}
