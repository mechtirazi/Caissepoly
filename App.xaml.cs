﻿using CaissePoly.Model;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CaissePoly
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Utilisateur UtilisateurConnecte { get; set; }

    }

}
