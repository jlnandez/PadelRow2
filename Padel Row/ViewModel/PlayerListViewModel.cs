using Padel_Row.Model;
using Padel_Row.Services.Interfaces;
using Padel_Row.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Padel_Row.ViewModel
{
    public class PlayerListViewModel : BaseViewModel
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly IPlayerService _playerService;

        public ObservableCollection<PlayerModel> Players { get; set; } = new ObservableCollection<PlayerModel>();
        #endregion

        #region Methods
        private void GetAllPlayers()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                var playersList = await _playerService.GetAllPlayers();

                Device.BeginInvokeOnMainThread(() =>
                {
                    Players.Clear();
                    if (playersList?.Count > 0)
                    {
                        foreach (var player in playersList)
                        {
                            Players.Add(player);
                        }
                    }
                    IsBusy = IsRefreshing = false;
                });

            });
        }
        #endregion

        #region Commands

        public ICommand RefreshCommand => new Command(() =>
        {
            IsRefreshing = true;
            GetAllPlayers();
        });

        public ICommand SelectedPlayerCommand => new Command<PlayerModel>(async (player) =>
        {
            if (player != null)
            {
                var response = await App.Current.MainPage.DisplayActionSheet("Opciones", "Cerrar", null, "Editar Empleado", "Borrar Empleado");

                if (response == "Editar Empleado")
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AddPlayerView(player));
                }
                else if (response == "Borrar Empleado")
                {
                    IsBusy = true;
                    bool deleteResponse = await _playerService.DeletePlayer(player.Key);
                    if (deleteResponse)
                    {
                        GetAllPlayers();
                    }
                }
            }
        });
        #endregion


    }
}
