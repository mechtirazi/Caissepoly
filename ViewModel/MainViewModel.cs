using CaissePoly.Model;
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
        private int _index;
        public int Index
        {
            get => _index;
            set { _index = value; OnPropertyChanged(); }
        }
        public int TotalQuantite => FilteredArticles?.Sum(a => a.quantiteVente) ?? 0;

        public decimal TotalPrix => FilteredArticles?.Sum(a => a.Total) ?? 0;

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
                if (SelectedTicket == ListeTickets.Last()+1 && estonloading)
                {
                    IsReadOnly = true;
                    estonloading = !estonloading;

                }
                else if (SelectedTicket == ListeTickets.Last() && !estonloading)
                {
                    IsReadOnly = true;
                }
                else
                {
                    IsReadOnly = false;
                }
                FilteredArticles.Clear();

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
                RecalculerTotalTicket();
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
                    if (!IsReadOnly) return;
                    SelectedArticle.quantiteVente = quantite;
                    _context.Article.Update(SelectedArticle);
                    _context.SaveChanges();
                    RecalculerTotalTicket();
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

        private bool _afficherFamilles;

        public bool AfficherFamilles
        {
            get => _afficherFamilles;
            set
            {
                _afficherFamilles = value;
                OnPropertyChanged();
                ChargerArticlesParFamille();
                FilteredArticles.CollectionChanged += (s, e) =>
                {
                    if (e.NewItems != null)
                    {
                        foreach (Article article in e.NewItems)
                        {
                            article.PropertyChanged += Article_PropertyChanged;
                        }
                    }

                    if (e.OldItems != null)
                    {
                        foreach (Article article in e.OldItems)
                        {
                            article.PropertyChanged -= Article_PropertyChanged;
                        }
                    }

                    OnPropertyChanged(nameof(TotalPrix));
                    OnPropertyChanged(nameof(TotalQuantite));
                };

            }
        }
        public bool estonloading;
        public MainViewModel()
        {
            estonloading = true ; 
            IsReadOnly = true;
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
                if (!IsReadOnly) return;
                if (article != null && !FilteredArticles.Contains(article) )
                {
                    FilteredArticles.Add(article);
                    article.PropertyChanged += Article_PropertyChanged;
                    MettreAJourIndices(); 
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
                if (!IsReadOnly) return;
                if (SelectedArticle != null)
                {
                    FilteredArticles.Remove(SelectedArticle);
                    SelectedArticle = null;
                    MettreAJourIndices(); 
                }
            });

            ValiderCommandeCommand = new RelayCommand(() =>
            {
                RecalculerTotalTicket();

                var nouveauTicket = new Ticket
                {
                    DateTicket = DateTime.Now,
                    Total = TotalTicket,
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
                var currentWindow = System.Windows.Application.Current.Windows
                                      .OfType<Window>()
                                      .FirstOrDefault(w => w.IsActive);

                if (currentWindow != null)
                {
                    // Créer une nouvelle instance de la même fenêtre
                    var newWindow = new MainWindow(); // Remplace par le nom réel de ta fenêtre

                    newWindow.Show();   // Ouvre la nouvelle fenêtre
                    currentWindow.Close(); // Ferme l'ancienne
                }
            });

            if (ListeTickets.Any())
            {
                IsReadOnly = true;

                SelectedTicket = ListeTickets.Last() + 1;
                ListeTickets.Add(SelectedTicket);



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
            if (IsReadOnly) return;
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
                MettreAJourIndices(); 
                IsReadOnly = false; 
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
            OnPropertyChanged(nameof(TotalPrix));
            OnPropertyChanged(nameof(TotalQuantite));
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
            if (e.PropertyName == nameof(Article.quantiteVente))
            {
                RecalculerTotalTicket();
                OnPropertyChanged(nameof(TotalPrix));
                OnPropertyChanged(nameof(TotalQuantite));
            }
        }
        private void MettreAJourIndices()
        {
            for (int i = 0; i < FilteredArticles.Count; i++)
            {
                Index = i;// Commence à 1
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
