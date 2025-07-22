using CaissePoly.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using static CaissePoly.espece;

namespace CaissePoly.ViewModel
{
    public class MainViewModel :INotifyPropertyChanged
    {
        private readonly CDBContext _context = new CDBContext();

        public ObservableCollection<Famille> Familles { get; set; }
        public ObservableCollection<int> ListeTickets { get; set; } = new();
        public ObservableCollection<Article> Articles { get; set; } = new ObservableCollection<Article>();

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

        private string _searchText = "";

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SearchCommand { get; }

        public event Action<string> OnSearchRequested; // Si tu veux réagir dans un parent


        private List<Article> TousLesArticles = new(); // ✅ Liste complète à filtrer


        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged();
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
                ChargerTicketActuel(_selectedTicket);
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

        private decimal totalTicket;
        public decimal TotalTicket
        {
            get => totalTicket;
            set
            {
                if (totalTicket != value)
                {
                    totalTicket = value;
                    OnPropertyChanged(nameof(TotalTicket));
                }
            }
        }
        private string _paiement;
        public string Paiement
        {
            get => _paiement;
            set { _paiement = value; OnPropertyChanged(); }
        }
        private bool _afficherFamilles;

        public bool AfficherFamilles
        {
            get => _afficherFamilles;
            set
            {
                _afficherFamilles = value;
                OnPropertyChanged();
                ChargerArticlesParFamille();
            }
        }
        private Ticket _ticketActuel;
        public Ticket TicketActuel
        {
            get => _ticketActuel;
            set
            {
                _ticketActuel = value;
                OnPropertyChanged(nameof(TicketActuel));
            }
        }


        public Action CloseWindowAction { get; set; }



        public ICommand SelectFamilleCommand { get; }
        public ICommand SelectArticleCommand { get; }
        public ICommand ValiderCommandeCommand { get; }
        public ICommand NumberCommand { get; }
        public ICommand EnterCommand { get; }
        public ICommand DeleteCharCommand { get; }
        public ICommand DeleteArticleCommand { get; }
        public ICommand ValiderPaiementCommand { get; }
        public ICommand EspeceCommand{ get; }
        public ICommand CancelCommand { get; }

        public ICommand AfficherFamillesCommand { get; }

        public MainViewModel()
        {
            _context.Database.EnsureCreated();
            Familles = new ObservableCollection<Famille>(_context.Famille.ToList());
            ListeTickets = new ObservableCollection<int>(_context.Tickets.Select(t => t.IdT));
            ArticlesParFamille = new ObservableCollection<Article>(_context.Article.ToList());

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
                if (IsReadOnly) return; // Prevent modification if in read-only mode
                if (int.TryParse(ValeurSaisie, out int quantite) && SelectedArticle != null)
                {
                    SelectedArticle.quantiteVente = quantite;
                    _context.Article.Update(SelectedArticle);
                    _context.SaveChanges();
                    ValeurSaisie = "";
                    RecalculerTotalTicket();
                }

            });
            SearchCommand = new RelayCommand(() =>
            {
                Console.WriteLine($"🔍 Recherche lancée : {SearchText}");
                OnSearchRequested?.Invoke(SearchText);

                if (SearchText != null)
                {
                    var filteredArticles = _context.Article
                        .Where(a => a.designation.ToLower().Contains(SearchText.ToLower()))
                        .ToList();

                    ArticlesParFamille = new ObservableCollection<Article>(filteredArticles);
                }
                else
                {
                    ChargerArticlesParFamille();
                }
            });



            AfficherFamillesCommand = new RelayCommand(() =>
            {
                AfficherFamilles = !AfficherFamilles;
                ArticlesParFamille.Clear();
                ArticlesParFamille = new ObservableCollection<Article>(_context.Article.ToList());
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

                // Si TicketActuel n'existe pas, on le crée
                if (TicketActuel == null)
                {
                    TicketActuel = new Ticket
                    {
                        DateTicket = DateTime.Now,
                        Ventes = new List<Vente>() // s'assurer qu'il est prêt à recevoir des ventes
                    };
                }

                // Ajouter les ventes au ticket actuel
                foreach (var article in FilteredArticles)
                {
                    if (article.quantiteVente > 0)
                    {
                        var vente = new Vente
                        {
                            IdA = article.idA,
                            Quantite = article.quantiteVente,
                            PrixUnitaire = article.prixunitaire ?? 0,
                            Article = article,
                            Ticket = TicketActuel
                        };

                        TicketActuel.Ventes.Add(vente);
                    }
                }

                TicketActuel.Total = TotalTicket;

                // Sauvegarder uniquement si le ticket n'est pas déjà dans la base
                if (_context.Entry(TicketActuel).State == EntityState.Detached)
                {
                    _context.Tickets.Add(TicketActuel);
                }

                _context.SaveChanges();

                MessageBox.Show($"✅ Commande validée. Total : {TotalTicket:F3} Dinars");

                // Nettoyer les articles
                FilteredArticles.Clear();
                OnPropertyChanged(nameof(FilteredArticles));
                OnPropertyChanged(nameof(TotalTicket));

                // Préparer un nouveau ticket vide (sans enregistrer pour l'instant)
                TicketActuel = new Ticket
                {
                    DateTicket = DateTime.Now,
                    Ventes = new List<Vente>()
                };
                CreerNouveauTicket();
            });




