using Padel_Row.Model;
using Padel_Row.ViewModel;


namespace Padel_Row.Views;

public partial class TorneoAddPage : ContentPage
{
    public TorneoAddPage()
    {
        InitializeComponent();
        this.BindingContext = new TournamentViewModel();
    }

    public TorneoAddPage(TournamentModel tournament)
    {
        InitializeComponent();
        this.BindingContext = new TournamentViewModel(tournament);
    }
}