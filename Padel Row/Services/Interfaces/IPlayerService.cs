using Padel_Row.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Padel_Row.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<bool> AddOrUpdatePlayer(PlayerModel playerModel);
        Task<bool> DeletePlayer(string idPlayer);
        Task<List<PlayerModel>> GetAllPlayers();
    }
}
