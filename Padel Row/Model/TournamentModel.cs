namespace Padel_Row.Model
{
    public class TournamentModel
    {
        public string Id { get; set; }
        public string Name { get; set; } // Nombre del torneo
        public DateTime Date { get; set; } // Fecha del torneo
        public List<PlayerModel> Players { get; set; } = new List<PlayerModel>(); // Jugadores asociados

        public string UserId { get; set; }

    }
}