            if (ListeTickets.Any())
            {
                SelectedTicket = ListeTickets.Last();
            }
            CancelCommand = new RelayCommand(() =>
            {
                CloseWindowAction?.Invoke();
            });

            ValiderPaiementCommand = new RelayCommand(() =>
            {
                var ticket = _context.Tickets.FirstOrDefault(t => t.IdT == SelectedTicket);
                if (ticket != null)
                {
                    ticket.ModePaiement = Paiement; // Paiement est la propriété qui sera "Espèce" ou autre
                    _context.SaveChanges();

                    MessageBox.Show($"Le mode de paiement du ticket {SelectedTicket} est mis à jour à {Paiement}");
                }
                else
                {
                    MessageBox.Show("Aucun ticket sélectionné");
                }

                CloseWindowAction?.Invoke();
            });

            EspeceCommand = new RelayCommand(() =>
            {

                var ticket = _context.Tickets.FirstOrDefault(t => t.IdT == SelectedTicket);
                if (ticket != null)
                {
                    var vm = new EspeceViewModel(ticket);
                    var window = new espece(vm);
                    window.ShowDialog();
                    UpdateTicketInfo(); // Recharge les infos après fermeture
                }
                else
                {
                    MessageBox.Show("Aucun ticket sélectionné");
                }
            });
            }

        private void ChargerArticlesParFamille()
        {
            if (SelectedFamille != null)
            {
                TousLesArticles = _context.Article
                    .Where(a => a.idF == SelectedFamille.idF)
                    .ToList();

                ArticlesParFamille = new ObservableCollection<Article>(TousLesArticles);
                
            }
            else
            {
                TousLesArticles = new List<Article>();
                ArticlesParFamille.Clear();
                FilteredArticles.Clear();
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
                IsReadOnly = true; // Or add logic to determine if it's an old ticket
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
        public void ValiderPaiementEnEspece()
        {
            Paiement = "Espèce";

            var ticket = _context.Tickets.FirstOrDefault(t => t.IdT == SelectedTicket);
            if (ticket != null)
            {
                ticket.ModePaiement = Paiement;
                _context.SaveChanges();
            }

            CloseWindowAction?.Invoke();
        }


        private void Article_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
                if (e.PropertyName == nameof(Article.quantiteVente) || e.PropertyName == nameof(Article.Total))
                {
                    RecalculerTotalTicket(); // Recalculer le total à chaque changement important
                    _context.Article.Update(sender as Article);
                    _context.SaveChanges();
                }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CreerNouveauTicket()
        {
            TicketActuel = new Ticket
            {
                DateTicket = DateTime.Now,
                ModePaiement = null,
                Total = 0
            };

            _context.Tickets.Add(TicketActuel);
            _context.SaveChanges();

            ListeTickets.Add(TicketActuel.IdT);
            SelectedTicket = TicketActuel.IdT;

            Articles.Clear();
            FilteredArticles.Clear();

            OnPropertyChanged(nameof(TicketActuel));
            OnPropertyChanged(nameof(TotalTicket));
            OnPropertyChanged(nameof(FilteredArticles));
        }
        private void ChargerTicketActuel(int idTicket)
        {
            TicketActuel = _context.Tickets
                .Include(t => t.Ventes) 
                .ThenInclude(v => v.Article)
                .FirstOrDefault(t => t.IdT == idTicket);

            if (TicketActuel == null)
            {
                // Si pas trouvé, crée un ticket vide (optionnel)
                TicketActuel = new Ticket
                {
                    DateTicket = DateTime.Now,
                    Ventes = new List<Vente>()
                };
            }

            // Met à jour les articles filtrés en fonction des ventes actuelles
            FilteredArticles.Clear();
            foreach (var vente in TicketActuel.Ventes)
            {
                var article = vente.Article;
                article.quantiteVente = vente.Quantite;
                FilteredArticles.Add(article);
            }

            RecalculerTotalTicket();
        }
    }
}
