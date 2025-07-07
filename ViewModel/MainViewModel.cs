using CaissePoly.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CaissePoly.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CDBContext _context = new CDBContext();

        public ObservableCollection<Famille> Familles { get; set; }
        public ObservableCollection<int> ListeTickets { get; set; } = new();

        private Famille _selectedFamille;
        public Famille SelectedFamille
        {
            get => _selectedFamille;
            set
            {
                _selectedFamille = value;
                OnPropertyChanged();
                ChargerArticlesParFamille();
            }
        }

        private int _selectedTicket;
        public int SelectedTicket
        {
            get => _selectedTicket;
            set
            {
                _selectedTicket = value;
                OnPropertyChanged();
                UpdateTicketInfo();
            }
        }

        private ObservableCollection<Article> _filteredArticles = new();
        public ObservableCollection<Article> FilteredArticles
        {
            get => _filteredArticles;
            set
            {
                _filteredArticles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Article> _articlesParFamille = new();
        public ObservableCollection<Article> ArticlesParFamille
        {
            get => _articlesParFamille;
            set { _articlesParFamille = value; OnPropertyChanged(); }
        }

        private Article _selectedArticle;
        public Article SelectedArticle
        {
            get => _selectedArticle;
            set
            {
                _selectedArticle = value;
                OnPropertyChanged();
            }
        }

        private string _valeurSaisie = "";
        public string ValeurSaisie
        {
            get => _valeurSaisie;
            set
            {
                _valeurSaisie = value;
                OnPropertyChanged();

                if (SelectedArticle != null && int.TryParse(_valeurSaisie, out int quantite))
                {
                    SelectedArticle.quantiteVente = quantite;
                    _context.Article.Update(SelectedArticle);
                    _context.SaveChanges();
                    OnPropertyChanged(nameof(SelectedArticle));
                }
            }
        }

        private decimal _totalTicket;
        public decimal TotalTicket
        {
            get => _totalTicket;
            set { _totalTicket = value; OnPropertyChanged(); }
        }

        private string _paiement;
        public string Paiement
        {
            get => _paiement;
            set { _paiement = value; OnPropertyChanged(); }
        }

        public ICommand SelectFamilleCommand { get; }
        public ICommand SelectArticleCommand { get; }
        public ICommand ValiderCommandeCommand { get; }
        public ICommand NumberCommand { get; }
        public ICommand EnterCommand { get; }
        public ICommand DeleteCharCommand { get; }
        public ICommand DeleteArticleCommand { get; }

        public MainViewModel()
        {
            _context.Database.EnsureCreated();
            Familles = new ObservableCollection<Famille>(_context.Famille.ToList());
            ListeTickets = new ObservableCollection<int>(_context.Tickets.Select(t => t.IdT));

            SelectFamilleCommand = new RelayCommand<Famille>(fam =>
            {
                if (fam != null)
                {
                    SelectedFamille = fam;
                }
            });

            SelectArticleCommand = new RelayCommand<Article>(article =>
            {
                if (article != null && !FilteredArticles.Contains(article))
                {
                    FilteredArticles.Add(article);
                    article.PropertyChanged += Article_PropertyChanged;
                    RecalculerTotalTicket();
                }
            });

            NumberCommand = new RelayCommand<string>(chiffre => { ValeurSaisie += chiffre; });

            EnterCommand = new RelayCommand(() =>
            {
                if (int.TryParse(ValeurSaisie, out int quantite) && SelectedArticle != null)
                {
                    SelectedArticle.quantiteVente = quantite;
                    _context.Article.Update(SelectedArticle);
                    _context.SaveChanges();
                    ValeurSaisie = "";
                }
            });

            DeleteCharCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(ValeurSaisie))
                    ValeurSaisie = ValeurSaisie[..^1];
            });

            DeleteArticleCommand = new RelayCommand(() =>
            {
                if (SelectedArticle != null)
                {
                    FilteredArticles.Remove(SelectedArticle);
                    SelectedArticle = null;
                }
            });

            ValiderCommandeCommand = new RelayCommand(() =>
            {
                RecalculerTotalTicket();

                var nouveauTicket = new Ticket
                {
                    DateTicket = DateTime.Now,
                    Total = TotalTicket,
                    ModePaiement = "Espèce"
                };

                foreach (var article in FilteredArticles)
                {
                    if (article.quantiteVente > 0)
                    {
                        var vente = new Vente
                        {
                            IdA = article.idA,
                            Quantite = article.quantiteVente,
                            PrixUnitaire = article.prixunitaire ?? 0,
                            Article = article
                        };
                        nouveauTicket.Ventes.Add(vente);
                    }
                }

                _context.Tickets.Add(nouveauTicket);
                _context.SaveChanges();

                MessageBox.Show($"✅ Commande validée. Total : {TotalTicket} Dinars");
                FilteredArticles.Clear();
            });

            if (ListeTickets.Any())
            {
                SelectedTicket = ListeTickets.First();
            }
        }

        private void ChargerArticlesParFamille()
        {
            if (SelectedFamille != null)
            {
                ArticlesParFamille = new ObservableCollection<Article>(
                    _context.Article.Where(a => a.idF == SelectedFamille.idF).ToList()
                );
            }
            else
            {
                ArticlesParFamille.Clear();
            }
        }

        private void UpdateTicketInfo()
        {
            var ticket = _context.Tickets
                .Where(t => t.IdT == SelectedTicket)
                .Select(t => new
                {
                    t.ModePaiement,
                    t.Total,
                    Ventes = t.Ventes.Select(v => new { v.Quantite, Article = v.Article }).ToList()
                })
                .FirstOrDefault();

            if (ticket != null)
            {
                Paiement = ticket.ModePaiement ?? "----";
                TotalTicket = ticket.Total ?? 0;
                FilteredArticles.Clear();

                foreach (var vente in ticket.Ventes)
                {
                    if (vente.Article != null)
                    {
                        vente.Article.quantiteVente = vente.Quantite;
                        FilteredArticles.Add(vente.Article);
                    }
                }
            }
            else
            {
                Paiement = "----";
                TotalTicket = 0;
                FilteredArticles.Clear();
            }
        }

        private void RecalculerTotalTicket()
        {
            TotalTicket = FilteredArticles.Sum(a => a.Total);
        }

        private void Article_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Article.quantiteVente))
            {
                var article = sender as Article;
                if (article != null)
                {
                    _context.Article.Update(article);
                    _context.SaveChanges();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
