using Padel_Row.Model;

namespace Padel_Row.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<bool> AddOrUpdateTournament(TournamentModel tournament);
        Task<bool> DeleteTournament(string id);
        Task<List<TournamentModel>> GetAllTournaments();
        Task<TournamentModel> GetTournamentById(string id);
    }
}
