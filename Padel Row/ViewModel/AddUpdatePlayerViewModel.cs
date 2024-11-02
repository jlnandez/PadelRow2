using Padel_Row.Model;
using Padel_Row.Services.Implementations;
using Padel_Row.Services.Interfaces;
using System.Windows.Input;


namespace Padel_Row.ViewModel
{
    public class AddUpdatePlayerViewModel : BaseViewModel
    {
        #region Properties
        private readonly IPlayerService _playerService;

        private PlayerModel _playerDetail = new PlayerModel();

        public PlayerModel PlayerDetail
        {
            get => _playerDetail;
            set => SetProperty(ref _playerDetail, value);
        }
        #endregion

        #region Constructor
        public AddUpdatePlayerViewModel()
        {
            _playerService = DependencyService.Resolve<IPlayerService>();
        }

        public AddUpdatePlayerViewModel(PlayerModel playerResponse)
        {
            _playerService = DependencyService.Resolve<IPlayerService>();
            PlayerDetail = new PlayerModel
            {
                Key = playerResponse.Key,
                Player = playerResponse.Player,
                Team = playerResponse.Team
            };
        }
        #endregion

        #region Commands

        public ICommand SavePlayerCommand => new Command(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            bool res = await _playerService.AddOrUpdatePlayer(PlayerDetail);
            if (res)
            {

                if (!string.IsNullOrWhiteSpace(PlayerDetail.Key))
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "Record Updated successfully.", "Ok");
                }
                else
                {
                    PlayerDetail = new PlayerModel() { };
                    await App.Current.MainPage.DisplayAlert("Success!", "Record added successfully.", "Ok");
                }
            }
            IsBusy = false;
        });
        #endregion
    }
}
