using Padel_Row.Model;
using Padel_Row.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Padel_Row.ViewModel
{
    public class TournamentPlayersViewModel : BaseViewModel
    {
        private readonly ITournamentService _tournamentService;

        public TournamentModel Tournament { get; }

        public ObservableCollection<PlayerModel> Players { get; set; } = new ObservableCollection<PlayerModel>();

        private string _newPlayerName;
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

        public ICommand AddPlayerCommand => new Command(async () =>
        {
            if (string.IsNullOrWhiteSpace(NewPlayerName)) return;

            var newPlayer = new PlayerModel { Name = NewPlayerName };
            Players.Add(newPlayer);
            Tournament.Players.Add(newPlayer);

            await _tournamentService.AddOrUpdateTournament(Tournament);

            NewPlayerName = string.Empty;
        });
    }
}
