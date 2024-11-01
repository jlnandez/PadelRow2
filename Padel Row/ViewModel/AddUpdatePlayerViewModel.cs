using Padel_Row.Model;
using Padel_Row.Services.Interfaces;
using System.Windows.Input;

namespace Padel_Row.ViewModel
{
    public class AddUpdatePlayerViewModel
    {
        private readonly IPlayerService _playerService;

        private PlayerModel _playerDetail = new PlayerModel();

        //public PlayerModel PlayerDetail
        //{ 
        //    get => _playerDetail; 
        //    set => SetProperty(ref _playerDetail, value);
        
        //}
    }
}
