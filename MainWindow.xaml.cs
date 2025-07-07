using CaissePoly.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaissePoly
{
    
    public partial class MainWindow : Window
    {


        private familleViewModel familleVM = new familleViewModel();
        private NumeroTicketViewModel ticketVM = new NumeroTicketViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

       




        // Exemple pour changer de mode :

    }


}