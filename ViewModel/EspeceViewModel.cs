using CaissePoly.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
namespace CaissePoly.ViewModel
{
    public class EspeceViewModel : ViewModelBase
    {
        private readonly CDBContext _context = new CDBContext();

        private string _modePaiement;
        public string ModePaiement
        {
            get => _modePaiement;
            set { _modePaiement = value; OnPropertyChanged(); }
        }

        public Ticket TicketActuel { get; set; }
        private decimal _montantRecu;
        public decimal MontantRecu
        {
            get => _montantRecu;
            set { _montantRecu = value; OnPropertyChanged(); }
        }

        public decimal TotalTicket => TicketActuel?.Total ?? 0;

        public ICommand ValidatePaymentCommand { get; }
        public ICommand CancelCommand { get; }

        public Action CloseWindowAction { get; set; }

        public EspeceViewModel(Ticket ticket)
        {
            TicketActuel = ticket;

            ValidatePaymentCommand = new RelayCommand(() =>
            {
                if (MontantRecu >= TotalTicket)
                {
                    // Met à jour le mode de paiement
                    ModePaiement = "Espèce";
                    TicketActuel.ModePaiement = ModePaiement;

                    var t = _context.Tickets.FirstOrDefault(x => x.IdT == TicketActuel.IdT);
                    if (t != null)
                    {
                        t.ModePaiement = ModePaiement;
                        _context.SaveChanges();
                    }

                    CloseWindowAction?.Invoke(); // ✅ Ferme la fenêtre
                }
                else
                {
                    MessageBox.Show($"Montant insuffisant. Montant à payer : {TotalTicket:F3} DT");
                    // ❌ Ne ferme pas la fenêtre
                }
            }); 

            CancelCommand = new RelayCommand(() =>
            {
                CloseWindowAction?.Invoke();
            });
        }
    }
}

