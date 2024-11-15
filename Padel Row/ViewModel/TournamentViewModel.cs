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
            TournamentDetail.Date = DateTime.Now; // Inicializa con la fecha actual
            GetAllTorneos();
        }

        public TournamentViewModel(TournamentModel tournamentResponse)
        {
            _tournamentService = DependencyService.Resolve<ITournamentService>();
            TournamentDetail = new TournamentModel
            {
                Name = tournamentResponse.Name,
                Date = tournamentResponse.Date == default ? DateTime.Now : tournamentResponse.Date, // Usa la fecha actual si no se proporciona una
                Id = tournamentResponse.Id
            };
        }


        //--------------------------Metodos

        public async void GetAllTorneos()
        {
            IsBusy = true;
            var userId = await SecureStorage.Default.GetAsync("user_id");
            var torneosList = await _tournamentService.GetAllTournaments(userId);

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
        }

        //------------------------comandos
        public ICommand AddTournamentCommand => new Command(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;

            // Obtenemos el UserId del usuario actual
            var userId = await SecureStorage.Default.GetAsync("user_id");
            TournamentDetail.UserId = userId;

            // Variable temporal para verificar si es un nuevo torneo o uno existente
            bool isNewTournament = string.IsNullOrWhiteSpace(TournamentDetail.Id);

            // Llamada para agregar o actualizar el torneo
            bool res = await _tournamentService.AddOrUpdateTournament(TournamentDetail);
            if (res)
            {
                // Muestra el mensaje correcto según si es nuevo o actualizado
                if (isNewTournament)
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "Se creó el torneo.", "Ok");
                    // Limpiar TournamentDetail después de crear un nuevo torneo
                    TournamentDetail = new TournamentModel() { Date = DateTime.Now };
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "Torneo actualizado.", "Ok");
                }

                // Regresa a la pantalla anterior
                await App.Current.MainPage.Navigation.PopAsync();
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
                var response = await App.Current.MainPage.DisplayActionSheet("Opciones", "Cerrar", null,
                    "Editar Torneo",
                    "Borrar Torneo",
                    "Gestionar Jugadores");

                if (response == "Editar Torneo")
                {
                    await App.Current.MainPage.Navigation.PushAsync(new TorneoAddPage(tournament));
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
                else if (response == "Gestionar Jugadores")
                {
                    await App.Current.MainPage.Navigation.PushAsync(new TorneoPlayersPage(tournament));
                }
            }
        });
    }
}
