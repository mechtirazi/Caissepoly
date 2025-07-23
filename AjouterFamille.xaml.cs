﻿using CaissePoly.ViewModel;
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

namespace CaissePoly
{
    /// <summary>
    /// Logique d'interaction pour AjouterFamille.xaml
    /// </summary>
    public partial class AjouterFamille : Window
    {
        public AjouterFamille()
        {
            InitializeComponent();
            DataContext = new AjouterFamilleViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
