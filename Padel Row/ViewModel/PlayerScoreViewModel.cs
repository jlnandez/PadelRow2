using Padel_Row.Model;
using Padel_Row.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Padel_Row.ViewModel
{
    public class PlayerScoreViewModel : BaseViewModel
    {
        private readonly ITournamentService _tournamentService;

        public ObservableCollection<PlayerModel> Players { get; set; } = new ObservableCollection<PlayerModel>();

        public TournamentModel Tournament { get; private set; }


        public PlayerScoreViewModel(TournamentModel tournament)
        {
            _tournamentService = DependencyService.Resolve<ITournamentService>();
            Tournament = tournament;
            Players = new ObservableCollection<PlayerModel>(tournament.Players);
        }

        public ICommand IncreaseScoreCommand => new Command<PlayerModel>(async (player) =>
        {
            if (player != null)
            {
                player.Score++;
                await UpdatePlayerScore(player);
            }
        });

        public ICommand DecreaseScoreCommand => new Command<PlayerModel>(async (player) =>
        {
            if (player != null && player.Score > 0)
            {
                player.Score--;
                await UpdatePlayerScore(player);
            }
        });

        private async Task UpdatePlayerScore(PlayerModel player)
        {
            // Actualiza la lista de jugadores en el torneo
            Tournament.Players = Players.ToList();

            // Llama al servicio para actualizar el torneo en Firebase
            var success = await _tournamentService.AddOrUpdateTournament(Tournament);
            if (!success)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar el puntaje en Firebase.", "OK");
            }
        }
    }
        



    
}
