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
    }
}
