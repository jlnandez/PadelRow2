using Padel_Row.Model;
using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class TorneoPlayersPage : ContentPage
{
    public TorneoPlayersPage(TournamentModel tournament)
    {
        InitializeComponent();

        // Establece el contexto de datos con el torneo seleccionado
        this.BindingContext = new TournamentPlayersViewModel(tournament);
    }
}