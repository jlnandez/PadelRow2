using Firebase.Database;
using Firebase.Database.Query;
using Padel_Row.Model;
using Padel_Row.Services.Interfaces;

namespace Padel_Row.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        public async Task<bool> AddOrUpdatePlayer(PlayerModel playerModel)
        {
            if (!string.IsNullOrWhiteSpace(playerModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(EmployeeModel)).Child(playerModel.Key).PutAsync(playerModel);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                var response = await firebase.Child(nameof(PlayerModel)).PostAsync(playerModel);
                if (response.Key != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeletePlayer(string key)
        {
            try
            {
                await firebase.Child(nameof(PlayerModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<PlayerModel>> GetAllPlayers()
        {
            return (await firebase.Child(nameof(PlayerModel)).OnceAsync<PlayerModel>()).Select(f => new PlayerModel
            {
                Player = f.Object.Player,
                Team = f.Object.Team,
                Key = f.Key
            }).ToList();
        }

    }
}
