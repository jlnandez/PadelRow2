using Padel_Row.Model;
using Padel_Row.ViewModel;

namespace Padel_Row.Views;

public partial class PlayerScorePage : ContentPage
{
    public PlayerScorePage(TournamentModel tournament)
    {
        InitializeComponent();
        BindingContext = new PlayerScoreViewModel(tournament);
    }
}