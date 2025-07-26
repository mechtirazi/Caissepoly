using CaissePoly.Model;
using CaissePoly.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.EntityFrameworkCore;

namespace CaissePoly.admin
{
    /// <summary>
    /// Logique d'interaction pour BonSortieWindow.xaml
    /// </summary>
    public partial class BonSortieWindow : Window
    {
        public ObservableCollection<VenteViewModel> SortieArticles { get; set; }

        public BonSortieWindow()
        {
            InitializeComponent();
            LoadVentes(); // <- ça initialise SortieArticles
            DataContext = this; // <- maintenant le DataContext contient SortieArticles
        }

        private void LoadVentes()
        {
            using var context = new CDBContext();
            SortieArticles = new ObservableCollection<VenteViewModel>(
                context.Vente
                       .Include(v => v.Article)
                       .Include(v => v.Ticket)
                       .Select(v => new VenteViewModel
                       {
                           Article = v.Article,
                           Quantite = v.Quantite,
                           PrixUnitaire = v.PrixUnitaire,
                           TicketDate = v.Ticket.DateTicket,
                           TicketId = v.Ticket.IdT    // <-- ici
                       })
                       .ToList()
            );
        }

        private void RetourAccueilButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Content is CaissePoly.admin.MenuPrincipale)
                {
                    window.Activate();             // Affiche cette fenêtre existante
                    this.Close();                  // Ferme la fenêtre actuelle (par exemple Inventaire)
                    return;
                }
            }

            // Si elle n’est pas déjà ouverte, tu peux en créer une
            var newMenu = new Window
            {
                Title = "Menu Principale",
                Content = new CaissePoly.admin.MenuPrincipale(),
                Width = 800,
                Height = 600
            };

            newMenu.Show();
            this.Close();
        }

    }
    
}