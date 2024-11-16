using Firebase.Database;
using Firebase.Database.Query;
using Padel_Row.Model;
using Padel_Row.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Padel_Row.Services.Implementations
{
    public class TournamentService : ITournamentService
    {
        FirebaseClient firebase = new FirebaseClient(Settings.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Settings.FireBaseSecret)
        });

        public async Task<bool> AddOrUpdateTournament(TournamentModel tournament)
        {
            if (!string.IsNullOrWhiteSpace(tournament.Id))
            {
                // Actualizar torneo existente (sin incluir el Id en los datos)
                await firebase.Child(nameof(TournamentModel)).Child(tournament.Id).PutAsync(new TournamentModel
                {
                    Name = tournament.Name,
                    Date = tournament.Date,
                    UserId = tournament.UserId,
                    Players = tournament.Players
                });
                return true;
            }
            else
            {
                // Crear nuevo torneo
                var response = await firebase.Child(nameof(TournamentModel)).PostAsync(new TournamentModel
                {
                    Name = tournament.Name,
                    Date = tournament.Date,
                    UserId = tournament.UserId,
                    Players = tournament.Players
                });

                // Asignar el Id generado por Firebase al objeto en memoria
                tournament.Id = response.Key;
                return response.Key != null;
            }
        }

        public async Task<bool> DeleteTournament(string id)
        {
            try
            {
                await firebase.Child(nameof(TournamentModel)).Child(id).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<TournamentModel>> GetAllTournaments(string userId)
        {
            return (await firebase.Child(nameof(TournamentModel)).OnceAsync<TournamentModel>())
                .Where(f => f.Object.UserId == userId)
                .Select(f => new TournamentModel
                {
                    Id = f.Key,
                    Name = f.Object.Name,
                    Date = f.Object.Date,
                    UserId = f.Object.UserId,
                    Players = f.Object.Players
                }).ToList();

        }

        public async Task<TournamentModel> GetTournamentById(string tournamentId)
        {
            var result = await firebase
                .Child(nameof(TournamentModel))
                .Child(tournamentId)
                .OnceSingleAsync<TournamentModel>();

            // Agregar el ID al modelo devuelto, ya que no está incluido en los datos almacenados
            if (result != null)
            {
                result.Id = tournamentId;
            }
            return result;
        }

    }
}
