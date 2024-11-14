using Padel_Row.Model;
using Padel_Row.Services.Interfaces;
using Padel_Row.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Padel_Row.ViewModel
{
    public class TournamentViewModel : BaseViewModel
    {
        //------------------------------Properties
        
        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly ITournamentService _tournamentService;

        public ObservableCollection<TournamentModel> Tournaments { get; set; } = new ObservableCollection<TournamentModel>();


        private TournamentModel _tournamentDetail = new TournamentModel();

        public TournamentModel TournamentDetail
        {
            get => _tournamentDetail;
            set => SetProperty(ref _tournamentDetail, value);
        }

        //-------------------------------Constructores
        public TournamentViewModel()
        {
            _tournamentService = DependencyService.Resolve<ITournamentService>();
            GetAllTorneos();
        }

        public TournamentViewModel(TournamentModel tournamentResponse)
        {
            _tournamentService = DependencyService.Resolve<ITournamentService>();
            TournamentDetail = new TournamentModel
            {
                Name = tournamentResponse.Name,
                Date = tournamentResponse.Date,
                Id = tournamentResponse.Id
            };
        }


        //--------------------------Metodos

        private void GetAllTorneos()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var torneosList = await _tournamentService.GetAllTournaments();

                Device.BeginInvokeOnMainThread(() =>
                {
                    Tournaments.Clear();
                    if (torneosList?.Count > 0)
                    {
                        foreach (var torneo in torneosList)
                        {
                            Tournaments.Add(torneo);
                        }
                    }
                    IsBusy = IsRefreshing = false;
                });
            });
        }

        //------------------------comandos
        public ICommand AddTournamentCommand => new Command(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            bool res = await _tournamentService.AddOrUpdateTournament(TournamentDetail);
            if (res)
            {

                if (!string.IsNullOrWhiteSpace(TournamentDetail.Id))
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "Torneo Actualizado.", "Ok");

                }
                else
                {
                    TournamentDetail = new TournamentModel() { };
                    await App.Current.MainPage.DisplayAlert("Success!", "Se creo Torneo.", "Ok");
                }
            }
            IsBusy = false;
        });

        public ICommand RefreshCommand => new Command(() =>
        {
            IsRefreshing = true;
            GetAllTorneos();
        });

        public ICommand SelectedTorneoCommand => new Command<TournamentModel>(async (tournament) =>
        {
            if (tournament != null)
            {
                var response = await App.Current.MainPage.DisplayActionSheet("Opciones", "Cerrar", null, "Editar Torneo", "Borrar Torneo");

                if (response == "Editar Torneo")
                {
                    await App.Current.MainPage.Navigation.PushAsync(new TorneoAddPage(tournament));
                    //TournamentDetail = tournament; // Setea el torneo a editar
                    
                    await App.Current.MainPage.Navigation.PushAsync(new TorneoAddPage());
                }
                else if (response == "Borrar Torneo")
                {
                    IsBusy = true;
                    bool deleteResponse = await _tournamentService.DeleteTournament(tournament.Id);
                    if (deleteResponse)
                    {
                        GetAllTorneos();
                    }
                }
            }
        });
    }
}
