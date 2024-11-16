using Padel_Row.Model;
using Padel_Row.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Padel_Row.ViewModel
{
    public class TournamentPlayersViewModel : BaseViewModel
    {
        private string _newPlayerName;

        private readonly ITournamentService _tournamentService;

        public TournamentModel Tournament { get; }

        public ObservableCollection<PlayerModel> Players { get; set; } = new ObservableCollection<PlayerModel>();

        public string NewPlayerName
        {
            get => _newPlayerName;
            set => SetProperty(ref _newPlayerName, value);
        }

        public TournamentPlayersViewModel(TournamentModel tournament)
        {
            _tournamentService = DependencyService.Resolve<ITournamentService>();
            Tournament = tournament;

            LoadPlayers();
        }

        private void LoadPlayers()
        {
            Players.Clear();
            foreach (var player in Tournament.Players)
            {
                Players.Add(player);
            }
        }


        // Comando para agregar un jugador
        //public ICommand AddPlayerCommand => new Command(() =>
        //{
        //    var newPlayer = new PlayerModel { Name = "Nuevo Jugador" }; // Nombre predeterminado
        //    Players.Add(newPlayer);
        //    Tournament.Players.Add(newPlayer);
        //});

        public ICommand AddPlayerCommand => new Command(async () =>
        {
            if (string.IsNullOrWhiteSpace(NewPlayerName)) return;

            var newPlayer = new PlayerModel { Name = NewPlayerName };
            Players.Add(newPlayer);
            Tournament.Players.Add(newPlayer);

            await _tournamentService.AddOrUpdateTournament(Tournament);

            NewPlayerName = string.Empty;
        });

        // Comando para eliminar un jugador
        public ICommand RemovePlayerCommand => new Command<PlayerModel>(async (player) =>
        {
            if (player != null)
            {
                Players.Remove(player);
                Tournament.Players.Remove(player);

                await _tournamentService.AddOrUpdateTournament(Tournament);
            }
        });

        // Comando para guardar cambios
        public ICommand SavePlayersCommand => new Command(async () =>
        {
            try
            {
                await _tournamentService.AddOrUpdateTournament(Tournament);
                await App.Current.MainPage.DisplayAlert("Éxito", "Jugadores actualizados.", "OK");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"No se pudieron guardar los cambios: {ex.Message}", "OK");
            }
        });


    }
}
