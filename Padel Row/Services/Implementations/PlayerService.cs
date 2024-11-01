using Firebase.Database;
using Firebase.Database.Query;
using Padel_Row.Model;
using Padel_Row.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Padel_Row.Services.Implementations
{
    public class PlayerService //: IPlayerService
    {
        private readonly FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        public async Task<bool> AddOrUpdatePlayer(PlayerModel playerModel)
        {
            if (!string.IsNullOrWhiteSpace(playerModel.IdPlayer))
            {
                try
                {
                    await firebase.Child(nameof(playerModel)).Child(playerModel.IdPlayer).PutAsync(playerModel);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                var response = await firebase.Child(nameof(PlayerModel)).PostAsync(playerModel);
                playerModel.IdPlayer = response.Key;
                return response.Key != null;
            }
        }

        public async Task<bool> DeletePlayer(string idPlayer)
        {
            try
            {
                await firebase.Child(nameof(PlayerModel)).Child(idPlayer).DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<PlayerModel>> GetAllJugadores()
        {
            return (await firebase.Child(nameof(PlayerModel)).OnceAsync<PlayerModel>()).Select(j => new PlayerModel
            {
                IdPlayer = j.Key,
                Player = j.Object.Player,
                Team = j.Object.Team
            }).ToList();
        }

    }
}
